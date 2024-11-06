using WebAPI_DatabaseApproach.Data;
using WebAPI_DatabaseApproach.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_DatabaseApproach.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _context;

        public OrderService(MyDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAllOrders() => _context.Orders.ToList();

        public Order GetOrderById(int id) => _context.Orders.FirstOrDefault(o => o.OrderId == id);

        public int AddNewOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.OrderId;
        }

        public string UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.TotalPrice = order.TotalPrice;
                existingOrder.ShippingAddress = order.ShippingAddress;
                existingOrder.CustomerId = order.CustomerId;
                _context.Entry(existingOrder).State = EntityState.Modified;
                _context.SaveChanges();
                return "Order updated successfully";
            }
            return "Order not found";
        }

        public string DeleteOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return $"Order with ID {id} has been removed";
            }
            return "Order not found";
        }
    }
}
