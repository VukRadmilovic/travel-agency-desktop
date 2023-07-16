using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Tourist_Agency.ValidationRules
{
    internal class EmailFormatValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return !Regex.IsMatch(value.ToString().Trim(), "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$")
                ? new ValidationResult(false, "Email adresa mora biti u formatu prvideo@drugideo.trecideo.")
                : ValidationResult.ValidResult;
        }
    }
}
