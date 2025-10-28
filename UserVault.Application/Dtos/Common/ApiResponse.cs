using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserVault.Application.Dtos.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public Dictionary<string, object> Meta { get; set; } = new();
        public List<string> Errors { get; set; } = new();

        public ApiResponse() { }

        public ApiResponse(
            bool success,
            int statusCode,
            string message,
            T? data)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public ApiResponse(
            bool success,
            int statusCode,
            string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = default;
        }
    }
}
