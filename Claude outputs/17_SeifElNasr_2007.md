# Seif El-Nasr, Niedenthal, Knez, Almeida & Zupko(2007) — Dynamic Lighting for Tension in Games

## 1. 서지사항

- **정식 인용**: Seif El-Nasr, M., Niedenthal, S., Knez, I., Almeida, P., & Zupko, J. (2007). Dynamic lighting for tension in games. *Game Studies, 7*(1). http://gamestudies.org/0701/articles/elnasr_niedenthal_knez_almeida_zupko
- **파일**: 렌더링_최적화/Game_Studies_-_Dynamic_Lighting_for_Tension_in_Games.pdf
- **종류**: **학술지 논문** — *Game Studies: the international journal of computer game research*, Volume 7, Issue 1, August 2007, ISSN 1604-7982. 온라인 저널이므로 **쪽 번호가 없다**(추출 PDF의 "1/12"~"12/12"는 브라우저 인쇄 쪽수이지 저널 쪽수가 아니다).
- **서지 세부 (원문에서 확인)**:
  - 머리말 영역에 "the international journal of computer game research / volume 7 issue 1 / August 2007 / ISSN:1604-7982"가 찍혀 있다.
  - 저자 5인과 소속이 원문 좌측 프로필란에 전부 명시:
    - **Magy Seif El-Nasr** — Assistant Professor, College of Information Sciences and Technology, Pennsylvania State University, USA. RAEL(Real-time Aesthetic and Experience Lab) 공동 디렉터.
    - **Simon Niedenthal** — Associate Professor, School of Arts and Communication (K3), Malmö University, Sweden. 논문 "Shadowplay: Simulated illumination in game worlds"로 DiGRA 2005 Best Paper 수상.
    - **Igor Knez** — Associate Professor of psychology, Centre for Built Environment / Laboratory of Applied Psychology, University of Gävle, Sweden. 인지심리학 박사(Uppsala University).
    - **Priya Almeida** — Project Engineer, Impact Technologies, Rochester, NY, USA. Pennsylvania State University 전기공학 석사(2005). 석사 논문 'Identifying low level patterns in movies and video games that influence emotions and moods'가 본 논문에 기여.
    - **Joseph Zupko** — Ph.D. candidate, College of Information Sciences and Technology, Pennsylvania State University, USA.
  - 저작권: "©2001 - 2007 Game Studies ... By virtue of their appearance in this open access journal, articles are free to use, with proper attribution, in educational and other non-commercial settings."
  - ※ 원문에서 확인하지 못함 — DOI(Game Studies는 DOI를 부여하지 않는다), 논문 번호, 저널 쪽 범위.
- **등급 메모**:
  - *Game Studies*는 게임학 분야에서 자리가 확립된 **동료심사 오픈액세스 학술지**다. 출처의 무게 자체는 문제없다.
  - **문제는 저널 등급이 아니라 분과다. 이 논문은 게임 미학·정서 연구이지 렌더링 기법 논문도, 성능 논문도 아니다.** 6번을 반드시 읽을 것.
  - **또 하나의 문제: 이 논문의 경험적 검증은 저자들 자신이 "informal study"라고 부르는 것이다.** 설문도 없고 측정도 없고 통계도 없다. 3번과 4-(4)를 반드시 읽을 것.
  - 인용할 때 반드시 **5인 공저**로 적을 것. 흔히 "Seif El-Nasr et al."로 축약되지만 참고문헌 항목에는 5인을 모두 적어야 한다. 제1저자 성은 **"Seif El-Nasr"** 전체가 성이다(El-Nasr만 적으면 안 된다).

## 2. 한 줄 요약

영화 30편 이상을 질적으로 분석해 긴장(tension)의 고조·이완과 관련된 조명 패턴 열두 가지를 도출하고, 이를 게임 상태에 따라 실시간으로 적용하는 시스템 TDELE(ELE의 시간축 확장)을 Unreal 2.5 엔진에 통합해 Unreal Tournament 2004 모드로 구현한 뒤, CHI 2005 데모 부스에서 100명 남짓과 나눈 **비공식(informal) 관찰·대화**로 반응을 수집한 게임 미학 연구다. 반응은 갈렸다 — 비FPS 플레이어는 좋아했고, **숙련 FPS 플레이어 상당수는 불편해하며 "게임을 통제하지 못하는 느낌"을 받았다고 답했다.**

## 3. 이 연구가 실제로 한 것

