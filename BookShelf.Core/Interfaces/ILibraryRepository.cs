using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Interfaces
{
    public interface ILibraryRepository
    {
        Task<Library> AddAsync(Library library);
        Task<IEnumerable<Library>> GetUserLibraryAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId, Guid bookId);
    }
}
