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
- 인덱싱된 아이템들을 가진 컨트롤
- ItemsControl에서 파생된 셀렉터 클래스는 선택 처리를 할 수 있도록 프로퍼티들을 추가했음
**세 개의 프로퍼티**
- SelectedIndex:
    - 아이템은 인덱스가 0부터 시작함
    - 아이템이 컬렉션에 추가될 때마다 순서대로 값이 설정됨.
- SelectedItem:
    - 현재 선택된 실제 아이템의 인스턴스
- SelectedValue:
    - 현재 선택된 아이템의 값.
    - 기본적으로 아이템 그 자체를 값으로 처리함 
    - SelectedValue프로퍼티는 SelectedItem과 동일

세 가지 프로퍼티는 모두 읽기/쓰기가 가능 -> 현재의 선택 항목을 변경하고 값을 재설정 가능함.
셀렉터 컨트롤은 개별 아이템마다 적용 가능한 두 개의 첨부 프로퍼티를 지원함
- IsSelected: 아이템의 선택 여부나 아이템의 현재 선택 상태를 가져오는 불리언 타입의 프로퍼티
- IsSelectionActive: 현재 선택 아이템에 포커스가 있는지를 알려주는 읽기 전용의 불리언 타입 프로퍼티

셀렉터 컨트롤은 SelectionChanged라는 이벤트를 정의
현재 선택 상태에 변화가 있는지를 알 수 있게 함

WPF에 있는 Selector로부터 파생된 네 개의 컨트롤
- 콤보박스
- 리스트박스
- 리스트뷰
- 탭컨트롤


**콤보박스**
- 사용자들에게 목록에서 한 항목을 선택할 수 있게 해줌
- 이 컨트롤의 선택상자에서는 선택된 현재 아이템만 보여줌, 나머지는 요구가 있을 때만 보여줌

**사용자 지정 선택상자**
- 콤보박스: 사용자가 선택 상자에 임의의 텍스트를 입력할 수 있는 모드를 지원
- 텍스트가 목록에 있음 -> 자동으로 선택됨
- 텍스트가 목록에 없음 -> 어떤 아이템도 선택이 안 됨, 사용자가 타이핑한 텍스트는 콤보박스의 Text 프로퍼티에 저장됨 -> 적절할 때, 이용가능하게 됨
- IsEditable과 IsReadOnly 프로퍼티를 통해 기능 조절, 기본값은 false
- StaysOpenOnEdit 프로퍼티는 사용자가 선택박스 클릭해서 펼침 목록이 열린 상태에서만 true값을 가짐

**콤보박스의 IsEditable과 IsReadOnly프로퍼티의 차이점**

| IsEditable | IsReadOnly | 의미                                                         |
| ---------- | ---------- | ------------------------------------------------------------ |
| false      | false      | 선택 상자는 선택된 항목을 보여주긴 함, 그러나 어떤 텍스트도 입력 불가 |
| false      | true       | 위와 동일                                                    |
| true       | false      | 선택 상자는 선택된 항목을 텍스트로 보여줌, 임의의 텍스트를 입력 가능함 |
| true       | true       | 선택된 항목의 텍스트를 보여주고 어떤 텍스트도 입력 불가능    |


```XAML
<ComboBox>
        <StackPanel Orientation="Horizontal" Margin="5">
            <Image Source="CurtainCall.PNG"/>
            <StackPanel Width="200">
                <TextBlock Margin="5,0" FontSize="14" FontWeight="Bold"
                           VerticalAlignment="center">Curtain Call</TextBlock>
                <TextBlock Margin="5" TextWrapping="Wrap" VerticalAlignment="center">
                    Whimsical, with a red curtain background that represents a stage.
                </TextBlock>
            </StackPanel>
        </StackPanel>
        <StackPanel Orientation="Horizontal" Margin="5">
            <Image Source="firework.PNG"/>
            <StackPanel Width="200">
                <TextBlock Margin="5,0" FontSize="14" FontWeight="Bold"
                           VerticalAlignment="center">Fireworks</TextBlock>
                <TextBlock Margin="5" VerticalAlignment="center" TextWrapping="Wrap">
                    Sleek, with a black sky containing fireworks. When you need to 
                    celebrate PowerPoint-style, this design is for you!
                </TextBlock>
            </StackPanel>     
        </StackPanel>
    </ComboBox>
```
- 선택 상자에서 System.Windows.Controls.StackPanel이라는 타입 이름이 아이템의 이름으로 사용되는 것이 어색함
    -> TextSearch클래스를 이용해서 이름 변경 가능함