이 논문은 성격이 다른 세 부분이 이어 붙어 있다. **각 부분의 검증 수준이 전혀 다르므로 구분해서 읽어야 한다.**

### (A) 영화 조명 패턴의 질적 분석 — 패턴 도출

- **방법**: 질적 분석(qualitative study and analysis).
- **표본**: **영화 30편 이상.** 장르는 Horror, Science Fiction, Drama 등. 원문에 예시로 열거된 작품: *The Cook, The Thief, His Wife and Her Lover*, *Equilibrium*, *Shakespeare in Love*, *Citizen Kane*, *The Matrix*.
  > "We have performed a qualitative study and analysis of over thirty movies within several genres, including Horror, Science Fiction and Drama."
- **분석자**: **연구자 2명.** 그중 1명은 **극장 조명 디자이너로 2년간 훈련**받은 경력이 있다고 밝힌다.
  > "In addition, this study was performed by two researchers, one of whom has spent two years of training as a theatre lighting designer, and thus has gained tacit knowledge of theatre lighting design."
- **한계**: 코딩 체계, 분석 단위, 신뢰도(inter-rater reliability), 분석 절차가 **일절 보고되지 않는다.** 영화 30편의 전체 목록도 없다. 사실상 **두 연구자의 전문가적 판단에 의한 패턴 정리**다.

### (B) 시스템 설계·구현 — ELE와 TDELE

- **ELE (Expressive Lighting Engine)**: 본 논문의 저자 중 1인의 선행 연구(Seif El-Nasr & Horswill, 2004). 영화·연극 조명 이론에 기반한 **자동 지능형 조명 시스템**이다.
  - 입력: 무대 배치 또는 씬 그래프, 캐릭터 위치, 창·횃불·램프 등 국소 발광 소품, 스타일 파라미터(low-key/high-key, 원하는 깊이값, 방향, 전체 대비 수준, 전체 팔레트, 특정 영역의 이상적 채도·온도·강도·색조), 장면의 극적 강도(dramatic intensity).
  - 출력: 사용할 광원의 **개수**, 각 광원의 **색·각도·위치·감쇠** 등. 이 값을 렌더링 엔진에 넘겨 프레임을 렌더링한다.
  - 동작: 씬을 **n개의 원통형 영역**으로 나눈 뒤 **focus / non-focus / background** 세 종류로 분류한다. 조명 디자인 규칙을 **수학적 최적화 함수**로 표현하고, **제약 비선형 최적화(constrained nonlinear optimization)**로 각 광원의 색을 고른다.
    > "These rules are represented mathematically in an optimization function. The use of optimization is important to balance conflicting lighting design goals."
- **TDELE (Temporal Dynamic Expressive Lighting Engine)** — **의뢰에서 이름을 확인하라고 한 시스템.** 원문에 정확히 이 표기로 있다.
  - 정체: ELE의 **시간축 확장**. 본 논문에서 새로 개발한 부분이다.
    > "In order to account for the temporal dimension of lighting, we expanded ELE developing another system called Temporal Dynamic Expressive Lighting Engine (TDELE)."
  - 동작: 틱(시뮬레이션 시간)과 **과거에 사용된 조명 색 구성의 이력**(대비 값, 대비 유형, 사용된 색조 등)을 상태로 유지한다. 이 상태 정보와 원하는 패턴·원하는 긴장 수준으로부터 **현재 긴장 값을 조명 이력을 이용해 계산**하고, 원하는 채도·온도·대비 수준 같은 제약값을 산출해 **ELE에 넘겨 현재 프레임을 조정**한다.
    > "This system includes a state that keeps track of ticks (simulation time) as well as the history of lighting colour compositions used in the past, in terms of the contrast value, contrast type, hues used, etc. Based on this state information, the desired pattern and the desired tension level, the system calculates current tension value using history of lighting values. It also calculates values of constraints, including desired saturation level, desired warmth value and desired contrast level within an environment. These values are then fed to ELE to manipulate the current frame."
  - **통합 대상 엔진**: **Unreal 2.5 Engine**. 레벨 에디터 "Unreal Edit" 안에 인터페이스를 추가해, 개발자가 특정 게임 상태에 원하는 조명 패턴을 트리거할 수 있게 했다. 디자이너가 **자신의 긴장 공식(tension formula)**을 만들어 패턴에 연결하는 것도 가능하다.
    > "We have integrated this system with the Unreal 2.5 Engine (Seif El-Nasr et al., 2005). We added an interface within the Unreal Tournament Level Editor, 'Unreal Edit,' to enable developers to trigger a desired lighting pattern given a specific game state."
