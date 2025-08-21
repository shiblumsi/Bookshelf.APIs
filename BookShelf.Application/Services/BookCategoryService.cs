using BookShelf.Application.DTOs.Requests;
using BookShelf.Application.DTOs.Responses;
using BookShelf.Application.Interface;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IBookCategoryRepository _repository;
        public BookCategoryService(IBookCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryResponseDto> AddAsync(AddCategoryRequestDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
            };
            await _repository.AddAsync(category);
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null)
            {
                await _repository.DeleteAsync(category);
                return true;
            }
            return false;
        }

        public async Task<List<CategoryResponseDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate
            }).ToList();
        }

        public async Task<CategoryResponseDto?> GetByIdAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return null;
            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate,
            };
        }

        public async Task<CategoryResponseDto?> UpdateAsync(Guid id, AddCategoryRequestDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) 
                return null;

            category.Name = dto.Name;
            category.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(category);

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate
            };

        }
    }
}
