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
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _repository;

        public AuthorsController(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> GetAuthors()
        {
            var authors = _repository.GetAllAuthors();
            if (authors != null && authors.Any())
            {
                // Mapping Author entities to AuthorDTO
                List<AuthorDTO> authorDTOs = _mapper.Map<List<AuthorDTO>>(authors);
                return Ok(authorDTOs);
            }
            else
            {
                return NotFound("No authors found.");
            }
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            var author = _repository.GetAuthorById(id);
            if (author != null)
            {
                // Mapping Author entity to AuthorDTO
                AuthorDTO authorDTO = _mapper.Map<AuthorDTO>(author);
                return Ok(authorDTO);
            }
            else
            {
                return NotFound("Author not found.");
            }
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult> PostAuthor(AuthorDTO authorDTO)
        {
            // Mapping AuthorDTO to Author entity
            var author = _mapper.Map<Author>(authorDTO);
            _repository.AddAuthor(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorId }, authorDTO);
        }
        
        [HttpPut]
        public IActionResult Put(Author author)
        {
            string result = _repository.UpdateAuthor(author);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = _repository.DeleteAuthor(id);
            return Ok(result);
        }
    }
}
