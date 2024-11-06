using Assignment_DBApproach.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_DBApproach.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDBContext _context;

        public CategoryRepository(LibraryDBContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public int AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category.CategoryId;
        }

        public string UpdateCategory(Category category)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();
                return "Category updated successfully";
            }
            return "Category not found";
        }

        public string DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return $"Category with ID {id} has been removed";
            }
            return "Category not found";
        }
    }
}
