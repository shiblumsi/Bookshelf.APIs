using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class SubscriptionPlan
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }        
        public int DurationDays { get; set; }    

        public string? Description { get; set; }    

        // Relations
        public ICollection<UserSubscription>? UserSubscriptions { get; set; }
        public ICollection<PaymentTransaction>? PaymentTransactions { get; set; }
    }
}
