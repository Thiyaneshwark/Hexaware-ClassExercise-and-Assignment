using WebAPI_DatabaseApproach.Models;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Repositories
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        int AddNewOrder(Order order);
        string UpdateOrder(Order order);
        string DeleteOrder(int id);
    }
}
