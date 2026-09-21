# Oliveira 외(2020) — A Framework for Metroidvania Games

## 1. 서지사항

- **정식 인용**: Oliveira, B. P., Franco, A. de O. da R., Silva, J. W. F. da, Gomes, F. A. de C., & Maia, J. G. R. (2020). A framework for Metroidvania games. In *SBC – Proceedings of SBGames 2020* (Industry Track – Full Papers, pp. 836–844). Sociedade Brasileira de Computação.
- **파일**: 서바이벌_메트로베니아/SBGames2020_Metroidvania_industry.txt
- **종류**: **학술대회 정식논문(full paper)** — SBGames 2020(XIX Brazilian Symposium on Computer Games and Digital Entertainment), **Industry Track – Full Papers**, 개최지 Recife – PE, Brazil, 2020년 11월 7–10일. 페이지 러너 기준 **pp. 836–844(총 9쪽)**. ISSN 2179-2259.
- **저자 전원과 소속 (원문 p. 836 헤더 그대로)**:

  | 저자 | 소속 | 도시 |
  |---|---|---|
  | **Bruno Pinheiro Oliveira** | Federal University of Ceará | Fortaleza, Brazil |
  | **Artur de Oliveira da Rocha Franco** | Federal University of Ceará | Fortaleza, Brazil |
  | **José Wellington Franco da Silva** | Federal University of Ceará | Fortaleza, Brazil |
  | **Fernando Antônio de Carvalho Gomes** | Federal University of Ceará | Fortaleza, Brazil |
  | **José Gilvan Rodrigues Maia** | Federal University of Ceará | Fortaleza, Brazil |

  → **저자 5인 전원이 브라질 세아라 연방대학교(Federal University of Ceará, Fortaleza) 소속이다.** 단일 기관 논문이다.
- **키워드 (원문)**: metroidvania, game development, genre-specific framework
- **등급 메모**:
  - 10번 문헌(Wahlberg 2015, 학사 학위논문)보다 **출처의 무게가 분명히 무겁다.** 정식 학술대회 논문집에 실린 full paper이고, 저자 5인의 소속이 명확하며, ISSN이 부여되어 있다. **3.6절에서 Wahlberg 단독 의존을 보강하는 두 번째 출처로 쓰기에 적합하다.**
  - 다만 **Industry Track**이다. SBGames는 트랙이 나뉘어 있고 이 논문은 Computing Track이 아니라 산업 트랙의 full paper다. 심사 기준이 연구 트랙과 다를 수 있으므로, 우리 논문에서 소개할 때는 "SBGames 2020 산업 트랙 정식논문"처럼 **트랙까지 밝히는 편이 정직하다.**
  - **이 논문은 10번 문헌(Wahlberg 2015)을 참고문헌 [16]으로 직접 인용하고 있다(§II-B, p. 838).** 즉 **정식 학술대회 논문이 Wahlberg를 인용했다는 사실 자체가, 우리가 Wahlberg를 인용하는 것을 방어해 주는 근거가 된다.** 3.6절에서 이 관계를 활용할 것(5번 참조).
  - 영문 편집이 거친 편이다("so, we we hoped", "in their tur", "NCPs", "it is tries to move"). 직접인용 시 원문 그대로 옮기되 필요하면 `[sic]` 처리할 것.

---

## 2. 한 줄 요약

메트로베니아 장르에 특화된 게임 엔진 아키텍처(대규모 맵 탐색, 전투, 인벤토리, 경로탐색, 조력 시스템, 스킬 트리, 퍼즐, 동적 맵 로딩, NPC 인스턴스 재활용)를 제안하고 Unity 3D/C#로 구현한 뒤, **프로토타입 게임 1개**로 포커스 그룹 평가와 성능 측정을 수행한 브라질 SBGames 2020 산업 트랙 정식논문이다.

---

## 3. 이 연구가 실제로 한 것

### 연구 방법

