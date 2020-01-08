# Chapter9. 데이터 바인딩🦅

데이터 바인딩: 임의의 닷넷 객체를 함께 묶는 것
데이터 소스 통해 순서를 반복하거나 수동으로 리스트박스 아잍메을 리스트박스에 하나씩 추가하는 것이 아니라
이런 식으로 시키는 것
"리스트박스! 여기서 아이템들을 가져가! 그리고 그것들을 갱신해줘! 오호~ 이렇게 좀 바꿔줘" 
데이터 바인딩은 단순한 일 뿐만 아니라 훨씬 더 많은 일을 할 수 있음

## 바인딩 클래스 소개
- 데이터 바인딩의 핵심: System.Windows.Data.Binding 클래스
    - 두 개의 프로퍼티를 함께 이용 -> 두 프로퍼티 사이에 연결된 채널을 유지함
    - 바인딩 클래스는 한 번만 설정 -> 프로그램이 실행되는 나머지 시간 동안 동기적으로 작업 가능함

### 프로그래밍 코드에서 바인딩 사용하기
- 리스트박스에 이같은 텍스트블록을 추가한다고 가정하자
```XAML
<TextBlock x:Name="currentFolder" DockPanel.Dock="Top" Background="AliceBlue" FontSize="16" />
```
- 코드를 통해 트리뷰의 SelectedItem이 변경될 때마다 수동으로 텍스트블록을 갱신 가능함
```C#
void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
{
    currentFolder.Text=(treeView.SelectedItem as TreeViewItem).Header.ToString();
    Refresh();
}
```
- 바인딩 클래스를 사용하면, MainWindow의 생성자에서 한번만 초기화시키면 위의 코드를 사용하지 않아도 됨
```C#
public MainWindow()
{
    InitializeComponent();

    Binding binding = new Binding();
    //소스 객체를 설정한다
    binding.Source=treeView;
    //소스 프로퍼티를 설정한다.
    binding.Path=new PropertyPath("SelectedItem.Header");
    //타깃 프로퍼티를 추가한다.
    currentFolder.SetBinding(TextBlock.TextProperty, binding);
}
```
- treeView.SelectedItem.Header가 변경될 때마다 currentFolder.Text도 자동으로 갱신됨
- Header 프로퍼티가 없은 트리뷰의 아이템이 선택되면, 데이터 바인딩은 실패했다는 어떤 메시지도 보여주지 않음
- 바인딩 클래스는 **소스(Source)** 프로퍼티와 **타깃(Target)** 프로퍼티를 이용함
- TreeView.SelectedItem.Header 처럼 소스 프로퍼티는 두 단계를 거쳐서 설정됨
    1. 바인딩할 소스 객체를 Source 프로퍼티에 할당
    2. 의존 프로퍼티와 바인딩 인스턴스를 인수로 사용하는 SetBinding 메소드를 호출
    3. 타깃 프로퍼티와 바인딩 객체를 연결함
- FrameworkElement나 FrameworkContentElement 를 상속받은 모든 클래스들은 SetBinding 메소드를 갖고 있음

**바인딩 제거하기**
`BindingOperations.ClearBinding(currentFolder, TextBLock.TextProperty);`
- 프로그램이 실행되는 동안 바인딩을 원하지 않으면, 위의 메소드를 사용하여 언제든지 연결을 끊을 수 있음
- 바인딩 대상 객체와 의존 프로퍼티를 인수로 넘겨주면 됨

### XAML에서 바인딩 사용하기

