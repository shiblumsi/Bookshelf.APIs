using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class UserSubscription
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid PlanId { get; set; }
        public SubscriptionPlan? Plan { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public required DateTime EndDate { get; set; }

        // Helper property
        public bool IsActive => EndDate >= DateTime.UtcNow;

       
    }
}
