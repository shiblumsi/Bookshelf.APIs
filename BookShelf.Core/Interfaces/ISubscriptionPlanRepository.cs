using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces
{
    public interface ISubscriptionPlanRepository
    {
        Task<IEnumerable<SubscriptionPlan>> GetAllAsync();
        Task<SubscriptionPlan?> GetByIdAsync(Guid id);
        Task<SubscriptionPlan> AddAsync(SubscriptionPlan plan);
        Task<SubscriptionPlan> UpdateAsync(SubscriptionPlan plan);
        Task<bool> DeleteAsync(Guid id);
    }
}
