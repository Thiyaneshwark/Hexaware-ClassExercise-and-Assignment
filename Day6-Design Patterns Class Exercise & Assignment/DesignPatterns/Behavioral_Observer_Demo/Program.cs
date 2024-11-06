using Behavioral_Observer_Demo;

Stock petrolStock = new Stock("pet", 56);
Stock bitcoinStock = new Stock("bit", 800);

Investor investor1 = new Investor("Peter");
Investor investor2 = new Investor("Ram");
Investor investor3 = new Investor("Sonna");

petrolStock.Attach(investor1);
petrolStock.Attach(investor3);

bitcoinStock.Attach(investor1);
bitcoinStock.Attach(investor2);
bitcoinStock.Attach(investor3);

petrolStock.Price = 78;
bitcoinStock.Price = 900;

Console.ReadLine();


