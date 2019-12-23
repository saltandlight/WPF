# Chapter02. XAML 신비를 벗다
## XAML의 정의
- XAML은 닷넷의 객체들을 초기화하거나 생성하기에 적합하도록 설계된 간편하고 범용적인 선언형 프로그래밍 언어
- 닷넷 프레임워크 3.0: 컴파일러, XAML 실행 파서, WPF 기반 XAML 파일을 독립적으로 실행시킬 수 있는 플러그인 포함
- **느슨한 XAML 페이지:**
    - XAML만으로 구성되어 있는 파일
- XAML은 오로지 닷넷의 API만을 사용함
- XAML은 파서나 컴파일러가 처리하도록 정의한 규칙과 다수의 키워드로 이루어져 있음
- 그러나 이미 정의된 것 외에는 어떠한 엘리먼트도 정의하지 않음
- XAML과 WPF도 함께 사용되어야 강력한 성능을 발휘함
- WPF와 XAML은 서로 독립적으로 사용 가능함
- XAML 의 범용적인 성격 때문에, 닷넷을 사용하는 어떤 분야에도 적용 가능함

## 엘리먼트와 어트리뷰트
- XAML의 명세: 닷넷의 네임스페이스, 데이터 타입, 프로퍼티, 이벤트 등을 XML 네임스페이스, 엘리먼트, 어트리뷰트와 매핑되도록 해주는 규칙들이 정의되어 있음
- **오브젝트 엘리먼트 선언 시**: 항상 기본 생성자를 사용해서 상응하는 닷넷 객체를 생성하는 것과 동일함
- **프로퍼트 어트리뷰트 설정 시**: 생성된 닷넷 객체에 동일한 프로퍼티를 설정하는 것과 동일함
- **이벤트 어트리뷰트 설정 시**: 객체의 이벤트 처리기와 연결됨

```XAML
<Button xmlns="https://schemas.microsoft.com/winfx/2006/xaml/presentation"
Content="OK" Click="button_Click"/>
```

```C#
System.Windows.Controls.Button b = new System.Windows.Controls.Button();
b.Click += new System.Windows.RoutedEventHandler(button_Click);
b.Content="OK";
```
- XAML은 C#처럼 대소문자를 구별한다.

## 네임스페이스
- **XAML에 대한 의문점:**
    - http://schemas.microsoft.com/winfx/2006/xaml/presentation의 XMl 네임스페이스가 닷넷의 System.Windows.Controls CLR 네임스페이스와 어떻게 매핑될 수 있을까
- **해답:**
    - WPF의 어셈블리 내부에 하드코딩되어 있는 정보와 XmlnsDefinitionAttribute의 몇몇 인스턴스를 이용해서 서로를 매핑함
- **schemas.microsoft.com이라는 URL에 대한 의문점:**
    - 웹 브라우저에서 이 URL을 쳐보면 어떤 내용도 없음
    - 임의의 네임스페이스를 구별하기 위한 키워드임

- XAML 파일의 루트 엘리먼트는 적어도 하나의 XML 네임스페이스가 사용되어야 함
    -> 자신과 자식 엘리먼트가 적절하게 매핑됨
- 기본 네임스페이스 외에 다른 네임 스페이스 추가 가능... 이를 구분할 수 있는 접두사가 사용되어야 함
- WPF는 두 번째 네임스페이스를 사용하기 위해서 xmlnsx처럼 'x' 접두사를 사용함
```XAML
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml
```
- 이 네임스페이스는 XAML 언어 네임스페이스, System.Windows.Markup에 있는 타입들과 매핑됨
- XAML 컴파일러나 파서에서 사용하는 특별한 지시자들을 정의함

- 일반적으로 `http://schemas.microsoft.com/winfx/2006/xaml/presentation`이 기본 네임스페이스
- `http://schemas.microsoft.com/winfx/2006/xaml`을 두 번째 네임 스페이스로 사용함, 두 번째 네임 스페이스에 x접두사를 붙이는 것이 관례

