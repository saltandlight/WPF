using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace visualstudiostyle
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        ColumnDefinition column1CloneForLayer0;
        ColumnDefinition column2CloneForLayer0;
        ColumnDefinition column2CloneForLayer1;
        public MainWindow()
        {
            InitializeComponent();

            //도킹할 때 사용되는 더미 칼럼을 초기화함
            column1CloneForLayer0 = new ColumnDefinition();
            column1CloneForLayer0.SharedSizeGroup = "column1";
            column2CloneForLayer0 = new ColumnDefinition();
            column2CloneForLayer0.SharedSizeGroup = "column2";
            column2CloneForLayer1 = new ColumnDefinition();
            column2CloneForLayer1.SharedSizeGroup = "column2";
        }

        // 창 #1의 도킹 여부를 전환함
        public void pane1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Collapsed)
                UndockPane(1);
            else
                DockPanel(2);
        }

        public void pane2Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane2Button.Visibility == Visibility.Collapsed)
                UndockPane(1);
            else
                DockPanel(2);
        }

        //버튼 위에 마우스가 올라오면 창 #1을 보여준다
        public void pane1Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer1.Visibility = Visibility.Visible;
            //창이 최상위로 올라가도록 Z-order를 조정함
            Grid.SetZIndex(layer1, 1);
            Grid.SetZIndex(layer2, 0);

            //도킹이 해제되면 다른 창이 감춰지도록 처리함
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }
        //버튼 위에 마우스가 올라오면 창 #2를 보여준다.
        public void pane2Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;

            //창이 최상위로 올라가도록 Z-order를 조정한다
            Grid.SetZIndex(layer2, 1);
            Grid.SetZIndex(layer1, 0);

            //도킹이 해제되면 다른 창이 감춰지도록 처리한다.
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
        }

        //마우스가 레이어 0 안으로 들어오면 도킹되지 않은 창들을 감춘다.
        public void layer0_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        //마우스가 창 #1 안으로 들어왔을 떄 도킹되지 않았다면 다른 창을 감춘다.
        public void pane1_MouseEnter(object sender, RoutedEventArgs e)
        {
            //도킹되지 않았다면 다른 창이 보이지 안호록 처리한다.
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }

        //마우스가 창 #2 안으로 들어왔을 때 도킹되지 않았다면 다른 창을 감춘다
        public void pane2_MouseEnter(object sender, RoutedEventArgs e)
        {
            //도킹되지 않았다면 다른 창이 보이지 않도록 처리한다.
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
        }

        //창을 도킹하고 해당 버튼을 감춘다.
        public void DockPanel(int paneNumber)
        {
            if(paneNumber == 1)
            {
                pane1Button.Visibility = Visibility.Collapsed;
                pane1PinImage.Source = new BitmapImage(new Uri("pin.png", UriKind.Relative));

                //레이어 0에 복제된 컬럼을 추가함
                layer0.ColumnDefinitions.Add(column1CloneForLayer0);
                //창 #2가 도킹되어 있을 때만 레이어1에 복제된 컬럼을 추가함
                if (pane2Button.Visibility == Visibility.Collapsed)
                    layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                pane2Button.Visibility = Visibility.Collapsed;
                pane2PinImage.Source = new BitmapImage(new Uri("pin.png", UriKind.Relative));

                //레이어 0에 복제된 컬럼을 추가함
                layer0.ColumnDefinitions.Add(column2CloneForLayer0);
                //창 #1이 도킹되어 있을 때만 레이어 1에 복제된 컬럼을 추가함
                if (pane1Button.Visibility == Visibility.Collapsed)
                    layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
        }

        //창의 도킹을 해제하고 해당 버튼을 다시 보여준다.
        public void UndockPane(int paneNumber)
        {
            if(paneNumber == 1)
            {
                layer1.Visibility = Visibility.Visible;
                pane1Button.Visibility = Visibility.Visible;
                pane1PinImage.Source = new BitmapImage(new Uri("pin.png", UriKind.Relative));

                //레이어 0과 1에서 복제된 컬럼들을 제거함
                layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
                //복제된 컬럼을 제거한다
                layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }
            else if(paneNumber == 2)
            {
                layer2.Visibility = Visibility.Visible;
                pane2Button.Visibility = Visibility.Visible;
                pane2PinImage.Source = new BitmapImage(new Uri("pin.png", UriKind.Relative));

                //레이어 0과 1에서 복제된 컬럼들을 제거함
                layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
                //복제된 컬럼을 제거한다
                layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }

        }
    }
}
