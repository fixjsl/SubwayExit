# Pichlmair & Johansen(2022) — Designing Game Feel. A Survey

## 1. 서지사항

- **정식 인용**: Pichlmair, M., & Johansen, M. (2022). Designing game feel: A survey. *IEEE Transactions on Games, 14*(2), 138–152. https://doi.org/10.1109/TG.2021.3072241
- **파일**: 게임필_관련/[2011.09201]_Designing_Game_Feel.A_Survey.txt (ar5iv 렌더링 HTML을 PDF로 출력한 것, 54쪽)
- **종류**: 학술지 (IEEE Transactions on Games, SCIE 등재 저널)
- **등급 메모**:
  - 저널 게재 논문이므로 출처의 무게에는 문제가 없다. 다만 **우리가 읽은 파일은 저널 최종본이 아니라 arXiv 프리프린트(arXiv:2011.09201 v1)의 ar5iv HTML 렌더링**이다. 쪽수 인용이 필요한 경우 저널판(138–152쪽)과 쪽 번호가 일치하지 않으므로, 직접인용을 넣을 때는 쪽수 대신 **절 번호(III-A8, IV-C 등)를 쓰는 편이 안전**하다. 본 메모의 모든 인용은 절 번호로 위치를 표시했다.
  - 이 논문은 **실증 실험이나 사용자 연구가 아니라 문헌 서베이**다. "이 기법이 효과가 있음이 검증되었다"는 식의 주장은 이 문헌으로 할 수 없다(6절 참조).

### 연도 확정 — 결론: **2022년**이 맞다. 현재 우리 논문의 "2021"은 틀렸으므로 고쳐야 한다.

지시받은 대로 원문(추출 텍스트) 전체를 검색했으나, **추출 텍스트 안에는 저널 게재 정보가 전혀 없다.** 해당 파일은 ar5iv가 arXiv v1을 렌더링한 것이고, 마지막 쪽에 "View original on arXiv (https://arxiv.org/abs/2011.09201)"만 있을 뿐 journal-ref 필드가 렌더링되지 않았다. 그래서 다음 두 가지 외부 근거로 확정했다.

**근거 1 (결정적) — 우리가 함께 읽은 05번 문헌의 참고문헌 목록.**
Bhatnagar 외(2025) FDG '25 논문의 참고문헌 [36]번 항목이 이 논문의 정식 서지사항을 그대로 싣고 있다. 즉 동료심사를 거친 다른 논문이 확인해 준 서지사항이다.

> [36] Martin Pichlmair and Mads Johansen. 2022. Designing Game Feel. A Survey.
> IEEE Transactions on Games 14, 2 (June 2022), 138–152. https://doi.org/10.1109/
> TG.2021.3072241 arXiv:2011.09201 [cs].
> — 파일 `게임필_관련__3723498.3723808.txt` 952–954행

**근거 2 — arXiv 초록 페이지(arxiv.org/abs/2011.09201)의 메타데이터.**
- Submission history: **[v1] Wed, 18 Nov 2020** (그래서 arXiv ID가 2011.xxxxx이다)
- Journal reference / DOI: **10.1109/TG.2021.3072241** (IEEE Transactions on Games)
- Comments: "26 pages, 4 figures"

**세 개의 연도가 돌아다니는 이유와 판단:**

| 연도 | 무엇인가 | 인용에 쓸 것인가 |
|---|---|---|
| 2020 | arXiv 최초 제출(2020-11-18) | ✗ 저널 게재본이 존재하므로 프리프린트로 인용할 이유가 없다 |
| 2021 | DOI 문자열(`TG.2021.…`)에 박힌 IEEE **Early Access** 연도 | ✗ **우리 논문이 지금 쓰고 있는 이 연도가 오류의 원인일 가능성이 높다.** DOI에 2021이 들어 있어 착각하기 쉽다 |
| **2022** | 정식 호(volume 14, issue 2, June 2022) 게재 연도, 138–152쪽 | ✓ **이걸 쓴다** |

**→ 조치: 본문 3.4절과 참고문헌 목록의 "Pichlmair & Johansen(2021)"을 전부 "Pichlmair & Johansen(2022)"로 바꿀 것.**

---

## 2. 한 줄 요약

게임 필(game feel) 관련 학술 연구와 실무자 문헌 200여 건을 모아 분류한 서베이로, 게임 필 디자인을 physicality / amplification / support 세 영역으로 나누고 각 영역의 폴리싱 행위를 tuning / juicing / streamlining으로 정식화한 어휘 체계를 제안한다.

