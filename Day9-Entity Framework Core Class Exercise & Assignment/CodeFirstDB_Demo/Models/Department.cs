namespace CodeFirstDB_Demo.Models
{
    public class Department
    {
        //Plain Old CLR Object
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }
}
