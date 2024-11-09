using Microsoft.AspNetCore.Mvc;
using WebAPI_DatabaseApproach.Models;
using WebAPI_DatabaseApproach.Repositories;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAllOrders());

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _service.GetOrderById(id);
            return order != null ? Ok(order) : NotFound("Order not found");
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            int result = _service.AddNewOrder(order);
            return result > 0 ? Ok(result) : BadRequest("Failed to add order");
        }

        [HttpPut]
        public IActionResult Put(Order order) => Ok(_service.UpdateOrder(order));

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) => Ok(_service.DeleteOrder(id));
    }
}