---

## 3. 이 연구가 실제로 한 것

### 연구 방법
- **문헌 서베이(survey)**. 학술 연구와 실무자 출판물(블로그, 팟캐스트, 컨퍼런스 발표, 유튜브 영상 포함)을 함께 수집해 그 내용을 "제시된 디자인 목적(design purpose)"에 따라 분류했다.

> We analysed over 200 sources and categorised their content according to the design purpose presented. (Abstract)

> This survey paper gives an overview of the history, context, and state of the art of the understanding of game feel and how to design it. It is based on research in the field and publications by practitioners in order to capture both, conceptual and the practical knowledge. (§II 서두)

### 분석 대상과 표본 규모
- **200건이 넘는 출처(over 200 sources)**. 우리 논문의 "200여 개 문헌"이라는 표현은 원문과 일치한다.
- 실제 참고문헌 목록은 [1]–[213]까지 번호가 매겨져 있고, 여기에 게임 타이틀 목록([147]~[190]대 구간에 섞여 있음)이 포함된다.
- 다룬 디자인 요소는 Table I에 정리되어 있고, 6개 대분류(Movement and Actions / Event Signification / Time Manipulation / Persistence / Scene Framing)와 그 아래 약 27개 세부 요소로 구성된다.

### 검증 방식과 그 한계
- **검증 절차 없음.** 사용자 실험, 플레이테스트, 통계 분석이 전혀 없다. 문헌을 읽고 저자 둘이 범주화한 개념적 작업이다.
- 문헌 선정 기준(검색어, 데이터베이스, 포함·배제 기준 등 체계적 문헌고찰 프로토콜)은 **※ 원문에서 확인하지 못함**. PRISMA류의 절차를 밝히지 않았으므로 "체계적 문헌고찰(systematic review)"이라고 부르면 안 되고 "서베이"라고 해야 한다.
- 저자들이 스스로 밝힌 범위 한정과 불완전성(중요):

> While games are multi-sensory experiences, we are focusing on the haptic and visual aspects of game feel in this article, aware that narrative content, sound, music, art, and many other aspects of a game influence how it feels. (§I)

> The table is not an exhaustive overview of all aspects of game feel from a practitioner's perspective. It is a starting point for going deeper into practices most relevant for designing game feel. (§III, Table I 직전)

> This list of elements of game feel design is by no means exhaustive. (§III-F)

> This is not a complete list of all design aspects of games but intended as a starting point for further research into the relation between design intent and game feel. (§IV 서두, Table II 설명)

> The purpose of this paper is to give an overview of existing research and techniques in order to make this crucial area of game development more accessible to game designers and researchers. That means that less reflected aspects are given less or no weight in this paper. (§V)

---

## 4. 인용 가능한 내용

### (1) 세 영역(physicality / amplification / support)과 세 폴리싱 행위(tuning / juicing / streamlining)의 대응 관계 — **원문 확인 완료**

우리 논문 3.4절이 주장하는 대응 관계는 **원문과 정확히 일치한다.** 초록과 §IV, 그리고 Table II 세 곳에서 모두 확인된다.

초록:

> We analysed over 200 sources and categorised their content according to the design purpose presented. This resulted in three different domains of intended player experiences: physicality, amplification, and support. In these domains, the act of polishing that determines game feel, takes the shape of tuning, juicing, and streamlining respectively.

즉 순서대로 1:1 대응이다.

| 디자인 영역 (Design Domain) | 폴리싱 행위 (Polishing Task) | 원문 절 |
|---|---|---|
| Physicality (물리성) | **Tuning** | §IV-A Tuning Physicality |
| Amplification (증폭) | **Juicing** | §IV-B Juicing Amplification |
| **Support (지원)** | **Streamlining** | **§IV-C Streamlining Support** |

원문 Table II (Game Feel Design Domains)의 Description 칸:

> Physicality — Tuning — Setting parameters to specify the behaviour of objects.
> Amplification — Juicing — Adding feedback to emphasise and amplify.
> Support — Streamlining — Acting on player intent by interpreting the input in context of the gameplay situation.

초록의 Support/Streamlining 요약:

> Streamlining allows a game to act on the intention of the player, supporting the execution of actions in the game.

**→ 우리 3.4절의 "세 영역 중 하나로 '지원(Support)'을 꼽았으며, 이는 입력 체계를 다듬는(Streamlining) 과정" 서술은 원문에 부합한다.** 다만 아래 (2)에서 표현을 한 군데 다듬을 것을 권한다.

