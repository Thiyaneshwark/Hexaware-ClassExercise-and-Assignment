using Assignment_DBApproach.Models;
using System.Collections.Generic;

namespace Assignment_DBApproach.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        int AddCategory(Category category);
        string UpdateCategory(Category category);
        string DeleteCategory(int id);
    }
}