- XAML에서는 SetBinding 메소드를 호출할 수 없음 -> Binding을 선언으로 사용가능하도록 마크업 확장식을 제공
- Binding 자체도 마크업 확장식 클래스임
- XAML에서 바인딩을 사용하기 위해 타깃 프로퍼티에 바인딩 인스턴스를 직접 설정, 마크업 확장식을 통해 프로퍼티처럼 사용 가능함
- 이전에 바인딩한 코드는 currentFolder에 추가적인 선언 통해 다음처럼 수정이 가능함
```XAML
<TextBlock x:Name="currentFolder" DockPanel.Dock="Top" Text="{Binding ElementName=treeView, Path=SelectedItem.Header}" Background="AliceBlue" FontSize="16"/>
```
- 소스와 타깃 프로퍼티간 연결은 프로그래밍 코드를 통해 간결하고 쉽게 사용이 가능함
- 바인딩 클래스는 소스 객체를 설정하기 위해 Source 프로퍼티가 아닌 ElementName 프로퍼티를 사용한 것을 볼 수 있음
- XAML에서 Source 프로퍼티를 사용하려면, 대상 객체가 ResourceDictionary에 리소스로서 적절하게 정의되어야 함
- 이런 내용을 반영해서 예제를 수정하면 treeView는 리소스의 키가 됨
```XAML
<TextBlock x:Name="currentFolder" DockPanel.Dock="Top" Text="{Binding Source={StaticResource treeView}, Path=SelectedItem.Header}" Background="AliceBlue" FontSize="16"/>
```

### 단순 프로퍼티와 바인딩
- 의존 프로퍼티들은 소스와 타깃 프로퍼티의 동기화에 필수적인 변경 통지 기능이 있음
- WPF는 임의의 닷넷 객체의 어떤 프로퍼티와도 데이터 바인딩이 가능함
```XAML
<Label x:Name="numItemsLabel" Content="{Binding Source={StaticResource photos}, Path=Count}" DockPanel.Dock="Bottom"/>
```
- 의존 프로퍼티를 데이터 바인딩의 소스로 활용 시, 변경 통보가 일어나지만 평범한 닷넷 프로퍼티를 활용하면 문제가 생김
    - 이 프로퍼티들은 변경 통보가 자동으로 일어나지 않음 -> 특별한 처리 안 하면 소스 프로퍼티가 변경되어도 갱신이 안 됨

- 타깃과 소스 프로퍼티를 동기적으로 유지해야 함 -> 이 문제 해결
- 소스 객체는 다음 중 한 가지 처리를 해야 함
    - PropertyChanged 이벤트를 정의하고 있는 System.ComponentModel.InotifyPropertyChanged 인터페이스를 구현해야 함
    - XXXChanged 이벤트를 구현해야 함. XXX는 값이 변하는 프로퍼티의 이름
- 첫 방법에 WPF가 최적화되어 있음
- XXXChanged 이벤트 처리하는 방법: 예전 클래스와 호환성을 위해 지원
- photos 컬렉션을 가진 Changed 이벤트가 일어날 때, Add, Remove,Clear, Insert처럼 그림의 개수에 영향을 주는 동작들을 가로챌 수 있음
- WPF는 이런 처리를 돕는 ObservableCollection 클래스를 내장함
- photos.Count에 다음처럼 한 줄만 고치면 바인딩에서 동기화를 지원함
`public class Photos: Collection<Photo>`
를
`public class Photos:ObservableCollection<Photo>`
로 고침

### 객체 전체와 바인딩
- 바인딩 객체의 Path 프로퍼티처럼 소스 프로퍼티를 사용하는 것은 선택의 문제
- 전체 소스 객체도 타깃 프로퍼티와 바인딩 가능

- 객체 전체와 바인딩한다는 것의 의미는?
```XAML
<Label x:Name="numItemsLabel"
       Content="{Binding Source={StaticResource photos}}"
       DockPanel.Dock="Bottom"/>
```
- photos 객체는 UIElement를 상속받은 객체가 아님 -> ToString 메소드에서 반환되는 문자열로 렌더링됨

### 컬렉션에 바인딩하기
- 라벨에 photos.Count를 바인딩한다는 의도는 좋음... 그러나 photos 컬렉션에 리스트박스를 바인딩하는 것이 더 좋은 생각!

**초기 바인딩**
- 타깃 프로퍼티로 ListBox.Items 컬렉션을 사용할 것이라고 생각하지만, 
  Items는 의존 프로퍼티가 아니어서 사용 불능, 대신 데이터바인딩에 적합한 ItemsSource라는 의존 프로퍼티를 사용함
- ItemsSource 프로퍼티는 IEnumerable 타입임.
-> 전체 photos 객체를 소스로 사용하여 다음처럼 바인딩 가능함

