using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces
{
    public interface IUserBookRepository
    {
        Task<UserBook> AddAsync(UserBook entity);
        Task<IEnumerable<UserBook>> GetByUserIdAsync(int userId);
        Task<UserBook> GetByUserIdAndBookIdAsync(int userId, int bookId);
        Task DeleteAsync(UserBook entity);
    }
}
