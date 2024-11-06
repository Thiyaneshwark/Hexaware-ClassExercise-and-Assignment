using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstAndFirstOrDefaultDemo
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CourseId { get; set; }
    }

    internal class FirstAndFirstOrDefaultDemo
    {
        IList<Student> studentList = new List<Student>()
        {
            new Student() { StudentId = 1, Name = "Gem", Age = 18, CourseId = 31 },
            new Student() { StudentId = 2, Name = "Prajitha", Age = 25, CourseId = 33 },
            new Student() { StudentId = 3, Name = "Julie", Age = 15, CourseId = 32 },
            new Student() { StudentId = 4, Name = "Nithiksha", Age = 20, CourseId = 31 },
            new Student() { StudentId = 5, Name = "Ron", Age = 19, CourseId = 32 },
            new Student() { StudentId = 6, Name = "Yuvan", Age = 25, CourseId = 33 },
            new Student() { StudentId = 7, Name = "Raja", Age = 20, CourseId = 31 },
            new Student() { StudentId = 8, Name = "Sam", Age = 19, CourseId = 32 }
        };

        List<int> numberList = new List<int>() { 45, 23, 556, 45 };
        List<string> stringList = new List<string>() { "Riya", "Manan", "Vivek", "Peter" };
        List<int> emptyList = new List<int>();

        public void FirstOrDefaultDemo()
        {
            Console.WriteLine($"First Element in the Number List: {numberList.FirstOrDefault()}");
            Console.WriteLine($"1st Even No. from the List: {numberList.FirstOrDefault(n => n % 2 == 0)}");
            Console.WriteLine($"(emptyList.FirstOrDefault()): {emptyList.FirstOrDefault()}");
            Console.WriteLine($"Last Element in the Number List: {numberList.LastOrDefault()}");
            Console.WriteLine($"Last Even No. from the List: {numberList.LastOrDefault(n => n % 2 == 0)}");
            Console.WriteLine($"(emptyList.LastOrDefault()): {emptyList.LastOrDefault()}");
        }

        public void FirstDemo()
        {
            var firstResult = studentList.First();
            Console.WriteLine($"First element from the Student List: {firstResult.Name}, StudentId: {firstResult.StudentId}");
            Console.WriteLine($"1st Even No. from the List: {numberList.First(n => n % 2 == 0)}");
            // Uncomment the following line to see the exception for emptyList
            // Console.WriteLine($" {emptyList.First()}");

            var lastResult = studentList.Last();
            Console.WriteLine($"Last element from the Student List: {lastResult.Name}, StudentId: {lastResult.StudentId}");
            Console.WriteLine($"Last Even No. from the List: {numberList.Last(n => n % 2 == 0)}");
            // Uncomment the following line to see the exception for emptyList
            // Console.WriteLine($" {emptyList.Last()}");
        }
    }
}