- **아키텍처 설계 → 구현 → 프로토타입 1개로 검증**의 공학적 접근. 사람에 대한 실험 연구가 아니다.
- 구현 환경: **Unity 3D 엔진 + C#**, 개발 기간 **2개월**.

  > The Unity 3D game engine and the C# language were chosen for the implementation of the framework. C# is a powerful, expressive language which supports reflection and other sophisticated features of object-oriented programming. Unity 3D was chosen due to its learning curve, documentation, community support, and free license. **Development took 2 months.** (§IV, p. 843)

- 아키텍처의 뼈대는 **entities(엔티티)와 code blocks(코드 블록) 두 요소**로 구성된다.

  > Our architecture is composed by two main components: entities and code blocks (see Fig. 1). Entities have both attributes and codes. Code blocks, in their turn, encapsulate the processing logic and can be subdivided into smaller blocks responsible for specific functions. (§III-A, p. 839)

### ★ 검증에 쓰인 프로토타입 수 — **지시받은 확인 항목**

**프로토타입은 단 1개다.** 초록·서론·결론이 모두 단수형으로 일관되게 서술한다.

> **A prototype game was built using the proposed framework to validate our approach.** A specialized character tagging mechanic was introduced in our prototype with the view to demonstrate our approach is flexible enough to adapt to different mechanics. (Abstract, p. 836)

> We present practical evaluation results by means of **a prototype game** built on top of our framework. Our prototype features mechanics such as character tagging, skill tree, player following, and a customized combat system. Results were evaluated by users in a focus group. (§I-B, p. 837)

> We implemented a framework prototype that abstracts many complexities found in real world projects by proposing solutions to common problems found in this genre. **We developed a game prototype with the purpose of verifying our framework prototype.** (§VI, p. 844)

→ **정리: 프레임워크 프로토타입 1개 + 그 위에 올린 게임 프로토타입 1개.** 복수의 게임으로 교차 검증한 것이 아니다.

### 분석·실험 대상과 표본 규모 (숫자 정확히)

| 항목 | 수치 |
|---|---|
| 검증용 프로토타입 게임 | **1개** |
| 개발 기간 | **2개월** |
| 포커스 그룹 참가자 수 | **※ 원문에서 확인하지 못함** (숫자가 전혀 제시되지 않음) |
| 사용자 평가 척도 | 0–10점 (게임의 성능과 전반 점수) |
| 성능 테스트 장비 | i5 3330 / 6GB RAM / XFX AMD Radeon R7750 1GB DDR5 / Ubuntu 17.04 |
| 프로토타입 기본 프레임레이트 | **60fps** (모든 해상도에서 유지) |
| NPC 50개까지 | 오버헤드 없음 |
| NPC 125개 | 약 **20fps** |
| NPC 175–200개 | 평균 약 **10fps** |
| GMM 메모리 테스트 | 한 Scene의 **4개 Phase**를 통과하며 RAM 측정 |
| 예상 플레이 시간 | 약 10분 (실제로는 퍼즐 난도 때문에 전원이 초과) |

원문 근거:

> Tests were carried out in a computer with i5 3330, 6GB RAM, and XFX AMD Radeon R7750 1 GB DDR5 GPU running Ubuntu 17.04. First we tested the game using all possible resolutions. The prototype game kept running at 60fps. (§V-B, p. 843)

> We performed stress tests on the average FPS versus the number of active NPCs and interacting constantly ( see Fig. 6). Up to 50 NPCs do not cause any overhead in our prototype. Then, the FPS drop rate remains constant up to a total of 125 NPCs, where we have approximately 20fps. Similar behavior is obtained from 175 to 200 NPCs when average FPS stabilizes around 10. **Metroidvania titles rarely display more than 30 active enemies, thus our prototype game achieved very satisfactory performance.** (§V-B, p. 843)

> When asked about the performance and the overall score of the game, the participants assigned a maximum score on a scale ranging from 0 to 10. (§V-A, p. 843)

### 검증 방식과 그 한계 — **반드시 반영할 것**