```XAML
<ListBox x:Name="pictureBox"
 ItemsSource="{Binding Source={StaticResource photos}}" ...>
 ...
</ListBox>
```  
- 엘리먼트를 추가하거나 제거하는 소스 컬렉션의 변화로 인해 타깃 프로퍼티가 갱신되어야 한다면, 소스 컬렉션은
  INotifyCollectionChanged라는 인터페이스를 구현해야 함
- ObservableCollection은 INotifyPropertyChanged와 INotifyCollectionChanged 인터페이스를 구현하고 있음
  ObservableCollection<Photo>를 상속받은 Photos 컬렉션이 바인딩 처리과정에 이런 결과를 잘 반영함

**표시방법 개선하기**
- photos 컬렉션은 ToString 메소드가 반환한 문자열을 렌더링해서 보여줌 -> 보기 좋지 않음
- 개선하는 한 가지 방법: 모든 ItemsControl 컨트롤이 갖고 있는 DisplayMemberPath를 사용하는 방법이 있음
- 포토 갤러리에서 컬렉션은 프로그램에 종속된 Photo 객체로 구성되어 있음, 각 객체마다 이미지의 이름, 생성날짜, 크기를 가짐
- 이런 방식으로 수정하면 향상된 결과를 보여줌
```XAML
<ListBox x:Name="pictureBox" DisplayMemberPath="Name" ItemsSource="{Binding Source={StaticResource photos}}" ...>
...
</ListBox>
```
- Photo 클래스를 별도로 정의 가능 -> 전체 경로를 반환하는 대신 ToSTring 메소드가 파일명을 반환하도록 Photo 클래스를 수정 가능
- 리스트박스에 표시할 실제 이미지를 얻기 위해서는?
    - Photo 클래스에 Image 프로퍼티를 추가
    - DisplayMemberPath를 사용
    - 소스 객체를 변화시키지 않고도 바인딩된 데이터를 효과적으로 표현하는 방법이 있음
- **일부 변경을 쉽게 반영할 수 있다는 것**은 중요함

- 데이터 바인딩에 종속되지 않는 다른 방법: 데이터 템플릿을 사용하는 것, 밸류 컨버터를 사용하는 것

**선택된 아이템 관리하기**
- 리스트박스 같은 셀렉터 컨트롤들은 선택된 아이템이라는 개념을 가짐
- 셀렉터가 컬렉션이나 IEnumerable을 구현한 객체들과 바인딩 시, WPF 자체적으로 선택된 아이템의 정보를 갖고 있음
- WPF 자체적으로 선택된 아이템의 정보를 갖고 있어서 동일한 소스로 다른 타깃과 바인딩 시에도 별다른 처리 없이 이 정보를 이용 가능함

- 이런 지원을 최적화하려면, 모든 셀렉터 컨트롤들이 공통으로 갖고 있는 IsSynchronizedWithCurrentItem 프로퍼티를 true로 설정하면 됨
```XAML
  <ListBox IsSynchronizedWithCurrentItem="True" DisplayMemberPath="Name" ItemsSource="{Binding Source={StaticResource photos}}"></ListBox>
  <ListBox IsSynchronizedWithCurrentItem="True" DisplayMemberPath="DateTime" ItemsSource="{Binding Source={StaticResource photos}}"></ListBox>
  <ListBox IsSynchronizedWithCurrentItem="True" DisplayMemberPath="Size" ItemsSource="{Binding Source={StaticResource photos}}"></ListBox>
```
- 개별 리스트박스는 IsSynchronizedWithCurrentItem="True"로 설정되었고 동일한 소스 컬렉션을 사용함 -> 선택 아이템이 변경되면 다른 두 곳도 동시에 변경됨(선택이 동기화되는 것!)
- 리스트박스 중 한 곳에서라도 IsSynchronizedWithCurrentItem 프로퍼티가 생략되거나 false로 설정되면 다른 리스트박스와의 연결고리가 끊어짐 -> 영향 안 받고 독립적인 리스트박스로 동작함

### 데이터 컨텍스트를 이용해서 소스 공유하기 

