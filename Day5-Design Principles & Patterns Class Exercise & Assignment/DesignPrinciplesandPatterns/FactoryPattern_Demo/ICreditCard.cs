namespace FactoryPattern_Demo
{
    internal interface ICreditCard
    {
        string GetCardType();
        int GetCreditLimit();
        int GetAnnualCharge();
    }
    public class MoneyBack : ICreditCard
    {
        public int GetAnnualCharge()
        {
            return 1200;
        }
        public string  GetCardType()
        {
            return "MoneyBack";
        }
        public int GetCreditLimit()
        {
            return 90000;
        }
    }
    public class Platinum : ICreditCard
    {
        public int GetAnnualCharge()
        {
            return 900;
        }
        public string GetCardType()
        {
            return "Platinum";
        }
        public int GetCreditLimit()
        {
            return 88888;
        }
    }
    public class Titanium : ICreditCard
    {
        public int GetAnnualCharge()
        {
            return 1100;
        }
        public string GetCardType()
        {
            return "Titanium";
        }
        public int GetCreditLimit()
        {
            return 78000;
        }
    }
}
