namespace SingleResponsibilityPrinciple
{
    public class StatementGenerator //--> For Handles Statement Generation
    {
        private readonly List<Transaction> _transactions;

        public StatementGenerator(List<Transaction> transactions)
        {
            _transactions = transactions;
        }

        public void GenerateMonthlyStatement(int month, int year)
        {
            Console.WriteLine($"\nMonthly Statement for {month}/{year}");
            Console.WriteLine("Date       \tType       \tAmount");
            Console.WriteLine(new string('-', 40));
            foreach (var transaction in _transactions)
            {
                if (transaction.Date.Month == month && transaction.Date.Year == year)
                {
                    Console.WriteLine($"{transaction.Date.ToShortDateString(),-10}\t{transaction.Type,-10}\t{transaction.Amount:F2}");
                }
            }
        }

        public void GenerateYearlyStatement(int year)
        {
            Console.WriteLine($"\nYearly Statement for {year}");
            Console.WriteLine("Date       \tType       \tAmount");
            Console.WriteLine(new string('-', 40));
            foreach (var transaction in _transactions)
            {
                if (transaction.Date.Year == year)
                {
                    Console.WriteLine($"{transaction.Date.ToShortDateString(),-10}\t{transaction.Type,-10}\t{transaction.Amount:F2}");
                }
            }
        }
    }
}
