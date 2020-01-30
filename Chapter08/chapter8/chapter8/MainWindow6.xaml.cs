using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace chapter8
{
    /// <summary>
    /// MainWindow6.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow6 : Window
    {
        public MainWindow6()
        {
            InitializeComponent();
        }

        void previous_Click(object sender, RoutedEventArgs e)
        {
            //기본 뷰를 얻어옴
            ICollectionView view = CollectionViewSource.GetDefaultView(
                this.FindResource("photos"));
            //뒤로 이동
            view.MoveCurrentToPrevious();
            //마지막 부분에서 래핑함
            if (view.IsCurrentBeforeFirst) view.MoveCurrentToLast();
        }
        void next_Click(object sender, RoutedEventArgs e)
        {
            //기본 뷰를 얻어옴
            ICollectionView view = CollectionViewSource.GetDefaultView(
                this.FindResource("photos"));
            //앞으로 이동
            view.MoveCurrentToNext();
            if (view.IsCurrentAfterLast) view.MoveCurrentToFirst();
        }
    }
}
