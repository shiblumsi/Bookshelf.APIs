using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using BookShelf.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShelfDbContext _context;
        public BookRepository(BookShelfDbContext context) => _context = context;

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var existing = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id && !b.IsDeleted);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(book);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _context.Books.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (existing == null) return false;

            existing.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