```XAML
<WpfNamespace:Button
xmlns:WpfNamespace="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
Content="OK">
```
- 기본 네임스페이스에는 가독성을 높이기 위해서 접두사 없이 사용하는 것이 좋음
- 사용하는 네임스페이스마다 짧지만 의미가 통하는 접두사를 적절하게 붙이면 됨

## 프로퍼티 엘리먼트

```C#
System.Windows.Controls.Button b = new System.Windows.Controls.Button();
System.Windows.Shapes.Rectangle r = new System.Windows.Shapes.Rectangle();
r.Width = 40;
r.Height = 40;
r.Fill = System.Windows.Media.Brushes.Black;
b.Content = r;
```
- 버튼의 컨텐트 프로퍼티는 System.object 타입
- XAML에서 프로퍼티 어트리뷰트 혹은 컨텐트 프로퍼티를 어떻게 사용해야 Rectanlge 객체를 이용한 예제와 같은 결과를 얻을 수 있을까?
    - 버튼에는 Rectangle이라는 어트리뷰트를 붙일 수 없음
    - XAML은 표현이 길어지는 단점을 감수해서라도 복잡한 프로퍼티를 설정할 수 있도록 대안을 제공함 -> **프로퍼티 엘리먼트**
    ```XAML
    <Button xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation">
    <Button.Content>
      <Rectangle Height="40" Width="40" Fill="Black"/>
    </Button.Content>
    </Button>
    ```
- C# 코드와 동일한 결과를 얻기 위해서 컨텐트 프로퍼티로 어트리뷰트 대신에 엘리먼트를 사용함
- Button.Content에서 마침표는 프로퍼티 엘리먼트와 오브젝트 엘리먼트를 구분해줌
- 프로퍼티 엘리먼트는 항상 `타입명.프로퍼티`의 형태로 쓰임, `타입명`으로 쓰인 오브젝트 엘리먼트의 내부에 포함되고 자신은 어떤 어트리뷰트도 갖지 않음
- 프로퍼티 엘리먼트는 단순한 프로퍼티로 사용 가능함

- Content와 Background 프로퍼티를 사용한 예
```xaml
 <Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
 Content="OK" Background="White"/>
```
- 이것을 프로퍼티 엘리먼트로 표현하면...
```xaml
  <Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
  <Button.Content>
   OK
  </Button.Content>
  <Button.Background>
   White
  </Button.Background>
  </Button>
```
- XAML에서는 어트리뷰트를 사용하는 것이 간편하고 좋은 방법

## 타입 컨버터
- 앞서 선언한 것과 동일한 내용을 C#으로 작성해보면...
```c#
System.Windows.Controls.Buttton b = new System.Windows.Controls.Button();
b.Content="OK";
b.Background = System.Windows.Media.Brushes.White; //1
```
- 어떻게 1번 문장이 XAML의 순수 문자열인 "White"와 같은 결과를 나타낼 수 있을까?
- XAML 프로퍼티는 단순 문자열을 System.String이나 System.Object가 아닌 다른 타입으로 전환해줌!
- XAML 파서나 컴파일러는 문자열을 적절한 데이터 타입으로 바꿔주는 **타입 컨버터(Type Converter)**를 찾음
- WPF는 Brush, Color, FointWeight, Point처럼 많이 쓰이는 데이터 타입을 처리하기 위해서 타입 컨버터를 제공함
- 모든 타입 컨버터는 TypeConverter 클래스에서 파생된 클래스들
- 독자적인 데이터 타입을 위해서 자신만의 타입 컨버터를 만들 수도 있음.
- XAML과의 차이점: 타입 컨버터는 일반적으로 XAML에 설정된 값의 대소문자를 구별하지 않음
                  White/white/WHite 모두 동일한 값으로 인식함
- 브러시의 타입 컨버터가 없다면, XAML의 백그라운드에 프로퍼티 엘리먼트를 사용해야 함

