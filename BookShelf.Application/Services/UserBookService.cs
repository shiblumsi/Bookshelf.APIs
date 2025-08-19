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
    public class UserBookService : IUserBookService
    {
        private readonly IUserBookRepository _userBookRepository;
        private readonly IBookRepository _bookRepository;

        public UserBookService(IUserBookRepository userBookRepository, IBookRepository bookRepository)
        {
            _userBookRepository = userBookRepository;
            _bookRepository = bookRepository;
        }

        public async Task<UserBookDto> AddToLibraryAsync(int userId, int bookId)
        {
            var existing = await _userBookRepository.GetByUserIdAndBookIdAsync(userId, bookId);
            if (existing != null) return null;

            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) return null;

            var userBook = new UserBook
            {
                UserId = userId,
                BookId = bookId,
                AddedDate = DateTime.UtcNow
            };

            var addedBook = new UserBookDto
            {
                Title = book.Title,
                Author = book.Author,
                FileUrl = book.FileUrl,
                CoverImageUrl = book.CoverImageUrl
            };

            await _userBookRepository.AddAsync(userBook);
            return addedBook;
        }

        public async Task<IEnumerable<UserBookDto>> GetLibraryAsync(int userId)
        {
            var userBooks = await _userBookRepository.GetByUserIdAsync(userId);

            return userBooks.Select(ub => new UserBookDto
            {
                Title = ub.Book.Title,
                Author = ub.Book.Author,
                FileUrl = ub.Book.FileUrl,
                CoverImageUrl = ub.Book.CoverImageUrl
            });
        }

        public async Task<bool> RemoveFromLibraryAsync(int userId, int bookId)
        {
            var existing = await _userBookRepository.GetByUserIdAndBookIdAsync(userId, bookId);
            if (existing == null) return false;

            await _userBookRepository.DeleteAsync(existing);
            return true;
        }
    }
}
