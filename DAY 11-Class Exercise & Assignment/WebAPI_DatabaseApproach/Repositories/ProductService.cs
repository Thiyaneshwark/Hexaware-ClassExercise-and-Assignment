using WebAPI_DatabaseApproach.Data;
using WebAPI_DatabaseApproach.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_DatabaseApproach.Repositories
{
    public class ProductService : IProductService
    {
        private readonly MyDbContext _context;

        public ProductService(MyDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts() => _context.Products.ToList();

        public Product GetProductById(int id) => _context.Products.FirstOrDefault(p => p.ProductId == id);

        public int AddNewProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.ProductId;
        }

        public string UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.StockQuantity = product.StockQuantity;
                _context.Entry(existingProduct).State = EntityState.Modified;
                _context.SaveChanges();
                return "Product updated successfully";
            }
            return "Product not found";
        }

        public string DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return $"Product with ID {id} has been removed";
            }
            return "Product not found";
        }
    }
}
