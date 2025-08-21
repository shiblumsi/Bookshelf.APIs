using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public bool IsPremiumActive =>
            Subscriptions?.Any(s => s.IsActive) ?? false;

        // Relations
        public ICollection<UserBook>? UserBooks { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<UserSubscription>? Subscriptions { get; set; }
    }

}
