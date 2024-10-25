using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderByDemo
{
    internal class OrderByDemo
    {
        public void order()
        {
            IList<Student> studentList = new List<Student>()
            {
                new Student() { StudentId = 1, Name = "Gem", Age = 18 },
                new Student() { StudentId = 2, Name = "Julie", Age = 15 },
                new Student() { StudentId = 3, Name = "Prajitha", Age = 25 },
                new Student() { StudentId = 4, Name = "Nithiksha", Age = 20 },
                new Student() { StudentId = 5, Name = "Ron", Age = 19 },
            };
            var studentListNameAsc = from s in studentList
                                     orderby s.Name
                                     select s;
            var studentListNameDesc = from s in studentList
                                      orderby s.Name descending
                                      select s;

            var studentListNameAsc1 = studentList.OrderBy(s => s.Name);
            var studentListNameDesc1 = studentList.OrderByDescending(s => s.Name);

            Console.WriteLine("Orderby Asc with Query Syntax");
            foreach (var student in studentListNameAsc)

            {
                Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");
            }

            Console.WriteLine("Orderby Asc with Method syntax");

            foreach (var student in studentListNameAsc1)
            {
                Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");
            }

            Console.WriteLine("Orderby Descending with query syntax");

            foreach (var student in studentListNameDesc)

            {
                Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");

            }

            Console.WriteLine("Orderby Descending with Method syntax");
            foreach (var student in studentListNameDesc1)

            {
                Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");

            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            var orderByAgel = from s in studentList
                              orderby s.Age ascending, s.Name descending
                              select s;

            var orderByAge = studentList.OrderBy(s => s.Age).ThenByDescending(s => s.Name); ;

            Console.WriteLine("\n Order By Age");

            foreach (var item in orderByAge)

            {
                Console.WriteLine($"{item.StudentId} {item.Name} {item.Age}");

            }

        }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