- 동일한 UI를 사용하는 많은 엘리먼트에서 하나의 소스 객체에 바인딩하는 것이 일반적인 경우
- WPF는 바인딩을 데이터 소스를 명시적으로 지정해서 사용하기보다 Source, RelativeSource, ElementName 등을 사용해서 암시적으로 사용하는 것을 선호함
- 암시적인 데이터 소스 = 데이터 컨텍스트

- 소스 객체를 데이터 컨텍스트로 사용하려면 공통적인 부모 엘리먼트를 찾아서 DataContext 프로퍼티를 설정하면 됨
- FrameworkElement 나 FrameworkContentElement를 상속받은 모든 엘리먼트들은 Object 타입의 DataContext 프로퍼티를 갖고 있음
- 명시적인 소스 객체 없이 바인딩하려고 하면 WPF는 널값이 아닌 DataContext를 만날 때까지 로지컬 트리를 찾음
```XAML
<StackPanel x:Name="parent" DataContext="{StaticResource photo}">
 <Label >
```
컬렉션: 데이터를 담는 자료구조 
ex) 배열은 아니고 ArrayList, Stack, Queue ...

## 렌더링 조절하기
- 소스와 타깃 프로퍼티가 서로 호환되는 데이터 타입이고 처리고정 없이 기본적인 렌더링만으로 충분하다면 데이터 바인딩은 단순해짐
- WPF는 소스를 원하는 형태로 처리할 수 있는 두 가지 기능(데이터 템플릿, 밸류 컨버터)을 제공함 
  -> 복잡한 경우에도 데이터 바인딩을 사용 가능

### 데이터 템플릿 사용하기
- 데이터 템플릿: 임의의 닷넷 객체가 렌더링될 때 적용 가능한 UI의 한 부분임
    - 타깃을 원하는 형태로 렌더링하도록 조정함
- 많은 WPF 컨트롤들은 데이터 템플릿을 효과적으로 사용하기 위해 DataTemplate 타입의 프로퍼티들을 갖고 있음
- DataTemplate 의 인스턴스를 ContentTemplate, ItemTemplate 같은 프로퍼티들에 적용하면, 비주얼 트리가 새롭게 바뀜
- ItemsTemplate처럼, DataTEmplate은 FrameworkTemplate을 상속받았음 -> VisualTree 컨텐트 프로퍼티를 가지고 있음
- 이 프로퍼티는 FrameworkElement 클래스의 트리 설정을 바꿀 수 있게 해줌
```xaml
<ListBox x:Name="pictureBox" ItemsSource="{Binding Source={StaticResource photos}}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Image Source="thumbnail.png" Height="35"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
<ListBox x:Name="pictureBox1" ItemsSource="{Binding Source={StaticResource photos}}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <Image Source="thumbnail.png" Height="35"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```
- 모든 아이템이 이미지를 보여주지만 적어도 문자열이 아니라 이미지임
- 어떻게 Source 프로퍼티가 현재 Photo 객체의 FullPath 프로퍼티를 알 수 있는가?
    - 답: 데이터 바인딩
- 데이터 템플릿 적용 시, 적절한 데이터 컨텍스트(=소스 객체)가 암시적으로 사용됨 
- ItemTemplate이 적용되면, 데이터 컨텍스트는 ItemsSource에서 현재 아이템을 가리킴

- DataTemplate은 인라인으로 선언할 필요가 없음
- 대부분 리소스로 사용함 -> 다중 엘리먼트에서도 공유가 가능함
- 원하는 타입에 DataType 프로퍼티를 설정하는 곳마다 그 타입이 자동으로 적용된 DataTemplate을 얻을 수 있음

- DataTemplate 의 하위 클래스 중에는 XML처럼 계층적 구조의 데이터와 잘 맞도록 설계된 것들이 있음
    - HierarchicalDataTemplate이 그럼
- 계층적 데이터의 표현을 변경할 수 있게 해줌
- 트리뷰나 메뉴 컨트롤처럼 계층적인 데이터를 기본적으로 지원하는 엘리먼트와 직접 바인딩시킬 수 있음
 
### 밸류 컨버터 사용하기
- 밸류 컨버터: 소스를 원하는 값으로 완벽하게 변경함
- 서로 다른 데이터 타입의 소스와 타깃을 함께 사용할 경우 종종 사용함

