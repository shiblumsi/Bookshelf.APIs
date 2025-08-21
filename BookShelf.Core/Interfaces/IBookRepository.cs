using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(Guid id);
        Task<Book> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
        Task<bool> DeleteAsync(Guid id);

    }
}
