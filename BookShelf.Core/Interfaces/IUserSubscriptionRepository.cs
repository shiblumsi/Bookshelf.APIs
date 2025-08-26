using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces
{
    public interface IUserSubscriptionRepository
    {
        Task<UserSubscription> AddAsync(UserSubscription subscription);
        Task<UserSubscription?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserSubscription>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<UserSubscription>> GetAllAsync();
        Task UpdateAsync(UserSubscription subscription);
        Task DeleteAsync(UserSubscription subscription);
    }
}
