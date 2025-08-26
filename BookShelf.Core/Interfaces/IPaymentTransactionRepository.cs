using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces
{
    public interface IPaymentTransactionRepository
    {
        Task<PaymentTransaction> AddAsync(PaymentTransaction transaction);
        Task<PaymentTransaction?> GetByTransactionIdAsync(string transactionId);
        Task UpdateAsync(PaymentTransaction transaction);
    }
}
