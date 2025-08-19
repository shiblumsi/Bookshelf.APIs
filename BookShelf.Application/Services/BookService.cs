using BookShelf.Application.DTOs;
using BookShelf.Core.Entities;
using BookShelf.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Services
{
    public class BookService
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
                Price = dto.Price,
            };
            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllAsync();
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
