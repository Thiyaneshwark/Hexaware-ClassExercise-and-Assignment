using Microsoft.AspNetCore.Mvc;
using WebAPI_DatabaseApproach.Models;
using WebAPI_DatabaseApproach.Repositories;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllProducts());

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _service.GetProductById(id);
            return product != null ? Ok(product) : NotFound("Product not found");
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            int result = _service.AddNewProduct(product);
            return result > 0 ? Ok(result) : BadRequest("Failed to add product");
        }

        [HttpPut]
        public IActionResult Put(Product product) => Ok(_service.UpdateProduct(product));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok(_service.DeleteProduct(id));
    }
}
