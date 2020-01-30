using System;
using System.Collections;
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
    /// MainWindow4.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow4 : Window
    {
        public MainWindow4()
        {
            InitializeComponent();
        }

        private void OnButtonClick1(object sender, RoutedEventArgs e)
        {
            SortHelper(this.ListBox1.ItemsSource, "Name");
        }

        private void OnButtonClick2(object sender, RoutedEventArgs e)
        {
            SortHelper(this.ListBox2.ItemsSource, "DateTime");
        }
        
        private void OnButtonClick3(object sender, RoutedEventArgs e)
        {
            SortHelper(this.ListBox3.ItemsSource, "Size");
        }
        void SortHelper(IEnumerable aSource, string propertyName)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(aSource);

            //현재 프로퍼티가 이미 내림차순으로 정렬되었는지 체크함
            if (view.SortDescriptions.Count > 0
                && view.SortDescriptions[0].PropertyName == propertyName
                && view.SortDescriptions[0].Direction == ListSortDirection.Ascending)
            {
                //이미 오름차순으로 정렬되어 있으므로 내림차순으로 정렬이 바뀜
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(
                    propertyName, ListSortDirection.Descending));
            }
            else
            {
                //오름차순 정렬
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(
                    propertyName, ListSortDirection.Ascending));
            }
        }
    }
}