**호환성이 없는 데이터 타입 연결하기**
- 라벨의 배경색을 photos 컬렉션에 포함된 아이템 숫자에 따라 변경한다면..?
`<Label Background="{Binding Path=Count, Source={StaticResource photos}}" ... />`
- 이 경우, 바인딩 객체는 브러시 대신 숫자를 배경색으로 어떻게 설정해야 하는지 모름

- 이것을 고치려면, 바인딩의 Converter 프로퍼티 이용 -> 밸류 컨버터를 추가해야 함
```XAML
<Label Background="{Binding Path=Count, Converter={StaticResource myConverter}, Source={StaticResource photos}}" />
```
- 브러시를 숫자로 전환가능한 별도의 클래스를 작성함, 리소스가 정의되었다는 가정하에 작성
```XAML
<Window.Resources>
    <local:CountToBackgroundConverter x:Key="myConverter"/>
</Window.Resources>
```
- 밸류 컨버터를 구현하려면 System.Windows.Data 네임 스페이스에 있는 IValueConverter 인터페이스를 구현해야 함
- 이 인터페이스는 넘겨받는 소스 인스턴스를 타깃 인스턴스로 전환해주는 Covnert 메소드와 그 반대 처리를 해주는 ConvertBack으로 구성됨

[CountToBackgroundConverter.cs]
```C#
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
```
- 소스의 값이 변경될 때마다 호출됨, 정수 값이 주어지면 Brushes.Yellow를 반환, 0이 되면 Brushes.Red값을 반환
- IValueConverter 인터페이스의 메소드들: 파라미터와 컬처를 한 개씩 인수로 사용
- 파라미터: null, 컬처: 타깃 엘리먼트의 Language 프로퍼티의 값으로 설정
- FrameworkElement와 FrameworkContentElement에 정의된 Language 프로퍼티가 처음 설정될 경우 루트 엘리먼트에서 상속받고 기본 값으로 "en-US"를 사용함
- 바인딩을 사용하는 경우 Binding.ConverterParmeter와 Binding.ConverterCulture를 이용 -> 원하는 값으로 변경 가능함
- 모든 마크업 확장식 형식의 파라미터들처럼 ConverterParameter의 값을 형변환함
  -> Yellow라는 단순 문자열을 사용해도 밸류 컨버터가 적당한 브러시로 받을 수 있음
- 비슷한 처리를 하는 ConverterCulture는 IETF 언어 태그를 문자열로 설정하면 적당한 CultureInfo객체로 전환되어 사용 가능함

[item이 0개일 때]<br>
![](pic3.PNG)

[item이 1개 이상일 때]<br>
![](pic4.PNG)


**데이터를 원하는 형태로 나타내기**
- 밸류 컨버터는 상황에 알맞은 문자열을 수정할 수 있게 해줌
- RawCountToDescriptionConverter를 사용하면 추가적인 라벨을 사용하지 않고도 수정이 가능함
```XAML
<Window x:Class="chapter8.MainWindow4"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:chapter8"
        mc:Ignorable="d"
        Title="MainWindow4" Height="300" Width="300">
    <Window.Resources>
        <local:Photos x:Key="photos">
            <local:Photo Name1="Photo1_1" Name2="Photo1_2" Name3="Photo1_3"/>
            <local:Photo Name1="Photo2_1" Name2="Photo2_2" Name3="Photo2_3"/>
            <local:Photo Name1="Photo3_1" Name2="Photo3_2" Name3="Photo3_3"/>
        </local:Photos>
        <local:RawCountToDescriptionConverter xmlns:local="clr-namespace:chapter8" x:Key="myConverter"/>
    </Window.Resources>
    <DockPanel>
        <TextBlock Text="{Binding Path=Count, Converter={StaticResource myConverter}, Source={StaticResource photos}}" />
    </DockPanel>
</Window>
```
```C#
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
```
[item이 0개일 때 또는 1개일 때]<br>
![](pic1.PNG)

[item이 1개 이상일 때]<br>
![](pic2.PNG)

