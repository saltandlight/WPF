# Chapter06. 패널을 이용한 화면배치🏇
## 캔버스
- 가장 기본적인 패널
- 명시적인 좌표값을 이용해서 엘리먼트의 위치를 결정하는 개념만 지원
- 장치 독립적인 픽셀을 사용함 -> 사방을 자신의 좌표값으로 사용 가능함
- Left/Top/Right/Bottom 첨부 프로퍼티를 갖고 있음, 엘리먼트의 위치를 조정 가능함
```XAML
    <Canvas>
        <Button Background="Red">Left=0, Top=0</Button>
        <Button Canvas.Left="18" Canvas.Top="18"
                Background="Orange">Left=18, Top=18</Button>
        <Button Canvas.Right="18" Canvas.Bottom="18"
                Background="Yellow">Right=18, Bottom=18</Button>
        <Button Canvas.Right="0" Canvas.Bottom="0"
                Background="Lime">Right=0, Bottom=0</Button>
        <Button Canvas.Right="0" Canvas.Top="0"
                Background="Aqua">Left=0, Top=0</Button>
        <Button Canvas.Left="0" Canvas.Bottom="0"
                Background="Magenta">Left=0, Bottom=0</Button>
    </Canvas>
```
![](cap1.PNG)

**자식 간의 화면배치 프로퍼티와 캔버스의 상호작용**

| 프로퍼티명                              | 캔버스 안에서의 역할 |
| --------------------------------------- | -------------------- |
| Margin                                  | 부분 반영. 엘리먼트의 위치를 잡는 데 사용된 두 변에서는 네 개의 마진 중 적당한 두 값이 첨부 프로퍼티 값으로 추가됨            |
| HorizontalAlignment와 VerticalAlignment | 미반영. 엘리먼트들은 자신에게 필요한 공간만 정확히 주어짐                    |
| LayoutTransform                         | 모두 반영. LayoutTransform일 때 엘리먼트들은 항상 캔버스의 선택된 코너와 일정한 거리만큼 떨어져 렌더링됨. RenderTransform과는 차이가 있음                     |

## 스택패널
- 이용하기에 간편하고 유용한 패널
- 먼저 추가된 것이 아래에 있음(스택이자너...!)
- 자신만의 첨부 프로퍼티를 정의하지 않음
- 자식 엘리먼트를 배열하는 데 사용하는 첨부 프로퍼티는 없음
- 오직 오리엔테이션 프로퍼티를 사용해서 조정함
- 이 프로퍼티는 System.Windows.Controls.Orientation타입임. Horizontal이나 Vertical 중 하나를 설정 가능(기본값은 Vertical)

**자식 엘리먼트의 화면배치 프로퍼티와 스택패널의 상호작용**

| 프로퍼티명                              | 스택패널 안에서의 역할                                         |
| --------------------------------------- | ------------------------------------------------------------ |
| Margin                                  | 반영. 마진은 엘리먼트 사이의 공간뿐만 아니라 엘리먼트와 스택패널의 경계 사이도 조정함. |
| HorizontalAlignment와 VerticalAlignment | 부분반영. 정렬이 엘리먼트가 누적되는 방향이면 자식 엘리먼트들 자신이 필요한 공간만 정확하게 차지함 -> 무시됨. 오리엔테이션이 우선순위가 높으므로 오리엔테이션에 따라 결정됨 |
| LayoutTransform                         | 반영. LayoutTransform일 때 스택에 남아있는 엘리먼트들은 자신이 필요한 공간을 만들기 위해 밀고 나옴 -> RenderTransform일 떄와는 차이가 있음. 또한, Stretch와 만나서 RotateTransform이나 SkewTransform 형태변형이 일어나면  90도의 배수(90,180,270)에서만 엘리먼트들이 늘어남. |

- 패널 안의 엘리먼트가 늘어나는 경우: 다른 엘리먼트와 평행하거나 수직일 때만 일어남, 엘리먼트는 한 방향으로만 늘어남
- LayoutTransform일 때만 일어나고 RenderTransform일 때는 일어나지 않음

## 랩패널
- 스택패널과 기본적으로 유사함
- 추가되는 엘리먼트마다 충분한 공간이 없을 때는 누적되는 방향으로 행이나 열을 바꿔서 래핑함
- 엘리먼트의 위치를 조정하는 첨부 프로퍼티는 없음. 

**화면배치 과정을 돕는 세 개의 프로퍼티**
- Orientation: 기본 값이 Horizontal이라는 것을 제외하고는 스택패널과 동일함. 엘리먼트는 좌측에서 우측으로 배치, 잘릴 것 같으면 상단에서 하단으로 래핑되어 줄바꿈함.

- ItemHeight: 자식 엘리먼트를 배치하기 위한 공통의 높이

