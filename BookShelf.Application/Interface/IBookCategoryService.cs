using BookShelf.Application.DTOs.Responses;
using BookShelf.Application.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Interface
{
    public interface IBookCategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();
        Task<CategoryResponseDto?> GetByIdAsync(Guid id);
        Task<CategoryResponseDto> AddAsync(AddCategoryRequestDto dto);
        Task<CategoryResponseDto?> UpdateAsync(Guid id, AddCategoryRequestDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
