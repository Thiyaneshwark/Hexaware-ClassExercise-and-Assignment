//LINQ_DEMO---------------------------------------------------
//string[] names = { "Prateek", "Thiyanesh", "Hrithik", "Varsha", "Manan" };

////Query Syntax
//var nameContainsA= from name in names
//                   where (name.Contains('a'))
//                   select name;
//Console.WriteLine("\n Namewith A List ");
//foreach (var name in nameContainsA)
//{
//    Console.WriteLine(name);
//}

//// Method Syntax
//var namesWithR = names.Where(n => n.Contains('r')).ToList();
//Console.WriteLine("\n Names with R List ");
//foreach (var name in namesWithR)
//{
//    Console.WriteLine(name);
//}
//Console.ReadLine(); 



//OfTypeOperatorDemo---------------------------------------------------
//using System;

//namespace LINQ_Demo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            OfTypeOperatorDemo demo = new OfTypeOperatorDemo();


//            demo.ofTypeDemo();

//            Console.ReadKey();
//        }
//    }
//}


//OrderByDemo------------------------------------------------
//using System;

//namespace LINQ_Demo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Create an instance of the OrderByDemo class
//            OrderByDemo demo = new OrderByDemo();

//            // Call the order method to demonstrate ordering students
//            demo.order();

//            // Keep the console window open until a key is pressed
//            Console.ReadKey();
//        }
//    }
//}




//GroupByDemo------------------------------------------------
//using System;

//namespace LINQ_Demo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            GroupByDemo groupByDemo = new GroupByDemo();
//            groupByDemo.groupby();
//            Console.WriteLine("\nPress any key to exit...");
//            Console.ReadKey();
//        }
//    }
//}


//JoinsDemo------------------------------------------------
//using System;

//namespace LINQ_Demo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            JoinsDemo joinsDemo = new JoinsDemo();
//            joinsDemo.JoinDemo();
//            Console.WriteLine("\nPress any key to exit...");
//            Console.ReadKey();
//        }
//    }
//}


//FirstAndFirstOrDefaultDemo------------------------------------------------
using System;

namespace LINQ_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstAndFirstOrDefaultDemo demo = new FirstAndFirstOrDefaultDemo();
            demo.FirstOrDefaultDemo();
            demo.FirstDemo();

            // Wait for user input before closing the console
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}