- ⚠ **프로토타입 1개로만 검증했다.** 프레임워크의 일반성(다른 프로젝트에서도 재사용 가능한가)은 실제로 시험되지 않았다. 저자들이 유연성의 근거로 든 것은 **"character tagging"이라는 메커닉 하나를 추가로 얹어 봤다**는 사실뿐이다(Abstract, §I-B).
- ⚠ **포커스 그룹의 참가자 수가 논문에 없다.** "participants", "focus group"이라고만 쓸 뿐 인원, 모집 방법, 인구통계, 설문 도구가 전혀 제시되지 않았다. ※ 원문에서 확인하지 못함. 따라서 **사용자 평가 결과("최고점을 줬다")를 정량적 근거로 인용할 수 없다.**
- ⚠ **저자들이 스스로 밝힌 한계 (§IV, p. 843)**:

  > Some serious limitations were found and resolved in the early stages implementation. For example, jumping was handling collisions in the character entity. **We limited user tests before presentation to the focus groups.** For example, our estimate of the time required for solving the puzzle was based on our own knowledge of this mechanic.

  > It is necessary to emphasize that the parts that consumed the most time of implementation were pathfinding and artificial intelligence, plus debugging. Both aspects have to solve complex problems with generic algorithms for the sake of reuse. **We found it difficult to analyze results we obtained.** For example, after some tests the tester IA starts to get used of the strategies displayed by NPCs. Consequently, **the game seems to be simpler and easier than it actually is for the general audience.**

- ⚠ **저자들이 스스로 밝힌 사용자 피드백의 부정적 부분 (§V-A, p. 843)**:

  > Initially, we calculated that players would take around 10 minutes completing the map. However, **all players ended up spending more time because of the challenge found in the puzzle.** According to players' comments, **the worst part was the unbalanced, hard to fight boss AI and the time to master the character tagging mechanic.**

- ⚠ **선행연구 조사 자체의 한계 (§II-D, p. 838)**: 저자들은 유사 연구가 거의 없다고 밝힌다.

  > We searched for similar works with the objective to delineate a framework for the genre Metroidvania. **Only a few scientific articles on the subject were found even by trying several combinations of keywords in many search engines.** All attempts included the term "metroidvania".

  > The best results were obtained with the Google Scholar and the combination of the terms "metroidvania" and "toolkit", which resulted in **only one article**. The combination of "metroidvania " and "game engine" produced **48 articles**, most of these are related to Procedural Content Generation (PCG)...

  > Our investigation on the existence of frameworks especially designed for the development of Metroidvanias suggests **a lack or deficit of existing solutions.**

  → **이 대목은 우리 논문의 2장에서 "메트로베니아 구현을 다룬 학술 문헌이 드물다"는 문제 제기의 직접 근거로 쓸 수 있다.**

- ⚠ 결론의 주장 범위: 저자들의 최종 주장은 "feasible, useful, and efficient" 세 단어뿐이고, 재미나 플레이어 경험에 대한 주장이 아니다.

  > Experimental evaluation demonstrate that our framework is both feasible, useful, and efficient. (§VI, p. 844)

---

## 4. 인용 가능한 내용

### (1) ★ 프레임워크 구성요소 목록 (원문 초록 그대로) — **지시받은 확인 항목**

초록에 아홉 항목이 세미콜론으로 구분되어 한 문장에 나열되어 있다. **순서와 표기를 원문 그대로 옮긴다.**

> In this paper, we propose a framework for building Metroidvania games comprising the following key aspects of this genre: **large map navigation; battle system; inventory; pathfinding; assistance system, allowing for getting help from companion NPCs (Non-Player Characters); skill tree; puzzle-solving; dynamic map loading; and recycling of NPC instances.**
> — Abstract, p. 836

**→ 구성요소 9개 정리 (원문 표기 / 초록 순서)**

| # | 원문 표기 | 초록에 붙은 설명 | 본문에 독립 절이 있는가 |
|---|---|---|---|
| 1 | **large map navigation** | — | §III-B Game Map Management (GMM), p. 839 |
| 2 | **battle system** | — | §III-F Combat System, p. 841 |
| 3 | **inventory** | — | 독립 절 없음 (§II-B에서 "Access to the scenery is limited by inventory and skills"로만 언급) |
| 4 | **pathfinding** | — | §III-E Pathfinding, pp. 840–841 |
| 5 | **assistance system** | "allowing for getting help from companion NPCs (Non-Player Characters)" | §III-G Character Tagging, p. 841 / §III-H, p. 842 |
| 6 | **skill tree** | — | 독립 절 없음 (§III-F에서 "ability tree", "combos trees"로 언급) |
| 7 | **puzzle-solving** | — | 독립 절 없음 (§III-C에서 레버 예시, §III-B에서 "puzzle management") |
| 8 | **dynamic map loading** | — | §III-B GMM, p. 839 |
| 9 | **recycling of NPC instances** | — | §III-D Character Recycling, p. 840 |

