using CustomMiddleware_Demo.CustomExceptions;
using System.Net;

namespace CustomMiddleware_Demo.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = ex switch
            {
                EntityNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };
            context.Response.StatusCode = (int)statusCode;
            var result = new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Detailed = ex.StackTrace
            };
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}