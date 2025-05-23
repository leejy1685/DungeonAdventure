# Block Lock

## 목차
|목차|
|:---:|
|[프로젝트 목적](#프로젝트-목적) |
|[게임 설명](#게임-설명) |
|[기본 조작법](#기본-조작법) |
|[주요 기능](#주요-기능)|
|[기술 스택](#기술-스택)|
|[기능 설명](#기능-설명)|

## 🧭프로젝트 목적
내일배움캠프의 강의에서 배운 것과 3D 기본제공 애셋을 메인으로 활용하여</br>
PC 게임 'Only UP' 을 모티브로 </br>
유니티 새 프로젝트에서부터 완성된 3D 게임 만들어 보기

## 📗게임 설명
_[ 프로젝트 명 - **Block Lock** ]_

![image](https://github.com/user-attachments/assets/cc24eb9a-96a9-4d01-bcc2-a60476976173)

- 주인공은 블록으로 된 세상(감옥)에 갇혀있다. 이 곳을 탈출하기 위해서 주인공은 올라야 한다.
</br>

#### ◇ 캐릭터 이동/점프/달리기
캐릭터는 W/A/S/D 키로 이동할 수 있습니다.</br>
캐릭터는 Space 키로 점프 할 수 있습니다.</br>
캐릭터는 Shift 키로 달리기 할 수 있습니다.</br>

#### ◇ 아이템 정보 표시 기능
카메라 중앙으로 아이템을 보면 아이템의 사용 방법과 아이템의 효과가 표시됩니다. </br>

#### ◇ 아이템 상호작용 기능
E키로 아이템과 상호작용하여 아이템을 사용 또는 장착 할 수 있습니다. </br>
가득 차면 자동으로 게이지를 소모하며 일정 시간 공격속도가 2배 증가합니다.

#### ◇ 다양한 아이템 기능
![image](https://github.com/user-attachments/assets/78fc5c22-b7de-405e-8f57-27a65cf993fd)<br>
![image](https://github.com/user-attachments/assets/a6d37ce3-db5b-4d3c-bf9c-c0a8f5fd4535)<br>
![image](https://github.com/user-attachments/assets/d0cd8dcf-798e-4035-bb1c-424c33984552)<br>
![image](https://github.com/user-attachments/assets/3e8e6682-0ab8-4d1e-9931-07b4fbff18e1)<br>
![image](https://github.com/user-attachments/assets/e0a9048f-386f-40ad-95f2-8e74be4952fa)<br>
![image](https://github.com/user-attachments/assets/d5607475-08e3-4d3b-9437-4723a84d1e62)<br>

보라색 블록을 밟으면 아이템이 생성 됩니다. <br>
해당 아이템에 빨간 점에 대고 E키를 누르면 아이템의 기능을 사용 가능합니다. <br>
장비 아이템은 하나 만 장착 가능하며, 이미 껴져있는 상태 일 때 장착되어 있는 아이템을 버리고, 상호작용한 아이템으로 교체합니다.

#### ◇ 다양한 블록 기능
![image](https://github.com/user-attachments/assets/5ca98302-f249-41d1-a14b-a8f10a82a302)<br>

- 아이템 생성 블록 입니다.
- 위에서 밟으면 아이템이 생성 됩니다.(아이템 설정 가능)

![image](https://github.com/user-attachments/assets/bb314d00-2328-4aea-9b9c-21f97843948e)<br>

- 점프대 블록 입니다.
- 밝으면 위로 날라갑니다. (날리는 파워 설정 가능)

![image](https://github.com/user-attachments/assets/c35dda0f-2c8b-47a4-88fe-4c921a880e0b)<br>

- 오르기 블록 입니다.
- 캐릭터가 오를 수 있습니다.

![image](https://github.com/user-attachments/assets/9d3c7276-c42f-482b-ad30-bcf964164f0e)<br>

- 함정 블록입니다. 
- 전방으로(+z축 방향)으로 보이지 않는 ray를 쏴서 유저를 맞추면 일시적으로 화면을 가리는 효과보입니다.

![image](https://github.com/user-attachments/assets/dafd7f42-bd78-437c-ba8a-70e53dfc9c9a)

- 움직이는 블록입니다.
- X,Y,Z 축, 이동 거리, 이동 속도 등을 설정해서 배치 할 수 있습니다.

  
## 🕹️기본 조작법
앞 - W </br>
뒤 - S </br>
좌 - A </br>
우 - D </br>
점프 - Space</br>
달리기 - Shift</br>
카메라 전환 - Tab</br>
아이템 상호작용 - E</br>
</br>


## 📽️주요 기능
- W/A/S/D 를 통한 캐릭터 이동
- 마우스 이동에 따른 카메라 시점 변환
- Space 키 : 점프 기능
- Shift 키 : 달리기 기능 
- Tab 키 : 카메라 시점 변환 기능(1인칭/3인칭)
- E 키 : 아이템 상호 작용 기능
- 점프대, 움직이는 블록, 오를 수 있는 블록, 함정 블록, 아이템 소환 블록 기능
- 소모, 장비 아이템 기능

## 🛠️기술 스택
### 개발 환경
- Unity 2022.3.17f1
- Windows 10 & 11

### 리소스
- 내일배움캠프 제공
- Kenney 무료 에셋
- Unity 무료 에셋
   

### 플레이 해보기
https://leejy1685.github.io/DungeonAdventure/

