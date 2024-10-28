using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple_Demo
{
    internal class SavingsAccount : IBasicAccount
    {
        private decimal balance;
        public void CheckBalance(int accountNumber)
        {
            Console.WriteLine($"Current Balance ={balance}");
        }
        public void Deposit(decimal amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount:F2} \n cirrent Balance {balance:F2}");
        }
        public void Withdraw(decimal amount)
        {
            if(amount<=balance)
            {
                balance-=amount;
                Console.WriteLine($"Withdraw the {amount:F2} \n Current Balance :{balance}");
            }
        }
    }
}
