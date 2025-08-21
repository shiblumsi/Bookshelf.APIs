//using BookShelf.Application.Common;
//using BookShelf.Application.Interface;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BookShelf.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LibraryController : ControllerBase
//    {
//        private readonly IUserBookService _userBookService;

//        public LibraryController(IUserBookService userBookService)
//        {
//            _userBookService = userBookService;
//        }
//        [Authorize]
//        [HttpPost("add-to-library")]
//        public async Task<IActionResult> AddToLibrary([FromQuery] int bookId)
//        {
//            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };

//            try
//            {
//                var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");

//                var result = await _userBookService.AddToLibraryAsync(userId, bookId);
//                if (result == null)
//                {
//                    response.ResponseStatus = false;
//                    response.ErrMsg = "Book already exists in your library or not found.";
//                    response.ResponseCode = 400;
//                }
//                else
//                {
//                    response.ResponseStatus = true;
//                    response.SuccessMsg = "Book added to your library successfully.";
//                    response.ResponseCode = 201;
//                    response.ResponseBusinessData = result;
//                }
//            }
//            catch (Exception ex)
//            {
//                response.ResponseStatus = false;
//                response.ErrMsg = ex.Message;
//                response.ResponseCode = 500;
//            }

//            return StatusCode(response.ResponseCode, response);
//        }

//        [Authorize]
//        [HttpGet("books")]
//        public async Task<IActionResult> GetLibrary()
//        {
//            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };
//            try
//            {
//                var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
//                var books = await _userBookService.GetLibraryAsync(userId);

//                response.ResponseStatus = true;
//                response.SuccessMsg = "Library fetched successfully.";
//                response.ResponseCode = 200;
//                response.ResponseBusinessData = books;
//                response.TotalRecords = books != null ? books.Count() : 0;
//            }
//            catch (Exception ex)
//            {
//                response.ResponseStatus = false;
//                response.ErrMsg = ex.Message;
//                response.ResponseCode = 500;
//            }

//            return StatusCode(response.ResponseCode, response);
//        }

//        [Authorize]
//        [HttpDelete("remove/{bookId}")]
//        public async Task<IActionResult> RemoveFromLibrary(int bookId)
//        {
//            var response = new APIServiceResponse { ResponseDateTime = DateTime.UtcNow.ToString("s") };
//            try
//            {
//                var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");

//                var success = await _userBookService.RemoveFromLibraryAsync(userId, bookId);
//                if (!success)
//                {
//                    response.ResponseStatus = false;
//                    response.ErrMsg = "Book not found in your library.";
//                    response.ResponseCode = 404;
//                }
//                else
//                {
//                    response.ResponseStatus = true;
//                    response.SuccessMsg = "Book removed from your library successfully.";
//                    response.ResponseCode = 200;
//                }
//            }
//            catch (Exception ex)
//            {
//                response.ResponseStatus = false;
//                response.ErrMsg = ex.Message;
//                response.ResponseCode = 500;
//            }

//            return StatusCode(response.ResponseCode, response);
//        }
//    }
//}