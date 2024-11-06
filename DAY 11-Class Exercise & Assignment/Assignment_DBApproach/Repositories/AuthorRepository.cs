using Assignment_DBApproach.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_DBApproach.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDBContext _context;

        public AuthorRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthorById(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.AuthorId == id);
        }

        public int AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author.AuthorId;
        }

        public string UpdateAuthor(Author author)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.AuthorId == author.AuthorId);
            if (existingAuthor != null)
            {
                existingAuthor.Name = author.Name;
                _context.SaveChanges();
                return "Author updated successfully";
            }
            return "Author not found";
        }

        public string DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
                return $"Author with ID {id} has been removed";
            }
            return "Author not found";
        }
    }
}
