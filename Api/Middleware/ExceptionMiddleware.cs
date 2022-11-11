using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Api.Errors;

namespace Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment host;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
        {
            this.next = next;
            this.logger = logger;
            this.host = host;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = this.host.IsDevelopment() ?
                new ApiException((int)HttpStatusCode.InternalServerError, ex.Message,
                ex.StackTrace.ToString()) : new ApiException((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);


            }
        }

    }
}