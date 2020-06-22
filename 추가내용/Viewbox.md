# Viewbox의 특성
- 아주 큰 Width와 Height를 지정해도 윈도우 내에서 잘 조절되어 나타남
- 좌표계를 알아서 바꿔준다
- ArrangeOverride라는 함수가 Viewbox의 자식들을 화면에 알맞게 Width와 Height를 조정해주는 역할을 함
```C#
    //
    // 요약:
    //     Arranges the content of a System.Windows.Controls.Viewbox element.
    //
    // 매개 변수:
    //   arrangeSize:
    //     The System.Windows.Size this element uses to arrange its child elements.
    //
    // 반환 값:
    //     System.Windows.Size that represents the arranged size of this System.Windows.Controls.Viewbox
    //     element and its child elements.
    protected override Size ArrangeOverride(Size arrangeSize);
```
- 신의 선물이라고도 불리는 Control