- ItemWidth: 자식 엘리먼트를 배치하기 위한 공통의 폭
- ItemHeight 와 IteWidth는 값이 정해지지 않거나 Double.NaN을 가짐
- 오리엔테이션이 Vertical이면 래패널은 각 컬럼 중 가장 넓은 엘리먼트의 폭을 기준으로 ItemWidth의 값을 결정
- 오리엔테이션이 Horizontal이면 가장 높이가 큰 엘리먼트를 기준으로 ItemHeight의 값을 결정함
- 결과적으로 랩패널의 내부에서는 어떤 잘림도 없음

**자식 엘리먼트의 화면배치 관련 프로퍼티와 랩패널의 상호작용**

| 프로퍼티명                              | 랩패널 안에서의 역할                                              |
| --------------------------------------- | ----------------------------------------------------------------- |
| Margin                                  | 반영. 마진은 랩패널이 누적되는 아이템마다 폭과 높이를 결정하는 계산과정에 포함됨. |
| HorizontalAlignment와 VerticalAlignment | 부분반영. 정렬은 스택패널처럼 누적되는 방향이 반대 방향일 떄만 사용됨. 랩패널의 ItemHeight나 ItemWidth가 정렬할 만한 여유공간을 엘리먼트에게 줄 수 있다면 누적되는 방향에서도 유용하게 사용 가능함 |
| LayoutTransform                         | 반영. LayoutTransform일 때 오리엔테이션에 의존하는 랩패널의 ItemHeight나 ItemWidth가 아직 설정이 안 되어 있을 때, 남아있는 엘리먼트들은 공간을 만들고 밀고 나옴 -> RenderTransform일 때와는 결과가 다름 |

## 도크패널
- 사방으로 엘리먼트가 쉽게 도킹할 수 있게 함 -> 전체 높이와 폭을 채울 수 있음
- 도크패널은 System.Windows.Controls.Dock 타입의 Dock 첨부 프로퍼티를 정의하고 있음
        - Left/Top/Right/Bottom(default:Left) 중 한 개 이상을 사용해서 자식 엘리먼트의 도킹을 조정 가능함
- 마지막 엘리먼트의 LastChildFill 프로퍼티가 false로 설정되지 않으면 나머지 여분의 공간을 다 채울 수 있음.
- LastChildFill이 기본값인 true로 설정되면 마지막으로 추가되는 자식 엘리먼트의 Dock 프로퍼티는 무시됨
- 반대로 false로 설정되면 값이 명시적으로 설정되지 않는 한 기본적으로 Left가 적용되어 도킹됨
- 엘리먼트들이 늘어나는 이유: HorizontalAlignment나 VerticalAlignment의 기본값이 Stretch이기 때문임.
- 도크패널은 윈도우나 페이지 엘리먼트의 최상위 UI를 배열하는 데 유용함.
- 개별 자식 엘리먼트가 도킹된 경계의 나머지 공간을 차지함 -> 추가되는 자식의 순서는 매우 중요함
```XAML
<DockPanel>
        <Button DockPanel.Dock="Top" Background="Red">1 (Top)</Button>
        <Button DockPanel.Dock="Left" Background="Orange">2 (Left)</Button>
        <Button DockPanel.Dock="Right" Background="Yellow">3 (Right)</Button>
        <Button DockPanel.Dock="Bottom" Background="Lime">4 (Bottom)</Button>
        <Button Background="Aqua">5</Button>
</DockPanel>
```
- 도크패널은 제한 없이 자식 엘리먼트를 가질 수 있음
![](cap16.PNG)
- 도크패널의 기능은 스택패널의 기능을 확장한 것
- LastChildFill 프로퍼티가 false일 때, 모든 자식 엘리먼트가 좌측으로 도킹되어 있으면 수평방향의 스택패널처럼 동
    - 모든 자식 엘리먼트가 상단에 도킹되어 있으면 수직 방향의 스택패널처럼 동작
| 프로퍼티명                              | 도크패널 안에서의 역할                                       |
| --------------------------------------- | ------------------------------------------------------------ |
| Margin                                  | 반영. 마진은 엘리먼트 사이 공간뿐만 아니라 엘리먼트와 도크패널의 경계도 조절함 |
| HorizontalAlignment와 VerticalAlignment | 부분반영. 스택패널처럼, 정렬은 도킹되는 방향일 때는 무시됨. 도킹되는 방향이 Left나 Right일 때, HorizontalAlignment가 의미 없음. Top이나 Bottom일 때는 VerticalAlignment가 의미 없음 |
| LayoutTransform                         | 반영. LayoutTransform으로 동작할 때, 남아있는 엘리먼트들은 여분의 공간을 만들기 위해 밀고 나옴 -> RenderTransform일 때와는 다름. LayoutTransform일 때 Stretch로 설정된 도크패널에서 RotateTransform이나 SkewTransform 형태변형이 일어나면 90의 배수일 때만 엘리먼트가 늘어남 |

