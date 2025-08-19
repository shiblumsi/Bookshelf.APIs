using BookShelf.Application.DTOs;
using BookShelf.Application.Interface;
using BookShelf.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);
            return StatusCode(result.ResponseCode, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.Login(dto);
            return StatusCode(result.ResponseCode, result);
        }

        [Authorize]
        [HttpPost("upgrade-premium")]
        public async Task<IActionResult> UpgradePremium()
        {
            var userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
            var result = await _authService.UpgradePremium(userId);
            return StatusCode(result.ResponseCode, result);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = int.Parse(User.FindFirst("sub")?.Value ?? "0");
            var result = await _authService.GetUserById(userId);
            return StatusCode(result.ResponseCode, result);
        }
    }
}