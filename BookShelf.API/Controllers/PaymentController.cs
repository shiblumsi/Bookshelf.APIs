using BookShelf.Application.DTOs.PaymentTransaction;
using BookShelf.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Initiate Payment
        [HttpPost("initiate")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentTransactionRequestDto dto)
        {
            var response = await _paymentService.InitiatePaymentAsync(dto);
            return Ok(response);
        }

        // ✅ SSLCommerz Success Callback (server→server POST)
        [HttpPost("success")]
        public async Task<IActionResult> PaymentSuccess([FromForm] SslCommerzSuccessDto form)
        {
            Console.WriteLine("***** SSLCommerz Success Callback *****");
          

            // Process transaction & subscription
            var result = await _paymentService.HandleSuccessCallbackAsync(form.tran_id, form);

            // FE redirect
            return Redirect("http://localhost:4200/payment-status?status=success");
        }

        // Fail Callback
        [HttpPost("fail")]
        public async Task<IActionResult> PaymentFail([FromForm] SslCommerzSuccessDto form)
        {
            await _paymentService.HandleFailCallbackAsync(form.tran_id, form);
            return Redirect("http://localhost:4200/payment-status?status=fail");
        }

        // Cancel Callback
        [HttpPost("cancel")]
        public async Task<IActionResult> PaymentCancel([FromForm] SslCommerzSuccessDto form)
        {
            await _paymentService.HandleCancelCallbackAsync(form.tran_id, form);
            return Redirect("http://localhost:4200/payment-status?status=cancel");
        }
    }

}
