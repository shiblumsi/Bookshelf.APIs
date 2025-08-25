using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.SubscriptionPlan
{
    public class SubscriptionPlanRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string? Description { get; set; }
    }
}
