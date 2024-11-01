namespace ProductApp
{
    public class ProductService:IProductService
    {
        private readonly List<Product> _products = new List<Product>();
        public void AddProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Name == product.Name);
            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                _products.Add(product);
            }
        }
        public int CheckStock(string productName)
        {
            var product = _products.FirstOrDefault(p => p.Name == productName);
            return product?.Quantity ?? 0;
        }
        public bool UpdateStock(string productName, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Name == productName);
            if (product != null)
            {
                product.Quantity = quantity;
                return true; 
            }
            return false; 
        }

    }
}
