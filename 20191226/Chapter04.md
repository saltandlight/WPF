# Chapter04. WPF에서 새롭게 소개되는 중요한 개념들
## 컨텐트 컨트롤
- 오직 하나의 아이템만 갖도록 제한된 단순한 컨트롤
- 이 컨트롤들은 모두 System.Windows.Controls.ContentControl에서 파생됨

- 이 프로퍼티는 어떤 객체도 가질 수 있음 -> 중복된 객체의 트리 구조도 포함 가능, 그러나... 하위의 자식 컨트롤은 하나만 가질 수 있음
- ContentControl 클래스는 컨텐트 프로퍼티 뿐만 아니라 불리언 타입의 HasContent 프로퍼티도 갖고 있음.
- 이 프로퍼티는 컨텐트 프로퍼티가 널일 경우 false를 반환, 그렇지 않으면 true를 반환

### 버튼
- UI에 사용되는 컨트롤 중 가장 익숙하고 많이 사용됨

- 기본적인 버튼은 단순 클릭만 가능하고, 더블클릭은 불가능함

- 실제 버튼들의 정의: 버튼베이스(ButtonBase)라는 추상클래스에 있음
  
    - 클릭 이벤트뿐만 아니라 이 이벤트와 동일한 역할을 하는 로직을 함께 가짐
    
- 눌려진 상태에서 어떤 작업을 처리하기 원할 때 사용하는 IsPressed 라는 불리언 타입의 프로퍼티를 정의하고 있음

- 가장 흥미로운 것: 클릭모드 프로퍼티
    - 이 프로퍼티는 이벤트 발생 시, 더 세밀한 처리를 위해 ClickMode 열거형을 사용하여 값을 설정함.
    - 열거형의 값: Release(기본값), Press, Hover
    - 의미상: 버튼을 누르는 것=클릭하는 것
    
- 버튼 클래스의 자식 컨트롤
    - Button
    - RepeatButton
    - ToggleButton
    - CheckBox
    - RadioButton
**버튼**
    
- WPF의 버튼 클래스는 버튼 클래스의 기능 확장 -> 취소 버튼, 기본 버튼 기능을 추가함

- -> 대화상자의 단축키를 편리하게 사용하도록 함

- 대화상자 버튼에서 Button.IsCancel을 true로 설정하면 윈도우 클래스는 자동으로 DialogResult프로퍼티를 false로 설정하고 닫힘

**리피트버튼**

- 버튼과 기능이 거의 유사

- 차이점: 눌려있는 동안 계속해서 클릭 이벤트가 발생함

- 클릭 이벤트가 일어나는 빈도 수는 리피트버튼의 Delay와 Interval 프로퍼티로 조정

- 기본 값은 SystemParameters.KeyboardDelay와 SystemParameters.KeyboardSpeed임

- 버튼 누를 때마다 일정하게 증가 또는 감소를 표현하는 데 유용함

**토글버튼**
- 버튼의 취소 버튼과 기본 버튼 기능이 없음 -> 클릭했을 때 상태를유지할 수 있는 접착성 강한 버튼
- 처음 클릭하면 IsChecked 프로퍼티가 true가 되고 다시 클릭 시 false가 됨
- 토글 버튼은 IsThreeState 프로퍼티를 가짐 
    - 이 프로퍼티가 true로 설정될 경우, IsChecked 프로퍼티는 널 값이 추가됨
    - 본래 IsChecked 프로퍼티는 Nullable<Boolean> 타입
    - 첫 클릭 시 true(Checked 이벤트 발생), 두 번째는 null(Indeterminate 이벤트 발생), 세 번쨰는 false(Unchecked 이벤트 발생) 순으로 순회
- IsCheckedChanged라는 하나의 이벤트 처리기만 정의함

**체크박스**
- **체크박스 특징들**
    - 한 개의 컨텐트만 가질 수 있음 (표면적으로)
    - 마우스나 키보드에서 발생하는 클릭의 개념을 사용함
    - 클릭 시 checked나 unchecked의 상태를 유지함
    - 토글버튼처럼 checked/indeterminate/unchecked의 세 가지 상태 모드를 지원함
    - 토글 버튼의 모양만 바꾼 것, 토글 버튼에서 파생됨
**라디오 버튼**
- 토글 버튼에서 파생된 또 다른 컨트롤
- 다른 점: 
    - 상호배제(mutual exclusion)을 지원하는 내장 기능이 있음
- 여러 개의 라디오 버튼을 하나의 그룹으로 묶으면 단 하나만 선택 가능함
- 한 번 체크되면 원상 복구가 불가능 -> 프로그래밍 코드로만 가능
- 다수의 라디오 버튼을 동일한 그룹 내에 두는 것은 생각보다 간단함
```XAML
<StackPanel>
    <RadioButton>Option 1</RadioButton>
    <RadioButton>Option 2</RadioButton>
    <RadioButton>Option 3</RadioButton>
</StackPanel>
```

