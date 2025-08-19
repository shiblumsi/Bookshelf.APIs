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
        Task<Book> AddAsync(Book book);
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);

    }
}
