using Deserialization;
using System.Text.Json;

string readResult = File.ReadAllText("D:\\HEXAWARE\\C#\\DOT NET FSD\\Assignments\\Day4-C# Class Exercise\\Products.json");
Product product = JsonSerializer.Deserialize<Product>(readResult); ;
Console.WriteLine($"Id: {product.Id}\n Name: {product.Name}\n"+
    $"Description: {product.Description}\n Price: {product.Price}");
Console.ReadLine();
