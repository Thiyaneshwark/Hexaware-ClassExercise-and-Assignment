using System;
using System.Text.RegularExpressions;
namespace AssignmentQn12
{
    public static class Validator
    {
        // Validating Phone Number (10 digits):-
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d{10}$");
        }

        // Validate Email using a regular expression:-
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.(com|in|net|org|gov)$";
            return Regex.IsMatch(email, pattern);
        }

        // Validate Date of Birth (Check if it's a valid date, not a future date, and if the person is at least 18 years old)
        //public static bool ValidateDateOfBirth(DateTime dateOfBirth)
        //{
        //    var today = DateTime.Today;

        //    // Check if the date is valid (not DateTime.MinValue), not in the future, and if the person is at least 18 years old
        //    if (dateOfBirth == DateTime.MinValue || dateOfBirth > today)
        //    {
        //        return false;
        //    }

        //    int age = today.Year - dateOfBirth.Year;
        //    if (dateOfBirth.Date > today.AddYears(-age)) age--;

        //    // Check if age is 18 or more
        //    return age >= 18;
        //}
        public static bool ValidateDateOfBirth(string dateOfBirth)
        {
            string pattern = @"^(19|20)\d{2}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";
            return Regex.IsMatch(dateOfBirth, pattern);
        }
    }
}
