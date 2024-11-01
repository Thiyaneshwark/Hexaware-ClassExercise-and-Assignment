using System;
namespace ProductApp
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productService;
        public OrderService(IProductService productService)
        {
            _productService = productService;
        }
        public bool PlaceOrder(string productName, int quantity)
        {
            if (_productService.CheckStock(productName) >= quantity)
            {
                _productService.UpdateStock(productName, _productService.CheckStock(productName) - quantity);
                return true;
            }
            return false;
        }
    }
}
