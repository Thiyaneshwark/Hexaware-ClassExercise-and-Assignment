using Structural_Decorator_Pattern_Demo;

PlainPizza plainPizza = new PlainPizza();
string plainPizza1 = plainPizza.MakePizza();
Console.WriteLine(plainPizza1);

PizzaDecorator pizzaDecorator = new ChickenPizza(plainPizza);
string chickenPizza = pizzaDecorator.MakePizza();
Console.WriteLine($"{chickenPizza} using PizzaDecorator");

pizzaDecorator = new VegetablePizza(plainPizza);
string vegPizza = pizzaDecorator.MakePizza();
Console.WriteLine($"{vegPizza} using PizzaDecorator");