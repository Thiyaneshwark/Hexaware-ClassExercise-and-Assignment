using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Demo2
{
    public class StudentQueries
    {
        // Query Syntax:
        public List<Student> GetTeenageStudentsUsingQuerySyntax(List<Student> students)
        {
            var teenageStudents = (from student in students
                                   where student.Age >= 13 && student.Age <= 19
                                   select student).ToList();
            return teenageStudents;
        }

        // Method Syntax
        public List<Student> GetTeenageStudentsUsingMethodSyntax(List<Student> students)
        {
            var teenageStudents = students.Where(student => student.Age >= 13 && student.Age <= 19).ToList();
            return teenageStudents;
        }
    }
}
