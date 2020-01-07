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

### 데이터 템플릿 사용하기

### 밸류 컨버터 사용하기

## 컬렉션 뷰의 커스터마이징

### 정렬

### 그룹핑

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