```xaml
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
Content="OK">
<Button.Background>
  <SolidColorBrush Color="White">
</Button.Background>
</Button>
```
- "White"이라는 문자열을 자동으로 Color 타입으로 형변환시켜줌 -> Color 타입의 타입 컨버터가 없다면 더 복잡한 구성을 사용해야 함
```xaml
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation" 
Content="OK">
<Button.Background>
  <SolidColorBrush>
  <SolidColorBrush.Color>
    <Color A="255" R="255" G="255" B="255"/>
  </SolidColorBrush Color>
  </SolidColorBrush>
</Button.Background>
</Button>
```

## 마크업 확장식
- 마크업 확장식은 XAML로 표현할 수 있는 범위를 대폭 확장해줌
- 두 기능은 보통 런타임 시에 주어진 값을 체크해서 문자열에 맞는 적절한 객체를 생성함
- 몇몇의 내장 마크업 확장식은 성능상의 문제 때문에 컴파일 시에 적절성을 평가함.
- 타입 컨버터처럼, WPF는 이미 많이 사용하는 다양한 마크업 확장식을 내장하고 있음.
- MarkupExtension 클래스에서 파생된 것들이 여기에 해당됨

- 타입 컨버터와 다른 점: XAML을 명시적이고 일관된 문법을 이용해서 가져온다는 것
- 마크업 확장식은 여러 가지 면에서 장점이 많음 -> XAML에서 자주 사용함.
- 타입 컨버터는 개발자가 수정할 권한 없음 -> 잠재적 한계 존재, 마크업 확장식은 이런 한계를 극복할 수 있는 확장성을 제공함
ex) 컨트롤 배경 색상을 "fancy gradient brush"라는 특별한 문자열 이용해서 바꾸고 싶을 때,
    BrushConverter는 이 문자열 해석 못함, 그러나 사용자 지정 마크업 확장식을 만들어 사용하면 가능

- 중괄호로 감싸진 어트리뷰트를 만날 때마다, XAML 컴파일러나 파서는 문자열이 아니라 마크업 확장식의 처리 대상으로 생각함
```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Background="{x:Null}" //마크업 확장식
        Height="{x:Static SystemParameters.IconHeight}"  //위치 파라미터
        Content = "{Binding Path=Height, RelativeSource={RelativeSource Self}}"/>  //명명 파라미터, 위치 파라미터
```
- 중괄호에 정의된 값들: 접두사를 제외한 첫 번째 식별자가 마크업 확장식 클래스의 이름
- 일반적으로 이름의 끝에 Extension 접미사를 붙여서 클래스로 활용함, 그러나 XAML에서는 접미사를 떼고 사용함.
- x:Null은 NullExtension이나 x:Static은 StaticExtensnion 클래스와 동일함.
- 이들이 포함된 System.Windows.Markup CRL 네임스페이스는 두 번째 XMl 네임스페이스와 매핑됨 -> 반드시 x 접두사가 쓰여야 함
- Binding 은 기본 네임스페이스를 사용하는 System.Windows.Data 네임스페이스에 속하므로 Extension 접미사나 접두사를 붙이지 않음

- 마크업 확장식이 지원해준다면, 쉼표로 구분되는 파라미터를 사용 가능함
- SystemParameters.IconHeight과 같은 위치 파라미터는 인수를 사용하는 확장식 클래스의 생성자에 문자열 인수로 사용됨
- Path나 RelativeSource 같은 명명 파라미터(named parameter)는 이미 생성된 확장식 객체에 동일한 이름의 프로퍼티를 설정함.
- 그 프로퍼티에 설정된 값들은 RelativeSource처럼 자신 스스로를 중괄호로 감싸서 마크업 확장식에서 사용하는 경우가 있음
- 정상적인 형변환 과정을 거치는 문자열로 쓰일 수도 있음.

- NullExtension은 백그라운드 프로퍼티에 널 값을 가진 브러시를 설정할 수 있게 해줌
- 그러나 BrushConverter는 이것을 기본적으로 지원하지 않음, 다른 타입 컨버터들도 동일한 문제가 발생함

