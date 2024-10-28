namespace SingleResponsibilityPrinciple
{
    public class BankSystem
    {
        public Account Account { get; private set; }
        public StatementGenerator StatementGenerator { get; private set; }

        public BankSystem(decimal initialBalance)
        {
            Account = new Account(initialBalance);
            StatementGenerator = new StatementGenerator(Account.Transactions);
        }
    }
}