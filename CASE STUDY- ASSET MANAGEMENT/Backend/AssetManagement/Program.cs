using AssetManagement.Interface;
using AssetManagement.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Logs can be viewed in console

 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAsset, AssetService>();
builder.Services.AddScoped<IAssetAllocation, AssetAllocationService>();
builder.Services.AddScoped<IAssetRequest, AssetRequestService>();
builder.Services.AddScoped<IAuditRepo, AuditRepo>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<IMaintenanceLogRepo, MaintenanceLogRepo>();
builder.Services.AddScoped<IReturnReqRepo, ReturnRequestRepo>();
builder.Services.AddScoped<IServiceRequest, ServiceRequestImpl>();
builder.Services.AddScoped<ISubCategory, SubCategoryService>();
//builder.Services.AddScoped<IUserProfileRepo, UserProfileRepo>();

 
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

// Add authorization policy for Employee role
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("Employee"));

});

// JWT Authentication setup
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // Validates token expiration
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var claims = context.Principal?.Claims;
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim: {claim.Type} - {claim.Value}");
            }
            return Task.CompletedTask;
        }
    };
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:3000")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

// Configure CORS to allow cross-origin requests
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure controllers with JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Swagger configuration for JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Asset Management API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Enable detailed error pages in development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asset Management API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
//app.UseCors("AllowReactApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