- TextSearch 클래스는 편집 가능한 선택 상자에 나타나는 텍스트를 쉽게 조절가능한 두 개의 첨부 프로퍼티를 정의하고 있음
- TextSearch의 Text첨부 프로퍼티는 융통성이 있음.
- TextSearch.Text에 원하는 텍스트를 추가하기만 하면 됨
- TextSearch.TextPath를 사용하면서 동시에 각 아이템마다 TextSearch.Text를 사용 가능함

**콤보박스 아이템**
- 콤보박스는 암시적으로 콤보박스아이템 객체에 아이템을 배치함
- 각 아이템의 비주얼 트리를 살펴보려 한다면 프로그래밍 코드를 작성해야 함
- TextSearch.Text 첨부 프로퍼티를 사용한다면 스택 패널이 가장 바깥쪽 엘리먼트가 아님 
  -> 그 프로퍼티를 콤보박스아이템 엘리먼트로 옮길 필요가 있음.
- 엘리먼트의 구조가 변경되므로 TextSearch.TextPath의 값을 Content.Children[1].Children[0].Text로 변경해야 함

**리스트박스**
- 콤보박스만큼 친숙한 리스트박스는 아이템들이 보여지는 방식만 다름, 거의 유사한 컨트롤
- 기본적으로 모든 아이템 보여줌, 아이템이 많을 경우 스크롤이 생김
- 리스트박스의 가장 중요한 특징: SelectionMode 프로퍼티를 통해 동시에 복수의 선택이 가능하다
    - SelectionMode 열거형을 통해 세 가지 상태의 값을 가짐
    - Single(기본값): 콤보박스처럼 한 번에 하나만 선택 가능함.
    - Multiple: 동시에 여러 아이템을 선택 가능함. 선택되지 않은 항목 클릭 시, 리스트박스의 SelectedItems 컬렉션에 추가됨
    - Extended: 다수의 아이템을 선택할 수 있지만, 하나를 선택하는 경우에 적합함.
- 리스트박스도 리스트박스아이템 클래스를 가지고 있음
- 콤보박스아이템은 리스트박스아이템에서 파생됨, IsSelected 프로퍼티와 Selected/Unselected 이벤트를 가지고 있음 
  -> 유용하게 사용 가능함

**리스트뷰**
- 리스트박스에서 파생한 리스트뷰 컨트롤은 SelectionMode 프로퍼티의 기본값이 Extended라는 것을 제외하면 리스트박스와 다른 점이 없음
- 리스트뷰는 뷰 프로퍼티를 추가 -> 사용자 지정 ItemsPanel을 선택하는 것보다 더 다양한 방법으로 표현할 수 있게 해줌
- 뷰 프로퍼티: ViewBase타입 - ViewBase는 추상 클래스
- WPF는 그리드뷰라는 컨트롤을 갖고 있음 (원래 WPF의 배타판에서의 이름이 DetailsView 였음)

```XAML
<ListView  xmlns:sys="clr-namespace:System;assembly=mscorlib">
        <ListView.View>
            <GridView>
                <GridViewColumn Header="Date"/>
                <GridViewColumn Header="Day of Week"
                                DisplayMemberBinding="{Binding DayOfWeek}"/>
                <GridViewColumn Header="Year" DisplayMemberBinding="{Binding Year}"/>
            </GridView>
        </ListView.View>
        <sys:DateTime>1/1/2007</sys:DateTime>
        <sys:DateTime>1/2/2007</sys:DateTime>
        <sys:DateTime>1/3/2007</sys:DateTime>
    </ListView>
```
![](cap6.PNG)