- StaticExtension은 static 속성의 프로퍼티나 필드, 상수, 열거형을 사용할 수 있게 해줌
- XAML에 문자열을 하드코딩하는 것보다 효과적임

- 버튼의 높이는 운영체제 아이콘의 현재 높이가 값으로 설정됨, System.Windows.SystemParameters 클래스의 IconHeight 스태틱 프로퍼티 값을 가져옴

**중괄호 사용하기**
- 프로퍼티 어트리뷰터 값에 열린 중괄호({})를 가진 문자열을 설정하려면 그 문자를 이스케이프 문자처럼 처리 -> 마크업 확장식으로 사용되지 않게 해야 함
```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        Content="{}{This is not a Markup Extension}">
```
- 다른 방법 살펴보면, 본래 중괄호 문자는 특별한 의미가 있으므로 사용할 때 조심해야 함
- 길지만 프로퍼티 엘리먼트를 사용하면 앞의 내용을 보기 좋게 다시 작성 가능함 
```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
            {This is not a Markup Extension}
</Button>
```
- XAML로 내용을 표현한 것
```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
            <Button.Background>
                <x:Null/>
            </Button.Background>
            <Button.Height> <!--Member 프로퍼티 가짐-->
                <x:Static Member="SystemParameters.IconHeight"/>
            </Button.Height>
            <Button.Content>
                <Binding Path="Height">
                    <Binding.RelativeSource>
                        <RelativeSource Mode="Self"/> <!--모드 프로퍼티 가짐-->
                    </Binding.RelativeSource>
                </Binding>
            </Button.Content>
        </Button>
```
- 이렇게 풀어 쓰는 것은 모든 마크업 확장식이 클래스로 쓰일 때 사용하는 인수와 상응하는 프로퍼티를 가지고 있기 떄문에 가능함.

**심화학습**
- 마크업 확장식과 프로그래밍 코드의 사용
 마크업 확장식의 실제 작업은 개별 마크업 확장식에서 처리됨. 
 C# 코드는 XAML기반의 NullExtension, StaticExtension, Binding을 사용한 버튼과 동일한 결과를 보여줌
 ```C#
  System.Windows.Controls.Button b = new System.Windows.Controls.Button();
  //백그라운드 프로퍼티 설정
  b.Background = null;
  //높이 값을 설정
  b.Height = System.Windows.SystemPArameters.IconHeight;
  //컨텐트 프로퍼티를 설정
  System.Windows.Data.Binding binding = new System.Windows.Data.Binding();
  binding.Path = new System.Windows.PropertyPath("Height");
  binding.RelativeSource = System.Windows.Data.RelativeSource.Self;
  b.SetBinding(System.Windows.Controls.Button.ContentProperty, binding);
 ```
- XAML 파서나 컴파일러처럼 처리하지 않음
- 런타임 시에 개별 마크업 확장식에 알맞는 provideValue 메소드를 호출하여 적절한 값을 부여하는 방식을 사용함 
- 작성된 코드의 처리과정은 복잡-> 오로지 파서만이 사용가능한 프로그램 문맥을 요구함

## 오브젝트 엘리먼트의 자식 요소들
- XAML도 XML을 기반으로 함 -> 하나의 루트 엘리먼트만을 가질 수 있음
- XML 엘리먼트처럼 오브젝트 엘리먼트도 자식 엘리먼트들을 가질 수 있음
- 프로퍼티 엘리먼트는 예외적인 경우임
- 오브젝트 엘리먼트는 세 가지 형태의 자식 엘리먼트를 가질 수 있음 
    - 1. 컨텐트 프로퍼티의 값
    - 2. 컬렉션 아이템
    - 3. 부모 엘리먼트의 타입

### 컨텐트 프로퍼티
- 대부분의 WPF 클래스들은 사용자 지정 어트리뷰트 사용 -> XML 엘리먼트 안쪽의 내용과 관계없이 프로퍼티를 설정할 수 있도록 설계됨
- 그 역할을 하는 것: **컨텐트 프로퍼티**
- 이 프로퍼티는 XAML을 간결하게 표현하는 정말 편리한 방법

