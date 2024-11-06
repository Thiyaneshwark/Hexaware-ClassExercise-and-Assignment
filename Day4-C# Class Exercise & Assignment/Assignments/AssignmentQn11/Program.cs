using System.Text.Json;
using System.Xml.Serialization;
namespace AssignmentQn11
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating test data's
            var authors = new List<Author>
            {
                new Author { Id = 1, Name = "Author 1", Age = 45 },
                new Author { Id = 2, Name = "Author 2", Age = 50 },
                new Author { Id = 3, Name = "Author 3", Age = 39 },
                new Author { Id = 4, Name = "Author 4", Age = 60 },
                new Author { Id = 5, Name = "Author 5", Age = 30 }
            };

            var books = new List<Book>
            {
                new Book { Id = 101, Title = "Book 1", Genre = "Fiction", AuthorId = 1 },
                new Book { Id = 102, Title = "Book 2", Genre = "Adventure", AuthorId = 2 },
                new Book { Id = 103, Title = "Book 3", Genre = "Romance", AuthorId = 3 },
                new Book { Id = 104, Title = "Book 4", Genre = "Sci-Fi", AuthorId = 4 },
                new Book { Id = 105, Title = "Book 5", Genre = "Horror", AuthorId = 5 }
            };

            // JSON Serialization
            string jsonAuthors = JsonSerializer.Serialize(authors);
            string jsonBooks = JsonSerializer.Serialize(books);

            File.WriteAllText("authors.json", jsonAuthors);
            File.WriteAllText("books.json", jsonBooks);

            // XML Serialization
            var authorSerializer = new XmlSerializer(typeof(List<Author>));
            using (var authorStream = new FileStream("authors.xml", FileMode.Create))
            {
                authorSerializer.Serialize(authorStream, authors);
            }

            var bookSerializer = new XmlSerializer(typeof(List<Book>));
            using (var bookStream = new FileStream("books.xml", FileMode.Create))
            {
                bookSerializer.Serialize(bookStream, books);
            }

            // JSON Deserialization and displaying
            var jsonAuthorsFromFile = File.ReadAllText("authors.json");
            var jsonBooksFromFile = File.ReadAllText("books.json");

            var deserializedAuthors = JsonSerializer.Deserialize<List<Author>>(jsonAuthorsFromFile);
            var deserializedBooks = JsonSerializer.Deserialize<List<Book>>(jsonBooksFromFile);

            Console.WriteLine("Authors from JSON:");
            foreach (var author in deserializedAuthors)
            {
                Console.WriteLine($"ID: {author.Id}, Name: {author.Name}, Age: {author.Age}");
            }

            Console.WriteLine("\nBooks from JSON:");
            foreach (var book in deserializedBooks)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Genre: {book.Genre}, AuthorId: {book.AuthorId}");
            }

            // XML Deserialization and displaying
            using (var authorStream = new FileStream("authors.xml", FileMode.Open))
            {
                var xmlAuthors = (List<Author>)authorSerializer.Deserialize(authorStream);
                Console.WriteLine("\nAuthors from XML:");
                foreach (var author in xmlAuthors)
                {
                    Console.WriteLine($"ID: {author.Id}, Name: {author.Name}, Age: {author.Age}");
                }
            }

            using (var bookStream = new FileStream("books.xml", FileMode.Open))
            {
                var xmlBooks = (List<Book>)bookSerializer.Deserialize(bookStream);
                Console.WriteLine("\nBooks from XML:");
                foreach (var book in xmlBooks)
                {
                    Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Genre: {book.Genre}, AuthorId: {book.AuthorId}");
                }
            }
        }
    }
}