- **프로토타입**: **Unreal Tournament 2004 (Epic, 2004)의 모드로 만든 일인칭 슈터 1종.** 조명 규칙은 "위험도의 함수로 환경 광원의 온도와 채도의 affinity를 올리고 내리는" 방식이었다.
  > "In particular, we increased and decreased affinity warmth and saturation of lights' colours within the environment as a function of how dangerous the situation is to the user. Therefore, if the player is confronted with many monsters and his health is dropping over time, the warmth and saturation of colour will increase over time showing an increase in tension. While if the player is killing monsters, and danger level is diminishing, the warmth and saturation will decrease through time, releasing tension."
  - **HUD를 제거했다는 점이 중요하다.** 조명 자체가 정보 전달 역할을 대신했기 때문이다.
    > "It should be noted that we removed the original HUD of the Unreal Tournament 2004 game because the lighting itself gave the player the information he/she needed through the patterns used."
- **한계**: 성능 측정이 **전혀 없다.** 프레임률, 렌더링 비용, 광원 수의 상한, 최적화 연산의 소요 시간 — 어느 것도 보고되지 않는다. 구현 가능성 자체를 보인 프로토타입이다.

### (C) 검증 — **비공식 연구(informal study). 이 논문의 가장 약한 고리다.**

- **장소·형식**: **CHI 2005 (Computer Human Interaction Conference 2005)의 "Interactivity venue"에서의 인터랙티브 데모.** 노트북 2대에 같은 게임을 띄워 한 대는 TDELE 적용, 다른 한 대는 정적 조명으로 두고 비교하게 했다.
- **모집 방식**: **자원 참여(voluntary study).** 전단지를 나눠 주고 게임 설명을 한 뒤, 원하면 두 버전을 모두 플레이하고 경험을 이야기해 달라고 요청했다. 참가자에게는 **한쪽이 강화된 시간적 조명을, 다른 쪽이 정적 조명을 쓴다는 사실을 미리 알렸다**(블라인드가 아니다).
- **표본 규모**: **"100명 이상"이라고 적혀 있으나, 그 근거가 나눠 준 전단지 수다.**
  > "Through observation and interaction with over 100 participants who played the game (judging by the number of flyers we handed out), we collected several interesting responses."

  → **참가자 수를 직접 센 것이 아니다.** 실제로 플레이한 사람 수, 두 버전을 모두 플레이한 사람 수, FPS 숙련자와 비숙련자의 비율 — 어느 것도 보고되지 않는다.
- **데이터 수집**: **설문 없음. 측정 없음. 통계 없음.** 저자들이 직접 이렇게 쓴다:
  > "Since we ran this as an informal study, we did not ask participants to fill out surveys. However, we noted their responses."
- ※ 원문에서 확인하지 못함 — 실제 플레이어 수, 인구통계, 플레이 시간, 숙련도 분류 기준, 응답을 기록·분류한 방법, 윤리 심의 여부.

## 4. 인용 가능한 내용

### (1) 도출된 조명 패턴의 개수와 성격 — **의뢰에서 확인하라고 한 항목**

**핵심 확인 사항: 원문은 "열두 개의 패턴(twelve patterns)"을 도출했다고 밝히고, 그것을 다섯 개 범주로 분류해 제시한다. 열거된 항목은 다섯 개이지 열두 개가 아니다.**

> "Based on these observations, we have identified twelve patterns. We categorize these patterns into the following:
> 1. patterns that subject an audience to low contrast images followed by high contrast images (in terms of brightness contrast or warm/ cool colour contrast) increase projected tension
> 2. patterns that subject an audience to low affinity of colour (in terms of saturation/ brightness/ warmth, followed by high affinity of colour, in terms of saturation/ brightness/ warmth) increase projected tension
> 3. patterns that subject an audience to high contrast images followed by low contrast images (where contrast is defined in terms of brightness or warm/ cool colours) releases projected tension
> 4. patterns that subject an audience to high affinity of colour (in terms of saturation/ brightness/ warmth, followed by low affinity of colour, in terms of saturation/ brightness/ warmth) releases projected tension
> 5. patterns that subject an audience to a long duration of high contrast or high affinity of colour (in terms of saturation/ brightness/ warmth) causes an increase in projected tension."

