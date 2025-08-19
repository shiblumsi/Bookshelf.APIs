using BookShelf.Application.DTOs;
using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface IUserBookService
    {
        Task<UserBookDto> AddToLibraryAsync(int userId, int bookId);
        Task<IEnumerable<UserBookDto>> GetLibraryAsync(int userId);
        Task<bool> RemoveFromLibraryAsync(int userId, int bookId);
    }
}
