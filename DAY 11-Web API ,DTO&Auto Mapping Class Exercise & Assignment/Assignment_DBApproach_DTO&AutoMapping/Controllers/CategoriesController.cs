using Assignment_DBApproach.Models;
using Assignment_DBApproach.Repositories;
using Assignment_DBApproach.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Assignment_DBApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> categories = _repository.GetAllCategories();
            // Map Category list to CategoryDTO list
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            Category category = _repository.GetCategoryById(id);
            if (category != null)
            {
                // Map Category to CategoryDTO
                CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(category);
                return Ok(categoryDTO);
            }
            return NotFound("Category not found");
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            int result = _repository.AddCategory(category);
            if (result > 0)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add category");
        }

        [HttpPut]
        public IActionResult Put(Category category)
        {
            string result = _repository.UpdateCategory(category);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string result = _repository.DeleteCategory(id);
            return Ok(result);
        }
    }
}
