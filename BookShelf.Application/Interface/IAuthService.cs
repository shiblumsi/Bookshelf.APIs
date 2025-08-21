using BookShelf.Application.Common;
using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterAsync(RegisterUserRequestDto dto);
        Task<UserResponseDto> Login(LoginRequestDto dto);
       
    }
}
