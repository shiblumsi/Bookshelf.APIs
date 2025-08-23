using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class Library
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        public DateTime AddedDate { get; set; }

        // Future Features
        public bool IsFavorite { get; set; } = false;
        public double ProgressPercentage { get; set; }  // 0-100
    }

}
