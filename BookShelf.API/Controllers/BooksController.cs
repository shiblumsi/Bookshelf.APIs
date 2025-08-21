using BookShelf.Application.Common;
using BookShelf.Application.DTOs;
using BookShelf.Application.DTOs.Books;
using BookShelf.Application.Interface;
using BookShelf.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace BookShelf.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var books = await _service.GetAllAsync();
                return Ok(ApiResponse<List<BookResponseDto>>.Success(books, "Books fetched successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var book = await _service.GetByIdAsync(id);
                if (book == null)
                    return NotFound(ApiResponse<string>.Fail("Book not found", HttpStatusCode.NotFound));

                return Ok(ApiResponse<BookResponseDto>.Success(book, "Book fetched successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddBookRequestDto dto)
        {
            try
            {
                var created = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id },
                    ApiResponse<BookResponseDto>.Success(created, "Book created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddBookRequestDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                if (updated == null)
                    return NotFound(ApiResponse<string>.Fail("Book not found", HttpStatusCode.NotFound));

                return Ok(ApiResponse<BookResponseDto>.Success(updated, "Book updated successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound(ApiResponse<string>.Fail("Book not found", HttpStatusCode.NotFound));

                return Ok(ApiResponse<bool>.Success(true, "Book deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }
    }
}
