using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class PaymentTransaction
    {
        public Guid Id { get; set; }

        
        public Guid UserId { get; set; }
        public User? User { get; set; }

        // UserSubscription relation
        public Guid SubscriptionId { get; set; }
        public UserSubscription? Subscription { get; set; }

        // Payment details
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "BDT";
        public string PaymentGateway { get; set; } = "SSLCommerz";
        public string TransactionId { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = false;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string? GatewayResponse { get; set; }

        // Navigation property for UserSubscription
        // (UserSubscription -> PaymentTransactions)
    }
}
