﻿namespace AssetManagement.Interface
{
    public interface ISubCategory
    {
        Task<List<SubCategory>> GetAllSubCategories();
        Task<SubCategory> GetSubCategoryById(int id);

        Task AddSubCategory(SubCategory subcategory);
        Task<SubCategory> UpdateSubCategory(SubCategory subcategory);
        Task DeleteSubCategory(int id);

        Task Save();
    }
}
