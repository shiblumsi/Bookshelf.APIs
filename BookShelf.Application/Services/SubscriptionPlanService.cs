using BookShelf.Application.DTOs.SubscriptionPlan;
using BookShelf.Application.Interface;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _repository;

        public SubscriptionPlanService(ISubscriptionPlanRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubscriptionPlanResponseDto>> GetAllAsync()
        {
            var plans = await _repository.GetAllAsync();
            return plans.Select(p => new SubscriptionPlanResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                DurationDays = p.DurationDays,
                Description = p.Description
            });
        }

        public async Task<SubscriptionPlanResponseDto?> GetByIdAsync(Guid id)
        {
            var plan = await _repository.GetByIdAsync(id);
            if (plan == null) return null;

            return new SubscriptionPlanResponseDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                Description = plan.Description
            };
        }

        public async Task<SubscriptionPlanResponseDto> CreateAsync(SubscriptionPlanRequestDto dto)
        {
            var plan = new SubscriptionPlan
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Price = dto.Price,
                DurationDays = dto.DurationDays,
                Description = dto.Description
            };

            await _repository.AddAsync(plan);

            return new SubscriptionPlanResponseDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                DurationDays = plan.DurationDays,
                Description = plan.Description
            };
        }

        public async Task<SubscriptionPlanResponseDto> UpdateAsync(Guid id, SubscriptionPlanRequestDto dto)
        {
            var existingPlan = await _repository.GetByIdAsync(id);
            if (existingPlan == null) throw new KeyNotFoundException("Plan not found");

            existingPlan.Name = dto.Name;
            existingPlan.Price = dto.Price;
            existingPlan.DurationDays = dto.DurationDays;
            existingPlan.Description = dto.Description;

            await _repository.UpdateAsync(existingPlan);

            return new SubscriptionPlanResponseDto
            {
                Id = existingPlan.Id,
                Name = existingPlan.Name,
                Price = existingPlan.Price,
                DurationDays = existingPlan.DurationDays,
                Description = existingPlan.Description
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

