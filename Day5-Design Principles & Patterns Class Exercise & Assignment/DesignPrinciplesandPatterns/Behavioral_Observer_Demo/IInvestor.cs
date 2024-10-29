namespace Behavioral_Observer_Demo
{
    public interface IInvestor
    {
        void Update(Stock stock);
    }
    public class Stock
    {
        private string _symbol;
        private double _price;
        private List<IInvestor> _investors = new List<IInvestor>();

        public Stock(string symbol, double price)
        {
            _symbol = symbol;
            _price = price;
        }
        public string Symbol => _symbol;
        public double Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }
        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }
        public void Detch(IInvestor investor)
        {
            _investors.Remove(investor);
        }
        public void Notify()
        {
            foreach (var investor in _investors)
            {
                investor.Update(this);
            }
        }
    }
    public class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;
        public Investor(string name)
        {
            _name = name;
        }
        public void Update(Stock stock)
        {
            _stock = stock;
            Console.WriteLine($"Notified {_name} of {_stock.Symbol}'s Price Chnge to {stock.Price}");
        }
    }
}
