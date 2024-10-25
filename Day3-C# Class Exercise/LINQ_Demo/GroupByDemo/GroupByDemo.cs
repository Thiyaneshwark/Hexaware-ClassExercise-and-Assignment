using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupByDemo
{
    internal class GroupByDemo
    {
        public void groupby()
        {
            IList<Student> studentList = new List<Student>()
            {
                new Student() { StudentId = 1, Name = "Gem", Age = 18 },
                new Student() { StudentId = 2, Name = "Julie", Age = 15 },
                new Student() { StudentId = 3, Name = "Prajitha", Age = 25 },
                new Student() { StudentId = 4, Name = "Nithiksha", Age = 20 },
                new Student() { StudentId = 5, Name = "Ron", Age = 19 },
            };

            // Query Syntax
            var groupedQueryResult = from s in studentList
                                     group s by s.Age;

            // Method Syntax
            var groupedMethodResult = studentList.GroupBy(s => s.Age); // Deferred execution

            var groupedLookupResult = studentList.ToLookup(s => s.Age); // Immediate execution

            // Displaying results
            Console.WriteLine("Group by Query Syntax");
            foreach (var ageGroup in groupedQueryResult)
            {
                Console.WriteLine($"\nAge Group: {ageGroup.Key}");
                foreach (Student student in ageGroup)
                {
                    Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");
                }
            }

            Console.WriteLine("\nGroup by Method Syntax (Deferred Execution)");
            foreach (var ageGroup in groupedMethodResult)
            {
                Console.WriteLine($"\nAge Group: {ageGroup.Key}");
                foreach (Student student in ageGroup)
                {
                    Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");
                }
            }

            Console.WriteLine("\nGroup by Lookup Syntax (Immediate Execution)");
            foreach (var ageGroup in groupedLookupResult)
            {
                Console.WriteLine($"\nAge Group: {ageGroup.Key}");
                foreach (Student student in ageGroup)
                {
                    Console.WriteLine($"{student.StudentId} {student.Name} {student.Age}");
                }
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
