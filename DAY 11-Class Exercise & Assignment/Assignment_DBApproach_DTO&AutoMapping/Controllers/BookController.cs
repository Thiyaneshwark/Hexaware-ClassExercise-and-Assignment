using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Assignment_DBApproach.DTOs;
using Assignment_DBApproach.Models;
using Assignment_DBApproach.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_DBApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            var books = _repository.GetAllBooks();
            if (books != null && books.Any())
            {
                // Mapping Book entities to BookDTO
                List<BookDTO> bookDTOs = _mapper.Map<List<BookDTO>>(books);
                return Ok(bookDTOs);
            }
            else
            {
                return NotFound("No books found.");
            }
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            var book = _repository.GetBookById(id);
            if (book != null)
            {
                // Mapping Book entity to BookDTO
                BookDTO bookDTO = _mapper.Map<BookDTO>(book);
                return Ok(bookDTO);
            }
            else
            {
                return NotFound("Book not found.");
            }
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult> PostBook(BookDTO bookDTO)
        {
            // Mapping BookDTO to Book entity
            var book = _mapper.Map<Book>(bookDTO);
            _repository.AddBook(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, bookDTO);
        }
        
        [HttpPut]
        public IActionResult Put(Book book)
        {
            string result = _repository.UpdateBook(book);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = _repository.DeleteBook(id);
            return Ok(result);
        }
    }
}
