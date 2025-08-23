using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.Library
{
    public class LibraryBookResponseDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public DateTime AddedDate { get; set; }
    }

}
