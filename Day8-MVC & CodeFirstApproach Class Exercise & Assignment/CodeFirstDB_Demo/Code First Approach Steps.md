
create Console(or)MVC(or) web API Application

Create Models Folder

Add New Class inside the Models Folder

Department.cs
```cs
  //Plain Old CLR Object
  public class Department
  {
      public int DepartmentId { get; set; }
      public string Name { get; set; }
      public string Location { get; set; }
  }
```

we need install 2 packages from nuget Package manager

 <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="6.0.35" />
 <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="6.0.33">

Add new class MyContext.cs in Models Folder
 ```cs
   public class MyContext:DbContext
 {
     public MyContext(DbContextOptions<MyContext> options):base(options) { }
    
     public DbSet<Department> Departments { get; set; }
    

 ````

 goto appsettings.json
 ```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
   "ConnectionStrings": {
  "myconnection": "Server=(localdb)\\MSSQLLocalDB;Database=Hexa_oct_22_CFDB;Integrated Security=true;"
}
}

 ```

 Goto program.cs

 ```cs
 using Code_First_Demo1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection")));
var app = builder.Build();
````


build your applicaction ==> 

Goto package Manager Console

PM> add-migration "dbcreation"

It will create Migrations Folder==>using Microsoft.EntityFrameworkCore.Migrations;
```cs
#nullable disable

namespace Code_First_Demo1.Migrations
{
    public partial class dbcreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
```

PM> update-database

![alt text](image.png)