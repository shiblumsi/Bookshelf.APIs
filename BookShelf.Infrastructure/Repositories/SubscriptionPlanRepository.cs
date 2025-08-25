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
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly BookShelfDbContext _context;

        public SubscriptionPlanRepository(BookShelfDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionPlan>> GetAllAsync()
        {
            return await _context.SubscriptionPlans.ToListAsync();
        }

        public async Task<SubscriptionPlan?> GetByIdAsync(Guid id)
        {
            return await _context.SubscriptionPlans.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<SubscriptionPlan> AddAsync(SubscriptionPlan plan)
        {
            _context.SubscriptionPlans.Add(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<SubscriptionPlan> UpdateAsync(SubscriptionPlan plan)
        {
            _context.SubscriptionPlans.Update(plan);
            await _context.SaveChangesAsync();
            return plan;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var plan = await _context.SubscriptionPlans.FindAsync(id);
            if (plan == null) return false;

            _context.SubscriptionPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}