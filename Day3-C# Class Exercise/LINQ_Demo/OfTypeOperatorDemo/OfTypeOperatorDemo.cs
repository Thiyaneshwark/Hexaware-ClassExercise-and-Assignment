using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfTypeOperatorDemo
{
    internal class OfTypeOperatorDemo
    {
        public void ofTypeDemo()
        {
            IList mixedList = new ArrayList();
            mixedList.Add(1);
            mixedList.Add(4500);
            mixedList.Add("Hexaware");
            mixedList.Add(new Student() { StudentId = 1, name = "Ram" });
            mixedList.Add(new Student() { StudentId = 2, name = "Priya" });

            var stringResult = from m in mixedList.OfType<string>()
                               select m;
            var intResult = from m in mixedList.OfType<int>()
                            select m;
            var studentResult = from m in mixedList.OfType<Student>()
                                select m;

            Console.WriteLine("\n StringType from MixedList \n------------------------");
            foreach (var item in stringResult)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n IntType from MixedList \n------------------------");
            foreach (var item in intResult)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n Student Result from MixedList \n------------------------");
            foreach (var item in studentResult)
            {
                Console.WriteLine($"{item.StudentId} {item.name}");
            }
        }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string name { get; set; }
    }
}
