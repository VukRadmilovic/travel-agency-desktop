using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Turisticka_Agencija.ValidationRules
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