⚠ **초록의 9개 항목과 본문의 절 구성이 1:1로 대응하지 않는다.** inventory, skill tree, puzzle-solving은 초록에 구성요소로 올라 있으나 본문에 독립된 설계 절이 없고 다른 절 안에서 스치듯 언급될 뿐이다. 우리 논문에서 이 9개를 표로 옮길 때는 **초록의 목록임을 밝히고**, "각 요소의 설계가 본문에서 동일한 깊이로 다뤄지지는 않았다"는 점을 감추지 말 것.

### (1-b) 저자들이 §I-B "Contributions"에서 별도로 정리한 기여 목록 (원문 그대로)

초록의 목록과 **항목 구성이 다르므로** 함께 확인해야 한다.

> • **Game Map Management.** Avoids memory overload and enhances reuse of common elements.
> • **Character recycling.** Improves performance during saving, loading, restarting game zones, creating enemy waves, and other common operations during gameplay.
> • **Pathfinding and basic path planning** [12], which abstracts the intricacies of integration with the Game Map Manager. Moreover, this component also adapts methods found in the literature, usually developed for 2D or 3D case, to work in the case of side-scrollers.
> • **Combat System.** Designed to prevent game-breaking behavior in terms of infinite combos [13]. In particular, we compute damage based on data assigned to animation frames during attacks, thus allowing for inflicting varying damage over time. Moreover, the combat system is integrated into an ability tree, so our results can generalize to virtually any moves made available in combat.
> • **NPC Behavior System.** We implement a combination of outstanding characteristics found in previous models in order to produce an adequate AI for the game genre [2].
> • We implemented the proposed framework on top of **Unity 3D**.
> • We present practical evaluation results by means of a prototype game built on top of our framework...
> — §I-B, p. 837

한국어 간접인용 예시:
> Oliveira 외(2020)는 메트로베니아 게임 개발을 위한 프레임워크를 제안하면서, 이 장르의 핵심 요소로 대규모 맵 탐색, 전투 시스템, 인벤토리, 경로탐색, 동료 NPC의 조력 시스템, 스킬 트리, 퍼즐 해결, 동적 맵 로딩, NPC 인스턴스 재활용의 아홉 가지를 들었다.

---

### (2) 메트로베니아 장르의 특징 규정 (§II-B) — **3.6절에서 Wahlberg를 보강하는 핵심 대목**

저자들이 여러 출처를 근거로 나열한 장르 특징 목록이다. 참고문헌 [18][10][16][11][19]를 함께 달고 있다(여기서 **[16]이 Wahlberg 2015**다).

> These are the main are main characteristics of this genre6 [18] [10] [16] [11] [19]: **scroll is allowed in both horizontal and vertical axes; players are tasked with finding and pursuing in-game goals; Access to the scenery is limited by inventory and skills, so maps are therefore gradually and recurrently exploited until players find items such as keys, doors, and heights, or acquire abilities such as a high jump, flying, and gliding;** the gameplay rewards players for their creative ways of moving on the map, even if they seem to deviate from the overall purpose of the game; **the game offers items and abilities as the player advances through the scenarios and story. These power-ups reinforce the sense of progress; as the player progresses in the game in terms of options for exploration, her curiosity is poised to revisit locations in order to try to discover secrets, passages, items, and challenges using her newly discovered ideas;** and despite the focus on exploration, this type of game does not limit its ability to tell a story.
> — §II-B, p. 838

> In a typical Metroidvania, **the lack of clear direction in gameplay encourages scenario exploration and at the same time forces the player to revisit the path she has gone through.**
> — §II-B, pp. 837–838

