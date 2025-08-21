using System;
using System.Net;

namespace BookShelf.Application.Common
{
    public class ApiResponse<T>
    {

        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;

        public bool IsSuccess { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string? Message { get; set; }
        public string? Error { get; set; }


        public T? Data { get; set; }
        public object? Meta { get; set; }


        // Success factory method

        public static ApiResponse<T> Success(
            T data,
            string? message = null,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            object? meta = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = statusCode,
                Meta = meta,
                ResponseTime = DateTime.UtcNow
            };
        }


        // Fail factory method

        public static ApiResponse<T> Fail(
            string error,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            T? data = default,
            object? meta = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                Error = error,
                StatusCode = statusCode,
                Data = data,
                Meta = meta,
                ResponseTime = DateTime.UtcNow
            };
        }
    }
}
