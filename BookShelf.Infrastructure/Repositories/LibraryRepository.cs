using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using BookShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Infrastructure.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly BookShelfDbContext _context;

        public LibraryRepository(BookShelfDbContext context)
        {
            _context = context;
        }

        public async Task<Library> AddAsync(Library library)
        {
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();
            return library;
        }

        public async Task<IEnumerable<Library>> GetUserLibraryAsync(Guid userId)
        {
            return await _context.Libraries
                .Include(l => l.Book)
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid bookId)
        {
            return await _context.Libraries.AnyAsync(l => l.UserId == userId && l.BookId == bookId);
        }
    }

}
