using BookShelf.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Infrastructure.Persistence
{
    public class BookShelfDbContext:DbContext
    {
        public BookShelfDbContext(DbContextOptions<BookShelfDbContext> options) : base(options) { }
      
        public DbSet<Book> Books { get; set; }
    }
}
