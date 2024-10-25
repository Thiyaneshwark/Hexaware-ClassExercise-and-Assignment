using System;

namespace GroupByDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            GroupByDemo groupByDemo = new GroupByDemo();
            groupByDemo.groupby();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}