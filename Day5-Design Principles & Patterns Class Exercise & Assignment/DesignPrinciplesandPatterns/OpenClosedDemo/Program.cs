namespace OpenClosedDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee empJohn = new PermanentEmployee(1, "John");
            Employee empJason = new PermanentEmployee(2, "Jason");
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}", empJohn.ToString(),empJohn.CalculateBonus(100000).ToString()));
            Console.WriteLine(string.Format("Employee {0} Bonus: {1}", empJason.ToString(), empJason.CalculateBonus(150000).ToString()));
            Console.ReadLine();
        }
    }
}
