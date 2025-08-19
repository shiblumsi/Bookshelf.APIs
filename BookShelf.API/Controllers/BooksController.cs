using BookShelf.Application.Common;
using BookShelf.Application.DTOs;
using BookShelf.Application.Interface;
using BookShelf.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService) 
        {
            _bookService = bookService;
        }

        [HttpPost("add")]
        public async Task<APIServiceResponse> AddBook(APIServiceRequest reqObject)
        {
            var objResponse = new APIServiceResponse();
            try
            {
                objResponse.RequestDateTime = reqObject.RequestDateTime;

                var dto = JsonConvert.DeserializeObject<BookDto>(reqObject.BusinessData.ToString());
                var addedBook = await _bookService.AddBook(dto);

                objResponse.ResponseStatus = true;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
                objResponse.SuccessMsg = "Book added successfully";
                objResponse.ResponseBusinessData = addedBook;
                objResponse.ResponseCode = 200;
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = false;
                objResponse.ResponseCode = 500;
                objResponse.ErrMsg = ex.InnerException != null ? ex.GetBaseException().Message : ex.Message;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
            }

            return objResponse;
        }


        [HttpGet("get-all")]
        public async Task<APIServiceResponse> GetAllBooks()
        {
            var objResponse = new APIServiceResponse();
            objResponse.RequestDateTime = DateTime.Now.ToString();

            try
            {
                var books = await _bookService.GetAllBooks();

                objResponse.ResponseStatus = true;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
                objResponse.SuccessMsg = "Books fetched successfully";
                objResponse.ResponseBusinessData = books;
                objResponse.TotalRecords = books.Count;
                objResponse.ResponseCode = 200;
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = false;
                objResponse.ResponseCode = 500;
                objResponse.ErrMsg = ex.InnerException != null ? ex.GetBaseException().Message : ex.Message;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
            }

            return objResponse;
        }

        [HttpGet("get/{id}")]
        public async Task<APIServiceResponse> GetBook(int id)
        {
            var objResponse = new APIServiceResponse();
            objResponse.RequestDateTime = DateTime.Now.ToString();

            try
            {
                var book = await _bookService.GetBookById(id);

                if (book != null)
                {
                    objResponse.ResponseStatus = true;
                    objResponse.ResponseDateTime = DateTime.Now.ToString();
                    objResponse.SuccessMsg = "Book fetched successfully";
                    objResponse.ResponseBusinessData = book;
                    objResponse.ResponseCode = 200;
                }
                else
                {
                    objResponse.ResponseStatus = false;
                    objResponse.ResponseCode = 404;
                    objResponse.ErrMsg = "Book not found";
                }
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = false;
                objResponse.ResponseCode = 500;
                objResponse.ErrMsg = ex.InnerException != null ? ex.GetBaseException().Message : ex.Message;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
            }

            return objResponse;
        }

        [HttpDelete("{id}")]
        public async Task<APIServiceResponse> DeleteBook(int id)
        {
            var objResponse = new APIServiceResponse();
            objResponse.RequestDateTime = DateTime.Now.ToString();

            try
            {
                await _bookService.DeleteBook(id);

                objResponse.ResponseStatus = true;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
                objResponse.SuccessMsg = "Book deleted successfully";
                objResponse.ResponseBusinessData = $"Book with ID {id} deleted";
                objResponse.ResponseCode = 200;
            }
            catch (Exception ex)
            {
                objResponse.ResponseStatus = false;
                objResponse.ResponseCode = 500;
                objResponse.ErrMsg = ex.InnerException != null ? ex.GetBaseException().Message : ex.Message;
                objResponse.ResponseDateTime = DateTime.Now.ToString();
            }

            return objResponse;
        }
    }
}
