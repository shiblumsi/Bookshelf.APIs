using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs
{
    public class UserBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string FileUrl { get; set; }
        public string CoverImageUrl { get; set; }

    }
}
