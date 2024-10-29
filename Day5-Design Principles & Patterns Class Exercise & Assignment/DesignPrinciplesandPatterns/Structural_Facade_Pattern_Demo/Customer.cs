namespace Structural_Facade_Pattern_Demo
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MobileNumber { get; set; }
        public string Location { get; set; }

        public void GetCustomerDetail()
        {

        }
    }
    public class ValidateCustomer
    {
        public bool IsValid(Customer customer)
        {
            Console.WriteLine("ID Valid");
            Console.WriteLine("Mobile Number Vaild");
            return true;
        }
    }
    public class CustomerDataAccessLayer
    {
        public void SaveCustomer(Customer customer)
        {
            Console.WriteLine("Customer details saved in the DataBase");
        }
    }
    public class Notification
    {
        public bool SendNotification(Customer customer)
        {
            Console.WriteLine("Registration Success!");
            return true;
        }
    }
}
