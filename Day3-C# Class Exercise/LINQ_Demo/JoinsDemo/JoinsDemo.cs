using System;
using System.Collections.Generic;
using System.Linq;

namespace JoinsDemo
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CourseId { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    internal class JoinsDemo
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

        IList<Course> courseList = new List<Course>()
        {
            new Course() { CourseId = 31, CourseName = "AWS" },
            new Course() { CourseId = 32, CourseName = "Azure" },
            new Course() { CourseId = 33, CourseName = "Vue.js" }
        };

        public void JoinDemo()
        {
            // Join students with courses based on CourseId
            var joinedResult = studentList.Join(
                courseList,
                student => student.CourseId,
                course => course.CourseId,
                (student, course) => new
                {
                    studentName = student.Name,
                    studentAge = student.Age,
                    studentCourse = course.CourseName
                });

            Console.WriteLine("\nData from Student and Course Lists:");

            foreach (var studentInfo in joinedResult)
            {
                Console.WriteLine($"{studentInfo.studentName} ({studentInfo.studentAge}) - {studentInfo.studentCourse}");
            }
        }
    }
}