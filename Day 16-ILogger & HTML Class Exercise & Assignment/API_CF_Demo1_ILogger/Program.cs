using API_CF_Demo1.Data;
using API_CF_Demo1.Middleware;
using API_CF_Demo1.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection")));
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });

    options.AddPolicy("AllowSpecificOrigin", 
        policy => { policy.WithOrigins("http://127.0.0.1:5500")
        .AllowAnyHeader()
        .AllowAnyMethod(); });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomMiddleware();

app.UseHttpsRedirection();
app.UseExceptionHandlingMiddleware();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
