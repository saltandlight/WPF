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

## 랩패널
## 도크패널
## 그리드
## 기초 패널들
## 컨텐트 오버플로 처리하기
## 종합예제:비주얼 스튜디오 스타일의 창을 만들어보기
## 결론
## 표준 윈도우즈 응용 프로그램