## 컬렉션 뷰의 커스터마이징
- 컬렉션이나 IEnumerable을 구현한 객체에 바인딩할 때마다, 기본 뷰(default View) 가 소스와 타깃 객체 사이에 암시적으로 추가됨
- ICollectionView를 구현한 뷰는 현재 아이템을 저장하지만 별도로 정렬, 그룹핑, 필터링 및 아이템 탐색도 가능함

### 정렬
- ICollectionView 인터페이스는 SortDescriptions 프로퍼티를 사용해서 뷰의 아이템들을 정렬 가능함
- 정렬- 아이템 중에서 정렬하기 원하는 프로퍼티와 방향을 선택하면 됨
- Name, DateTime, Size 프로퍼티 중 하나만 선택 , 방향 지정하면 됨
`SortDescription sort = new SortDescription("Name", ListSortDirection.Ascending);`

- SortDescriptions 프로퍼티: SortDescription 객체들의 컬렉션 -> 동시에 여러 프로퍼티를 사용해서 정렬이 가능함
- 일반적으로 SortDescription 객체: 가장 중요한 프로퍼티를 설정, 갈수록 활용도가 떨어지는 것을 설정

- SortDescriptions 컬렉션은 기본 정렬된 뷰를 반환하는 Clear 메소드를 갖고 있음
```C#
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
```
[tip]
- 화면의 모든 UI는 이벤트와 관련 있음

### 그룹핑
- ICollectionView 인터페이스는 SortDescription보다 훨씬 기능이 강력한 GroupDescriptions 프로퍼티를 갖고 있음
- PropertyGroupDescription 객체들을 이 프로퍼티에 추가 -> 소스 컬렉션의 아이템들을 그룹이나 잠재적인 하위 그룹으로 구성하는 데 사용 가능함

```C#
// 기본 뷰를 얻어옴
ICollectionView view = CollectionViewSource.GetDefaultView(
    this.FindResource("photos"));
// 그룹핑함
view.GroupDescriptions.Clear();
view.GroupDescriptions.Add(new PropertyGroupDescription("DateTime"));
```
- 정렬과 다르게, 그룹핑의 결과는 ItemsControl에 데이터를 렌더링하지 않으면 즉시 확인 불가능
- 원하는 형태로 그룹핑한 후, ItemsControl의 GroupStyle 프로퍼티에 GroupStyle 객체의 인스턴스를 설정해야 함
- 이 객체는 그룹 헤더의 모양을 정의하기 위해 데이터 템플릿을 사용해야 하는 HeaderTemplate 프로퍼티를 갖고 있음

```XAML
<Window x:Class="chapter8.MainWindow5"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:chapter8"
        mc:Ignorable="d"
        Title="MainWindow5" Height="300" Width="300">
    <Window.Resources>
        <local:Photos x:Key="photos">
            <local:Photo Name="Photo1_1" DateTime="2020-01-08" Size="50"/>
            <local:Photo Name="Photo2_1" DateTime="2020-01-07" Size="30"/>
            <local:Photo Name="Photo3_1" DateTime="2020-01-10" Size="60"/>
        </local:Photos>
        <local:CountToBackgroundConverter x:Key="myConverter"/>
    </Window.Resources>
    <Grid>
        <ListBox x:Name="pictureBox" ItemsSource="{Binding Source={StaticResource photos}}">
            <ListBox.GroupStyle>
                <GroupStyle>
                    <GroupStyle.HeaderTemplate>
                        <DataTemplate>
                            <Border BorderBrush="Black" BorderThickness="1">
                                <TextBlock Text="{Binding Path=Name}" FontWeight="Bold"/>
                            </Border>
                        </DataTemplate>
                    </GroupStyle.HeaderTemplate>
                </GroupStyle>
            </ListBox.GroupStyle>
        </ListBox>
    </Grid>
</Window>
```


### 필터링

### 탐색

### 추가적인 뷰와 작업하기

## 데이터 프로바이더

### XmlDataProvider

### ObjectDataProvider

## 고급 주제

### 데이터 플로우 조절하기

### 바인딩에 검증 규칙 추가하기

### 흩어져 있는 소스와 작업하기

## 종합 예제: XAML으로만 만든 RSS 리더

## 결론