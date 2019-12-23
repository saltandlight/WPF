# Chapter02. XAMl 신비를 벗다
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

## 마크업 확장식
## 오브젝트 엘리먼트의 자식 요소들
### 컨텐트 프로퍼티
### 컬렉션 아이템
### 더 다양한 형변환
## XAMl과 프로그래밍 코드를 함께 컴파일하기
### 런타임 시에 XAML을 로드하고 파싱하기
### XAML 컴파일하기
### XAML의 키워드
## 결론
### 논쟁1. XML은 데이터 타입을 너무 장황하게 표현한다.
### 논쟁2. XML 기반 시스템은 성능이 형편없다.
