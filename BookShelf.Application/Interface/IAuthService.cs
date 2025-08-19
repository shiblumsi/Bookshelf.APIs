using BookShelf.Application.Common;
using BookShelf.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface IAuthService
    {
        Task<APIServiceResponse> Register(RegisterDto dto);
        Task<APIServiceResponse> Login(LoginDto dto);
        Task<APIServiceResponse> UpgradePremium(int userId);
        Task<APIServiceResponse> GetUserById(int userId);
    }
}
