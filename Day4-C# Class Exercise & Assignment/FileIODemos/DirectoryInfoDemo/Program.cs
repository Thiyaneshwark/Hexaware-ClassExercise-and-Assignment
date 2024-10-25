using System;

namespace DirectoryInfoDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfoDemo demo = new DirectoryInfoDemo();
            demo.Demo();

            Console.WriteLine("File and directory copying process completed.");
            Console.ReadLine();
        }
    }
}
