using SerializationandDeserializationDemos;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

//Product product = new Product() { Id = 1, Name = "Mobile", Description = "Filp Type", Price = 70000 };
//string jsonString=JsonSerializer.Serialize(product);
//Console.WriteLine($" Serialized Data \n {jsonString}");
//File.WriteAllText("D:\\HEXAWARE\\C#\\DOT NET FSD\\Assignments\\Day4-C# Class Exercise\\Products.json", jsonString);

//string readResult = File.ReadAllText("D:\\HEXAWARE\\C#\\DOT NET FSD\\Assignments\\Day4-C# Class Exercise\\Products.json");
//Product product = JsonSerializer.Deserialize<Product>(readResult); ;
//Console.WriteLine($"Id: {product.Id}\n Name: {product.Name}\n" +
//    $"Description: {product.Description}\n Price: {product.Price}");
//Console.ReadLine();


//Serialize inte XML Format Exmaple:
Product product = new Product(){ Id = 1, Name = "Mobile", Description = "Filp Type", Price = 70000 };
string filepath = "D:\\HEXAWARE\\C#\\DOT NET FSD\\Assignments\\Day4-C# Class Exercise\\Products1.dat";
XmlSerializer serializer = new XmlSerializer(typeof(Product));
using (FileStream fs = new FileStream(filepath, FileMode.Create))
{
    serializer.Serialize(fs, product);
}
Console.WriteLine("Product Seria; ized to XML Format");

// Example for Deserialize from XML to Product type
// string filpath = "D:\\Product1.dat";
//XmlSerializer serializer = new XmlSerializer(typeof(Product));

using(FileStream fs = new FileStream(filepath, FileMode.Open))
{
    Product product1 = (Product)serializer.Deserialize(fs);

    Console.WriteLine($"{product1.Id} \n" +
        $" {product1.Name} \n {product1.Description} \n {product1.Price}");
}
Console.ReadLine();

