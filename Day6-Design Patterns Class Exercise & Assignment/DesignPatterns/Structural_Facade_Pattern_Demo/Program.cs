using Structural_Facade_Pattern_Demo;

Customer customer=new Customer();
customer.CustomerId = 1;
customer.CustomerName = "Ram";
customer.MobileNumber = "7546891289";
customer.Location = "Chennai";

CustomerRegistration registration=new CustomerRegistration();
registration.RegisterCustomer(customer);

Console.ReadLine();