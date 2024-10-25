using Serialization;
using System.Text.Json;

Product product = new Product() { Id = 1, Name = "Mobile", Description = "Filp Type", Price = 70000 };
string jsonString = JsonSerializer.Serialize(product);
Console.WriteLine($" Serialized Data \n {jsonString}");
File.WriteAllText("D:\\HEXAWARE\\C#\\DOT NET FSD\\Assignments\\Day4-C# Class Exercise\\Products.json", jsonString);


