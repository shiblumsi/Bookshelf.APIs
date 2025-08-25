using BookShelf.Application.DTOs.SubscriptionPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface ISubscriptionPlanService
    {
        Task<IEnumerable<SubscriptionPlanResponseDto>> GetAllAsync();
        Task<SubscriptionPlanResponseDto?> GetByIdAsync(Guid id);
        Task<SubscriptionPlanResponseDto> CreateAsync(SubscriptionPlanRequestDto dto);
        Task<SubscriptionPlanResponseDto> UpdateAsync(Guid id, SubscriptionPlanRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