한국어 간접인용 예시:
> Pichlmair와 Johansen(2022)은 게임 필에 관한 200여 건의 학술·실무 문헌을 분석하여, 게임 필을 결정하는 디자인 영역을 물리성(physicality)·증폭(amplification)·지원(support)의 세 가지로 구분하고, 각 영역에서 이루어지는 폴리싱 작업을 각각 튜닝(tuning)·주이싱(juicing)·스트림라이닝(streamlining)으로 정리하였다.

### (2) Streamlining의 정의 — "입력을 상황 맥락에서 해석하여 플레이어의 의도대로 작동하게 하는 것"

Table II의 Support 열 설명이 가장 정확한 한 줄 정의다.

> Acting on player intent by interpreting the input in context of the gameplay situation.

§IV-C 본문:

> The third design domain is support. It covers techniques that help the player to execute a challenging action or just provide convenience. Doucet [27] calls the polishing of support mechanics 'oiling', whereas we adopt the less slick term 'streamlining' that he also mentions in his article. Streamlining prevents player frustration by making sure that the player receives help where it supports the experience of the game. […] The goal of streamlining is to make rough edges of the game disappear, in order to provide a smooth player experience. Most of the time, the player does not want to realise how much the game is supporting them.

Celeste 사례(우리 논문에도 쓸 수 있음):

> A large portion of the 5400 lines of code that comprises the Celeste character controller is dedicated to providing forgiveness for the player […]. This results in controls that are "working on the player's intent rather than making a precise simulation" [79].

**표현 주의**: 우리 3.4절이 쓴 "입력 체계를 다듬는 과정"은 뜻은 맞지만 원문의 강조점을 약간 놓친다. 원문의 핵심은 "입력을 **게임플레이 상황의 맥락에서 해석**하여 **플레이어의 의도대로** 게임이 작동하게 한다"는 쪽이다. 아래처럼 고치면 원문에 더 가깝다.

한국어 간접인용 예시:
> 스트림라이닝은 플레이어의 입력을 게임플레이 상황의 맥락 속에서 해석하여, 게임이 플레이어의 의도대로 반응하도록 만드는 폴리싱 작업으로 정의된다(Pichlmair & Johansen, 2022).

### (3) **입력 버퍼(input buffering)는 원문에 직접 언급된다 — §III-A8 Button Caching** ⭐

지시받은 대로 확인했다. **있다.** 'input buffer'라는 용어 자체는 쓰이지 않지만, **'Jump Buffering'과 'button caching'이라는 이름으로 독립된 절(§III-A8)을 할당받아 다뤄지며**, Table I에서 **Support 영역에만 ●로 분류**되어 있다. 이것이 우리 3.4절의 가장 강한 학술적 근거다.

§III-A8 전문:

> **III-A8 Button Caching**
> A common player support function is 'Jump Buffering' [90] (and other forms of button caching), where the controller code buffers the pressing of the jump button for a few frames and executes the jump after the player has landed. Mario [161] caches the button for 1-2 frames and Braid [162] for 0.23 seconds [90].

Table I (Game feel design elements overview) 해당 행 — Physicality/Amplification 칸은 비어 있고 Support 칸만 ●:

> Button Caching — (Physicality 공란) (Amplification 공란) — Support ● — Key References [90]

Table I에서 Support로 분류된 다른 이웃 항목들(같은 "Movement and Actions" 대분류):
Terminal Velocity, Coyote Time, Invincibility Frames, Corner Correction, Collision Shapes(Physicality와 중복), **Button Caching**, Assisted Aiming.

참조된 [90]은 다음 문헌이다(우리 논문에 2차 인용으로 언급할 때 필요):

> [90] M. Fasterholdt, M. Pichlmair, and C. Holmgård, "You Say Jump, I Say How High? Operationalising the Game Feel of Jumping," in Proceedings of the First International Joint Conference of DiGRA and FDG. Dundee, Scotland: Digital Games Research Association and Society for the Advancement of the Science of Digital Games, 2016.

한국어 간접인용 예시:
> Pichlmair와 Johansen(2022)은 점프 버퍼링(jump buffering)을 비롯한 버튼 캐싱을 대표적인 플레이어 지원(support) 기법으로 분류하고, 이를 "컨트롤러 코드가 점프 버튼 입력을 몇 프레임 동안 버퍼에 담아 두었다가 플레이어가 착지한 뒤 점프를 실행하는" 방식으로 설명하였으며, 실제 사례로 《Mario》가 1–2프레임, 《Braid》가 0.23초의 캐싱 시간을 사용함을 제시하였다.

