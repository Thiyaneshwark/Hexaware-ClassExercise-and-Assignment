namespace CustomMiddleware_Demo.Middleware
{
    public static class ExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}