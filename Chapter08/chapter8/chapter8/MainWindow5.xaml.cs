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
    /// MainWindow5.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow5 : Window
    {
        public MainWindow5()
        {
            InitializeComponent();
            ICollectionView view = CollectionViewSource.GetDefaultView(this.FindResource("photos"));
            view.Filter = delegate (object o)
            {
                return ((o as Photo).DateTime - DateTime.Now).Days ==0;
            };
        }
    }

}
