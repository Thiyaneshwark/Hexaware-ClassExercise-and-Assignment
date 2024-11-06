using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple_Demo
{
    internal class InvestmentAccount : IInvestmentAccount
    {
        private decimal balance;
        private int shares;

        public void BuyShares(int numberOfShares)
        {
            shares += numberOfShares;
            Console.WriteLine($"Bought {numberOfShares} shares. \n Total Shares= {shares}");
        }

        public void CheckBalance(int accountNumber)
        {
            Console.WriteLine($"Current Balance = {balance}");
        }

        public void Deposit(decimal amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:F2} \n Current Balance {balance:F2}");
        }
        public void SellShares(int numberOfShares)
        {
            if (numberOfShares <= shares)
            {
                shares -= numberOfShares;
                Console.WriteLine($"Sold {numberOfShares} shares. Total Shares {shares}");
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount:F2} \n Current Balance: {balance}");
            }
            else
            {
                Console.WriteLine("Insufficient Balance");
            }
        }
    }
}