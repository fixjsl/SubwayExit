# Wahlberg(2015) — Blockades in the Metroidvania genre of games - a examination of backtracking

## 1. 서지사항

- **정식 인용**: Wahlberg, T. (2015). *Blockades in the Metroidvania genre of games: A examination of backtracking* [Bachelor's thesis, Department of Game Design]. (대학명은 원문 표지에 기재되어 있지 않음 — 아래 등급 메모 참조)
- **파일**: 서바이벌_메트로베니아/ATTACHMENT01.txt
- **종류**: **학위논문(학사)** — 표지에 "Department of Game Design / Bachelor Thesis for a major in game design, **15 hp** / Program: Game Design and Graphics"라고 명시. 날짜 표기는 표지에 "**21, 09, 2015**".
  - 지도교수(Supervisors): Iwona Hrynczenko, Stellan Sundh
  - 심사자(Examiner): Hayashi Masaki
  - 본문 분량: 본문 29쪽 + 문헌목록·게임목록·용어집(pp. 30–31)
- **등급 메모**:
  - ⚠ **이 문헌은 동료심사를 거친 학술 출판물이 아니라, 스웨덴의 학사 학위논문(15 hp = 15 ECTS 학점 규모)이다.** 15 hp는 한 학기의 절반에 해당하는 학점 규모로, 석사·박사 학위논문보다 훨씬 작은 과제다. 본문에도 "Bachelor Thesis for a major in game design, 15 hp"라고 표지에 적혀 있다.
  - ⚠ **대학명이 추출 텍스트 어디에도 나오지 않는다.** 표지에는 학과명("Department of Game Design")만 있고 소속 대학의 이름이 없다. 지도교수·심사자 이름으로 특정 대학을 추정할 수는 있으나, **원문에서 확인하지 못한 사항이므로 추정으로 참고문헌에 적어 넣으면 안 된다.** 참고문헌에 올리기 전 원본 PDF 표지나 리포지터리 페이지에서 대학명을 반드시 직접 확인할 것. ※ 원문에서 확인하지 못함.
  - ⚠ **핵심 용어 SOB / IOB / DOB / SID framework / key trigger는 전부 저자 본인이 이 논문에서 새로 만든 신조어다.** 저자는 초록과 서론에서 "creating new knowledge explained through **making new terminology**", "level designers do not have a common framework, terminology to work with when making Metroidvania and backtracking games and **I aim to fix that with this paper**"라고 직접 밝혔다. 즉 **메트로베니아 장르의 표준 용어가 아니며, 이 논문 바깥에서는 통용되지 않는다.**
  - ⚠ **심사에서의 약점**: 한국 대학의 학부 졸업논문이 **다른 나라의 학부 졸업논문을** 자기 핵심 용어의 **유일한 근거**로 삼는 구조는, 근거의 무게 면에서 지적받기 쉽다. 학사 학위논문은 동료심사를 받지 않고 지도교수·심사자의 학위심사만 거치기 때문이다. 따라서 **11번 문헌(Oliveira 외, 2020, SBGames Industry Track 정식논문)을 반드시 병기**해 단독 의존을 피하고, 본문에서도 "Wahlberg(2015)는 …라는 용어를 제안했다"처럼 **저자 개인의 제안임이 드러나는 서술**로 쓸 것. "메트로베니아 장르에서는 SOB/IOB/DOB로 부른다" 같은 **장르 통설인 양 쓰는 문장은 쓰면 안 된다.**
  - ⚠ **영문 문법 오류가 많은 원문이다.** 제목부터 "a examination"(정관사 오류)이며, 본문에도 비문이 잦다. 직접인용할 때 원문 그대로 옮겨야 하므로, 필요하면 `[sic]`을 붙이거나 간접인용으로 바꿔 쓰는 편이 낫다.
  - 이 논문은 11번 문헌(Oliveira 외, 2020)의 참고문헌 [16]으로 실제 인용되어 있다. **외부 인용 사례가 확인된다는 점은 근거로 쓸 수 있다.**

---

## 2. 한 줄 요약

*Super Metroid*(SNES)의 문·장애물 메커니즘을 분석해 진행 차단물의 상태를 SOB(상호작용 불가) → IOB(상호작용 가능) → DOB(소멸)의 3단계로 정식화한 **SID 프레임워크**와, 그 상태 전이를 일으키는 **key trigger**의 속성 체계를 제안하고, Construct 2로 만든 데모 8개를 10명에게 테스트해 백트래킹 레벨 디자인과 선형 레벨 디자인의 재미를 비교한 학사 학위논문이다.

---

## 3. 이 연구가 실제로 한 것

### 연구 방법

저자가 5장 "The Method"에서 밝힌 절차는 다음 3단계다.

> As a method I will start with researching the mechanics of doors/blockades and everything that hinders progression in the game Super Metroid and see how it functions and put its logic into a framework.
> I will then compare these mechanics with other games that I have chosen and is in the list of game references that can be found in the end of this paper in the game reference list. (5장, p. 6)

> I will then if successful in proving that there is a syntax use the framework that I will have developed to understand how to develop and use the door/blockade mechanics in a demo test. This demo will be built in Construct 2 and will have a common variable that changes in a static linear and backtracking level design. (5장, p. 6)

1. **프레임워크 구축**: *Super Metroid* 단 **1개 게임**의 문/장애물 메커니즘을 관찰해 SID 프레임워크와 key trigger 속성을 도출.
2. **비교 검증**: *Megaman*(Capcom, NES), *Super Mario 64*(Nintendo, N64), *Dark Souls*(From Software) **3개 게임**과 대조해 같은 구문(syntax)이 나타나는지 확인.
3. **데모 실험**: Construct 2로 만든 데모 **8개**를 **10명**에게 플레이시키고 MDA의 8 kinds of fun 중 challenge와 discovery 두 종류의 재미를 0–100점으로 평정받음.

### 분석·실험 대상과 표본 규모 (숫자 정확히)

| 항목 | 수치 |
|---|---|
| 프레임워크 도출의 근거 게임 | **1개** (*Super Metroid*, SNES) |
| 비교 검증에 쓴 게임 | **3개** (*Megaman*, *Super Mario 64*, *Dark Souls*) |
| 데모 개수 | **8개** (demo 1–8) |
| 각 데모의 길이 | **약 10,000 픽셀** |
| 피험자 수 | **10명** |
| 측정한 재미의 종류 | **2종** (challenge, discovery) |
| 평정 척도 | 0(boring) – 100(fun) |

> All demos are approximately the same amount of length all of which is of a total of 10 000 pixels long from start to finishing each demo. In all the demos there contains a power up which makes a SOB state blockade go to IOB state and then when interacted into a permanent DOB state. **I made these demos myself from scratch in a 2D engine called Construct 2 and I tested these demos on random people that I know** this to get a general result on the perception of backtracking compared to linear level design. (§6.6, p. 21)

> Here is the average rating data results in a bar chart 0 being boring to 100 being fun in challenge and discovery type of fun in the 8 demo's that was tested on **a total of 10 players**. (7장, p. 25)

### 검증 방식과 그 한계 — **반드시 반영할 것**

- ⚠ **실험 표본이 10명이고, 그 10명은 "저자가 아는 아무 사람들(random people that I know)"이다.** 모집 절차, 인구통계, 게임 경험 수준, 통계 검정 여부가 **전혀** 제시되어 있지 않다. 제시된 것은 막대그래프(Figure 21) 하나뿐이고, 그 그래프의 실제 수치도 본문에는 두 개(demo 7이 demo 4보다 challenge에서 **16.1%**, discovery에서 **10%** 높음, §7.3, p. 26)만 적혀 있다. ※ 나머지 데모별 원 수치는 그림에만 있어 추출 텍스트에서 확인하지 못함.
- ⚠ **프레임워크의 근거가 단 1개 게임이다.** SID 프레임워크와 key trigger 속성은 전부 *Super Metroid* 하나에서 뽑았고, 나머지 3개 게임은 "패턴이 보인다"는 저자의 관찰에 의한 사후 대조일 뿐 체계적 코딩이나 코더 간 신뢰도 검증이 없다.
- ⚠ **저자 본인이 밝힌 한계 (8장 Discussion, p. 28)**:

  > When looking at how Metroidvanias games backtracking design works **I think I have only scratched the surface of understanding it.**

  > **Backtracking level design is complex and has long way ahead to be fully understood scientifically** but these new design definitions of the SID framework and research of the definitions of fun is a start.

  > Developers need to make backtracking level design as understandable and graspable as possible through research that will define more terminology and theories for it.

- ⚠ **저자 본인이 밝힌 한계 (9장 Conclusion, p. 29)**:

  > **We have only unlocked the basic fundamentals of what Backtracking level design is**, how it works and why it is considered so much fun to play. This is just the beginning of us backtracking to understand why we enjoy backtracking so much **the secret of backtracking has yet to be fully unlocked.**

- ⚠ **데모 실험 결과 자체가 내부적으로 일관되지 않다.** 저자는 §7.1에서 자신의 결과가 Bleszinski·Rouse의 주장을 "contradicts"한다고 했다가 같은 문단에서 "also validates their theory"라고 쓴다. 9장에서도 "These measurements both validates and contradicts what Bleszinski and Rouse says"라고 적었다. **재미에 관한 이 논문의 결론은 우리 논문에 인용하지 말 것**(6번 참조).

---

## 4. 인용 가능한 내용

### (1) SOB — Solid Obstacle Blockade (풀네임과 정의, 원문 그대로)

본문 §6.3(p. 11)의 도입부. 잠긴 문의 상태를 수식으로 먼저 쓴 뒤 명명한다.

> Player skill means the amount of skill of which the player is at and character skill means the same but what the avatars amount of skill is at and if they both needs to lower or higher in terms of unlocking a blockade.
> This state I translated to the formula.
> **Player skill x + character skill y < blockade z**
> As these doors are locked the doors can be defined as to be in a solid state:
> **This definition of the blockade works as the blockade completely shuts the player of from other game spaces so for now on this solid state will be defined as a solid obstacle blockade or in short a SOB state.**
> — §6.3, p. 11

용어집(Glossary, p. 31)의 간결한 정의:

> **SOB: a blockade which cannot be interacted with at this specific time.**

한국어 간접인용 예시:
> Wahlberg(2015)는 플레이어가 아직 대응하는 열쇠를 갖지 못해 상호작용 자체가 불가능한, 다른 게임 공간을 완전히 차단하는 장애물의 상태를 solid obstacle blockade(SOB)라고 명명했다.

---

### (2) IOB — Interactive Obstacle Blockade (풀네임과 정의, 원문 그대로)

> In contrast, here in figure 5 is an example from Super Metroid where the player then gets the corresponding upgrades that opens the corresponding red, yellow and green doors then backtracking does not occur as the blockades can be triggered.
> As in the previous case, this state is converted to a formula.
> **Player skill x + character skill y >= obstacle blockade.**
> As these doors are unlockable they can be defined to be in an interactive state.
> **So for now on the interactive state will be defined as an Interactive obstacle blockade or IOB for short.**
> — §6.3, p. 11

용어집(p. 31):

> **IOB: a blockade which can be interacted with at this specific time.**

⚠ 표기 주의: 본문에서 SOB의 수식은 우변이 `blockade z`이고 IOB의 수식은 우변이 `obstacle blockade`로, **원문 자체가 표기를 통일하지 않았다.** 우리 논문에 수식을 옮길 때는 이 불일치를 그대로 베끼지 말고 인용을 피하거나 각주로 처리할 것.

---

### (3) DOB — Disappeared Obstacle Blockade (풀네임과 정의, 원문 그대로)

> When the IOB state is interacted with the player is then able to get through the blockade as it disappears. **This disappeared state will be defined as a disappeared obstacle blockade or DOB for short.**
> — §6.3, p. 11

용어집(p. 31):

> **DOB: a blockade that has disappeared either temporarily or permanently.**

한국어 간접인용 예시:
> Wahlberg(2015)는 상호작용이 이루어져 장애물이 사라진 상태를 disappeared obstacle blockade(DOB)로 정의했으며, 이 소멸은 일시적일 수도 영구적일 수도 있다고 보았다.

---

### (4) SID 프레임워크의 정의 (원문 그대로) — **우리 3.6절의 핵심 근거**

> **The figure above shows that SOB state are causing backtracking and the SOB state then goes to a IOB state when the key trigger is found and lastly goes to DOB state I define this as the SID framework.**
> — §6.3, p. 12 (Figure 5: Metroidvania backtracking framework의 설명)

용어집(p. 31):

> **SID framework: a framework that explain the dynamic changes of SOB, IOB and DOB states**

상태 간의 인과 관계를 명시한 대목(§6.3, p. 12):

> **The SOB state is the only blockade state that causes backtracking** when the player has to get through it in order to progress while **the IOB state happens after the player has gotten the necessary key trigger that the previous SOB state needs in order to become an IOB state.**

> By this definition backtracking is differentiated by two states of accessibility aimed to separate game spaces in which a SOB state describes non available spaces that becomes an IOB state when the space is available.

결론부의 요약(9장, p. 29):

> We now have a start detailing the terms Metroidvania and backtracking putting them in a framework that describes the dynamic between the interaction of player key triggers in SOB, IOB and DOB states now defined as the SID framework.

**→ 상태 전이 정리 (원문 서술을 표로 옮긴 것)**

| 상태 | 조건 | 다음 상태로 가는 계기 |
|---|---|---|
| **SOB** (Solid Obstacle Blockade) | 플레이어 스킬 + 캐릭터 스킬 < 장애물 | key trigger를 획득하면 IOB로 |
| **IOB** (Interactive Obstacle Blockade) | 플레이어 스킬 + 캐릭터 스킬 ≥ 장애물 | 상호작용이 일어나면 DOB로 |
| **DOB** (Disappeared Obstacle Blockade) | 장애물이 사라진 상태 | (일시적 소멸이면 IOB로 복귀 가능) |

⚠ DOB에서 IOB로 되돌아가는 전이는 **용어집의 "either temporarily or permanently"와 §6.4의 Dark Souls 서술**("in this case the IOB state also goes to a permanent DOB state **rather than going back to an IOB state when passed through**", p. 19)에서 간접적으로만 확인된다. 이 역전이를 명시적으로 정의한 문장은 없다. ※ 원문에서 확인하지 못함.

한국어 간접인용 예시:
> Wahlberg(2015)는 메트로베니아의 백트래킹을 장애물의 상태 전이로 정식화하여, 상호작용이 불가능한 SOB 상태가 대응하는 key trigger의 획득으로 상호작용 가능한 IOB 상태로 바뀌고, 상호작용이 이루어지면 장애물이 소멸한 DOB 상태가 되는 구조를 SID 프레임워크라 명명했다. 그는 이 중 SOB 상태만이 백트래킹을 발생시킨다고 보았다.

---

### (5) key trigger의 정의 (원문 그대로)

> Backtracking design is caused when the player stands in front of a blockade and to open it the player needs to get a new key or get to a switch in another location. This so the character can get the right corresponding key to overcome that same blockade that was previously encountered.
>
> The definition of the word key works as the blockade that was inaccessible first is with the key beyond the other side accessible it accomplish a task of progression it triggers the blockade to open. **So I can then define the object of which the player uses on an obstacle blockade as a key trigger to add to terminology of how blockades works.**
> — §6.3, p. 10

용어집(p. 31):

> **Key trigger: the different ways that the player can interact with the IOB.**

⚠ 본문 정의("플레이어가 장애물에 사용하는 **객체**")와 용어집 정의("플레이어가 IOB와 상호작용할 수 있는 **방식들**")가 서로 다르다. **원문 내부의 불일치이므로, 우리 논문에서는 둘 중 하나만 골라 인용하고 그것이 원문 어디에서 온 것인지 밝힐 것.**

저자가 근거로 끌어온 외부 정의(Adams 2013, p. 331):

> "Locked door is a generic term for any obstacle that prevents the player from proceeding through the game until she learns the trick for disabling it"

---

### (6) key trigger의 속성 목록 (원문 그대로) — 10개

§6.3 마지막(p. 17)에 저자가 직접 나열한 목록이다. 원문의 순서와 표기를 그대로 옮긴다.

> As of now we have defined within the SID framework key triggers
>
> &nbsp;&nbsp;&nbsp;&nbsp;Basic
> &nbsp;&nbsp;&nbsp;&nbsp;Upgradable
> &nbsp;&nbsp;&nbsp;&nbsp;Infinite
> &nbsp;&nbsp;&nbsp;&nbsp;Finite
> &nbsp;&nbsp;&nbsp;&nbsp;Direct
> &nbsp;&nbsp;&nbsp;&nbsp;Indirect
> &nbsp;&nbsp;&nbsp;&nbsp;Hidden
> &nbsp;&nbsp;&nbsp;&nbsp;Revealed
> &nbsp;&nbsp;&nbsp;&nbsp;Temporary
> &nbsp;&nbsp;&nbsp;&nbsp;Permanent
> — §6.3, p. 17

**속성별 근거 (본문에서 실제로 설명된 것만 정리)**

| 속성 | 원문에 개별 정의가 있는가 | 본문에서 확인되는 의미 | 근거 위치 |
|---|---|---|---|
| **Basic** | 서술로만 | 게임 시작부터 플레이어가 가지고 있는 기본 능력 — "The first basic weapon that that the player has upon starting the game… The player starts with this weapon upon starting the game" | §6.3, p. 13 (Fig. 6) |
| **Upgradable** | 서술로만 (본문 표기는 "an upgrade") | 획득으로 새 행동이 가능해지는 강화 — "The player then finds an object that acts as an upgrade as it makes the player able to do more things in the game" | §6.3, p. 13 (Fig. 7) |
| **Infinite** | 서술로만 | 자원이 무한 — "the resources are infinite" | §6.3, p. 13 (Fig. 6) |
| **Finite** | 서술로만 | 보유 상한이 있는 자원 — "a resource that the character has a finite capacity of handle at any one time but can restock with infinitely" (미사일: 상한 5씩 증가, 발사 시 1 감소, 문 하나에 3발 필요) | §6.3, p. 13 (Fig. 8) |
| **Direct** | 서술로만 | 직접 접촉이 필요 — "the projectiles needs direct contact for the key trigger to function" | §6.3, p. 13 (Fig. 6) |
| **Indirect** | 서술로만 | 트리거와 장애물이 **같은 공간에 있지 않음** — "even though the key trigger and blockade is not in the same space" | §6.3, p. 15 (Fig. 10) |
| **Hidden** | 서술로만 | 겉보기 상태와 실제 상태가 다름 — "the block seems to be in a SOB state visually… but it has always been in this state mechanically" | §6.3, p. 16 (Figs. 11–12) |
| **Revealed** | **개별 정의·사례 없음** | 목록에만 등장. Fig. 9(슈퍼 폭탄) 설명의 "also reveals any other blocks that are not interactive to go to DOB states but more on that later"(p. 14)와 Fig. 11(레이더 스코프로 벽 투시)이 관련될 수 있으나, 저자가 이를 "Revealed" 속성으로 명명한 문장은 없다. | ※ 원문에서 확인하지 못함 |
| **Temporary** | **개별 정의·사례 없음** | 목록에만 등장. 용어집의 DOB 정의 "disappeared either **temporarily** or permanently"에서만 대응 개념이 보인다. | ※ 원문에서 확인하지 못함 |
| **Permanent** | **개별 정의·사례 없음** | 목록에만 등장. 본문에서는 상태(DOB)의 성질로만 쓰인다 — "stays in that state permanently"(p. 15), "goes to a **permanent** DOB state"(p. 19) | ※ 원문에서 확인하지 못함 |

⚠ **10개 중 7개(Basic, Upgradable, Infinite, Finite, Direct, Indirect, Hidden)만 사례와 함께 설명되어 있고, Revealed / Temporary / Permanent 3개는 목록에 이름만 올라 있을 뿐 개별 정의도 예시도 없다.** 우리 논문에서 이 10개 목록을 표로 옮길 때는 이 사실을 각주로 밝히거나, 설명이 있는 7개만 쓰는 편이 안전하다.

⚠ 표기 불일치: 목록에는 "**Upgradable**"이라고 적혀 있으나 본문 사례에서는 전부 "an **upgrade**"로 쓴다("the key trigger have the specifications of **an upgrade**, infinite and direct", p. 13).

---

### (7) **하나의 key trigger는 여러 속성을 동시에 갖는다** — 이 프레임워크의 구조적 성격

이것이 이 속성 체계의 핵심이다. 저자는 *Super Metroid*의 각 열쇠마다 **속성 2~3개를 조합해** 규격(specifications)을 부여한다. 즉 10개 속성은 **배타적 분류 항목이 아니라, 하나의 트리거를 여러 축에서 동시에 기술하는 태그**다.

본문에서 실제로 부여된 조합 7건 (원문 표현 그대로, 순서대로):

| # | 사례 (*Super Metroid*) | 저자가 부여한 규격 (원문) | 위치 |
|---|---|---|---|
| 1 | 시작 시 보유한 기본 빔 무기 (파란 문) | "the specifications of **basic, infinite and direct**" | §6.3, p. 13 (Fig. 6) |
| 2 | 변형 도구 업그레이드 | "the specifications of **an upgrade, infinite and direct**" | §6.3, p. 13 (Fig. 7) |
| 3 | 미사일 업그레이드 (빨간 문, 3발 필요) | "the specifications of **an upgrade, finite and direct**" | §6.3, p. 13 (Fig. 8) |
| 4 | 유리 터널 (슈퍼 폭탄으로 영구 파괴) | "the specifications of **a finite and direct**" | §6.3, p. 14 (Fig. 9) |
| 5 | **Tourian 개방용 보스 석상** | "the specifications of **a finite, indirect and direct**" | §6.3, p. 15 (Fig. 10) |
| 6 | 숨겨진 블록 / 레이더 스코프 | "both key triggers as **hidden, infinite and direct**" | §6.3, p. 16 (Fig. 11) |
| 7 | 숨겨진 블록 + 슈퍼 미사일 | "the specifications of being **hidden, finite and direct**" | §6.3, p. 16 (Fig. 12) |

**→ 구조에 대한 설명 (우리 논문에 쓸 형태)**

Wahlberg의 10개 속성은 실제로는 **서로 다른 축(axis)에 놓인 대립쌍들**이며, 하나의 key trigger는 각 축에서 하나씩 값을 가져 조합된다. 본문의 7개 사례에서 관찰되는 축은 다음과 같다.

| 축 | 값 | 무엇을 기술하는가 |
|---|---|---|
| 획득 경로 | Basic ↔ Upgradable | 시작부터 보유한 것인가, 게임 중 획득하는 강화인가 |
| 자원량 | Infinite ↔ Finite | 사용 자원에 상한이 있는가 |
| 작용 방식 | Direct ↔ Indirect | 장애물과 같은 공간에서 직접 작용하는가, 다른 공간에서 작용하는가 |
| 가시성 | Hidden ↔ Revealed | 장애물의 실제 상태가 플레이어에게 보이는가 |
| 지속성 | Temporary ↔ Permanent | 상태 변화가 유지되는가 |

⚠ **이 "5개 축" 정리는 원문에 있는 표가 아니라, 본문의 7개 사례에서 우리가 귀납한 것이다.** 저자는 10개를 단순 나열만 했고 축으로 묶은 적이 없다. 우리 논문에 이 표를 실을 경우 "Wahlberg(2015)가 제시한 10개 속성을 본 연구에서 대립쌍으로 재배열하면"처럼 **재구성임을 반드시 밝힐 것.**

⚠ 사례 5(보스 석상)에서 보듯 **Direct와 Indirect가 동시에 부여되기도 한다.** 즉 이 축들은 엄밀한 배타적 이분법이 아니다(아래 (8) 참조).

한국어 간접인용 예시:
> Wahlberg(2015)는 key trigger를 Basic, Upgradable, Infinite, Finite, Direct, Indirect, Hidden, Revealed, Temporary, Permanent의 열 가지 속성으로 기술했는데, 이 속성들은 배타적 분류가 아니라 하나의 열쇠에 둘 이상이 동시에 부여되는 기술 태그다. 예를 들어 *Super Metroid*의 시작 빔 무기는 basic·infinite·direct로, 미사일은 upgrade·finite·direct로 규정된다.

---

### (8) ★ *Super Metroid*의 Tourian(최종 구역) 개방 사례 — **우리 3.6절에 가장 직접 대응하는 대목**

우리 게임 Metro Escape는 **보스를 처치해 얻은 아이템으로 구역을 여는** 구조다. Wahlberg가 분석한 *Super Metroid*의 Tourian 개방은 **보스 처치가 다른 공간의 장애물 상태를 바꾸는** 사례로, 이 구조의 선행 사례에 해당한다.

**원문 전문 (§6.3, p. 15 — 이 문단 전체가 이 사례의 서술 전부다):**

> **The way that the player opens the last game space in Super Metroid called Tourian differs from when the blockades changes states from IOB state to DOB state that it is both indirect and direct activation.**
> **By this I mean that when the player beats all bosses the corresponding boss statues gets triggered in a IOB state even though the key trigger and blockade is not in the same space.**
> **When the player then gets to the space where the statues are located at with no input from the player the statues then goes to a DOB state and stays in that state permanently.**
> **This key trigger is a combination of both beating the boss indirectly and being in the room directly.**
> **With this information I will define this key trigger as having the specifications of a finite, indirect and direct.**
> — §6.3, p. 15

**Figure 10(Boss statues – Super Metroid)의 3프레임 설명 (원문 그대로):**

> In frame 1 the bosses are beaten and the blockade is still in IOB state as the boss statues is yet to fall down while in frame 2 the bosses statues gets destroyed and in frame 3 the boss statues are no longer visible and the blockade is in a DOB state.
> — §6.3, p. 15

**→ 이 사례의 메커니즘 정리 (원문 서술에 근거)**

| 단계 | 플레이어 행동 | 장애물(보스 석상)의 상태 | 원문 근거 |
|---|---|---|---|
| 1 | **다른 공간에서 모든 보스를 처치** | SOB → **IOB**로 전이 (열쇠와 장애물이 같은 공간에 있지 않음) | "when the player beats all bosses the corresponding boss statues gets triggered in a IOB state even though the key trigger and blockade is not in the same space" |
| 2 | 석상이 있는 방에 **도착** | IOB 유지 (석상은 아직 쓰러지지 않음) | "In frame 1 the bosses are beaten and the blockade is still in IOB state as the boss statues is yet to fall down" |
| 3 | **입력 없음** (도착 자체가 조건) | IOB → **DOB**로 전이, 그리고 **영구 유지** | "with no input from the player the statues then goes to a DOB state and stays in that state permanently" |

**→ 이 사례가 특별한 이유 (저자의 논지)**

저자는 이 트리거를 **indirect와 direct를 동시에 갖는 유일한 사례**로 다룬다. "보스를 잡는 행위"는 장애물과 다른 공간에서 일어나므로 **indirect**이고, "그 방에 있는 것" 자체는 장애물과 같은 공간에서 일어나므로 **direct**다. 즉 하나의 key trigger가 **두 공간에 걸쳐 작동**한다.

> **This key trigger is a combination of both beating the boss indirectly and being in the room directly.**

⚠ 정확히 옮길 것: 상태 전이 3단계 중 **플레이어의 입력(input)이 개입하는 지점이 없다.** 2단계에서 3단계로의 전이는 "방에 도착하는 것"만으로 자동 발생한다("with no input from the player"). 이는 다른 key trigger들(무기를 쏘거나 폭탄을 터뜨리는 것)과 근본적으로 다른 점이다.

⚠ 저자가 왜 이 트리거에 "**finite**"를 붙였는지는 원문에 설명이 없다. 보스의 수가 유한하다는 뜻으로 읽히지만, 그렇게 쓴 문장은 없다. ※ 원문에서 확인하지 못함.

한국어 간접인용 예시:
> Wahlberg(2015)는 *Super Metroid*의 최종 구역 Tourian이 열리는 과정을, 열쇠와 장애물이 같은 공간에 있지 않은 상태에서 작동하는 간접(indirect) 트리거의 사례로 분석했다. 플레이어가 다른 공간에서 모든 보스를 처치하면 대응하는 보스 석상이 IOB 상태로 전이하고, 이후 플레이어가 석상이 있는 방에 도착하면 별도의 입력 없이 석상이 DOB 상태로 전이해 영구히 그 상태에 머문다. 저자는 이 열쇠 트리거를 "finite, indirect and direct"로 규정했다.

---

### (9) 백트래킹과 선형 레벨의 정의 (참고)

> The definition of backtracking in level design means that the player avatar must go through a set route to then having to go back through that set route once more.
> The counterpart of backtracking then is not having to move backtrack over a route that has already been explored that is where the word linear in level design comes in.
> — §6.2, p. 8

백트래킹이 공간의 의미를 바꾼다는 서술(§6.2, p. 10):

> **At its core backtracking can flip the expectations that the player has when revisiting a game space as the games spaces can show change dynamically from the first time exploring that space.** This example from Super Metroid shows that designed progression through backtracking occurs when there is an obstacle in the form of a blockade and the player needs to find a corresponding key that triggers that same blockade.

---

### (10) 메트로베니아는 "장르"가 아니라 "하위 장르"라는 규정 (참고)

> By this definition Metroidvania has more than one mechanic that players does and the word genre is not enough to define it.
> — §6.1, p. 7

용어집(p. 31):

> **Metroidvania: the sub genre that mixes mechanics from both Metroid games and later Castlevania games that has backtracking in its level design.**

저자가 근거로 든 Adams(2013, p. 67)의 장르 정의:

> "In describing movies or books the term genre refers to the content of the work... With video games, however, genre refers to the types of challenges that a game offers."

---

## 5. 우리 논문에서 쓸 자리

- **3.6절 (메트로베니아 공간 확장) — 주 용도, 이 절의 핵심 근거.**
  1. **구역 잠금/해제를 상태 기계로 기술하는 틀**로 SID 프레임워크를 인용한다. 우리 게임의 "잠긴 구역 입구 → 보스 아이템 획득 → 개방"은 SOB → IOB → DOB 전이에 정확히 대응한다. (4-(4) 인용문)
  2. **보스 처치로 구역이 열리는 우리 설계의 선행 사례**로 *Super Metroid*의 Tourian 개방을 든다. (4-(8) — **이 절에서 가장 값어치 있는 대목**) 우리 구조가 "장애물과 다른 공간에서 조건이 충족되는 indirect 트리거"라는 점을 이 사례로 위치 지을 수 있다.
  3. 우리 게임의 열쇠 아이템을 key trigger 속성으로 기술한다. 보스 처치 아이템은 **upgrade(획득형) · indirect(다른 공간에서 조건 충족) · permanent(개방이 영구)** 로 기술할 수 있다. (4-(6), 4-(7))
  4. 잠긴 문 앞에 먼저 도착하게 하는 배치가 백트래킹을 의도적으로 발생시킨다는 서술("The SOB state is the only blockade state that causes backtracking", §6.3, p. 12)은 우리 1구역 설계의 근거로 쓸 수 있다.
- **2장 관련연구** — 메트로베니아를 하위 장르로 규정하는 대목(4-(10))과, 백트래킹이 "이미 탐색한 공간의 기대를 뒤집는다"는 서술(4-(9))을 장르 정의 문단에 쓴다.
- **3.6절 서술 형식 주의**: 반드시 **"Wahlberg(2015)가 제안한"**, **"Wahlberg(2015)의 용어를 빌리면"** 처럼 **개인 제안임이 드러나는 서술**로 쓰고, 같은 문단 안에서 **11번 문헌(Oliveira 외, 2020)을 병기**해 단독 의존을 피한다.

---

## 6. 쓰면 안 되는 주장

1. **⚠ 최우선 — SOB/IOB/DOB/SID framework/key trigger는 장르 표준 용어가 아니다.**
   - 이 다섯 용어는 **전부 저자가 이 학사 학위논문에서 처음 만든 신조어**다. 저자 본인이 "creating new knowledge explained through **making new terminology**"(Abstract), "level designers do not have a common framework, terminology to work with… and **I aim to fix that with this paper**"(1장, p. 1)라고 밝혔다.
   - 따라서 **"메트로베니아 장르에서는 장애물을 SOB, IOB, DOB로 구분한다"**, **"SID 프레임워크에 따르면"** 처럼 **확립된 이론인 양 쓰는 문장은 쓸 수 없다.** 안전한 형태는 **"Wahlberg(2015)는 … 라는 용어를 제안하여"**, **"Wahlberg(2015)의 분류를 빌리면"** 이다.

2. **⚠ 학부 졸업논문이 학부 졸업논문을 유일한 근거로 삼는 구조를 만들지 말 것.**
   - 이 문헌은 15 hp짜리 학사 학위논문이며 동료심사를 받지 않았다. 3.6절의 개념 틀 전체를 이 한 편에 의존하면 심사에서 근거의 무게를 지적받는다.
   - **대응**: (a) 11번 문헌 Oliveira 외(2020, SBGames Industry Track 정식논문)를 같은 문단에 병기한다. (b) 5장 한계에 "본 연구가 공간 확장 구조를 기술하는 데 사용한 용어 체계는 Wahlberg(2015)가 학사 학위논문에서 제안한 것으로, 장르 전반에 통용되는 표준 용어는 아니다"라는 문장을 넣는다.

3. **재미(fun)·플레이어 경험에 관한 어떤 주장에도 쓸 수 없다.**
   - 데모 실험은 저자가 만든 Construct 2 데모 8개를 **"저자가 아는 아무 사람들" 10명**에게 시킨 것이다(§6.6, p. 21). 통계 검정, 인구통계, 모집 절차, 측정 도구의 타당도가 전부 없다.
   - 따라서 **"백트래킹이 선형 레벨보다 재미있다"**, **"지름길이 있는 백트래킹이 더 낫다"**, **"적 배치를 바꾸면 재미가 올라간다"** 같은 문장은 이 문헌으로 정당화할 수 없다.
   - 저자 자신이 결과 해석에서 선행 주장을 "both validates and contradicts"(9장, p. 29)한다고 적었을 만큼 결론이 일관되지 않다.

4. **"모든 메트로베니아 게임이 이 패턴을 따른다"고 쓸 수 없다.**
   - 프레임워크의 근거 게임은 *Super Metroid* **1개**이고, 대조군은 *Megaman*, *Super Mario 64*, *Dark Souls* **3개**뿐이다. 게다가 이 중 *Super Mario 64*와 *Dark Souls*는 메트로베니아가 아니다. 저자가 "All key triggers listed from Super Metroid are used in Megaman, Super Mario 64 and Dark Souls 1"(§6.4, p. 19)이라고 단언하지만, 이는 저자 1인의 관찰이며 코딩 절차나 신뢰도 검증이 없다.

5. **Revealed / Temporary / Permanent 속성의 정의를 인용할 수 없다.** 세 속성은 §6.3(p. 17)의 목록에 이름만 올라 있고, 본문에 개별 정의도 사례도 없다(4-(6) 표 참조). 이 셋에 대해 "Wahlberg는 …라고 정의했다"고 쓰면 원문에 없는 말을 지어내는 것이 된다.

6. **key trigger의 정의를 하나로 단정해 인용할 수 없다.** 본문(§6.3, p. 10)은 "플레이어가 장애물에 사용하는 **객체**"로, 용어집(p. 31)은 "플레이어가 IOB와 상호작용할 수 있는 **방식들**"로 서로 다르게 정의한다. 둘 중 하나를 고르고 출처 위치를 밝혀야 한다.

7. **"5개 축(획득 경로/자원량/작용 방식/가시성/지속성)"은 Wahlberg의 정리가 아니다.** 4-(7)의 축 표는 본 메모가 사례에서 귀납한 재구성이다. 우리 논문에 실으려면 반드시 재구성임을 명시해야 한다.

8. **DOB에서 IOB로의 역전이를 Wahlberg가 정의했다고 쓸 수 없다.** "either temporarily or permanently"(용어집)와 Dark Souls 서술에서 암시될 뿐, 역전이 조건을 규정한 문장은 없다.

9. **대학명을 추정해서 참고문헌에 적지 말 것.** 표지에 학과명만 있고 대학명이 없다. 확인 전에는 비워 두거나, 원본 PDF/리포지터리에서 확인한 뒤 채워 넣는다.

10. **저자가 스스로 한 유보를 감추고 쓰지 말 것.** "I think I have only scratched the surface"(8장, p. 28), "has long way ahead to be fully understood scientifically"(8장, p. 28), "We have only unlocked the basic fundamentals"(9장, p. 29). 이 프레임워크는 저자 본인이 **출발점**이라고 규정한 것이다.

---

## 7. 참고문헌 항목

```
Wahlberg, T. (2015). Blockades in the Metroidvania genre of games: A examination of backtracking [Bachelor's thesis, Department of Game Design].
```

⚠ **대학명이 원문 표지에 없다.** 위 항목은 원문에서 확인 가능한 정보만으로 작성한 것이다. 참고문헌 목록에 올리기 전 반드시 원본 PDF 표지 또는 소장 리포지터리에서 소속 대학명을 확인하고 `[Bachelor's thesis, <대학명>]` 형식으로 보완하며, 가능하면 리포지터리 URL도 함께 적을 것. ※ 대학명·URL은 원문에서 확인하지 못함.

※ 참고로 11번 문헌(Oliveira 외, 2020)은 이 논문을 다음과 같이 인용했다(해당 논문 참고문헌 [16]):

```
T. Wahlberg, "Blockades in the metroidvania genre of games: A examination of backtracking," 2015.
```
