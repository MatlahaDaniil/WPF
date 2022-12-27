using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Convert
{
    class Date
    {

        private int year;
        private int month;
        private int day;

        public int Year
        {
            get { return year; }
            set { SetProperty(ref year, value, nameof(Year)); }
        }
        public int Month
        {
            get { return month; }
            set { SetProperty(ref month, value, nameof(Month)); }
        }
        public int Day
        {
            get { return day; }
            set { SetProperty(ref day, value, nameof(Day)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, string name)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class YearRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int year;
            if (!int.TryParse(value.ToString(), out year))
            {
                return new ValidationResult(false, "false parse");
            }

            if (year > 9999 || year < 1)
            {
                return new ValidationResult(false, "invalid year");
            }

            return new ValidationResult(true, null);
        }
    }

    public class MonthRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int month;
            if (!int.TryParse(value.ToString(), out month))
            {
                return new ValidationResult(false, "false parse");
            }

            if (month > 12 || month < 1)
            {
                return new ValidationResult(false, "invalid month");
            }

            return new ValidationResult(true, null);
        }
    }

    public class DayRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int day;
            if (!int.TryParse(value.ToString(), out day))
            {
                return new ValidationResult(false, "false parse");
            }

            return new ValidationResult(true, null);
        }
    }
}
