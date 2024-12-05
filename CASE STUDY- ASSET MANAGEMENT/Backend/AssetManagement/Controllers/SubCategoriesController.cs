using AssetManagement.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ISubCategory _subcategory;

        public SubCategoriesController(DataContext context, ISubCategory subCategory)
        {
            _context = context;
            _subcategory = subCategory;
        }

        // GET: api/SubCategories
        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetSubCategories()
        {
            var subCategories = await _subcategory.GetAllSubCategories();
            return Ok(subCategories);
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult<SubCategory>> GetSubCategory(int id)
        {
            var subCategory = await _subcategory.GetSubCategoryById(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return Ok(subCategory);
        }

        // PUT: api/SubCategories/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutSubCategory(int id, SubCategory subCategory)
        {
            if (subCategory.SubCategoryId == 0)
            {
                subCategory.SubCategoryId = id;
            }
            if (id != subCategory.SubCategoryId)
            {
                return BadRequest("ID mismatch.");
            }

            _subcategory.UpdateSubCategory(subCategory);

            try
            {
                await _subcategory.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubCategories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SubCategory>> PostSubCategory(SubCategory subCategory)
        {
            await _subcategory.AddSubCategory(subCategory);
            await _subcategory.Save();

            if (subCategory.SubCategoryId == 0)
            {
                return BadRequest("SubCategory ID is not properly set.");
            }

            return CreatedAtAction(nameof(GetSubCategory), new { id = subCategory.SubCategoryId }, subCategory);
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            try
            {
                await _subcategory.DeleteSubCategory(id);
                await _subcategory.Save();
                return NoContent();
            }
            catch (Exception)
            {
                if (!_context.SubCategories.Any(s => s.SubCategoryId == id))
                    return NotFound();

                return BadRequest();
            }
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.SubCategoryId == id);
        }
    }
}