```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
               Content="OK"/>
```
- 컨텐트 프로퍼티 사용하지 않고 이렇게 쓸 수도 있음
```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
   OK
</Button>
```

- 단순한 텍스트 뿐만 아니라 더 복잡한 컨텐트도 포함 가능함.
```XAML
<Button xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
    <Button.Content>
        <Rectangle Height="40" Width="40" Fill="Black"/>
    </Button.Content>
</Button>
```

- 컨텐트 프로퍼티는 실제로 Content 키워드 외에는 어떤 것도 필요로 하지 않음
- System.Windows.Controls 네임스페이스에 속한 컨트롤이라도 콤보박스, 리스트박스, 탭컨트롤처럼 몇몇 컨트롤은 컨텐트 프로퍼티를 대신해서 아이템즈 프로퍼티를 사용함 

### 컬렉션 아이템
XAML은 인덱싱을 지원하는 두 가지 컬렉션 형태의 프로퍼티를 가지고 있음
리스트와 딕셔너리가 바로 그러함

**리스트**
- ArrayList 처럼 System.Collections.IList를 구현한 컬렉션
- 예제는 IList 인터페이스를 구현한 Items 프로퍼티를 이용 -> 리스트박스에 두 개의 아이템을 추가함

```C#
        System.Windows.Controls.ListBox listbox = new System.Windows.Controls.ListBox();
        System.Windows.Controls.ListBoxItem item1 = new System.Windows.Controls.ListBoxItem();
        System.Windows.Controls.ListBoxItem item2 = new System.Windows.Controls.ListBoxItem();
        item1.Content = "Item 1";
        item2.Content = "Item 2";
        listbox.Items.Add(item1);
        listbox.items.Add(item2);
```
- Items는 리스트박스의 컨텐트 프로퍼티 -> 간결하게 줄일 수 있음
```XAML
<ListBox xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
    <ListBoxItem Content="Item 1"/>
    <ListBoxItem Content="Item 2"/>
</ListBox>
```
- 리스트박스의 아이템즈 프로퍼티가 자동으로 빈 컬렉션 객체를 생성 -> 코드 이상없이 작동
- 아이템즈 같은 컬렉션 프로퍼티가 빈 컬렉션 객체 생성하지 않고 널 값을 갖고 있다면 컬렉션을 인스턴스화하는 
  엘리먼트의 아이템즈 프로퍼티는 래핑(상속받아 재정의)해서 사용해야 함 

**딕셔너리**
- System.Windows.ResourceDictionary 클래스는 WPF에서 폭넓게 사용하는 컬렉션 타입
- System.Collections.IDictionary 인터페이스를 구현한 컬렉션
  -> 해시 테이블처럼 키와 값을 쌍으로 가진 데이터를 추가, 삭제, 열거가 가능함
- XAML에서도 IDictionary를 구현한 임의의 컬렉션에 키와 값을 가진 쌍을 추가 가능함

```xaml
<ResourceDictionary
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
            <Color x:Key="1" A="255" R="255" G="255" B="255"/>
            <Color x:Key="2" A="0" R="0" G="0" B="0"/>
</ResourceDictionary>
```

- 두 번째 네임스페이스에 정의된 Key 키워드를 이용해서 데이터를 컬렉션에 추가 가능함
- 이 키워드는 내부적으로 하나의 키마다 Color 구조체의 값을 추가함
- Color 타입은 자체적인 Key 프로퍼티가 정의되어 있지 않아서, 위의 XAML 코드와 같은 C#코드는 다음과 같음

```C#
System.Windows.ResourceDictionary d = new System.Windows.ResourceDictionary();
            System.Windows.Media.Color color1 = new System.Windows.Media.Color();
            System.Windows.Media.Color color2 = new System.Windows.Media.Color();
            color1.A = 255; color1.R = 255; color1.G = 255; color1.B = 255;
            color2.A = 0;   color2.R = 0;   color2.G = 0;   color2.B = 0;
            d.Add("1", color1);
            d.Add("2", color2);
```
- x:Key 키워드에 마크업 확장식이 사용되지 않는 한 사용된 프로퍼티는 항상 문자열로 처리되며 어떤 형변환도 일어나지 않음

