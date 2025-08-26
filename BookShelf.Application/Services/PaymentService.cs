using BookShelf.Application.DTOs.PaymentTransaction;
using BookShelf.Application.Interface;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace BookShelf.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentTransactionRepository _transactionRepo;
        private readonly ISubscriptionPlanRepository _planRepo;
        private readonly IUserSubscriptionRepository _subscriptionRepo;
        private readonly IConfiguration _config;

        private readonly string _storeId;
        private readonly string _storePass;
        private readonly string _sslUrl;

        public PaymentService(
            IPaymentTransactionRepository transactionRepo,
            ISubscriptionPlanRepository planRepo,
            IUserSubscriptionRepository subscriptionRepo,
            IConfiguration config)
        {
            _transactionRepo = transactionRepo;
            _planRepo = planRepo;
            _subscriptionRepo = subscriptionRepo;
            _config = config;

            _storeId = _config["SSLCommerz:StoreId"]!;
            _storePass = _config["SSLCommerz:StorePassword"]!;
            _sslUrl = _config["SSLCommerz:ApiUrl"]!;
        }

        // Step 1: Initiate Payment
        public async Task<PaymentTransactionResponseDto> InitiatePaymentAsync(PaymentTransactionRequestDto reqDto)
        {
            var plan = await _planRepo.GetByIdAsync(reqDto.SubscriptionPlanId);
            if (plan == null) throw new Exception("Plan not found");

            string transactionId = Guid.NewGuid().ToString();

            // Save transaction in DB (pending state)
            var transaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                UserId = reqDto.UserId,
                Amount = plan.Price,
                SubscriptionPlanId=reqDto.SubscriptionPlanId,
                TransactionId = transactionId,
                PaymentGateway = "SSLCommerz",
                IsSuccess = false
            };
            await _transactionRepo.AddAsync(transaction);

            // SSLCommerz Request Data
            var postData = new Dictionary<string, string>
            {
                {"store_id", _storeId},
                {"store_passwd", _storePass},
                {"total_amount", plan.Price.ToString("F2", CultureInfo.InvariantCulture)},
                {"currency", "BDT"},
                {"tran_id", transactionId},
                {"success_url", "https://localhost:7211/api/payment/success"},
                {"fail_url", "https://localhost:7211/api/payment/fail"},
                {"cancel_url", "https://localhost:7211/api/payment/cancel"},
                {"cus_name", "Test User"},
                {"cus_email", "test@test.com"},
                {"cus_phone", "01700000000"},
                {"cus_add1", "Dhaka"},
                {"cus_city", "Dhaka"},
                {"cus_country", "Bangladesh"},
                {"shipping_method", "NO"},
                {"product_name", "Test Subscription"},
                {"product_category", "Subscription"},
                {"product_profile", "general"},
            };

            using var client = new HttpClient();
            var response = await client.PostAsync(_sslUrl, new FormUrlEncodedContent(postData));
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(responseBody)!;
            string redirectUrl = json.GatewayPageURL;

            return new PaymentTransactionResponseDto
            {
                RedirectUrl = redirectUrl
            };
        }

        // Step 2: Success Callback
        public async Task<bool> HandleSuccessCallbackAsync(string transactionId, SslCommerzSuccessDto dto)
        {
            var transaction = await _transactionRepo.GetByTransactionIdAsync(transactionId);
            if (transaction == null) return false;

            transaction.IsSuccess = dto.status == "VALID";
            transaction.GatewayResponse = Newtonsoft.Json.JsonConvert.SerializeObject(dto);

            await _transactionRepo.UpdateAsync(transaction);


            var plan = await _planRepo.GetByIdAsync(transaction.SubscriptionPlanId);
            if (plan == null) return false;

            var subscription = new UserSubscription
            {
                UserId = transaction.UserId,
                PlanId = plan.Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(plan.DurationDays),
            };
            await _subscriptionRepo.AddAsync(subscription);

            return true;
        }

        // Step 3: Fail Callback
        public async Task<bool> HandleFailCallbackAsync(string transactionId, SslCommerzSuccessDto dto)
        {
            var transaction = await _transactionRepo.GetByTransactionIdAsync(transactionId);
            if (transaction == null) return false;

            transaction.IsSuccess = false;
            transaction.GatewayResponse = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            await _transactionRepo.UpdateAsync(transaction);

            return true;
        }

        // Step 4: Cancel Callback
        public async Task<bool> HandleCancelCallbackAsync(string transactionId, SslCommerzSuccessDto dto)
        {
            var transaction = await _transactionRepo.GetByTransactionIdAsync(transactionId);
            if (transaction == null) return false;

            transaction.IsSuccess = false;
            transaction.GatewayResponse = Newtonsoft.Json.JsonConvert.SerializeObject(dto);
            await _transactionRepo.UpdateAsync(transaction);

            return true;
        }
    }
}
