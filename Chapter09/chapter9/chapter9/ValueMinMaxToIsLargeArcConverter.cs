using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace chapter9
{
    class ValueMinMaxToIsLargeArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double value = (double)values[0];
            double minimum = (double)values[1];
            double maximum = (double)values[2];

            //값이 50%보다 더 클 때만 true를 반환함
            return ((value * 2) >= (maximum - minimum));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ValueMinMaxToPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            double value = (double)values[0];
            double minimum = (double)values[1];
            double maximum = (double)values[2];

            //0과 360 사이의 한 값으로 전환함
            double current = (value / (maximum - minimum)) * 360;

            //프로그레스의 상태가 종료되어 ArcSegment가 원을 다 그림
            if (current == 360)
                current = 359.999;

            //90마다 값을 변경해서 원의 꼭짓점에서는 0부터 시작하게 함
            current = current - 90;

            //각도를 라디언으로 전환함
            current = current * 0.017453292519943295;

            //원의 지점을 계산함
            double x = 10 + 10 * Math.Cos(current);
            double y = 10 + 10 * Math.Sin(current);
            return new Point(x, y);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
