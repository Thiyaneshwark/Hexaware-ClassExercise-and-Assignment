using LiskovSegregationPrinciple_Demo;
IFruit fruit = new Orange();

Console.WriteLine("Color of Orange is "+fruit.GetColor());
fruit = new Apple();

Console.WriteLine("Color of Apple is "+fruit.GetColor());
Console.ReadLine();