## 그리드
- 그리드: 여러가지 기능을 복합적으로 가진 패널(가장 자주 사용됨)
- 랩패널처럼 엘리먼트를 래핑해서 처리하지 않음
- 여러 행과 열로 자식 엘리먼트를 배치 가능하게 해줌. 
- HTML의 테이블과 아주 유사함
```XAML
<Grid Background="LightCyan">
        <!--네 개의 행을 선언-->
        <Grid.RowDefinitions>
            <RowDefinition/>
            <RowDefinition/>
            <RowDefinition/>
            <RowDefinition/>
        </Grid.RowDefinitions>

        <!--두 개의 열을 선언-->
        <Grid.ColumnDefinitions>
            <ColumnDefinition/>
            <ColumnDefinition/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>

        <!--자식 엘리먼트를 배치-->
        <Label Grid.Row="0" Grid.Column="0" Background="LightBlue" Foreground="White"
               HorizontalAlignment="Center">Start Page</Label>
        <GroupBox Grid.Row="1" Grid.Column="0" Background="White" 
               Header="Recent Projects">...</GroupBox>
        <GroupBox Grid.Row="2" Grid.Column="0" Background="White"
               Header="Getting Started">...</GroupBox>
        <GroupBox Grid.Row="3" Grid.Column="0" Background="White"
                  Header="Headlines">...</GroupBox>
        <GroupBox Grid.Row="1" Grid.Column="1" Background="White"
                  Header="Online Articles">
            <ListBox>
                <ListBoxItem>Article #1</ListBoxItem>
                <ListBoxItem>Article #2</ListBoxItem>
                <ListBoxItem>Article #3</ListBoxItem>
                <ListBoxItem>Article #4</ListBoxItem>
            </ListBox>
        </GroupBox>
    </Grid>
```
![](cap3.PNG)
- 그리드의 셀들은 비어있을 수도 있고 여러 엘리먼트가 한 셀 안에 배치되어 있을 수도 있음
- 후자의 경우, 엘리먼트들의 렌더링되는 순서는 Z-order에 따라서 처리됨
        - Z-order: 화면들을 개별 레이어로 생각하고 내려다보면 앞에 레이어에 의해 뒤의 레이어는 보이지 않음
- 로우스팬, 컬럼스팬의 기본값=1
- columnspan과 rowspan을 준 경우
![](cap4.PNG)


- 크기자동화하려면 RowDefinition의 Height과 ColumnDefinition의  Width에 Auto를 설정하면 됨
```XAML
    <Grid Background="LightBlue">
        <!--네 개의 행을 선언-->
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition/>
            <RowDefinition/>
            <RowDefinition/>
        </Grid.RowDefinitions>

        <!--두 개의 열을 선언-->
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>

        <!--자식 엘리먼트를 배치-->
        <Label Grid.Row="0" Grid.Column="0" Background="Blue" Foreground="White" Grid.ColumnSpan="2"
               HorizontalContentAlignment="Center">Start Page</Label>
        <GroupBox Grid.Row="1" Grid.Column="0" Background="White" 
               Header="Recent Projects">...</GroupBox>
        <GroupBox Grid.Row="2" Grid.Column="0" Background="White"
               Header="Getting Started">...</GroupBox>
        <GroupBox Grid.Row="3" Grid.Column="0" Background="White"
                  Header="Headlines">...</GroupBox>
        <GroupBox Grid.Row="1" Grid.Column="1" Background="White" Grid.RowSpan="3"
                  Header="Online Articles">
            <ListBox>
                <ListBoxItem>Article #1</ListBoxItem>
                <ListBoxItem>Article #2</ListBoxItem>
                <ListBoxItem>Article #3</ListBoxItem>
                <ListBoxItem>Article #4</ListBoxItem>
            </ListBox>
        </GroupBox>
    </Grid>
```
![](cap2.PNG)

### 행과 열의 크기 조절
- FrameworkElement 클래스의 Height, Width는 double 타입, 기본 값은 double.NaN
- RowDefinition의 Height나 ColumnDefinition의 Width 프로퍼티는 System.Windows.GridLength타입, 기본값은 Auto나 Double.NaN이 아님
- 그리드는 RowDefinition과 ColumnDefinition의 크기를 조절하기 위해 세 가지 방법 사용함
       - 절대 크기: RowDefinition에 Height나 ColumnDefinition에 Width의 값을 숫자로 설정함 = 장치 독립적인 픽셀을 사용한다는 의미
       - 자동 크기: Height나 Width가 Auto로 설정되면 WPF에서 기본값이 설정된 것처럼 자식 엘리먼트들은 필요한 공간만 허용됨,  행은 엘리먼트 중 가장 높이가 큰 값 설정됨, 열은 엘리먼트 중 가장 넓은 값이 설정됨
       - 비율 기반 크기: 이용 가능한 공간을 동일한 비율이나 크기로 나누는 특별한 방법, 비율로 지정된 행이나 열은 그리드의 크기가 변하면 그에 따라 줄어들거나 늘어남

