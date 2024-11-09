using Microsoft.AspNetCore.Mvc;
using WebAPI_DatabaseApproach.Models;
using WebAPI_DatabaseApproach.Repositories;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllCustomers());

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _service.GetCustomerById(id);
            return customer != null ? Ok(customer) : NotFound("Customer not found");
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            int result = _service.AddNewCustomer(customer);
            return result > 0 ? Ok(result) : BadRequest("Failed to add customer");
        }

        [HttpPut]
        public IActionResult Put(Customer customer) => Ok(_service.UpdateCustomer(customer));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok(_service.DeleteCustomer(id));
    }
}
