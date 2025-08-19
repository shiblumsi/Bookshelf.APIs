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

        public async Task<APIServiceResponse> Register(RegisterDto dto)
        {
            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };
            try
            {
                var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
                if (existingUser != null)
                {
                    response.ResponseStatus = false;
                    response.ErrMsg = "Email already registered";
                    response.ResponseCode = 400;
                    return response;
                }

                var user = new User
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    PasswordHash = HashPassword(dto.Password)
                };

                await _userRepository.AddAsync(user);

                response.ResponseStatus = true;
                response.SuccessMsg = "User registered successfully";
                response.ResponseCode = 201;
                response.ResponseBusinessData = user;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = false;
                response.ErrMsg = ex.Message;
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<APIServiceResponse> Login(LoginDto dto)
        {
            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };
            try
            {
                var user = await _userRepository.GetByEmailAsync(dto.Email);
                if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                {
                    response.ResponseStatus = false;
                    response.ErrMsg = "Invalid credentials";
                    response.ResponseCode = 401;
                    return response;
                }

                var token = GenerateJwtToken(user);

                response.ResponseStatus = true;
                response.SuccessMsg = "Login successful";
                response.ResponseCode = 200;
                response.ResponseBusinessData = new { user.Id, user.FullName, user.Email, Token = token };
            }
            catch (Exception ex)
            {
                response.ResponseStatus = false;
                response.ErrMsg = ex.Message;
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<APIServiceResponse> UpgradePremium(int userId)
        {
            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    response.ResponseStatus = false;
                    response.ErrMsg = "User not found";
                    response.ResponseCode = 404;
                    return response;
                }

                user.IsPremium = true;
                await _userRepository.UpdateAsync(user);

                response.ResponseStatus = true;
                response.SuccessMsg = "User upgraded to premium";
                response.ResponseCode = 200;
                response.ResponseBusinessData = user;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = false;
                response.ErrMsg = ex.Message;
                response.ResponseCode = 500;
            }
            return response;
        }

        public async Task<APIServiceResponse> GetUserById(int userId)
        {
            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    response.ResponseStatus = false;
                    response.ErrMsg = "User not found";
                    response.ResponseCode = 404;
                    return response;
                }

                response.ResponseStatus = true;
                response.SuccessMsg = "User fetched successfully";
                response.ResponseCode = 200;
                response.ResponseBusinessData = user;
            }
            catch (Exception ex)
            {
                response.ResponseStatus = false;
                response.ErrMsg = ex.Message;
                response.ResponseCode = 500;
            }
            return response;
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
