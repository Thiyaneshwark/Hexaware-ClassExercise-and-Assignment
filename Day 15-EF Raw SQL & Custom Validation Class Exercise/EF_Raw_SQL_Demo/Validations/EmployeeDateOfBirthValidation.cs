using System.ComponentModel.DataAnnotations;

namespace EF_Raw_SQL_Demo.Validations
{
    public class EmployeeDateOfBirthValidation : ValidationAttribute
    {
        public const string MINIMUM_DATE_OF_BIRTH =
        "The Customer age must be greater or equal to 18 years";
        private int MINIMUM_AGE = 18;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value != null ? value.ToString() : null;

            // If value is empty, show Success
            if (string.IsNullOrWhiteSpace(valueString))
            {
                return ValidationResult.Success;
            }

            // If dob cannot be parsed in Date then it is invalid
            if (!DateTime.TryParse(valueString, out DateTime dob))
            {
                return new ValidationResult("Please provide the date of birth in a valid format");
            }

            // Check if the age is under the minimum age. finding the calendar date of 18 years ago
            var minDateOfBirth = DateTime.Now.Date.AddYears(MINIMUM_AGE * -1);

            if (dob > minDateOfBirth)
            {
                return new ValidationResult(MINIMUM_DATE_OF_BIRTH);
            }

            return ValidationResult.Success;
        }
    }
}