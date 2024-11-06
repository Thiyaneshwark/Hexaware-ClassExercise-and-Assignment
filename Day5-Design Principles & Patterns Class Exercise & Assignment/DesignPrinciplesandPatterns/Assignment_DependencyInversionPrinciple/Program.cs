namespace ASSIGNMENT_DIP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = " Library Management System ";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("   Welcome to the Library System");
            Console.WriteLine("====================================\n");
            Console.ResetColor();

            var student = (Student)UserFactory.CreateUser("Student");
            var teacher = (Teacher)UserFactory.CreateUser("Teacher");
            var librarian = (Librarian)UserFactory.CreateUser("Librarian");

            var libraryService = new LibraryService();
            DisplaySectionTitle("Student Actions");
            libraryService.ExecuteBorrow(student, "Introduction to C#");

            DisplaySectionTitle("Teacher Actions");
            libraryService.ExecuteBorrow(teacher, "Advanced .NET Programming");
            libraryService.ExecuteReserve(teacher, "Database Systems");

            DisplaySectionTitle("Librarian Actions");
            libraryService.ManageInventory(librarian, "New Book on Design Patterns", true);
            libraryService.ManageInventory(librarian, "Old Magazine", false);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n====================================");
            Console.WriteLine("   All operations completed!");
            Console.WriteLine("====================================\n");
            Console.ResetColor();
        }
        private static void DisplaySectionTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n--- {title} ---");
            Console.ResetColor();
        }
    }
}
