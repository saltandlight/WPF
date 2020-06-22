# Viewbox의 특성
- 아주 큰 Width와 Height를 지정해도 윈도우 내에서 잘 조절되어 나타남
- 좌표계를 알아서 바꿔준다
- ArrangeOverride라는 함수가 Viewbox의 자식들을 화면에 알맞게 Width와 Height를 조정해주는 역할을 함
- `protected override Size ArrangeOverride(Size arrangeSize);`
- 신의 선물이라고도 불리는 Control
