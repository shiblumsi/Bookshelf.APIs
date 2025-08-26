using BookShelf.Application.DTOs.UserSubscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Services
{
    public interface IUserSubscriptionService
    {
        Task<UserSubscriptionResponseDto> SubscribeAsync(UserSubscriptionRequestDto dto);
        Task<IEnumerable<UserSubscriptionResponseDto>> GetUserSubscriptionsAsync(Guid userId);
        Task<UserSubscriptionResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserSubscriptionResponseDto>> GetAllAsync();
        Task<bool> CancelAsync(Guid id);
    }
}
