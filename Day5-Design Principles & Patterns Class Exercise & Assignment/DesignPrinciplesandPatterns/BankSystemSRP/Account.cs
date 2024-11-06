namespace ASSIGNMENT_SRP
{
    public class Account //---> Manages Balance, Deposit, and Withdrawals
    {
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(decimal initialBalance)
        {
            Balance = initialBalance;
            Transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }
            Balance += amount;
            Console.WriteLine($"Deposited {amount:F2}. New Balance: {Balance:F2}");
            AddTransaction("Deposit", amount);
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return;
            }
            if (amount > Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount:F2}. New Balance: {Balance:F2}");
            AddTransaction("Withdrawal", amount);
        }

        private void AddTransaction(string type, decimal amount)
        {
            Transactions.Add(new Transaction(DateTime.Now, type, amount));
        }
    }
}
