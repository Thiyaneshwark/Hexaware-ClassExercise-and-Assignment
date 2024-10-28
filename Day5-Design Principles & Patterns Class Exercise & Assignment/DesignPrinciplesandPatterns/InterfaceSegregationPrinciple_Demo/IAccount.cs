using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationPrinciple_Demo
{
    //public interface IAccount
    //{
    //    void Withdraw(decimal amount);
    //    void Deposit(decimal amount);
    //    void CheckBalance(int accountNumber);
    //    void BuyShares(int numberOfShares);
    //    void SellShares(int numberOfShares);
    //}

    public interface IBasicAccount
    {
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
        void CheckBalance(int accountNumber);
    }

    public interface IInvestmentAccount : IBasicAccount
    {
        void BuyShares(int numberOfShares);
        void SellShares(int numberOfShares);
    }
}