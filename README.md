## 개요

1. 게임 이름 : Running Forever
2. 게임 장르 : 플랫포머, 러닝 게임
3. 레퍼런스 게임 : 쿠키런
4. 개획 계발 링크 : https://codingloading.tistory.com/66
                   - 비밀번호 : KGA

## 게임 진행 방식

#### 1. 게임 시작

![게임시작](https://github.com/user-attachments/assets/b177afd1-eb05-41f6-add4-8c882e1f17a2)

- 게임 시작 화면에서 아무 키나 입력하면 게임이 시작된다.
- 게임 시작 화면에서는 지난 게임의 최고 점수가 보여진다.

#### 2. 게임 진행

  ![게임 화면](https://github.com/user-attachments/assets/b1f165ce-30d5-49b9-bbef-0f662b377b43)
  ![image](https://github.com/user-attachments/assets/97d44b24-05a5-40da-80a3-405f70415e64)

- 게임 진행 화면에서는 플레이어, 체력게이지, 최고 점수, 현재 점수와 각종 장애물과 아이템이 보여진다.
- 플레이어는 점프를 사용하여 점수 아이템과 체력 회복 아이템을 먹으며 게임을 진행한다.
- 플레이어의 이동키
    + Space : 1단/2단 점프
    + Shift : 슬라이딩
    + ESC : 게임 일시정지
- 장애물과 충돌 시 플레이어가 깜빡거리는 효과가 발생한다.
- 낭떨어지에서 추락 시 플레이어가 공중에 일시적으로 띄워지며, 투명한 막이 생긴 동안 무적상태가 지속된다.
  ![image](https://github.com/user-attachments/assets/3e1ea708-752d-4220-891d-218704c04441)
  ![image](https://github.com/user-attachments/assets/6df9ef77-16f7-4572-8a2e-a85aaa43b30a)

- 게임을 진행하여 현재 점수가 올라갈수록 게임의 속도가 증가한다.

#### 3. 게임 일시정지

![image](https://github.com/user-attachments/assets/d99835c2-8d6d-4b85-8840-7c8422abfe20)

- 게임 진행화면에서 ESC키를 눌러서 게임을 일시정지 할 수 있다.
- 아무 키나 입력하면 게임 플레이 화면으로 돌아갈 수 있다.

#### 4. 게임 종료

![image](https://github.com/user-attachments/assets/02e84743-59d7-4f26-80ee-f51ad924f80e)

- 체력게이지가 0이 되면 플레이어의 애니메이션이 변하고 게임이 종료된다.
- R을 누르면 재시작이 가능하다.
- ESC를 누르면 게임이 완전히 종료된다.

