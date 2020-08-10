# Behavior
- '행동'이라는 의미
- WPF에서 컨트롤에 어떤 행동이나 기능이 작동하게 하도록 함
- 기본적인 Width, Background 프로퍼티 설정들은 컴파일러가 내부적으로 Behavior를 처리하는 것임

## Attached Behavior
- 말 그대로 '붙이는 Behavior'
- 이것을 통해 컨트롤에 이벤트를 걸 수 있고, 어떤 컨트롤에 해당 이벤트가 발생 시 어떤 동작을 하도록 함
- WPF는 MVVM을 사용하고, MVVM 패턴은 사용자에게 보여지는 뷰와 로직을 분리하므로 이런 동작을 추가할 시에 설계자는 뷰모델이 Behavior를 몰랐으면 좋겠다는 생각을 함(이 생각이 기본 바탕에 깔려있음)
- 이런 동작과 View, ViewModel을 분리.

## Blended Behavior