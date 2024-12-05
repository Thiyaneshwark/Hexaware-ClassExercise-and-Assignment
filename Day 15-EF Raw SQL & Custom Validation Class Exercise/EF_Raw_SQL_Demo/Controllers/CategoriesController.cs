using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EF_Raw_SQL_Demo.Models;

namespace EF_Raw_SQL_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly BikeStoresContext _context;

        public CategoriesController(BikeStoresContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            //  await  _context.Categories.ToListAsync();
            var categories = await _context.Categories.FromSqlRaw("SELECT * FROM Production.categories").ToListAsync();
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            //   var category = await _context.Categories.FindAsync(id);
            var category = await _context.Categories.FromSqlRaw("SELECT * FROM Production.categories where category_id={0} ", id)
                .FirstOrDefaultAsync();
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }


            //  _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.Database.ExecuteSqlRawAsync($"update production.categories set category_name='{category.CategoryName}' where category_id={id}");
                // await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'BikestoresContext.Categories'  is null.");
            }
            await _context.Database
                .ExecuteSqlRawAsync
                ("INSERT INTO Production.Categories (category_name) VALUES ({0})", category.CategoryName);
            // _context.Categories.Add(category);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Database.ExecuteSqlRaw($"delete from Production.Categories where category_id={id}");
            //  _context.Categories.Remove(category);
            //  await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}