using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.Users
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public bool IsPremiumActive { get; set; }
        public string? AccessToken { get; set; }
    }
}
