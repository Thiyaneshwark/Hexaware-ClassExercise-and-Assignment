namespace API_CF_Demo1.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Custom logic before calling the next middleware
            Console.WriteLine("Custom Middleware: Before Request");

            // Call the next middleware in the pipeline
            await _next(context);

            // Custom logic after calling the next middleware
            Console.WriteLine("Custom Middleware: After Request");
        }
    }

}

