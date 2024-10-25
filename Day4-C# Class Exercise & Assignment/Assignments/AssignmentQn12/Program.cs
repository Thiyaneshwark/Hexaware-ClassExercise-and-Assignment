namespace AssignmentQn12
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new Customer object
            Customer customer = new Customer();

            // Prompt user to enter details
            Console.Write("Enter Name: ");
            customer.Name = Console.ReadLine();

            Console.Write("Enter Email: ");
            customer.Email = Console.ReadLine();

            Console.Write("Enter Phone Number (10 digits): ");
            customer.PhoneNumber = Console.ReadLine();

            //Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
            //string dobInput = Console.ReadLine();
            //DateTime dateOfBirth;
            //if (DateTime.TryParse(dobInput, out dateOfBirth))
            //{
            //    customer.DateOfBirth = dateOfBirth;
            //}
            //else
            //{
            //    Console.WriteLine("Invalid Date format, setting default date.");
            //    customer.DateOfBirth = DateTime.MinValue; // Set default if invalid
            //}

            Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
            string dobInput = Console.ReadLine();

            // Validate the Date of Birth input format
            bool isDOBValid = Validator.ValidateDateOfBirth(dobInput);
            if (isDOBValid)
            {
                // If valid format, convert to DateTime
                customer.DateOfBirth = DateTime.Parse(dobInput);
            }
            else
            {
                Console.WriteLine("Invalid Date format, setting default date.");
                customer.DateOfBirth = DateTime.MinValue; // Set default if invalid
            }

            // Validate customer details
            bool isPhoneNumberValid = Validator.ValidatePhoneNumber(customer.PhoneNumber);
            bool isEmailValid = Validator.ValidateEmail(customer.Email);

            // Display entered data
            Console.WriteLine("\nEntered Customer Details:");
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Phone Number: {customer.PhoneNumber}");
            Console.WriteLine($"Date of Birth: {customer.DateOfBirth.ToString("yyyy-MM-dd")}");

            // Display validation results
            Console.WriteLine("\nValidation Results:");
            Console.WriteLine($"Phone Number is valid: {isPhoneNumberValid}");
            Console.WriteLine($"Email is valid: {isEmailValid}");
            Console.WriteLine($"Date of Birth is valid: {isDOBValid}");
        }
    }
}
