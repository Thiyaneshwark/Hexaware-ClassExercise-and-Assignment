using InterfaceSegregationPrinciple_Demo;
IBasicAccount savings = new SavingsAccount();
savings.Deposit(19000);
savings.Withdraw(3000);
savings.CheckBalance(1);

IInvestmentAccount account = new InvestmentAccount();
account.Deposit(23000);
account.BuyShares(3);
account.SellShares(2);
account.Withdraw(1000);
Console.ReadLine();