using FactoryPattern_Demo;

Console.WriteLine("Enter the CardType \n MoneyBack, \n Platinum, \n Titanium");
string cardType=Console.ReadLine();

ICreditCard card=CreditcardFactory.GetCreditCard(cardType);
if(card != null )
{
    Console.WriteLine("Card Type ="+card.GetCardType());
    Console.WriteLine("Card Limit =" + card.GetCreditLimit());
    Console.WriteLine("Annual Charge =" + card.GetAnnualCharge());
}
else
{
    Console.WriteLine("Invalid CardType");
}
Console.ReadLine();