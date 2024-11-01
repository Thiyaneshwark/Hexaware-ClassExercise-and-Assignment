namespace ProductApp
{
    public interface IOrderService
    {
        bool PlaceOrder(string productName, int quantity);
    }
}
