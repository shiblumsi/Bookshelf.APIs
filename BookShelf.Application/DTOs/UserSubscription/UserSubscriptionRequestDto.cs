using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.UserSubscription
{
    public class UserSubscriptionRequestDto
    {
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
    }
}
