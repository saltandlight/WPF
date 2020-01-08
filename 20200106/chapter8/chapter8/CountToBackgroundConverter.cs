using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace chapter8
{

    public class CountToBackgroundConverter : IValueConverter
    {
        //public string Template { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("The target must be a Brush!");

            //넘어온 값이 적절치 않을 경우 Parse 메소드가 예외를 던지도록 함
            int num = int.Parse(value.ToString());

            return (num == 0 ? Brushes.Yellow : Brushes.Red);
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }    
}
