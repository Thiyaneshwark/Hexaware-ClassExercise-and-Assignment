namespace AssetManagement.Interface
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);

        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int id);
        Task Save();
    }
}
