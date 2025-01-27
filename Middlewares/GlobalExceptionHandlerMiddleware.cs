using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Task_Web_API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context,[FromServices] IHostEnvironment hostEnvironment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex, hostEnvironment);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, [FromServices] IHostEnvironment hostEnvironment)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                TaskNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new ResponseDto
            {
                Success = false,
                Message = ex?.Message ?? "An unexpected error occurred. Please try again later.",
                Data = hostEnvironment.IsDevelopment() ? ex?.InnerException?.StackTrace ?? ex?.StackTrace : "Contact Support Team !"
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
