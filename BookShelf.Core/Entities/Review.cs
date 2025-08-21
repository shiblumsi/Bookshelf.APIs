using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
