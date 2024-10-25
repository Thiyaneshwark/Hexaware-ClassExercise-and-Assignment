using System;

namespace OrderByDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the OrderByDemo class
            OrderByDemo demo = new OrderByDemo();

            // Call the order method to demonstrate ordering students
            demo.order();

            // Keep the console window open until a key is pressed
            Console.ReadKey();
        }
    }
}