using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.PaymentTransaction
{
    public class PaymentTransactionRequestDto
    {
        public Guid UserId { get; set; }
        public Guid SubscriptionPlanId { get; set; }
    }
}
