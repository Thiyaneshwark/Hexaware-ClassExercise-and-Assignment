namespace ProductApp
{
    public interface IProductService
    {
        void AddProduct(Product product);
        bool UpdateStock(string productName, int quantity);
        int CheckStock(string productName);
    }
}
