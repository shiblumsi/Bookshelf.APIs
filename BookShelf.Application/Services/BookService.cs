using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Books;
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
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookResponseDto>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            return books.Select(MapToDto).ToList();
        }

        public async Task<BookResponseDto?> GetByIdAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            return book == null ? null : MapToDto(book);
        }

        public async Task<BookResponseDto> AddAsync(AddBookRequestDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                Format = dto.Format,
                CoverImageUrl = dto.CoverImageUrl,
                AccessType = dto.AccessType,
                Price = dto.Price,
                PublishedDate = dto.PublishedDate,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.UtcNow
            };

            var created = await _repository.AddAsync(book);
            return MapToDto(created);
        }

        public async Task<BookResponseDto?> UpdateAsync(Guid id, AddBookRequestDto dto)
        {
            var book = new Book
            {
                Id = id,
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                Format = dto.Format,
                CoverImageUrl = dto.CoverImageUrl,
                AccessType = dto.AccessType,
                Price = dto.Price,
                PublishedDate = dto.PublishedDate,
                CategoryId = dto.CategoryId,
                UpdatedDate = DateTime.UtcNow
            };

            var updated = await _repository.UpdateAsync(book);
            return updated == null ? null : MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static BookResponseDto MapToDto(Book b) => new()
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Description = b.Description,
            FileUrl = b.FileUrl,
            Format = b.Format,
            CoverImageUrl = b.CoverImageUrl,
            AccessType = b.AccessType,
            Price = b.Price,
            PublishedDate = b.PublishedDate,
            CategoryName = b.Category?.Name ?? "N/A"
        };
    }
}
