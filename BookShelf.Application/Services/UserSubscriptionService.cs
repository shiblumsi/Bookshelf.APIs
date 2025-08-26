using BookShelf.Application.DTOs.UserSubscription;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Services
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _repo;
        private readonly ISubscriptionPlanRepository _planRepo;

        public UserSubscriptionService(
            IUserSubscriptionRepository repo,
            ISubscriptionPlanRepository planRepo)
        {
            _repo = repo;
            _planRepo = planRepo;
        }

        public async Task<UserSubscriptionResponseDto> SubscribeAsync(UserSubscriptionRequestDto dto)
        {
            var plan = await _planRepo.GetByIdAsync(dto.PlanId);
            if (plan == null) throw new Exception("Subscription plan not found");

            var subscription = new UserSubscription
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                PlanId = dto.PlanId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(plan.DurationDays)
            };

            var created = await _repo.AddAsync(subscription);

            return new UserSubscriptionResponseDto
            {
                Id = created.Id,
                UserId = created.UserId,
                PlanId = created.PlanId,
                PlanName = plan.Name,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
                IsActive = created.IsActive
            };
        }

        public async Task<IEnumerable<UserSubscriptionResponseDto>> GetUserSubscriptionsAsync(Guid userId)
        {
            var subscriptions = await _repo.GetByUserIdAsync(userId);
            return subscriptions.Select(s => new UserSubscriptionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                PlanId = s.PlanId,
                PlanName = s.Plan?.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsActive = s.IsActive
            });
        }

        public async Task<UserSubscriptionResponseDto?> GetByIdAsync(Guid id)
        {
            var s = await _repo.GetByIdAsync(id);
            if (s == null) return null;

            return new UserSubscriptionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                PlanId = s.PlanId,
                PlanName = s.Plan?.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsActive = s.IsActive
            };
        }

        public async Task<IEnumerable<UserSubscriptionResponseDto>> GetAllAsync()
        {
            var subscriptions = await _repo.GetAllAsync();
            return subscriptions.Select(s => new UserSubscriptionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                PlanId = s.PlanId,
                PlanName = s.Plan?.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                IsActive = s.IsActive
            });
        }

        public async Task<bool> CancelAsync(Guid id)
        {
            var sub = await _repo.GetByIdAsync(id);
            if (sub == null) return false;

            sub.EndDate = DateTime.UtcNow; // cancel immediately
            await _repo.UpdateAsync(sub);
            return true;
        }
    }
}
