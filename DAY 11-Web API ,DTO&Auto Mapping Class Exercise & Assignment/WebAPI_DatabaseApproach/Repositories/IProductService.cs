using WebAPI_DatabaseApproach.Models;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Repositories
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        int AddNewProduct(Product product);
        string UpdateProduct(Product product);
        string DeleteProduct(int id);
    }
}