- 라디오 버튼을 사용자가 지정하는 방식을 사용해서 그룹화하려면,
  그룹네임 프로퍼티에 동일한 이름을 설정하면 됨 
- 로지컬 트리의 루트 엘리먼트 아래 있고, 그룹 이름이 동일하면 부모 엘리먼트가 달라도
  어디에 있든지 동일한 그룹으로 묶임

```XAML
<StackPanel>
 <StackPanel>
    <RadioButton GroupName="A">Option 1</RadioButton>
    <RadioButton GroupName="A">Option 2</RadioButton>
 </StackPanel>
 <StackPanel>
    <RadioButton GroupName="A">Option 3</RadioButton>
 </StackPanel>
</StackPanel>
```
- 동일한 부모 엘리먼트에서 다른 그룹으로 묶을 수 있음
```XAML
<StackPanel>
    <RadioButton GroupName="A">Option 1</RadioButton>
    <RadioButton GroupName="A">Option 2</RadioButton>
    <RadioButton GroupName="B">A Different Option 1</RadioButton>
    <RadioButton GroupName="B">A Different Option 2</RadioButton>
</StackPanel>
```
### 단순 컨테이너
- 버튼처럼 클릭한다는 개념이 없는 내장 컨트롤이 있음

**라벨**
- 텍스트를 보여주는 곳에 사용하는 아주 익숙한 컨트롤
- 어떤 객체도 포함가능한 컨텐트 컨트롤이지만, 일반적으로 텍스트를 표현하는 데 유용함
- 유일하게 액세스 키를 지원함
- 액세스 키를 이용하여 라벨의 텍스트 중 한 글자에 특정 처리를 하도록 설정 가능함
- 사용자가 Alt키+특정 지정 문자 누름 -> 지정된 동작 실행함
- 윈도우에서는 원하는 글자 앞에 언더스코어 사용 -> 간단하게 지정, 라벨의 Target 프로퍼티를 설정하면 됨

- 라벨의 액세스키를 사용하는 일반적인 경우: 텍스트박스에 포커스가 가도록 하는 것
```XAML
<Label Target="{Binding ElementName=userNameBox}">_User Name:</Label>
<TextBox x:Name="userNameBox"/>
```

**툴팁**
- 툴팁 컨트롤은 이 컨트롤과 연결된 컨트롤에 마우스 올리면 나타나고 내리면 사라지는 플로팅 박스(floating box)를 가짐
```XAML
<Button>
    OK
    <Button.ToolTip>
        <ToolTip>
            Clicking this will submit your request.
        </ToolTip>
    </Button.ToolTip>
</Button>
```
- 툴팁 컨트롤은 다른 엘리먼트처럼 UI 엘리먼트의 트리 구조상에서 독립적으로 자리잡을 수 없음
- 대신, FrameworkElement와 FrameworkContentElement 클래스에 정의된 툴팁 프로퍼티를 설정해서 사용함

```XAML
    <CheckBox>
        CheckBox
        <CheckBox.ToolTip>
            <StackPanel>
                <Label FontWeight="Bold" Background="Blue" Foreground="White">
                    The CheckBox
                </Label>
                <TextBlock Padding="10" TextWrapping="WrapWithOverflow" Width="200">
                    CheckBox is a familiar control. But in WPF, it's not much
                    more than a ToggleButton styled differently!
                </TextBlock>
                <Line Stroke="Black" StrokeThickness="1" X2="200"/>
                <StackPanel Orientation="Horizontal">
                    <Image Margin="2" Source="help.gif"/>
                    <Label FontWeight="Bold">Press F1 for more help.</Label>
                </StackPanel>
            </StackPanel>
        </CheckBox.ToolTip>
    </CheckBox>
```

- 툴팁은 나타났다 사라지는 효과를 사용자가 조절할 수 있도록 Open과 Closed 이벤트를 정의함
- ToolTipService 클래스는 툴팁 컨트롤 자체보다는 툴팁을 사용하려는 엘리먼트에 적용할 수 있도록 첨부 프로퍼티를 정의함
- 툴팁과 동일한 프로퍼티를 포함할 뿐만 아니라 더 다양한 프로퍼티 가짐, 이 프로퍼티들은 이름이 동일한 툴팁의 프로퍼티보다 우선순위가 높음

**프레임**
- 프레임 컨트롤은 다른 컨텐트 컨트롤처럼 어떤 엘리먼트도 가질 수 있음.
- UI의 나머지 부분과 이 컨텐트는 분리됨
- 프레임 컨트롤의 장점: WPF뿐만 아니라 HTML의 컨텐트도 처리 가능함
- 프레임 컨트롤은 어떤 HTML이나 XAML 페이지도 값으로 설정할 수 있는 System.Uri 타입의 소스 프로퍼티를 갖고 있음
`<Frame Source="http://www.pinvoke.net">`
- HTML과 XAML에 모두 적용 가능한 탐색 추적 기능을 내부적으로 지원함

