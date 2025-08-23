using BookShelf.Application.Common;
using BookShelf.Application.DTOs.Library;
using BookShelf.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libService;

        public LibraryController(ILibraryService libService)
        {
            _libService = libService;
        }
        [Authorize]
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] AddBookToLibraryRequestDto dto)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst("userId")?.Value ?? throw new Exception("Unauthorized"));

                var result = await _libService.AddBookAsync(userId, dto.BookId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("my-library")]
        public async Task<IActionResult> GetMyLibrary()
        {
            var userId = Guid.Parse(User.FindFirst("userId")?.Value ?? throw new Exception("Unauthorized"));
            var result = await _libService.GetUserLibraryAsync(userId);
            return Ok(result);
        }

    }
}