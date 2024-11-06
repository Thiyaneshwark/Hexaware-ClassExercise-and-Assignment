
namespace ASSIGNMENT__SRP
{
    public class Transaction  //--> Represents a Transaction Record
    {
        public DateTime Date { get; }
        public string Type { get; }
        public decimal Amount { get; }

        public Transaction(DateTime date, string type, decimal amount)
        {
            Date = date;
            Type = type;
            Amount = amount;
        }
    }
}

