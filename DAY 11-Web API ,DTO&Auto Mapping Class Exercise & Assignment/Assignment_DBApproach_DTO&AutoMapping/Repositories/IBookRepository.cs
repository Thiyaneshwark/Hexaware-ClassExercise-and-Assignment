using Assignment_DBApproach.Models;
using System.Collections.Generic;

namespace Assignment_DBApproach.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
        Book GetBookById(int id);
        int AddBook(Book book);
        string UpdateBook(Book book);
        string DeleteBook(int id);
    }
}
