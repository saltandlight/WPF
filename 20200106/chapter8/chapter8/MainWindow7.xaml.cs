using System;
using System.Collections;
using System.Collections.Generic;
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
    /// MainWindow7.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow7 : Window
    {
        private Photos m_Photos = new Photos();

        public IEnumerable Photos
        {
            get
            {
                return this.m_Photos;
            }
        }

        public MainWindow7()
        {
            InitializeComponent();
            //CollectionViewSource viewSource = new CollectionViewSource();
            //viewSource.Source = photos;
            //viewSource.View는 ICollectionView를 구현한 기본 뷰가 아님
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            this.m_Photos.Add(new Photo { Name = "Photo" + this.m_Photos.Count, DateTime = DateTime.Now, Size = this.m_Photos.Count });
        }

        private void OnSort(object sender, RoutedEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(this.m_Photos);
            //view.SortDescriptions.Add(new System.ComponentModel.SortDescription())
        }
    }
}
