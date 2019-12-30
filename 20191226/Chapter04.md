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
<StackPannel>
    <RadioButton>Option 1</RadioButton>
    <RadioButton>Option 2</RadioButton>
    <RadioButton>Option 3</RadioButton>
</StackPannel>
```

- 라디오 버튼을 사용자가 지정하는 방식을 사용해서 그룹화하려면,
  그룹네임 프로퍼티에 동일한 이름을 설정하면 됨 
- 로지컬 트리의 루트 엘리먼트 아래 있고, 그룹 이름이 동일하면 부모 엘리먼트가 달라도
  어디에 있든지 동일한 그룹으로 묶임

```XAML
<StackPannel>
 <StackPannel>
    <RadioButton GroupName="A">Option 1</RadioButton>
    <RadioButton GroupName="A">Option 2</RadioButton>
 </StackPannel>
 <StackPannel>
    <RadioButton GroupName="A">Option 3</RadioButton>
 </StackPannel>
</StackPannel>
```
- 동일한 부모 엘리먼트에서 다른 그룹으로 묶을 수 있음
```XAML
<StackPannel>
    <RadioButton GroupName="A">Option 1</RadioButton>
    <RadioButton GroupName="A">Option 2</RadioButton>
    <RadioButton GroupName="B">A Different Option 1</RadioButton>
    <RadioButton GroupName="B">A Different Option 2</RadioButton>
</StackPannel>
```
### 단순 컨테이너
### 헤더를 가진 컨테이너
## 아이템즈 컨트롤
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
