using Assignment_DBApproach.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_DBApproach.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDBContext _context;

        public BookRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public int AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book.BookId;
        }

        public string UpdateBook(Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.BookId == book.BookId);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.AuthorId = book.AuthorId;
                existingBook.CategoryId = book.CategoryId;
                existingBook.Price = book.Price;

                _context.SaveChanges();
                return "Book updated successfully";
            }
            return "Book not found";
        }

        public string DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return $"Book with ID {id} has been removed";
            }
            return "Book not found";
        }
    }
}