**주의: 열두 개 패턴의 개별 목록은 원문에 제시되지 않는다.** 다섯 개 범주만 제시된다. 따라서 우리 논문에서 "열두 가지 패턴을 제시하였다"고 쓰는 것은 원문에 부합하지만, **그 열두 개가 무엇인지는 이 논문에서 확인할 수 없다.** 안전하게 쓰려면 "열두 가지 패턴을 도출해 다섯 범주로 분류하였다"라고 적는다.

**패턴의 기본 성격**: 저자들은 "lighting pattern"을 이렇게 정의한다.
> "We define a lighting pattern as a specific configuration of these basic elements of light and interrelation, occurring over time, and having an effect upon the viewer or player."

빛의 기본 특성은 다섯 가지로 열거된다: brightness(luminance), colour, hard or soft shadow quality, direction, variation over time.

**두 유형 구분**: 패턴은 크게 두 종류다.
> "The first is a colour composition sustained over time, where the composition and its lack of change over time causes an escalation of projected tension. The other is a group of specific variations across colour patterns in time, where the variation in a specific order or pattern causes rise or fall of projected tension."

**패턴은 영화에서 도출된 것이다.** 각주 2에 단서가 달려 있다:
> "While these patterns are identified as film patterns, they have been used in theatre lighting as well, and thus are not limited to film. However, since the study is based on film examples, we will use the words cinematic patterns and film patterns."

한국어 간접인용 예시:
> Seif El-Nasr 외(2007)는 영화 30편 이상을 질적으로 분석하여 긴장의 고조와 이완에 관여하는 조명 패턴 열두 가지를 도출하고 이를 다섯 범주로 분류하였다.

### (2) TDELE의 이름·동작 방식·통합 엔진

3-(B)에 원문 인용을 그대로 옮겨 두었다. 요약하면:
- **이름**: Temporal Dynamic Expressive Lighting Engine (TDELE). ELE(Expressive Lighting Engine)의 시간축 확장.
- **동작**: 시뮬레이션 틱과 과거 조명 색 구성 이력을 상태로 유지 → 현재 긴장 값 계산 → 채도·온도·대비 제약값 산출 → ELE에 전달 → ELE가 제약 비선형 최적화로 각 광원의 색을 결정.
- **엔진**: Unreal 2.5 Engine. Unreal Edit에 인터페이스 추가.
- **적용 게임**: Unreal Tournament 2004 모드로 만든 일인칭 슈터.

한국어 간접인용 예시:
> Seif El-Nasr 외(2007)는 게임 상태에 따라 조명을 실시간으로 조정하는 TDELE(Temporal Dynamic Expressive Lighting Engine)을 Unreal 2.5 엔진에 통합하여, 위험 수준의 함수로 환경 광원의 색온도와 채도를 변화시키는 일인칭 슈터 프로토타입을 구현하였다.

### (3) 동적 조명을 쓰는 이유 — 게임이 영화·연극과 다른 점

**우리 3.9절 첫 문단(조명 설계 동기)에 가장 잘 맞는 대목이 이것이다.**

> "While many lighting principles can be borrowed from film and theatre lighting design theories, the interactive nature of games distinguishes them substantially from film and theatre. Game environments are dynamic and unpredictable due to the interactive freedom afforded to users within the world, thus narrative context, users' positions and perspectives within the gameworld-crucial parameters to the calculation of lighting-cannot be assumed. Therefore, in this article, we argue for the use of dynamic lighting. Dynamic lighting is a type of simulated lighting where lighting calculations are computed in real time."

> "Therefore, using dynamic lighting enables on-the-fly lighting calculations accounting for real-time variations, such as change in game state, narrative, player's and characters' positions and camera movement. This practice privileges interaction, emotion and dramatic content, as opposed to the current methods that tend to rely on static lighting to emphasis virtual space."

**당시 게임의 관행에 대한 진술 (2007년 기준임에 주의)**
> "Even though games use the lighting patterns identified by the study above, these patterns are often experienced in time through virtual space; their variation is often dependant upon player movement from one environment to another. While game state and tension points vary depending on gameplay, most game environments are currently built with static lighting allowing very little variation to account for tension or state change."

한국어 간접인용 예시:
> 게임은 플레이어의 자유로운 이동 때문에 서사 맥락과 시점을 미리 가정할 수 없으므로, 영화·연극의 조명 원리를 그대로 옮기기 어렵고 실시간으로 계산되는 동적 조명이 필요하다(Seif El-Nasr 외, 2007).

