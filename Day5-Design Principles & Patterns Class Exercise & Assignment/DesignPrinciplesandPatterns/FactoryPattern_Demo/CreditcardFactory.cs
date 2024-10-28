namespace FactoryPattern_Demo
{
    internal class CreditcardFactory
    {
        public static ICreditCard GetCreditCard(string cardType)
        {
            ICreditCard card = null;
            if (cardType == "MoneyBack")
            {
                card = new MoneyBack();
            }
            else if (cardType == "Platinum")
            {
                card = new Platinum();
            }
            else if (cardType == "Titanium")
            {
                card = new Titanium();
            }
            return card;
        }
    }
}