> A typical Metroidvania features big level maps with non-linear exploration gameplay. Such maps contain a myriad of items and enemies of different levels of difficulty [6].
> — §I-A, p. 836

**→ 왜 이것이 3.6절에 중요한가**: "**Access to the scenery is limited by inventory and skills**"는 Wahlberg의 SOB/IOB 개념(플레이어가 대응하는 열쇠를 갖기 전에는 공간이 차단됨)을 **다른 출처에서 독립적으로 진술한 문장**이다. 우리 게임의 "보스 처치 아이템으로 구역을 여는" 구조를 이 문장 하나로도 장르 규범 안에 위치시킬 수 있다.

한국어 간접인용 예시:
> Oliveira 외(2020)는 메트로베니아 장르의 주요 특징으로, 수평·수직 양축 스크롤, 명확한 방향 지시의 부재, 그리고 **인벤토리와 습득한 능력에 의해 접근 가능한 공간이 제한되어 맵이 점진적·반복적으로 개방되는 구조**를 들었다.

---

### (3) Wahlberg(2015)에 대한 이 논문의 요약 (§II-B) — **10번 문헌과 3.6절을 잇는 연결 고리**

> According to Wahlberg [16], this "kickback" offers little variability with differences in some scenario parameters, such as difficulty, types of enemies, and how waves of enemies are fired. **Wahlberg developed a study based on the framework of the MDA Framework and its types of fun [17]. He concluded that the concept of recurring scenario exploration, entitled backtracking level design, is one of the pillars of the Metroidvania genre.**
> — §II-B, p. 838

참고문헌 항목(p. 844):

> [16] T. Wahlberg, "Blockades in the metroidvania genre of games: A examination of backtracking," 2015.

**→ 활용법**: 3.6절에서 Wahlberg를 인용할 때 이 문장을 함께 달면, "학사 학위논문 한 편에만 기댄다"는 지적을 상당 부분 막을 수 있다. 즉 **"Wahlberg(2015)가 제안한 …는 이후 Oliveira 외(2020)에서도 메트로베니아 장르의 기둥으로 인용되었다"**는 형태로 쓴다.

⚠ 단, 이 문장은 Oliveira 외가 Wahlberg를 **요약한 것**이지 독립적으로 재검증한 것이 아니다. "두 연구가 같은 결론에 이르렀다"고 쓰면 안 된다.

---

### (4) 동적 맵 로딩 / 메모리 관리 (§III-B, GMM) — 우리 3.9절에도 쓸 수 있는 대목

> We developed algorithms to avoid overloads by dividing the map into **Scenes**. This process is applied recursively, thus subdividing Scenes into **Phases**. **Phases are loaded according to a position of the player in the map.** Moreover, we also adjust the player position relative to the current level in order to avoid floating point issues.
> — §III-B, p. 839

> At first, we limit the loading to the characters, code blocks, and other information that will be persistent until the player "touches" the border of another scene. When this happens, the framework loads metadata, characters, and code blocks of the potentially new current scene. **Phases will also be managed, since their code blocks are responsible of puzzle management, playing ambient sounds, triggering enemy wares, and other specific gameplay behavior.**
> — §III-B, p. 839

장르가 왜 이것을 요구하는지에 대한 서술:

> For example, all of the content in the extended scenario may not fit at once in memory, so the genre demands intelligent management of characters, textures, meshes, and animations. **This is much like memory paging in operating systems.**
> On the other hand, **scenario loading should occur in background so the impact of this on gameplay does not cause an immersion break that harms user experience.**
> — §II-C, p. 838

> in a typical Metroidvania, there is a lack of total knowledge about the scenario, which is usually very broad to fit in memory and consequently **must be loaded dynamically as the player steers her avatar** [11].
> — §I-A, p. 837

한국어 간접인용 예시:
> Oliveira 외(2020)는 메트로베니아의 넓은 맵을 한 번에 메모리에 올릴 수 없으므로 맵을 Scene과 Phase로 계층 분할하고 플레이어의 위치에 따라 동적으로 로드·언로드하는 구조를 제안했으며, 이를 운영체제의 메모리 페이징에 비유했다.

---