> 즉 입력을 즉시 버리지 않고 일정 시간 보관했다가 실행 가능한 시점에 소비하는 구조는 게임 필 연구에서 '지원(support)' 영역의 확립된 기법으로 다루어지고 있다(Pichlmair & Johansen, 2022).

**버퍼 길이 수치 근거로서의 가치**: Mario 1–2프레임(60fps 기준 약 0.017–0.033초), Braid 0.23초라는 구체적 수치가 원문에 있으므로, 우리 구현의 `bufferTime` 값을 정당화할 때 "상용 게임의 버퍼 길이는 수 프레임에서 0.23초 수준으로 보고되어 있다"는 식으로 비교 근거를 댈 수 있다. **단, 이 수치의 원출처는 [90] Fasterholdt 외(2016)이므로 엄밀히는 재인용이다.**

### (4) Coyote Time — 입력 버퍼와 짝을 이루는 시간적 관용(temporal leniency) 기법

§III-A4:

> The term 'Coyote Time' refers to a movement system that allows a player to still instigate a jump a short time span after running off a cliff […]. It is perhaps the most famous example of supporting the intent of the player.

§IV-C의 Nijman 인용:

> Disc Room's [158] designer Nijman explains that their use of Coyote Time "has a bunch of good side effects that make it seem like the game knows your intentions." [101]

한국어 간접인용 예시:
> 코요테 타임과 같이 입력의 유효 시점을 시간적으로 확장하는 기법은 "게임이 플레이어의 의도를 알고 있는 것처럼" 느끼게 만드는 대표적인 지원 기법으로 꼽힌다(Pichlmair & Johansen, 2022).

### (5) 히트 스톱(Hit Stop) / 무적 프레임(Invincibility Frames) — 3.4절 전투 시스템의 다른 부분에 쓸 수 있음

Table I:
> Hit Stop — Amplification ● — Support ● — [122, 123, 81, 79]
> Invincibility Frames — Support ● — [97, 98, 99]

§III-A5:
> Short time spans where the player character is invincible. They are a side-effect of player actions like rolling, dodging, respawning, or attacking. […] The purpose for introducing a few frames of invincibility is usually to support the player, to give them a carefully measured amount of safety that allows them to pull off even more spectacular actions than if they were vulnerable all the time.

> Mora-Zamora and Brenes-Villalobos [98] described invincibility frames as a tool for balancing risk and reward.

한국어 간접인용 예시:
> 회피·구르기 등의 동작에 부여되는 무적 프레임은 플레이어에게 계산된 안전 구간을 제공하여 위험과 보상을 조절하는 지원 기법으로 분류된다(Pichlmair & Johansen, 2022).

### (6) 게임 필의 정의와 범위 (2장 관련연구용)

Swink의 정의 인용(원문이 [1]을 재인용한 것):
> "real-time control of virtual objects in a simulated space, with interactions emphasised by polish" [1]

저자들 자신의 입장:
> Unlike Swink's precise but narrow definition of 'Game Feel', we will look at game feel more broadly as the affective aspect of real-time interactivity. (§I)

> Designing game feel is designing the adequate feedback for eliciting a specific feeling or affective reaction. (§III)

---

## 5. 우리 논문에서 쓸 자리

- **3.4절(전투 시스템) — 최우선.** 입력 버퍼 구현의 학술적 정당화. §III-A8 Button Caching이 입력 버퍼를 support 영역 기법으로 명시적으로 분류하고 있다는 점, 그리고 Table II의 Support/Streamlining 정의("입력을 게임플레이 상황의 맥락에서 해석하여 플레이어 의도대로 작동")가 우리 구현(큐 보관 → 캔슬/콤보 윈도우에서 소비)의 설계 의도를 정확히 서술한다.
  - 현재 3.4절의 "200여 개 문헌", "세 영역 중 지원(Support)", "입력 체계를 다듬는(Streamlining)" 서술은 **모두 원문에 부합**하므로 내용 수정은 불필요하다. 단 (a) **연도를 2021 → 2022로 고칠 것**, (b) Streamlining 설명을 4절 (2)의 표현으로 다듬으면 더 정확하다.
  - 4절 (3)의 Mario 1–2프레임 / Braid 0.23초 수치를 우리 `bufferTime` 값 설정의 비교 근거로 덧붙일 수 있다.
