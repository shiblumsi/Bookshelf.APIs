using BookShelf.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.Books
{
    public class AddBookRequestDto
    {
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Description { get; set; } = default!;
        // Files
        public IFormFile File { get; set; } = default!;
        public IFormFile CoverImage { get; set; } = default!;

        public BookFormat Format { get; set; }
        public BookAccessType AccessType { get; set; }
        public decimal? Price { get; set; }
        public DateTime? PublishedDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