### (4) **"informal study"라고 스스로 부르는 대목 — 반드시 인용문으로 남겨야 할 부분**

의뢰에서 찾으라고 한 문장이 원문에 **명확히 존재한다.**

> "Since we ran this as an informal study, we did not ask participants to fill out surveys. However, we noted their responses."

참가자 수의 근거가 전단지 수라는 대목도 함께 남긴다.

> "Through observation and interaction with over 100 participants who played the game (judging by the number of flyers we handed out), we collected several interesting responses."

결론부에서도 "informal study"라는 표현을 반복한다.

> "In conclusion, we would like to discuss the implication of this prototype on game aesthetics by reflecting on the responses collected in the informal study."

**판정: 이 논문의 경험적 부분은 저자 스스로 비공식 연구로 규정한 관찰·대화 기록이다. 설문·측정·통계가 없다. "동적 조명이 긴장감을 높인다"는 인과 주장을 이 문헌으로 할 수 없다.**

### (5) **숙련 FPS 플레이어의 부정적 반응 — 의뢰에서 가장 중요하다고 한 부분**

원문에 해당 대목이 **명확히 존재한다.** 전문을 그대로 옮긴다.

> "We also had several experienced FPS gamers play the two versions of the game. We got very different responses from gamers than non-gamers. Some gamers commented that the dynamic lighting gave them too much information and that made the game too easy. Many others were disturbed by the lighting. One observation made was that many FPS players seem to try to get themselves emotionally detached from the game. Using the lighting patterns described in this article tends to escalate arousal subconsciously, and thus might have attached players emotionally to the game. Some commented that this effect made them feel as if they were not in control of the game."

**여기서 확인되는 부정적 반응은 세 종류다.**
1. **정보 과다 → 게임이 너무 쉬워졌다** ("gave them too much information and that made the game too easy")
2. **조명 자체가 거슬렸다** ("Many others were disturbed by the lighting")
3. **정서적으로 조작당해 통제권을 잃은 느낌** ("made them feel as if they were not in control of the game")

**세 번째에 대한 저자들의 해석**도 그대로 남겨 둔다 — 저자들은 FPS 플레이어들이 **의도적으로 게임에서 정서적으로 거리를 두려 하는데**, 이 조명 패턴이 무의식적으로 각성을 높여 그 거리를 무너뜨렸기 때문이라고 추정한다("might have"라는 추측형에 주목).

**대조되는 긍정적 반응 (비FPS 플레이어)**도 함께 적어 두어야 대비가 정확해진다.

> "An interesting outcome was that many non-first-person shooter (FPS) players loved the game with the dynamic lighting and liked the effect of the lighting. Some noted that it was beautiful and more aesthetically pleasing to play with the dynamic lighting than with the static lighting. In addition, some commented that they saw the lighting as a method of portraying game state information, which was unique in their experience."

**결론부에서 저자들이 이 양분된 반응을 어떻게 정리하는지가 결정적이다.**

> "The range of player responses-some found the lighting to be disturbing, fearing loss of control, while others found it beautiful-is a validation of the impact of dynamic simulated illumination on audiences' affect. Clearly, strong emotions can be evoked by dynamic lighting. However, the nature of the affective responses is dependent on individual difference, preferences and previous gaming experiences, as is evident by the range of comments collected."

**이 문장을 정확히 읽어야 한다.** 저자들이 검증되었다(validation)고 말하는 것은 **"동적 조명이 정서에 영향을 준다"는 것까지**이고, **"긴장감을 높인다"거나 "경험을 개선한다"는 것이 아니다.** 오히려 바로 다음 문장에서 **반응의 성격이 개인차·선호·이전 게임 경험에 달려 있다**고 명시한다.

한국어 간접인용 예시 (우리 논문에 그대로 쓸 수 있는 형태):
> Seif El-Nasr 외(2007)의 비공식 관찰에서는 동적 조명에 대한 반응이 갈렸다. 비FPS 플레이어들은 미적으로 만족스럽다고 평가한 반면, 숙련 FPS 플레이어들 가운데 상당수는 조명이 거슬린다고 하거나 정보가 과다해 게임이 쉬워졌다고 답했으며, 일부는 게임을 통제하지 못하는 느낌을 받았다고 보고하였다. 저자들은 정서적 반응의 성격이 개인차와 이전 게임 경험에 의존한다고 정리하였다.

### (6) 서바이벌 호러의 조명과 '불명료성(obscurity)' — 우리 게임과 직접 맞닿는 대목

