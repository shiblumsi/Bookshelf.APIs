using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{

    public enum BookAccessType
    {
        Free = 1,
        Premium = 2,
        Paid = 3
    }
    public enum BookFormat
    {
        Pdf = 1,
        Epub = 2,
        AudioBook = 3
    }


    public class Book
    {
        public Guid Id { get; set; }

        // Basic Info
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Description { get; set; }

        // Media Info
        public required string FileUrl { get; set; }
        public required BookFormat Format { get; set; }
        public required string CoverImageUrl { get; set; }

        // Access Control
        public required BookAccessType AccessType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        // Metadata
        public DateTime? PublishedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        // Auditing
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }

        // Relations
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<UserBook>? UserBooks { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }


}
