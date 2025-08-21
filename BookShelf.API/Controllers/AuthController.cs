using BookShelf.Application.Common;
using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Responses;
using BookShelf.Application.DTOs.Users;
using BookShelf.Application.Interface;
using BookShelf.Application.Services;
using BookShelf.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto dto)
        {
            try
            {
                var createdUser = await _authService.RegisterAsync(dto);
                return Ok(ApiResponse<UserResponseDto>.Success(createdUser, "User registered successfully", HttpStatusCode.Created));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var loginResult = await _authService.Login(dto);
                if (loginResult == null)
                    return Unauthorized(ApiResponse<string>.Fail("Invalid email or password", HttpStatusCode.Unauthorized));

                return Ok(ApiResponse<UserResponseDto>.Success(loginResult, "Login successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }



    }
}