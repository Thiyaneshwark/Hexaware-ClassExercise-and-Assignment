using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_DatabaseFirst_Demo.Models;
using WebApplication_DatabaseFirst_Demo.Repositories;

namespace WebApplication_DatabaseFirst_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerServices.GetAllCustomers();
                if (customers != null && customers.Count > 0)
                    return Ok(customers);  // Return status 200 with the list of customers
                return NoContent();  // Return status 204 if no customers found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var customer = _customerServices.GetCustomerById(id);
                if (customer != null)
                    return Ok(customer);  // Return status 200 with the customer data
                return NotFound();  // Return status 404 if customer not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult AddNewCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null.");
                }

                int customerId = _customerServices.AddNewCustomer(customer);
                if (customerId > 0)
                    return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customer); // Return status 201 (Created)
                return BadRequest("Failed to create customer.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Customer/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null.");
                }

                if (id != customer.CustomerId)
                {
                    return BadRequest("Customer ID mismatch.");
                }

                string updateResult = _customerServices.UpdateCustomer(customer);
                if (updateResult == "Customer record updated successfully")
                    return Ok(updateResult);  // Return status 200 with success message
                return NotFound("Customer not found.");  // Return status 404 if customer is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                string deleteResult = _customerServices.DeleteCustomer(id);
                if (deleteResult.Contains("Removed"))
                    return Ok(deleteResult);  // Return status 200 with success message
                return NotFound(deleteResult);  // Return status 404 if customer is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
