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

namespace MultiConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Date date = new Date();
        public MainWindow()
        {
            InitializeComponent();
            ug_info.DataContext = date;
        }

        private void textBoxYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dummy.Year = date.Year;
        }

        private void textBoxMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            Dummy.Month = date.Month;
        }
    }


    [ValueConversion(typeof(int[]), typeof(DateTime))]
    class ConvertIntToDateTime : IMultiValueConverter
    {
        DateTime dateTime;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return dateTime = new DateTime(System.Convert.ToInt32((values[0].ToString())), System.Convert.ToInt32((values[1].ToString())), System.Convert.ToInt32((values[2].ToString())));
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            object[] intArr = new object[3];

            intArr[0] = dateTime.Year;
            intArr[1] = dateTime.Month;
            intArr[2] = dateTime.Day;

            return intArr;
        }
    }
}
