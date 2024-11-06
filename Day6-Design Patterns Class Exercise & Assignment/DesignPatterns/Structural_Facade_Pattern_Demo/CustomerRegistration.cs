namespace Structural_Facade_Pattern_Demo
{
    public class CustomerRegistration
    {
        public bool RegisterCustomer(Customer customer)
        {
            ValidateCustomer validateCustomer = new ValidateCustomer();
            validateCustomer.IsValid(customer);

            CustomerDataAccessLayer layer = new CustomerDataAccessLayer();
            layer.SaveCustomer(customer);

            Notification notification = new Notification();
            notification.SendNotification(customer);
            return true;
        }
    }
}
