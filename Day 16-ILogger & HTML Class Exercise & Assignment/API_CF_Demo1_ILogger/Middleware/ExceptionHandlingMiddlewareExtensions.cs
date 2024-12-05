namespace API_CF_Demo1.Middleware
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
      
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }

}

