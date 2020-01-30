using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace chapter8
{
    class RawCountToDescriptionConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //입력된 데이터가 올바르지 않으면 던져지는 예외를 알아볼 수 있도록 수정
            int num = int.Parse(value.ToString());
            return num + (num == 1 || num == 0? " item" : " items");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
