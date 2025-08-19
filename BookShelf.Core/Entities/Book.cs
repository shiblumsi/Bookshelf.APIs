using System;
using System.Collections.Generic;
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


    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }   // PDF file link
        public string CoverImageUrl { get; set; }

        // Free / Premium / Paid
        public BookAccessType AccessType { get; set; }
        public decimal? Price { get; set; }

        public DateTime PublishedDate { get; set; }

        // Relations
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }


}
