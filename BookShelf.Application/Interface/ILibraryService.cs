using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Library;
using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface ILibraryService
    {
        Task<LibraryBookResponseDto> AddBookAsync(Guid userId, Guid bookId);
        Task<IEnumerable<LibraryBookResponseDto>> GetUserLibraryAsync(Guid userId);
    }
}
