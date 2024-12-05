namespace API_CF_Demo1.Middleware
{
    public static class CustomMiddlewareExtensions
    {
      
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }

}

