using CustomMiddleware_Demo.Middleware;
using CustomMiddleware_Demo.Models;

namespace CustomMiddleware_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BikeStoresContext>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            /// Use the custom middleware
            app.UseCustomMiddleware();

            app.UseHttpsRedirection();
            ///
            app.UseExceptionHandlingMiddleware();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
