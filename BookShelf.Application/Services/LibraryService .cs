using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Library;
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
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _libraryRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IUserRepository _userRepo;

        public LibraryService(ILibraryRepository libraryRepo, IBookRepository bookRepo, IUserRepository userRepo)
        {
            _libraryRepo = libraryRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
        }

        public async Task<LibraryBookResponseDto> AddBookAsync(Guid userId, Guid bookId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            var book = await _bookRepo.GetByIdAsync(bookId);

            if (book == null) throw new Exception("Book not found.");
            if (user == null) throw new Exception("User not found.");

            // Already added
            if (await _libraryRepo.ExistsAsync(userId, bookId))
                throw new Exception("Book already in library.");

            // Access Control
            if (book.AccessType == BookAccessType.Premium && !user.IsPremiumActive)
                throw new Exception("Premium subscription required to add this book.");
            if (book.AccessType == BookAccessType.Paid)
                throw new Exception("Purchase required to add this book.");

            var libraryEntry = new Library
            {
                UserId = user.Id,
                BookId = book.Id,
                AddedDate = DateTime.UtcNow
            };

            await _libraryRepo.AddAsync(libraryEntry);

            return new LibraryBookResponseDto
            {
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author,
                CoverImageUrl = book.CoverImageUrl,
                AddedDate = libraryEntry.AddedDate
            };
        }

        public async Task<IEnumerable<LibraryBookResponseDto>> GetUserLibraryAsync(Guid userId)
        {
            var books = await _libraryRepo.GetUserLibraryAsync(userId);
            return books.Select(l => new LibraryBookResponseDto
            {
                BookId = l.BookId,
                Title = l.Book!.Title,
                Author = l.Book.Author,
                CoverImageUrl = l.Book.CoverImageUrl,
                AddedDate = l.AddedDate
            });
        }
    }
}