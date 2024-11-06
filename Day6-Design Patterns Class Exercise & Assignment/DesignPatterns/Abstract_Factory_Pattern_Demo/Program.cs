using Abstract_Factory_Pattern_Demo;

IUFactory lightfactory = new LightUIFactory();
Client lightClient = new Client(lightfactory);
lightClient.RenderUI();

IUFactory darkfactory = new DarkUIFactory();
Client darkClient = new Client(darkfactory);
darkClient.RenderUI();

Console.ReadLine();