- **3.2절(플레이어 상태머신·입력 버퍼).** 입력 버퍼를 상태머신과 함께 설명하는 자리라면 3.4절보다 3.2절이 더 자연스러울 수 있다. 어느 절에 넣든 §III-A8이 근거다.
- **3.4절 보조 — 무적 프레임·히트 스톱.** 우리 게임의 실시간 근접 전투에서 회피 무적 구간이나 타격 정지 연출을 구현했다면 4절 (5)를 근거로 삼을 수 있다.
- **2장 관련연구.** 게임 필 연구의 전반적 지형을 한두 문단으로 정리할 때 이 서베이가 가장 표준적인 인용처다. Swink(2009)의 정의를 재인용하는 통로로도 쓸 수 있다.
- **3.9절(렌더링·최적화) — 약하게.** §III-B 계열(Screen Shake, Colour Flashing, One-shot Particle Effects)이 amplification 기법으로 정리되어 있으므로, 우리 게임의 피격 연출을 설명할 때 참조 가능. 다만 렌더링 파이프라인 자체와는 무관하다.

---

## 6. 쓰면 안 되는 주장

- ❌ **"입력 버퍼가 플레이어 만족도/조작감을 향상시킨다는 것이 (실험으로) 검증되었다."**
  이 논문은 사용자 실험을 하지 않은 **문헌 서베이**다. 기법의 존재와 분류를 보고할 뿐 효과를 측정하지 않았다. 효과를 주장하려면 별도의 실험 문헌이 필요하다.
- ❌ **"체계적 문헌고찰(systematic literature review)에서 밝혀진 바"** 같은 표현.
  문헌 선정 프로토콜이 제시되지 않았다(※ 원문에서 확인하지 못함). "서베이" 또는 "문헌 개관"이라고 써야 한다.
- ❌ **"게임 필을 구성하는 요소는 이 세 영역/27개 기법이 전부이다."**
  저자들이 네 곳에서 명시적으로 부정한다 — "by no means exhaustive", "not an exhaustive overview", "not a complete list", "a starting point for further research". "완전한 분류 체계"로 인용하면 안 되고 "출발점으로 제시된 어휘 체계"로 인용해야 한다.
- ❌ **"Mario는 1–2프레임, Braid는 0.23초의 입력 버퍼를 쓴다(Pichlmair & Johansen, 2022)."**
  이 수치의 원출처는 [90] Fasterholdt 외(2016)이다. 직접 인용하려면 Fasterholdt 외를 확인해 인용하거나, "Fasterholdt 외(2016)의 보고를 Pichlmair와 Johansen(2022)이 정리한 바에 따르면"처럼 재인용임을 밝혀야 한다.
- ❌ **"사운드·음악·서사가 게임 필에 미치는 영향은 규명되었다."**
  §I에서 촉각·시각 측면으로 범위를 한정한다고 명시했고, §V에서 사운드 디자인은 "더 연구되어야 할 분야(stands out as a field which demands to be researched further)"라고 적었다.
- ❌ **"3D·1인칭·모바일 게임의 게임 필"에 관한 주장.**
  §III-A에서 "Most writing on this aspect of game feel is concerned with 2D games"라고 밝혔다. 우리 게임은 2.5D 횡스크롤이라 비교적 가깝지만, 이 서베이를 3D 일반론의 근거로 쓰면 안 된다.
- ❌ **"우리 게임의 입력 버퍼 설계가 이 논문의 권고를 따랐다."**
  이 논문은 구체적 구현 지침이나 권장 수치를 제시하지 않는다. 용어 체계와 사례 정리를 제공할 뿐이다. "이 논문이 분류한 support/streamlining 범주에 해당한다"까지가 정확한 서술이다.
- ⚠️ **쪽수 직접인용 주의.** 우리가 읽은 것은 arXiv 프리프린트다. 저널판 138–152쪽의 어느 쪽인지 확인하지 않은 채 "(p. 145)" 식으로 쓰면 안 된다. 절 번호(§III-A8, §IV-C)로 표시할 것.

---

## 7. 참고문헌 항목

```
Pichlmair, M., & Johansen, M. (2022). Designing game feel: A survey. IEEE Transactions on Games, 14(2), 138–152. https://doi.org/10.1109/TG.2021.3072241
```

(한국어 논문 형식이 필요할 경우)
```
Pichlmair, M., & Johansen, M. (2022). Designing game feel: A survey. IEEE Transactions on Games, 14(2), 138-152.
```
