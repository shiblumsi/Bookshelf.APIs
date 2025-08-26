using BookShelf.Application.DTOs.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface IPaymentService
    {
        Task<PaymentTransactionResponseDto> InitiatePaymentAsync(PaymentTransactionRequestDto request);
        Task<bool> HandleSuccessCallbackAsync(string transactionId, SslCommerzSuccessDto dto);
        Task<bool> HandleFailCallbackAsync(string transactionId, SslCommerzSuccessDto dto);
        Task<bool> HandleCancelCallbackAsync(string transactionId, SslCommerzSuccessDto dto);
    }
}