### 더 다양한 형변환
- SolidColorBrush 에 쓰인 평이한 텍스트는 오브젝트 엘리먼트의 자식으로 자주 사용됨
`<SolidColorBrush>White</SolidColorBrush>`
- 비록 Color 프로퍼티가 컨텐트 프로퍼티는 아니지만 다음과 같이 간결하게 사용 가능
`<SolidColorBrush Color="White"/>`
- 처음에 XAML은 "White" 문자열을 SolidColorBrush 객체로 변환시켜주는 타입 컨버터가 있음 -> 올바르게 작동이 가능함
- 타입 컨버터가 XAML의 가독성을 높이는 데 매우 큰 역할 함 그러나.. 부정적으로 보면 내부적 처리가 많아짐
  -> 닷넷 객체와 어떻게 매핑되는 지 이해하기 어렵게 만듬
- XAML에서는 추상 클래스를 인스턴스화할 수 있는 방법이 없음 -> 추상 클래스 엘리먼트를 선언할 수 없다고 생각하게 될 수 있음.
- But... System.Windows.Media.Brush 클래스는 SolidColorBrush, GradientBrush나 다른 브러시의 기반 클래스이면서 추상 클래스이지만
  XAML에서도 간단히 사용가능함
  `<Brush>White</Brush>`
- 타입 컨버터가 브러시 엘리먼트를 SolidColorBrush 타입으로 처리하기 때문에 가능함
- XAML에서는 이렇게 기초적인 타입을 확인하고 처리할 수 있는 기능을 지원하는 것이 중요함.

## XAML과 프로그래밍 코드를 함께 컴파일하기
- WPF 프로그램은 닷넷의 어떤 언어로도 재작성이 가능함, 비교적 단순한 프로그램은 XAML만으로도 구현 가능함
- 대다수 WPF 프로그램은 XAML과 프로그래밍 코드를 함께 사용함

### 런타임 시에 XAML을 로드하고 파싱하기
- WPF 런타임 시에 XAMl 파서는 System.Windows.Markup 네임스페이스에 있는 XamlReader와 XamlWriter클래스를 이용함
- XamlReader와 XamlWriter 클래스는 각각 오버로드된 Load 메소드와 Save 메소드를 정의하고 있음
- 어떤 종류의 닷넷 언어로 작성되든지 간에 특별한 문제 없이 런타임 시에 XAML을 사용 가능함

**XamlReader**
- XamlReader.Load 메소드는 XAML을 파싱 -> 적절한 닷넷 객체 생성 -> 루트 엘리먼트의 인스턴스를 반환
- Window 객체를 루트 엘리먼트로 사용하는 MyWindow.xaml이라는 파일 있으면, 코드는 윈도우 객체를 로드하고 객체화해서 반환함
```C#
Window window = null;
using(FileStream fs = new FileStream("MyWindow.xaml", FileMode.Open, FileAccess.Read))
{
    window = (Window)XamlReader.Load(fs);
}
```
- Load 메소드는 System.IO.FileStream 클래스를 호출함
- Load 메소드가 반환된 후, XAML 파일에 있는 2전체 계층구조가 메모리에 인스턴스화됨 -> XAML 파일은 더 이상 필요 없음
- 위 코드는 FileStream 클래스에 using 블록이 설정되어 있음 -> 인스턴스화된 이후에 곧바로 소멸됨
- XamlReader는 상황에 맞춰 Stream 클래스를 사용할 수 있음 -> XAML을 인스턴스화하는 데 많은 융통성 발휘할 수 있음

- 루트 엘리먼트의 인스턴스가 존재함 -> 적당한 컨텐트 프로퍼티나 컬렉션 프로퍼티를 이요해서 자식 엘리먼트를 검색 가능

