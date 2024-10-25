using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAMBDA_Demo
{
    internal class LambdaDemo
    {
        public void Demo()
        {
            List<int> numberList = new List<int> { 10, 20, 33, 40, 55, 60, 71 };
            var SquaredValues = numberList.Select(n => n * n);
            Console.WriteLine(string.Join(" ", SquaredValues));

            List<int> evenNumbers = numberList.FindAll(n => n % 2 == 0);
            Console.WriteLine($"Even Number List : {string.Join(" ", evenNumbers)}");
        }
    }
}