**우리 게임은 손전등으로 시야가 제한되는 구조이므로, 이 대목이 3.9절 첫 문단에 가장 잘 맞는다.**

> "An example of lighting patterns in games can be seen in the way lighting contributes to gameplay in survival horror games such as those in the 'Silent Hill' and 'Resident Evil' series. One key way in which survival horror games create their emotional effect is by maintaining a state of player vulnerability, often by suspending the player in a state of incomplete knowledge. The perceptual conditions for this state of vulnerability are enhanced through visual obscurity. Obscurity supports a sense of vulnerability (uncertainty) and is thrilling because it is makes the object of terror indistinct. It should be noted that the opposite of obscurity is not light, but clarity; thus, obscurity can be produced by anything that thwarts clear perception: darkness, atmospheric phenomena (such as fog) or occlusion (blocking by architectural objects)."

구체적 사례 분석:
> "The sorts of illumination contrasts that one experiences in Resident Evil 4 (Capcom, 2005) and Silent Hill 2 (Konami, 2001) are day/ night, light/ dark, and warm/ cool. Both Resident Evil 4 and Silent Hill 2 exhibit a similar day/ night cycle over the game as a whole, beginning in the daytime, followed by dusk and night and completing at dawn or sunrise. ... Bright and dark sequences do exhibit a sort of logic in Resident Evil 4, the darkest spaces occur when one is playing Ashley: the character with the fewest resources and greatest vulnerability (Niedenthal, submitted)."

**주의**: 이 사례 분석 부분의 근거는 **"Niedenthal (submitted)"**로 표기되어 있다. 즉 **당시 미게재 원고**다. 이 대목을 깊이 의존하려면 해당 논문을 따로 찾아야 한다. 가볍게 참조하는 정도라면 무방하다.

한국어 간접인용 예시:
> 서바이벌 호러 게임은 플레이어를 불완전한 정보 상태에 묶어 취약함을 유지하며, 이 지각적 조건은 시각적 불명료성(obscurity)을 통해 강화된다. 불명료성의 반대는 빛이 아니라 명료함이므로, 어둠뿐 아니라 안개나 구조물에 의한 가림도 같은 효과를 낸다(Seif El-Nasr 외, 2007).

### (7) 시뮬레이션 조명이 정서에 영향을 준다는 선행 근거 — 재인용 주의

> "initial findings suggest that, just like light in real space, simulated illumination in virtual space has a direct effect upon participants' emotional experiences. Knez and Niedenthal (forthcoming) have demonstrated that warm and cool simulated illumination conditions have differing emotional and performance effects upon players navigating a virtual maze created in the 'Half Life 2.0' game engine."

**주의**: 근거가 **"Knez and Niedenthal (forthcoming)"** — 본 논문 게재 시점에 *CyberPsychology and Behavior*에 게재 예정이던 원고다. **우리 논문이 "조명이 플레이어 정서에 영향을 준다"는 주장을 하려면 이 문헌을 재인용하지 말고 실제 게재본을 찾아 직접 인용해야 한다.** 재인용은 출처의 무게를 떨어뜨린다.

### (8) 대비·친화도(affinity)의 개념 — 용어 정의가 필요할 때

> "Brightness contrast is a term we use to denote the difference between brightness of different areas in the scene. High brightness contrast denotes high difference between brightness in one or two areas in a shot and the rest of the shot. This effect is not new; it was used in paintings during the Baroque era and was termed 'chiaroscuro' which is an Italian word meaning light and dark."

색 친화도의 시간적 효과에 대한 저자들의 설명 — **이것은 가설임에 주의("We believe that")**:
> "We believe that the temporal factor is key to the effect of this approach; this is due to the nature of the eye. The eye tries to balance the projected colour to achieve white colour. Hence, when projected with red colour, the eye will try to compensate the red with cyan to achieve white colour. This causes eye fatigue, which in turn affects the participant's stress level, thus affecting arousal."

심리물리학 문헌으로 일부 확인되었다는 진술:
> "The impact of some of these patterns on tension projection has been confirmed experimentally in the psychophysics literature. For example, the effect of prolonged exposure of saturated warm colours can cause increased arousal as discussed in MacEvoy (2001)."

**주의**: "some of these patterns"이다. **열두 패턴 전부가 실험으로 확인되었다는 뜻이 아니다.** 그리고 MacEvoy(2001)는 *Handprint Media*의 "Light and the Eye"로, 참고문헌상 **학술지 논문이 아니다.**