### (5) NPC 인스턴스 재활용 (§III-D) — 우리 3.9절 최적화 서술에 쓸 수 있음

> To improve performance, characters keep their global position even in loading time. This simple information is sufficient for our recycling algorithm. **Characters discarded due to death or even leaving the borders of the screen view area are not unloaded immediately, but left disabled and collected into a list. So, these resources do not demand a new verification step and can be reused in the future.** It is worthy to mention that any persistent information about these characters are saved to Scenes, Phases, and Global scope before actual recycling.
> — §III-D, p. 840

→ 이는 일반적으로 오브젝트 풀링(object pooling)으로 불리는 기법이지만, **이 논문은 "object pooling"이라는 용어를 쓰지 않는다.** 우리 논문에서 용어를 바꿔 인용하지 말 것. ※ "object pooling"이라는 표현은 원문에서 확인하지 못함.

---

### (6) 전투 시스템의 프레임 단위 판정 (§III-F) — 우리 3.4절에 대응 가능

> The combat system is responsible for managing the battle actions of players and NPCs. **Each attack, defense, and reaction is implemented at the frame level, much like traditional fighting games.** This decision was made to allow a fine-grained control of the behavior of the characters while the battle unfolds. Such granularity is important for applying any balancing fixes or even fixing bugs.
> — §III-F, p. 841

> Each character has a list of possible actions, chosen according to the player's controls and current ability tree. **The damage is computed on a per-frame basis to prevent loops, which may lead to infinite combos, and to allow damage scaling as the combination progresses.**
> — §III-F, p. 841

→ 우리 게임의 근접 전투 판정을 애니메이션 프레임 구간에 붙여 처리한다면, **선행 사례로 인용 가능**하다.

---

### (7) 보스 AI와 일반 몬스터 AI의 분리 (§III-A, §III-H) — 우리 3.3절에 대응 가능

> **The Combat block, in its turn, is subdivided into minion and "boss" code blocks. We adopted this approach because bosses and secondary characters usually consume more resources since these NPCs display a broader variety of attacks and resort to more sophisticated AI methods.**
> — §III-A, p. 839

> The combat system provides two types of AI for battles, one being simpler and the other more advanced, so these mechanisms can be used in different NPCs. **The simple AI is used for ordinary minions and the more advanced is better suited for bosses and players' companion NPCs.**
> — §III-H-2, p. 842

→ 우리 게임이 일반 몬스터와 보스의 AI를 다른 구조로 구현했다면, 이 분리가 장르 프레임워크 수준에서도 채택된 설계라는 근거로 쓸 수 있다.

---

### (8) 성능 수치와 그 기준선 (§V-B) — 우리 4장 성능 측정에서 비교 기준으로 쓸 수 있음

> Up to 50 NPCs do not cause any overhead in our prototype. Then, the FPS drop rate remains constant up to a total of 125 NPCs, where we have approximately 20fps. Similar behavior is obtained from 175 to 200 NPCs when average FPS stabilizes around 10. **Metroidvania titles rarely display more than 30 active enemies, thus our prototype game achieved very satisfactory performance.**
> — §V-B, p. 843

⚠ **"메트로베니아 게임은 활성 적이 30개를 넘는 일이 드물다"는 진술에는 출처가 없다.** 저자들의 경험적 단언이며 인용 번호가 붙어 있지 않다. 우리 논문에서 이 수치를 쓰려면 반드시 "Oliveira 외(2020)는 …라고 보았다"처럼 **저자들의 판단임을 드러내는 형태**로만 써야 한다.

---

### (9) 장르의 학술적 공백 (§II-D) — 우리 2장 문제 제기의 직접 근거

> **Only a few scientific articles on the subject were found even by trying several combinations of keywords in many search engines.** All attempts included the term "metroidvania".
> — §II-D-1, p. 838

> Most results referred to game design, plus sociocultural and market analyzes.
> — §II-D-1, p. 838

> Our investigation on the existence of frameworks especially designed for the development of Metroidvanias suggests **a lack or deficit of existing solutions.** This finding corroborates the motivation of the present study to propose an architecture and its scientific contributions.
> — §II-D-2, p. 839

