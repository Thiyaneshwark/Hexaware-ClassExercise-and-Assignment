namespace ASSIGNMENT__SRP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bankSystem = new BankSystem(1000);

            // --->Performing deposit
            bankSystem.Account.Deposit(500);

            // --->Performing withdrawal
            bankSystem.Account.Withdraw(200);

            // --->Generating statements
            bankSystem.StatementGenerator.GenerateMonthlyStatement(DateTime.Now.Month, DateTime.Now.Year);
            bankSystem.StatementGenerator.GenerateYearlyStatement(DateTime.Now.Year);
        }
    }
}
