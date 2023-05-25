using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Turisticka_Agencija.ValidationRules
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
