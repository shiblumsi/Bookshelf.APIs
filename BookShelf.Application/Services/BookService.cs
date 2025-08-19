using BookShelf.Application.DTOs;
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
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> AddBook(BookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                Description = dto.Description,
                FileUrl = dto.FileUrl,
                CoverImageUrl = dto.CoverImageUrl,
                AccessType = (BookAccessType)dto.AccessType,
                Price = dto.Price,
                PublishedDate = dto.PublishedDate
            };

            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.ToList();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }
    }
}
