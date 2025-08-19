using BookShelf.Application.DTOs;
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
        Task<Book> AddBook(BookDto dto);
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBookById(int id);
        Task DeleteBook(int id);
    }
}
