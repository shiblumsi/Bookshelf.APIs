using BookShelf.Application.Common;
using BookShelf.Application.DTOs;
using BookShelf.Application.Interface;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using BookShelf.Application.DTOs.Users;

namespace BookShelf.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterUserRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new ApplicationException("Email already registered");
            }

            if (dto.Password != dto.ConfirmPassword)
            {
                throw new ApplicationException("Password and Confirm Password do not match");
            }



            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
            };

             await _userRepository.AddAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsPremiumActive = user.IsPremiumActive
            };
        }

        public async Task<UserResponseDto> Login(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                return null;

            var token = GenerateJwtToken(user);
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                IsPremiumActive=user.IsPremiumActive,
                AccessToken = token,
            };
        }

        

        // Helpers
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }


        // JWT generate function with custom claims
        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email ?? string.Empty),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), // or int.Parse(jwtSettings["ExpiryDays"])
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