## 5. 우리 논문에서 쓸 자리

- **3.9절 첫 문단(조명 설계 동기) — 이것이 이 문헌의 주된 자리다.**
  두 가지 방향으로 쓸 수 있다.

  **(가) 동적 조명이 필요한 이유** (4-(3))
  > 게임은 플레이어의 자유로운 이동 때문에 시점과 서사 맥락을 미리 가정할 수 없으므로, 영화·연극의 조명 기법을 정적으로 이식하기 어렵다. Seif El-Nasr 외(2007)는 이러한 이유로 실시간으로 계산되는 동적 조명의 필요성을 주장하였다.

  **(나) 시야 제한과 취약감의 관계** (4-(6)) — 우리 게임의 손전등 설계와 직결된다.
  > 서바이벌 호러 게임은 플레이어를 불완전한 정보 상태에 두어 취약감을 유지하며, 이는 시각적 불명료성을 통해 강화된다(Seif El-Nasr 외, 2007). 본 연구의 손전등 기반 시야 제한도 같은 설계 의도를 따른다.

  **(나)가 (가)보다 우리 게임에 더 정확히 맞는다.** 우리 게임의 조명은 긴장 수준에 따라 색온도를 바꾸는 TDELE식 시스템이 아니라 **시야를 제한하는 구조**이기 때문이다.

- **2장(관련연구) — 게임 조명 연구의 계보 한 줄.**
  > 게임 내 조명이 플레이어의 정서에 미치는 영향은 게임 미학 연구에서 꾸준히 다루어져 왔다(Seif El-Nasr 외, 2007).

- **5장(한계 또는 향후 과제) — 조명 강화의 역효과 가능성.**
  4-(5)를 근거로, 우리 조명 설계가 모든 플레이어에게 같은 효과를 낸다고 가정할 수 없다는 점을 적을 수 있다. **이것이 이 문헌의 두 번째로 유용한 용도다.**
  > Seif El-Nasr 외(2007)의 관찰에서는 숙련 FPS 플레이어 가운데 동적 조명이 거슬린다거나 게임을 통제하지 못하는 느낌을 준다고 응답한 사례가 보고되었다. 조명을 통한 정서 조절이 모든 플레이어에게 같은 방향으로 작용한다고 보기 어려우며, 본 연구의 조명 설계도 플레이어 집단에 따른 반응 차이를 검증하지 못하였다.

## 6. 쓰면 안 되는 주장

### (1) **성능이나 구현 기법을 말하는 문장에 붙일 수 없다 — 가장 중요하다**

- **이 논문은 게임 미학·정서 연구다.** 렌더링 기법 논문도, 성능 논문도 아니다.
- **성능 데이터가 전혀 없다.** 프레임률, 렌더링 비용, 드로우 콜, 광원 수 상한, 최적화 연산 시간 — **하나도 보고되지 않는다.**
- 따라서 다음 문장들에 이 문헌을 붙이면 **명백한 오인용**이다.
  - "동적 조명은 렌더링 비용이 높다/낮다"
  - "실시간 조명 계산은 성능에 부담을 준다"
  - "URP 사용자 정의 렌더 패스로 화면 공간 조명을 구현하였다" (기법 근거로)
  - "광원 수를 N개로 제한하였다" (근거로)
  - 우리 4장의 어떤 성능 수치에도
- **셰이더, 렌더 파이프라인, 화면 공간 기법, 그림자 알고리즘에 대한 서술은 이 논문에 없다.** ELE가 하는 일은 "광원의 개수·색·각도·위치를 고르는 것"이고, 그 값을 넘겨받는 **렌더링 엔진은 별개**다.
  > "These parameters are given to a rendering engine to render the frame."

### (2) "동적 조명이 긴장감을 높인다"고 단정할 수 없다 — **우리 논문에서 가장 조심해야 할 지점**