- 리스트뷰를 만드는 XAML코드. mscorlib.dll에 있는 System네임 스페이스를 사용 -> sys라는 접두사를 붙이기로 함
- 그리드뷰는 컬럼의 헤더를 조정가능한 프로퍼티뿐만 아니라 GridViewColumn객체의 컬렉션 정보를 가고 있는 Columns 라는 컨텐트 프로퍼티를 가지고 있음. WPF에는 리스트박스아이템에서 파생한 리스트뷰아이템을 정의하고 있음 
- 그리드뷰는 윈도우 탐색기의 '자세히'보다 더 훌륭한 기능을 자동으로 제공함
  - 컬럼을 드래그&드롭으로 순서 바꿔서 배치 가능
  - 컬럼 구분자를 드래그해서 크기 조절 가능
  - 컬럼 구분자를 더블클릭 시 내용에 맞게 자동으로 크기 조절 가능
- 컬럼의 헤더 클릭 시 자동으로 정렬하는 기능이 없음
- SortDescriptions 프로퍼티 사용 시 컬럼 헤더 클릭해서 아이템 정렬하는 것도 복잡하지 않음
- 컬럼이 어떤 아이템을 기준으로 어떻게 정렬되었는가는 작은 화살표를 수동으로 만들어야 함

**탭컨트롤**
- 여러 페이지 사이를 전환하는 데 유용함
- 탭컨트롤 사이 탭들은 보통 상단에 놓임, Dock타입인 TabScripPlacement 프로퍼티 이용 시 사방으로 배치 가능
- 아이템을 더할수록 분리된 탭이 늘어남
- 내부적으로 탭아이템을 사용함, 지원하지 않는 아이템을 탭컨트롤에 추가하려면 탭아이템으로 명시적으로 처리해야 함
```XAML
<TabControl>
    <TextBlock>Content for Tab 1.</TextBlock>
    <TextBlock>Content for Tab 2.</TextBlock>
    <TextBlock>Content for Tab 3.</TextBlock>
</TabControl>
```
![](cap7.PNG)

### 메뉴
- 흔히 접하는 메뉴와 컨텍스트메뉴를 내장하고 있음
- 메뉴 컨트롤: 다른 컨트롤보다 더 특별할 것도 없는 단지 아이템들을 계층적인 구조로 보일 수 있게 설계된 아이템즈 컨트롤의 일종
              가장 일반적으로 접하는 컨트롤

**메뉴 컨트롤**
- 회색바탕에 아이템들을 수평으로 배치한 컨트롤
- ItemsControl에서 상속받은 프로퍼티에 IsMainMenu라는 한 개의 프로퍼티만을 더 추가함
- 메뉴 컨트롤은 다른 아이템즈 컨트롤과 동일하게 어떤 객체도 아이템으로 사용 가능, 그러나 메뉴아이템(MenuItem)과 세퍼레이터(Separator) 엘리먼트를 사용함.

```XAML
<Menu>
        <MenuItem Header="_File">
            <MenuItem Header="_New..."/>
            <MenuItem Header="_Open..."/>
            <Separator/>
            <MenuItem Header="Sen_d To">
                <MenuItem Header="Mail Recipient"/>
                <MenuItem Header="My Documents"/>
            </MenuItem>
            <MenuItem Header="_Edit">
                ...
            </MenuItem>
            <MenuItem Header="_View">
                ...
            </MenuItem>
        </MenuItem>
</Menu>
```


**컨텍스트 메뉴**

### 다른 아이템즈 컨트롤
## 범위 컨트롤
### 프로그레스바
### 슬라이더
## 텍스트 및 잉크 컨트롤
### 리치텍스트박스
### 패스워드박스
### 잉크캔버스
## 결론
