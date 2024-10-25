using LAMBDA_Demo;

//namespace training
//{
//    internal class program
//    {
//        static void main(string[] args)
//        {
//            console.writeline("hello, world!");
//        }
//    }
//}
LambdaDemo lambdaDemo=new LambdaDemo();
lambdaDemo.Demo();

List<Employee> employees = new List<Employee>()
{new Employee{EmployeeId=101,Name="Thiyanesh",Experience=2,Salary=90000},
new Employee{EmployeeId=102,Name="Ramesh",Experience=3,Salary=74000},
new Employee{EmployeeId=103,Name="Anu",Experience=2,Salary=69000},
new Employee{EmployeeId=104,Name="Vijay",Experience=4,Salary=90000},
};

Console.WriteLine("\nEmployee List \n----------------------");
foreach (var employee in employees)
{
    Console.WriteLine($"{employee.EmployeeId} {employee.Name} " + $"{employee.Experience} {employee.Salary}");
}

var sortedEmployeeByIdDesc=employees.OrderByDescending(e => e.EmployeeId);
Console.WriteLine("\nEmployee List Order By ID Descending \n-------------------------------------");
foreach (var employee in sortedEmployeeByIdDesc)
{
    Console.WriteLine($"{employee.EmployeeId} {employee.Name} " + $"{employee.Experience} {employee.Salary}");
}

Console.ReadLine();