- 검증이 **informal study**다. 설문·측정·통계가 없다 (4-(4)).
- 참가자 수의 근거가 **나눠 준 전단지 수**다. 실제로 몇 명이 플레이했는지 모른다.
- **블라인드가 아니다.** 참가자에게 어느 쪽이 "강화된 조명"인지 미리 알렸다.
- **반응이 갈렸다.** 숙련 FPS 플레이어들은 부정적이었다 (4-(5)).
- 저자들 스스로 검증되었다고 말하는 범위는 **"동적 조명이 정서에 영향을 준다"**까지이고, 곧바로 **"반응의 성격은 개인차·선호·이전 게임 경험에 달려 있다"**고 단서를 단다.
- **따라서 "동적 조명이 긴장감을 높인다", "조명이 몰입을 개선한다", "조명 변화가 플레이어의 긴장을 조절한다"는 단정형 문장을 이 문헌으로 쓸 수 없다.**
- 쓸 수 있는 형태는 다음 정도다.
  - "동적 조명이 플레이어의 정서에 영향을 준다는 관찰이 보고된 바 있다"
  - "조명을 통한 긴장 조절이 제안되어 왔다"
  - "Seif El-Nasr 외(2007)는 조명 패턴과 긴장의 관계를 제안하고 프로토타입으로 구현하였다"

### (3) 열두 패턴의 개별 내용을 적을 수 없다

원문은 "twelve patterns"를 도출했다고 밝히지만 **다섯 범주만 제시**한다. 열두 개의 목록은 없다. 특정 패턴을 지목해 서술하려면 다른 출처가 필요하다. ※ 원문에서 확인하지 못함.

### (4) 영화 30편의 분석 결과를 일반 법칙으로 쓸 수 없다

- 코딩 체계, 분석 절차, 신뢰도, 전체 작품 목록이 **보고되지 않았다.**
- 분석자는 2명이고, 그중 1명의 연극 조명 디자인 경험(2년)이 근거의 일부로 제시된다. **전문가 판단에 기반한 질적 정리**다.
- "영화에서 검증된 조명 문법"이라는 식으로 쓰면 과장이다.

### (5) 2007년 시점의 기술 현황 서술을 현재형으로 옮길 수 없다

- "most game environments are currently built with static lighting" — **2007년의 진술**이다. 2026년의 Unity URP 환경에는 해당하지 않는다.
- 통합 엔진도 **Unreal 2.5**다. 우리는 **Unity 6**를 쓴다. **엔진 세대가 근본적으로 다르다.**
- **"현재 대부분의 게임이 정적 조명을 쓴다"는 취지로 이 문헌을 인용하면 심사에서 바로 지적된다.**

### (6) 재인용에 의존할 수 없는 항목들

- **Knez & Niedenthal** — 본 논문 시점에 "(forthcoming)". 조명의 정서 효과를 실증하려면 게재본을 직접 찾아야 한다.
- **Niedenthal (submitted)** — Resident Evil 4 / Silent Hill 2 분석의 출처. 당시 미게재 원고.
- **MacEvoy (2001)** — 참고문헌상 *Handprint Media*의 웹 자료. 학술지 논문이 아니다.
- 세 건 모두 **이 논문을 통한 재인용으로 근거를 삼으면 안 된다.**

### (7) TDELE를 우리가 구현했다는 식으로 읽히게 쓸 수 없다

우리 게임의 조명은 **URP 사용자 정의 렌더 패스로 구현한 화면 공간 조명**이고, 손전등으로 시야를 제한하는 구조다. **게임 상태에 따라 광원의 색온도·채도를 최적화 함수로 결정하는 TDELE와는 전혀 다른 시스템이다.** 3.9절에서 이 문헌을 인용할 때, 우리 구현이 TDELE 계열인 것처럼 읽히는 배치를 피해야 한다.

### (8) 프로토타입 1개, 장르 1개라는 점을 감출 수 없다

검증 대상은 **Unreal Tournament 2004 모드로 만든 일인칭 슈터 1종**이다. **우리 게임은 2.5차원 횡스크롤이다.** 시점 구조가 다르면 조명이 플레이어에게 전달하는 정보의 성격 자체가 달라진다. 이 문헌의 관찰을 우리 장르에 그대로 옮길 수 없다.

## 7. 참고문헌 항목

```
Seif El-Nasr, M., Niedenthal, S., Knez, I., Almeida, P., & Zupko, J. (2007). Dynamic lighting for tension in games. Game Studies, 7(1). http://gamestudies.org/0701/articles/elnasr_niedenthal_knez_almeida_zupko
```

(한국어 논문 양식으로 적을 경우 예시)

```
Seif El-Nasr, M., Niedenthal, S., Knez, I., Almeida, P. and Zupko, J., "Dynamic Lighting for Tension in Games," Game Studies, Vol. 7, No. 1, 2007.
```

(쪽 번호 없음 — 온라인 저널. 쪽수를 임의로 적지 말 것.)