### 헤더를 가진 컨테이너
- 버튼 크롬이나 체크박스처럼 매우 단순한 모양의 엘리먼트를 추가하거나 때론 어떤 것도 추가하지 않았음.
**그룹박스**
- 컨트롤을 조직화할 수 있는 컨트롤
```XAML
 <GroupBox Header="Grammar">
        <StackPanel>
            <CheckBox>Check grammar as you type</CheckBox>
            <CheckBox>Hide grammatical errors in this document</CheckBox>
            <CheckBox>Check grammar with spelling</CheckBox>
        </StackPanel>
    </GroupBox>
```
![](cap.PNG)

- 컨텐트 프로퍼티처럼, 헤더 프로퍼티는 Object 타입이므로 다양한 객체를 가질 수 있고
- 이 객체가 UIElement에서 파생된 것이라면 본래 모습대로 렌더링될 것
- 버튼을 헤더 프로퍼티에 설정하면 이렇게 됨
```XAML
<GroupBox>
        <GroupBox.Header>
            <Button>Grammar</Button>
        </GroupBox.Header>
        <StackPanel>
            <CheckBox>Check grammar as you type</CheckBox>
            <CheckBox>Hide grammatical errors in this document</CheckBox>
            <CheckBox>Check grammar with spelling</CheckBox>
        </StackPanel>
    </GroupBox>
```
![](cap2.PNG)

**익스팬더**
- 익스팬더 컨트롤: 윈폼 같은 Win32 기반에서 제공하지 않는 흥미로운 컨트롤
- 내부 내용을 접었다 폈다 할 수 있는 기능이 추가되어 있음
- XAML에서 그룹박스 컨트롤을 익스팬더 컨트롤로 변경했음

```XAML
<Expander Header="Grammar">
        <StackPanel>
            <CheckBox>Check grammar as you type</CheckBox>
            <CheckBox>Hide grammatical errors in this document</CheckBox>
            <CheckBox>Check grammar with spelling</CheckBox>
        </StackPanel>
</Expander>
```

![](cap3.PNG)

## 아이템즈 컨트롤
- 아이템즈 컨트롤: WPF 컨트롤의 또 다른 메인 컨트롤
- 아이템의 컬렉션을 갖도록 설계됨
- ContentControl 클래스처럼 Control의 하위 클래스인 ItemsControl 클래스에서 파생됨
- ItemsControl은 ItemCollection 타입의 Items 프로퍼티에 자신의 내용을 저장함

```XAML
<ListBox xmlns:sys="clr-namespace:System;assembly=mscorlib">
        <Button>Button</Button>
        <Expander Header="Expander"/>
        <sys:DateTime>1/1/2007</sys:DateTime>
        <sys:DateTime>1/2/2007</sys:DateTime>
        <sys:DateTime>1/3/2007</sys:DateTime>
</ListBox>
```
![](cap4.PNG)
- 버튼과 익스팬더 컨트롤은 모두 UIElement를 상속받음 
  -> 보았던 것과 동일하게 처리됨, 갖고 있는 기능 대부분을 정상적으로 사용 가능함
- Items 프로퍼티는 읽기 전용
    - 초기화될 때, 빈 컬렉션에 객체를 추가하거나 제거할 수 있음
    - 다른 컬렉션을 가리키도록 할 수 없음

- ItemsControl 클래스는 이미 존재하는 임의의 컬렉션, 자신의 Items프로퍼티를 채울 수 있는 별도의 프로퍼티를 갖고 있음

**ItemsControl이 가지고 있는 유용한 프로퍼티들**
- HasItems
    - XAML에서 선언된 컨트롤의 상태를 쉽게 파악할 수 있는 읽기전용의 불리언 타입 프로퍼티
- IsGrouping
    - 컨트롤의 아이템들이 최상위 그룹으로 분리가 가능한지 여부를 알려주는 불리언 타입의 일기 전용 프로퍼틴
- DisplayMemberPath
    - 복잡한 표현식이나 각 항목이 어떻게 렌더링될지 여부를 지정할 수 있는 문자열 프로퍼티

```XAML
<ListBox xmlns:sys="clr-namespace:System;assembly=mscorlib"
             DisplayMemberPath="DayOfWeek">
        <Button>Button</Button>
        <Expander Header="Expander"/>
        <sys:DateTime>1/1/2007</sys:DateTime>
        <sys:DateTime>1/2/2007</sys:DateTime>
        <sys:DateTime>1/3/2007</sys:DateTime>
</ListBox>
```

![](cap5.PNG)

### 셀렉터

### 메뉴
### 다른 아이템즈 컨트롤
## 범위 컨트롤
### 프로그레스바
### 슬라이더
## 텍스트 및 잉크 컨트롤
### 리치텍스트박스
### 패스워드박스
### 잉크캔버스
## 결론
