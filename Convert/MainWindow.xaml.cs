using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Convert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    }

    class ConvertIntToDate : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;

            try
            {
                date = new DateTime(System.Convert.ToInt32((values[0].ToString())), System.Convert.ToInt32((values[1].ToString())), System.Convert.ToInt32((values[2].ToString())));
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }

            return date;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            object[] intArr = new object[3];

            intArr[0] = date.Year;
            intArr[1] = date.Month;
            intArr[2] = date.Day;

            return intArr;
        }
    }
}