- 절대 크기나 자동 크기는 직관적, 특별한 값( `*`) 사용하는 비율 기반 방식은 설명이 더 필요함
      - 하나의 열, 행을 `*`로 설정 시, 남아있는 공간 채움
      - 복수 행 또는 열을 `*`로 설정 시, 남아있는 공간은 행이나 열의 개수로 동일하게 나눈 값을 가짐
      - 행이나 열이 더 많은 공간을 가지려면  `*`를 사용하는 것보다  `2*`또는 `5.5*`처럼 계수 사용 가능 

### GridSplitter를 이용해서 상호작용하는 크기 조절
- 그리드의 매력적인 특징: 마우스나 키보드 또는 스타일러스 사용 시 행이나 열과 상호작용하는 크기 조절을 지원하는 것
- 주로 이런 기능을 GridSplitter 클래스를 이용하여 처리함
- GridSplitter는 HorizontalAlignment의 기본값: Right, VerticalAlignment의 기본값은 Stretch
  -> 기본적으로 특정한 셀의 오른쪽에 도킹됨
- GridSplitter의 올바른 사용: 적어도 한 방향으로 Stretch(부모의 폭이나 높이를 가득 채움) 정렬을 함
- Gridsplitter의 정렬 관련 프로퍼티 이용해서 크기 조절, 방향의 모든 형태를 조절가능하지만 이 경우 사용 가능한 명시적이고 독립적인 두 개의 프로퍼티를 갖고 있음
  - GridResizeDirection 타입의 ResizeDirection(기본값:Auto)과 GridResizeBehavior타입의 ResizeBehavior
  
### 행과 열 크기 공유하기
- RowDefinitions의 ColumnDefinitions는 SharedSizeGroup이라는 프로퍼티 가짐
- GridSplitter처럼 런타임 시 엘리먼트 중 일부의 길이가 변경되어도 여러 엘리먼트 상호 간에 동일한 길이를 가질 수 있게 해줌
```XAML
<Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition/>
            <ColumnDefinition/>
        </Grid.ColumnDefinitions>

        <!--자식 엘리먼트를 배치-->
        <Label Grid.Column="0" Background="Red" 
               HorizontalContentAlignment="Center" VerticalContentAlignment="Center">1</Label>
        <GridSplitter Grid.Column="0" Width="5"/>
        <Label Grid.Column="1" Background="Orange" 
               HorizontalContentAlignment="Center" VerticalContentAlignment="Center">2</Label>
        <Label Grid.Column="2" Background="Yellow" 
               HorizontalContentAlignment="Center" VerticalContentAlignment="Center">3</Label>
    </Grid> 
```
[기본화면 배치]
![](cap5.PNG)
[GridSplitter를 우측으로 드래그한 결과]
![](cap6.PNG)

- 첫번쨰 열과 마지막 열이 동일한 SharedSizeGroup으로 묶였을 때의 결과
- SharedSizeGroup으로 묶인 모든 열들은 가장 크기가 큰 자동이나 절대 크기 값으로 초기화됨
- 첫 번째 열이 늘어나면 마지막 열도 똑같은 크기로 늘어남, 가운데 열은 `*` 로 설정됨 -> 나머지 공간 적절히 채움

[기본화면 배치]
![](cap7.PNG)
[GridSplitter를 우측으로 드래그한 결과]
![](cap8.PNG)
- GridSplitter를 우측으로 움직이면 1과 같은 비율로 3이 동시에 증가함
- IsSharedSizeScope 프로퍼티는 떨어져 있는 여러 그리드에 동일한 그룹명을 공유하려고 할 때 사용함
- 잠재적인 이름 충돌 피하고 처리하는 데 필요한 로지컬 트리의 양 줄이기 위해 같은 그룹명을 사용하는 엘림너트들은 일반저긍로 IssHaredSizeScope 프로퍼티가 true인 부모 엘리먼트에 있어야 함
- 이 프로퍼티는 Grid내에서는 의존 프로퍼티로, 다른 패널에서는 첨부 프로퍼티로 사용함

### 다른 패널들과 그리드의 비교

**그리드로 도크패널 흉내내기**

## 기초 패널들
### 탭패널
### 툴바오버플로패널
### 툴바트레이
### 유니폼그리드

## 컨텐트 오버플로 처리하기
### 클리핑
### 스크롤링
### 스케일링

## 종합예제:비주얼 스튜디오 스타일의 창을 만들어보기
## 결론
## 표준 윈도우즈 응용 프로그램