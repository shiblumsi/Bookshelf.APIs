using BookShelf.Application.Common;
using BookShelf.Application.DTOs.Requests;
using BookShelf.Application.DTOs.Responses;
using BookShelf.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService _bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddCategoryRequestDto dto)
        {
            try
            {
                var created = await _bookCategoryService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id },
                    ApiResponse<CategoryResponseDto>.Success(created, "Category created successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }



        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _bookCategoryService.GetAllAsync();
                return Ok(ApiResponse<List<CategoryResponseDto>>.Success(categories, "Categories fetched successfully"));
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
                var category = await _bookCategoryService.GetByIdAsync(id);
                if (category == null)
                    return NotFound(ApiResponse<string>.Fail("Category not found", HttpStatusCode.NotFound));

                return Ok(ApiResponse<CategoryResponseDto>.Success(category, "Category fetched successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddCategoryRequestDto dto)
        {
            try
            {
                var updated = await _bookCategoryService.UpdateAsync(id, dto);
                if (updated == null)
                    return NotFound(ApiResponse<string>.Fail("Category not found", HttpStatusCode.NotFound));

                return Ok(ApiResponse<CategoryResponseDto>.Success(updated, "Category updated successfully"));
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
                var result = await _bookCategoryService.DeleteAsync(id);
                if (!result)
                    return NotFound(ApiResponse<string>.Fail("Category not found", HttpStatusCode.NotFound));

                return Ok(ApiResponse<bool>.Success(true, "Category deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Fail(ex.Message, HttpStatusCode.InternalServerError));
            }
        }
    }

}
