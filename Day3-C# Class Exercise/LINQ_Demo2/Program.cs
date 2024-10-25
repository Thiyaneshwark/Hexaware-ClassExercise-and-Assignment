namespace LINQ_Demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>()
            {
                new Student(1, "Akash", 21),
                new Student(2, "Anna", 17),
                new Student(3, "Mohan", 12),
                new Student(4, "Sophia", 14),
                new Student(5, "Ram", 21)
            };


            StudentQueries studentQueries = new StudentQueries();


            List<Student> teenageStudentsQuery = studentQueries.GetTeenageStudentsUsingQuerySyntax(students);
            Console.WriteLine("Teenage Students (Query Syntax):");
            foreach (var student in teenageStudentsQuery)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}");
            }

            List<Student> teenageStudentsMethod = studentQueries.GetTeenageStudentsUsingMethodSyntax(students);
            Console.WriteLine("\nTeenage Students (Method Syntax):");
            foreach (var student in teenageStudentsMethod)
            {
                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}");
            }
        }
    }
}