```C#
Window window = null;
using (FileStream fs = new FileStream("MyWindow.xaml", FileMode.Open, FileAccess.Read))
{
    //Window 객체인 루트 엘리먼트를 얻어옴
     window= (Window)XamlReader.Load(fs);
}
//하드코딩된 파일에서 자식 엘리먼트들 중 OK버튼을 가져옴
StackPanel panel = (StackPanel)window.Content;
Button okButton = (Button)panel.Children[4];
```
- 버튼의 참조정보 알고 있다면 원하는 것은 어떤 것이든 할 수 있음

**XAML 엘리먼트에 이름 사용하기**
- XAML 언어 네임스페이스는 엘리먼트에 이름을 부여할 수 있는 Name이란 키워드를 갖고 있음.
- 이미지 처리하려는 OK 버튼은 윈도우 객체 내의 임의의 장소에 포함되어 있음.
- Name 키워드를 다음과 같이 사용하여 이름 부여 가능
```XAML
<Button x:Name="okButton">OK</Button>
```
- 이 방법 통해서, 윈도우 객체의 FindName 메소드 호출 -> 자식 엘리먼틀르 찾는 C#코드로 수정 가능
```C#
Window window = null;
using (FileStream fs = new FileStream("MyWindow.xaml", FileMode.Open, FileAccess.Read))
{
  //루트 엘리먼트를 얻어오는 것이 Window 객체라는 것을 알 수 있음.
  window = (Window)XamlReader.Load(fs);
}

//주어진 이름을 가진 OK 버튼을 찾아옴
Button okButton = (Button)window.FindName("okButton");
```

### XAML 컴파일하기
- XAML의 컴파일 3가지 종류:
    -1. XAML파일을 특별한 바이너리 형태로 전환하는 것
    -2. 전환된 컨텐트를 바이너리 리소스로 어셈블리 내부에 포함시키는 것
    -3. 자동으로 XAML과 프로그래밍 코드를 연결하는 부분을 처리하는 것

- 프로그래밍 코드를 함께 사용해서 컴파일하는 경우에 첫 번째 해야하는 일: XAML의 루트 엘리먼트의 클래스를 찾아내는 것
  XAML 언어 네임스페이스에 규정된 x:Class 키워드를 통해 처리 가능함
  ```XAML
  <Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
          xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
          x:Class = "MyNamespace.MyWindow">
  ...
  </Window>
  ```

- 동일한 프로젝트이지만 분리된 소스파일을 통해, 엘리먼트를 처리하는 클래스를 정의 가능하고 원하는 멤버들을 정의 가능함.
```C#
namespace MyNamespace
{
    partial class MyWindow:Window
    {
        public MyWindow
        {
            //XAML에 정의된 내용을 로드하기 위해서 호출할 필요가 있음
            InitializeComponent();
            ...
        }
        //다른 일반 멤버들이 여기에 올 수 있음.
    }
}
```
- 이 클래스들은 종종 코드비하인드 파일이라고 함
- 버튼 엘리먼트의 Click 이벤트 어트리뷰트처럼 XAML의 이벤트 처리기를 참조해야 한다면, 이것을 정의해야할 곳은 코드비하인드 파일(이벤트핸들러 같은 애들 말하나 봄)

- 클래스 정의 시, `partial`키워드는 매우 중요함. partial 클래스는 한 개 이상의 많은 파일로 분리되어 구현 가능함
```XAML
<Window xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        x:Class = "Mynamespace.MyWindow" x:SubClass="MyNamespace.MyWindow2">
...
</Window>
```
- 이런 변경을 통해, XAML 파일은 코드비하인드 파일에서는 Class 키워드로 정의한 파일을 제외하면
  SubClass 키워드를 통해서 지정된 클래스를 완벽하게 정의함
- XAML파일은 MyWindow를 코드비하인드 클래스로, MyWindow2를 하위클래스로 사용함
- 프로젝트에 새 WPF 항목 추가 시, 비주얼스튜디오는 자동으로 루트 엘리먼트에 x:Class를 사용 -> XAML 파일과 partial 클래스가 미리 정의된 코드비하인드 파일을 만들어줌
- 이 파일들이 잘 동작하도록 연결속성을 자동으로 생성해줌

