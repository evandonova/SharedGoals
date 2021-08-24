using System;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Controllers.Attributes
{
    public class CustomDateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateTime = (DateTime)value;

            if (dateTime >= DateTime.UtcNow && dateTime < DateTime.Parse("01/01/2100"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Due Date must be in the future and before '01/01/2100'.");
            }
        }
    }
}
