using AutoMapper;
using DTO_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DTO_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MyDbContext _context;
        public BooksController(MyDbContext context, IMapper Mapper)
        {
            _context = context;
            _mapper = Mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> GetBooks()
        {
            List<Book> books=_context.Books.ToList();
            if(books !=null)
            {
                //List<BookDTO> bookDTOs = books.ToDTOList();
                List<BookDTO> bookDTOs=_mapper.Map<List<BookDTO>>(books);
                return bookDTOs;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public ActionResult PostBook(BookDTO bookDTO)
        {
            var book=_mapper.Map<Book>(bookDTO);
            //var book = bookDTO.ToEntity();
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PostBook), new { id = book.Id }, bookDTO);
        }
    }
}