```XAML
<ItemGroup>
  <Page Include = "MyWindow.xaml">
</ItemGroup>
<ItemGroup>
  <Compile Include = "MyWindow.xaml.cs">
    <DependentUpon>MyWindow.xaml</DependentUpon>
    <SubType>Code</SubType>
  </Compile>
</ItemGroup>
```
- 빌드하다보면 MyWindow.xaml을 처리하는 과정 중 몇 가지 파일 만들어짐
    - MyWindow.baml: BAML 파일 만들어짐, 기본 값으로 바이너리 리소스로서 어셈블리 내부에 포함됨
    - MyWindow.g.cs: C#소스 파일, 다른 소스 코드처럼 어셈블리로 컴파일됨

**BAML**
- 파싱되고 구별될 수 있는 토큰을 가진 바이너리 형태로 바뀐 XAML 파일
- XAML에서 BAML로 컴파일되는 처리과정 중에는 프로그래밍 코드를 만들어내지는 않음
- BAML(Binary Application Markup Language) != MSIL(Microsoft intermediate language)
- BAML파일은 텍스트 형식의 XAML 파일보다 훨씬 더 크기가 작고 빠르게 로드됨, 파싱되도록 설계된 압축 선언형 포맷

**생성된 소스코드**
- x:Class 를 사용한다면 XAML 컴파일 과정에서 프로그래밍 코드가 생성됨
- 이 파일들은 런타임 시에 느슨한 XAML 파일(XML로만 구성됨)을 로드하고 파싱하기 위해서 작성되어야 하는 것과 유사하게 자동으로 생성되는 코드
- 그러한 파일에는 .g.cs나 .g.vb의 접미사와 확장자가 붙음

- 각각 생성된 소스파일은 루트 엘리먼트에 x:Class로 선언된 클래스를 위해서 partial 클래스의 정의가 포함되어 있음.
- partial 클래스는 XAML 파일에 있는 모든 엘리먼트의 이름을 가진 private 변수를 포함함
- 이 파일은 포함된 InitializeComponent메소드를 통해 어셈블리에 포함된 BAML 리소스를 로드함 
- XAML 에 선언된 엘리먼트의 인스턴스를 해당 이름의 변수에 설정
- XAML 파일에 정의된 이벤트 처리기가 있다면 그것을 연결하는 작업도 처리

### XAML의 키워드
- 어떤 XAML 컴파일러나 파서든지 특별하게 처리해야 할 키워드들을 정의함
- XAML 언어 네임스페이스에 있는 키워드(관습적으로 x 접두사가 붙음)

| 키워드          | 사용대상                                                     | 설명                                                         |
| --------------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| x:Class         | 루트 엘리먼트의 어트리뷰트                                   | 루트 엘리먼트를 위한 클래스를 정의함. 클래스 명칭에 전체 닷넷 네임스페이스명을 같이 사용 가능함. |
| x:ClassModifier | 루트 엘리먼트의 어트리뷰트, x:Class와 함께 사용되어야 함     | x:Class로 정의된 클래스의 접근제한자를 정의함. 기본값은 public임. 어트리뷰트값으로는 반드시 C#에서 사용되는 public이나 internal 같은 단어만 가능함. |
| x:Code          | XAML 내부의 어디서나 엘리먼트에 사용. x:Class와 함께 사용되어야 함 |                                                              |
|                 |                                                              |                                                              |
|                 |                                                              |                                                              |
|                 |                                                              |                                                              |
|                 |                                                              |                                                              |
|                 |                                                              |                                                              |
|                 |                                                              |                                                              |
|                 |                                                              |                                                              |



## 결론
### 논쟁1. XML은 데이터 타입을 너무 장황하게 표현한다.


### 논쟁2. XML 기반 시스템은 성능이 형편없다.
