using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GenerateDefaultMessageForStatusCode(StatusCode);
        }


        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GenerateDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Unauthorised, you are not",
                404 => "Resource found, it was not",
                500 => "Internal server error",
                503 => "Service Unavailable",
                _ => "Unknown error"
            };
        }

    }
}