> As far as we know, **there is no similar framework in literature** (see Section II-D).
> — §I-B, p. 837

한국어 간접인용 예시:
> Oliveira 외(2020)는 여러 검색엔진에서 키워드 조합을 바꿔 가며 조사했음에도 메트로베니아를 다룬 학술 논문이 소수에 그쳤으며, 검색 결과 대부분이 게임 디자인이나 사회문화·시장 분석에 관한 것이고 개발·구현 관점의 프레임워크는 사실상 부재하다고 보고했다.

---

## 5. 우리 논문에서 쓸 자리

- **3.6절 (메트로베니아 공간 확장) — 주 용도, 그리고 이 문헌을 확보한 목적.**
  1. **Wahlberg(2015) 단독 의존을 끊는 두 번째 출처.** 4-(2)의 "Access to the scenery is limited by inventory and skills, so maps are therefore gradually and recurrently exploited until players find items such as keys, doors, and heights, or acquire abilities…"는 우리 게임의 구역 개방 구조를 장르 규범으로 위치 짓는 데 바로 쓸 수 있다.
  2. **4-(3)을 써서 Wahlberg의 신뢰도를 보강한다.** "Wahlberg(2015)가 제안한 백트래킹 레벨 디자인 개념은 Oliveira 외(2020)에서도 메트로베니아 장르의 기둥 중 하나로 인용되었다"는 문장 한 줄이 심사에서 큰 차이를 만든다.
  3. 3.6절 구현 서술에서 **구성요소 목록(4-(1))을 표로 제시**하고, 우리 게임이 그중 어느 요소를 구현했고 어느 요소를 구현하지 않았는지 대조하면 절의 구조가 단단해진다.
- **2장 관련연구** — 4-(9)를 근거로 "메트로베니아의 구현·아키텍처를 다룬 학술 문헌이 드물다"는 문제 제기를 세운다. 이는 우리 논문이 **구현 중심의 학부 졸업논문**이라는 성격과도 잘 맞는다.
- **3.9절 (렌더링·최적화)** — 4-(4) 동적 맵 로딩(Scene/Phase 계층 분할)과 4-(5) NPC 인스턴스 재활용. 우리 게임이 구역 단위 로딩이나 오브젝트 재사용을 구현했다면 선행 사례로 인용 가능.
- **3.4절 (전투 시스템)** — 4-(6) 프레임 단위 피격 판정.
- **3.3절 (몬스터 AI)** — 4-(7) 보스 AI와 일반 AI의 구조적 분리.
- **4장 (성능 측정)** — 4-(8)의 수치를 **동종 프레임워크의 측정 사례**로 옆에 놓을 수 있다. 다만 하드웨어와 엔진 버전이 다르므로 직접 비교는 불가(6번 참조).

---

## 6. 쓰면 안 되는 주장

1. **⚠ 최우선 — 플레이어 경험·재미에 관한 주장에는 이 문헌을 쓸 수 없다.**
   - 초록 첫 문단의 **"This gameplay style develops an enthralling experience built around factors such as curiosity and challenge, besides the possibility of evolving, obtaining rare items, finding unusual usage to well-known abilities, and unprecedented encounters"**는 **연구의 동기를 서술한 배경 진술이지, 이 연구가 측정한 결과가 아니다.** 이 논문은 재미나 몰입을 측정한 적이 없다.
   - 이 논문이 실제로 수행한 평가는 (a) 인원 미상의 포커스 그룹에서 0–10점 총평을 받은 것, (b) FPS와 RAM을 측정한 것, 두 가지뿐이다. 결론에서 저자들이 주장한 것도 "feasible, useful, and efficient"(§VI, p. 844) 세 가지뿐이다.
   - 따라서 **"Oliveira 외(2020)에 따르면 메트로베니아의 공간 확장 구조는 플레이어에게 몰입감/재미를 준다"**, **"호기심과 도전을 유발한다"** 같은 문장은 **쓸 수 없다.** 같은 이유로 §II-B의 "her curiosity is poised to revisit locations"도 이 연구의 측정 결과가 아니라 게임 위키([18]/각주 6)와 선행문헌을 종합한 장르 기술이다.
   - 안전한 형태: **"Oliveira 외(2020)는 메트로베니아 장르의 특징으로 …를 들었다"**(장르 기술의 인용), **"Oliveira 외(2020)는 … 아키텍처를 제안하고 프로토타입 1개로 성능을 검증했다"**(수행 내용의 인용).

