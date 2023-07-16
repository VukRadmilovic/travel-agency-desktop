using System.Globalization;
using System.Windows.Controls;

namespace Tourist_Agency.ValidationRules
{
    internal class PasswordLengthValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return value.ToString().Length < 8
                ? new ValidationResult(false, "Lozinka mora da sadrzi bar 8 karaktera.")
                : ValidationResult.ValidResult;
        }
    }
}
