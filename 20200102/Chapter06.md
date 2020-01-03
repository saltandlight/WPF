# Chapter06. 패널을 이용한 화면배치
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
| 프로퍼티명                              | 랩패널 안에서의 역할                                         |
| --------------------------------------- | ------------------------------------------------------------ |
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

## 기초 패널들
## 컨텐트 오버플로 처리하기
## 종합예제:비주얼 스튜디오 스타일의 창을 만들어보기
## 결론
## 표준 윈도우즈 응용 프로그램
