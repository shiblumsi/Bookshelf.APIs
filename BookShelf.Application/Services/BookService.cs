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
            // Create folder if not exists
            var booksFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "books");
            var coversFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "covers");

            Directory.CreateDirectory(booksFolder);
            Directory.CreateDirectory(coversFolder);

            // Save Book File (PDF, EPUB, etc.)
            var bookFileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
            var bookFilePath = Path.Combine(booksFolder, bookFileName);
            using (var stream = new FileStream(bookFilePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // Save Cover Image
            var coverFileName = Guid.NewGuid() + Path.GetExtension(dto.CoverImage.FileName);
            var coverFilePath = Path.Combine(coversFolder, coverFileName);
            using (var stream = new FileStream(coverFilePath, FileMode.Create))
            {
                await dto.CoverImage.CopyToAsync(stream);
            }

            // Build accessible URLs (these will be served by ASP.NET Core Static Files)
            var bookUrl = $"/uploads/books/{bookFileName}";
            var coverUrl = $"/uploads/covers/{coverFileName}";
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description,
                FileUrl = bookUrl,
                Format = dto.Format,
                CoverImageUrl = coverUrl,
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
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null) return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // File Update
            if (dto.File != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                // পুরোনো file মুছে ফেলা (optional)
                if (!string.IsNullOrEmpty(existingBook.FileUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBook.FileUrl.TrimStart('/'));
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                existingBook.FileUrl = "/uploads/" + fileName;
            }

            // Cover Image Update
            if (dto.CoverImage != null)
            {
                string coverFileName = Guid.NewGuid() + Path.GetExtension(dto.CoverImage.FileName);
                string coverPath = Path.Combine(uploadsFolder, coverFileName);

                using (var stream = new FileStream(coverPath, FileMode.Create))
                {
                    await dto.CoverImage.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(existingBook.CoverImageUrl))
                {
                    var oldCoverPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingBook.CoverImageUrl.TrimStart('/'));
                    if (File.Exists(oldCoverPath))
                    {
                        File.Delete(oldCoverPath);
                    }
                }

                existingBook.CoverImageUrl = "/uploads/" + coverFileName;
            }

            // Update Other Properties
            existingBook.Title = dto.Title;
            existingBook.Author = dto.Author;
            existingBook.Description = dto.Description;
            existingBook.Format = dto.Format;
            existingBook.AccessType = dto.AccessType;
            existingBook.Price = dto.Price;
            existingBook.PublishedDate = dto.PublishedDate;
            existingBook.CategoryId = dto.CategoryId;
            existingBook.UpdatedDate = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(existingBook);
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
