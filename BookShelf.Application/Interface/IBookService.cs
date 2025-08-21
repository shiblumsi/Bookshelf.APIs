using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Books;
using BookShelf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface IBookService
    {
        Task<List<BookResponseDto>> GetAllAsync();
        Task<BookResponseDto?> GetByIdAsync(Guid id);
        Task<BookResponseDto> AddAsync(AddBookRequestDto dto);
        Task<BookResponseDto?> UpdateAsync(Guid id, AddBookRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
