using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using BookShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Infrastructure.Repositories
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly BookShelfDbContext _context;

        public UserSubscriptionRepository(BookShelfDbContext context)
        {
            _context = context;
        }

        public async Task<UserSubscription> AddAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task<UserSubscription?> GetByIdAsync(Guid id)
        {
            return await _context.UserSubscriptions
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<UserSubscription>> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserSubscriptions
                .Include(s => s.Plan)
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserSubscription>> GetAllAsync()
        {
            return await _context.UserSubscriptions
                .Include(s => s.Plan)
                .ToListAsync();
        }

        public async Task UpdateAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserSubscription subscription)
        {
            _context.UserSubscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}

