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
    public class UserBookRepository : IUserBookRepository
    {
        private readonly BookShelfDbContext _context;

        public UserBookRepository(BookShelfDbContext context)
        {
            _context = context;
        }

        public async Task<UserBook> AddAsync(UserBook entity)
        {
            _context.UserBooks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<UserBook>> GetByUserIdAsync(int userId)
        {
            return await _context.UserBooks
                .Include(ub => ub.Book)
                .Where(ub => ub.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserBook> GetByUserIdAndBookIdAsync(int userId, int bookId)
        {
            return await _context.UserBooks
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BookId == bookId);
        }

        public async Task DeleteAsync(UserBook entity)
        {
            _context.UserBooks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

}
