using Assignment_DBApproach.Models;
using System.Collections.Generic;

namespace Assignment_DBApproach.Repositories
{
    public interface IAuthorRepository
    {
        List<Author> GetAllAuthors();
        Author GetAuthorById(int id);
        int AddAuthor(Author author);
        string UpdateAuthor(Author author);
        string DeleteAuthor(int id);
    }
}