2. **포커스 그룹 결과를 정량적 근거로 인용할 수 없다.** 참가자 수, 모집 방법, 인구통계, 설문 도구가 **전부 논문에 없다**. "참가자들이 10점 만점을 줬다"는 문장은 표본 크기를 알 수 없으므로 우리 논문에 옮기면 안 된다. 게다가 저자들 스스로 "We limited user tests before presentation to the focus groups"(§IV, p. 843)라고 밝혔고, 참가자들이 보스 AI 밸런스와 태깅 메커닉 숙달 시간을 "the worst part"로 꼽았다고 적었다(§V-A, p. 843).

3. **"프레임워크가 여러 게임에서 검증되었다"고 쓸 수 없다.** 프로토타입은 **1개**다(3절 참조). 일반성·재사용성은 주장되었을 뿐 시험되지 않았다.

4. **성능 수치를 우리 4장의 비교 기준으로 직접 쓸 수 없다.** 측정 장비가 2020년 기준으로도 구형인 i5 3330 / Radeon R7750 / Ubuntu 17.04이고, 2D 측면 스크롤 프로토타입이며, Unity 버전이 명시되어 있지 않다(※ 원문에서 확인하지 못함). 우리 게임은 Unity 6, 2.5차원, URP 사용자 정의 렌더 패스를 쓰므로 조건이 전혀 다르다. **"동종 연구에서도 NPC 수에 따른 FPS 저하를 측정했다"는 방법론적 선례**로만 쓸 것.

5. **"메트로베니아 게임은 활성 적이 30개를 넘지 않는다"를 사실로 쓸 수 없다.** 출처 없는 저자들의 단언이다(4-(8) 참조).

6. **초록의 9개 구성요소를 "장르의 정의"로 쓸 수 없다.** 이것은 **저자들이 자기 프레임워크에 넣은 구성요소 목록**이지, 메트로베니아가 갖춰야 할 요건을 실증한 결과가 아니다. 또 그중 inventory·skill tree·puzzle-solving은 본문에 설계 절조차 없다(4-(1) 표 참조).

7. **"Wahlberg(2015)의 결론이 Oliveira 외(2020)에 의해 검증되었다"고 쓸 수 없다.** Oliveira 외는 Wahlberg를 **관련연구로 요약 인용**했을 뿐 재현·검증하지 않았다(4-(3) 참조).

8. **이 논문을 "저널 논문"이나 "peer-reviewed journal article"로 소개하면 안 된다.** SBGames 2020 학술대회 논문집의 **Industry Track full paper**다. 우리 논문 참고문헌과 본문 서술에서 학술대회 논문임을 정확히 표기할 것.

9. **Unity 버전, 프로토타입의 장르적 세부(2D인지 2.5D인지), 코드 공개 여부는 원문에 없다.** ※ 원문에서 확인하지 못함. 추측해서 쓰지 말 것.

---

## 7. 참고문헌 항목

```
Oliveira, B. P., Franco, A. de O. da R., Silva, J. W. F. da, Gomes, F. A. de C., & Maia, J. G. R. (2020). A framework for Metroidvania games. In SBC – Proceedings of SBGames 2020 (Industry Track – Full Papers, pp. 836–844). Sociedade Brasileira de Computação.
```

※ 한국 학회 양식(IEEE 스타일)으로 제출할 경우:

```
B. P. Oliveira, A. de O. da R. Franco, J. W. F. da Silva, F. A. de C. Gomes, and J. G. R. Maia, "A framework for Metroidvania games," in Proc. SBGames 2020 (Industry Track – Full Papers), Recife, Brazil, Nov. 2020, pp. 836–844.
```

⚠ DOI는 원문에 없다. ※ 원문에서 확인하지 못함. 제출 전 SBGames 논문집 아카이브에서 확인할 것.
