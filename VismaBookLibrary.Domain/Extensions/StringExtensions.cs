using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpper(this string input) =>
            
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),

                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),

                _ => input.First().ToString().ToUpper() + input[1..]
            };

        public static DateTime ParseStringToDate(this string value)
        {
            if (!DateTime.TryParseExact(value, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
            {
                throw new ArgumentException("\nCheck the date format(year-month-day)");
            }

            return DateTime.ParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}
