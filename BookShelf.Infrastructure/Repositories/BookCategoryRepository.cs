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
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly BookShelfDbContext _dbContext;
        public BookCategoryRepository(BookShelfDbContext bookShelfDbContext)
        {
            _dbContext = bookShelfDbContext;
        }

        public async Task AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
             _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
