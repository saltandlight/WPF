# DataGrid ☺️
- 데이터들을 행과 열로 보여줌
- 테이블이나 표와 같음
- ItemsSource를 이용하여 담을 데이터의 묶음을 정할 수 있음
- 행과 열을 가상화해서 만들기 떄문에 보여지는 영역만 실질적인 값이고 보여지지 않는 영역의 Row나 Column들은 가짜!
    - 그래서 ItemContainerGenerator.ContainerFromItem을 이용해서 Row를 꺼낼 때 null이 될 수 있음!
