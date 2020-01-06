# Chapter7. 프로그램의 구조화와 배포

## 표준 윈도우즈 프로그램
- 표준 윈도우즈 응용 프로그램은 로컬 컴퓨터에서 실행됨, 하나 이상의 윈도우를 사용하는 인터페이스를 가짐 
### 윈도우 클래스
- 컨텐트를 포함하는 가장 기본적인 엘리먼트
- WPF 윈도우는 Win32윈도 기반하에서 실행됨 -> WPF기반인지 Win32 기반인지 모름
- Win32와 동일한 방법으로 크롬을 렌더링하고 작업표시줄에 나타냄 
    - 크롬이란?
    - 최대화, 최소화, 닫기 버튼을 가진 비클라이언트 영역을 부르는 용어
- WPF 윈도우는 윈폼이 Win32를 이용하는 것과 유사한 방식으로 추상화를 위한 유용한 메소드와 프로퍼티를 지원
- Show 메소드 호출 -> 윈도우 나타남
- Hide 메소드 호출 -> 사라짐
- Close 메소드 호출 -> 영구히 사라짐
- 윈도우 엘리먼트의 외관은 Icon, Title, WindowStyle 프로퍼티를 이용하여 조정
- Left, Top을 이용하면 화면상의 위치 조정 가능
    - WindowStartupLocation 프로퍼티를 CenterScreen 또는 CenterOwner로 설정 시 윈도우가 처음 렌더링될 때 위치 더 쉽게 파악 가능
    - Topmost를 true로 설정 시 항상 모든 윈도우보다 앞에 있게 됨(레이어가 쌓여있고 설정된 레이어가 맨 위에 있다고 생각...!)
    - ShowInTaskbar를 false로 설정 시, 작업표시줄에 나타나는 전형적인 표시를 없앨 수 있음
- 윈도우의 속성은 윈도우가 노출하고 있는 프로퍼티를 통해서만 가능함

모덜리스 대화상자(modeless dialog):
- 윈도우 클래스가 Show 메소드를 이용해서 만든 자식 윈도우
- 다른 윈도우와 특별히 다른 점은 없지만, 부모 윈도우와 함께 종료되고 최소화됨 

- 윈도우가 다른 윈도우를 자식으로 만드려면 자식의 Owner 프로퍼티를 부모로 참조하도록 해야 함
- 부모 윈도우는 읽기 전용의 OwnedWindows 프로퍼티를 통해 자신의 자식 윈도우를 열거하고 접근 가능함

- InitializeComponent 메소드 호출이 중요한 이유:
    - 객체를 제대로 생성해주기 때문
    - 컴파일된 XAML의 런타임 시 처리가 이 메소드 안에서 이루어지기 때문
    - 비주얼 스튜디오를 이용하면 자동으로 이 메소드를 호출해주는 코드를 만들어주기 때문 -> 실수할 위험 줄어듬

### 애플리케이션 클래스
- 전체적인 프로그램이 시작되는 진입점이 있어야 함
- 만약 다음처럼 Main 메소드를 작성한다면...
```C#
public static void Main()
{
  MainWindow window = new MainWindow();
  window.Show();
}
```
- WPF 프로그램은 단일 스레드 방식(STA:single-threaded apartment)으로 실행되어야 함
    - Main 메소드는 STAThread라는 어트리뷰트가 선언되어야 함
- Show 메소드는 Win32의 ShowWindow 메소드 이용 -> 윈도우를 보여주고 바로 반환하는 넌블로킹을 이용함
- .... Show메소드를 Main 메소드의 가장 마지막에서 호출하면 눈에 안 보일 정도로 프로그램 깜빡이고 바로 종료됨... 흐규...
- MainWindow가 바로 종료되는 것을 막기 위해서, 종료시점까지 운영체제와 MainWindow 사이에 메세지 전달하는 처리를 할 필요가 있음
- 이 메세지들은 Win32 프로그램에서 만들어지는 WM_PAINT, WM_MOUSEMOVE 같은 윈도우 메시지와 동일함
- Win32 환경에서는 입력되는 메세지를 처리하고 절차에 따라 메시지를 보내 완료되었음을 알림
- 이 과정 = 메시지 루프 또는 메시지 펌프라고 함
- WPF는 이런 복잡한 작업을 쉽게 처리가능하도록 System.Windows.Application 클래스를 제공함

**Application.Run 사용하기**
- 애플리케이션 클래스는 프로그램을 활성화하고 메세지를 효과적으로 전달하기 위해 Run 메소드를 이용함
- Main 메소드를 다음과 같이 수정함
```C#
public static void Main()
{
  Application app = new Application();
  MainWindow window = new MainWindow();
  window.Show();
  app.Run(window);
}
```
- 애플리케이션 클래스는 StartupUri라는 프로퍼티 이용 -> 처음 로딩할 윈도우 지정 가능함
```C#
public static void Main()
{
  Application app = new Application();
  app.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
  app.Run();
}
```
- MainWindow는 XAML의 소스파일 위치를 URI를 통해 확인, 윈도우를 생성할 필요 없는 Run을 호출
- StartupUri 프로퍼티를 갖고 있는 이유: XAML을 이용해서 어느 곳에서나 동일한 초기화가 가능하도록 하기 위함

**애플리케이션 클래스의 다른 기능들**
- 애플리케이션 클래스: 진입점이나 프로그램 사이의 메시지 전달 뿐만 아니라 프로그램 차원의 처리를 위해 다수의 이벤트, 프로퍼티, 메소드를 정의하고 있음
- 비주얼스튜디오에서 만들어진 App클래스처럼 Application 을 상속받은 클래스들은 Startup, Exit, Activated, Deativated, SessionEnding 같은 이벤트를 재정의해서 처리 가능함
- Application의 다른 유용한 프로퍼티: Properties 컬렉션
- Properties 컬렉션: 윈도우나 다른 객체 사이에 데이터를 저장하고 공유하기 위해 사용하는 키와 값을 한 쌍으로 하는 딕셔너리의 일종
- 애플리케이션 수준에서 전역 변수로 public 멤버나 프로퍼티를 정의하는 것 < Properties 컬렉션에 데이터를 보관하는 방법
```C#
myApplication.Properties["CurrentPhotoFilename"]=filename;
```
- 다시 가져오기 위해 다음과 같이 사용함
```C#
string filename = myapplication.Properties["CurrentPhotoFilename"] as string;
```
- 키와 값이 모두 Object 타입 -> 어떤 데이터도 보관 가능, 문자열에 제한 없음

### 대화상자 만들기와 보여주기
- 윈도우즈: 운영체제 차원에서 파일 열고 저장하거나 폴더를 탐색하고 폰트나 색깔을 선택하거나 인쇄할 수 있게 해주는 공통적인 대화상자를 제공
#### 공통 대화상자
- WPF는 몇몇 공통 대화상자(common dialog)를 내장하고 있음
- 이를 편리하기 사용 가능하도록 메소드와 프로퍼티를 지원함
- 순수 WPF만으로는 이런 대화상자를 렌더링할 수 없음 -> 내부적으로 Win32의 API를 호출함

#### 사용자지정 대화상자

### 프로그램 상태를 유지하고 복원하기

## 탐색 기반 윈도우즈 응용 프로그램
## 윈도우즈 비스타의 룩앤필을 가진 응용 프로그램
## 가젯 스타일의 응용 프로그램
## XAML 브라우저 응용 프로그램
## 느슨한 XAML 페이지
## 결론
