# Bhatnagar 외(2025) — Beyond Satisfaction: Game Feel Design for Emotionally Impactful Experiences

## 1. 서지사항

- **정식 인용**: Bhatnagar, P., Laattala, M., Dutta, S., Cole, T., & Hämäläinen, P. (2025). Beyond satisfaction: Game feel design for emotionally impactful experiences. In *Proceedings of the International Conference on the Foundations of Digital Games (FDG '25)*. ACM. https://doi.org/10.1145/3723498.3723808
- **파일**: 게임필_관련/3723498.3723808.txt (ACM DL PDF, 17쪽)
- **종류**: 학술대회 정식논문 (FDG — Foundations of Digital Games, ACM 발간, 17쪽 풀페이퍼)
- **등급 메모**:
  - **출처의 무게에 문제 없음.** FDG는 게임 연구 분야의 주요 국제학술대회이며, ACM Digital Library에 정식 등재된 17쪽 분량의 풀페이퍼다(ACM ISBN 979-8-4007-1856-4/25/04). CC BY 4.0 라이선스로 공개되어 있다.
  - **저자·연도·학회명 확인 결과(원문 확인 완료)**: 원문 1쪽 상단과 ACM Reference Format에 다음이 그대로 적혀 있다.
    > Prabhav Bhatnagar, Markus Laattala, Supriya Dutta, Tom Cole, and Perttu Hämäläinen. 2025. Beyond Satisfaction: Game Feel Design for Emotionally Impactful Experiences. In International Conference on the Foundations of Digital Games (FDG '25), April 15–18, 2025, Graz, Austria. ACM, New York, NY, USA, 17 pages. https://doi.org/10.1145/3723498.3723808
    - 저자 소속: Aalto University(Espoo, Finland) 4인 + University of Greenwich(London, UK) 1인(Tom Cole)
    - 개최지·일자: 2025년 4월 15–18일, 오스트리아 그라츠
    - 몇 회째 대회인지(20th 등 서수)는 **※ 원문에서 확인하지 못함**. 원문의 ACM Reference Format이 서수를 적지 않았으므로, 참고문헌에도 서수를 넣지 말 것(추측 금지).
  - **질적 연구(근거이론)**임에 유의. 참가자 15명·비확률 표본이며 통계적 일반화를 의도하지 않는다. 저자들이 5.4절에서 표본 편향을 명시적으로 인정한다(3절 참조).

---

## 2. 한 줄 요약

게임 필 연구와 게임 감정 연구가 서로 단절되어 있다는 문제의식에서 출발해, 참가자 15명 인터뷰와 116개 게임 메커닉 코퍼스를 구성주의 근거이론으로 분석하여 감정을 불러일으키는 게임 필 디자인 기법 9가지와 이를 설명하는 '기대 변조(Expectation Modulation)' 이론을 제안한 연구다.

---

## 3. 이 연구가 실제로 한 것

### 연구 방법
- **구성주의 근거이론 분석(Constructivist Grounded Theory Analysis, Charmaz)**. HCI 분야의 GTM 사용·오용을 다룬 Cole & Gillies의 개관과 Salisbury & Cole의 게임 분야 GTM 권고를 참조했다고 밝힌다(§3.1).

> We adopt Grounded Theory Methodology (GTM), a form of qualitative analysis, to generate a theory that explains the phenomenon of emotionally impactful game feel. (§3)

- 연구 질문 3가지(§1):
  > • What kind of emotional impacts can game feel design have?
  > • What game feel design decisions and techniques can designers use to support emotional game design?
  > • How can we explain gameplay experiences where game feel design plays a significant role in creating an emotional impact?

### 분석 대상과 표본 규모 (숫자)
- **참가자 15명**. 모집 경로: 최초 4명 지인 소개 → 9명 공개 모집(알토대 캠퍼스 포스터, 텔레그램·디스코드 학생/동문 그룹) → 마지막 2명 다시 지인 소개.
- 보상: 20유로 식당 쿠폰. 인터뷰는 전원 대면, **32분~96분** 소요.
- 인구통계(§3.3): 남성 10명 / 여성 5명. 연령대 18–44세이며 **25–34세 구간이 11명**으로 최다. 자기기술 배경은 학생 10명, 업계 종사자 5명, 연구자 3명, 취미개발자 2명(중복 선택 허용). **13명이 게임 개발 경험 보유**, 자기식별 역할은 게임 디자이너가 11명으로 최다. 주 플랫폼은 PC/Mac 15명, 모바일 4명(최저).
- **게임 메커닉 코퍼스 116개**. 단계별 누적:
  - Phase 1(저자 내부 논의): 1저자가 15개 예시로 시작 → 이 단계 종료 시 **메커닉 46개 + 저자 내부 논의 4회** 분석, **디자인 기법 8개의 골격**과 예비 이론 'Motif and Deviation' 형성.
  - Phase 2(참가자 인터뷰·워크숍): 먼저 4명(개발 경험 2명/무경험 2명) → 개발 경험자 쪽 데이터가 더 풍부하여 **이후 표집에서 게임 개발 경험을 필수 조건으로 지정**. 이후 9명 공개 모집. 워크숍 Exercise 1(게임필 축 × 감정 축 데카르트 좌표에 게임 배치), Exercise 2(메커닉·감정·게임필 디자인 요소 카드 작성).
  - Phase 3(이론적 포화): 이론 'Expectation Modulation' 도출, 'Sensory Deprivation' 기법 신규 추가, 'Mechanical Recontextualization' 하위 범주 추가. **신규 참가자 2명 + 재참여 1명 + 2·3저자**로 이론 검증 후 포화 선언.
- 인터뷰는 WhisperX로 로컬 자동 전사 후 수작업 교정·익명화. 코퍼스와 전사본은 OSF(https://osf.io/m543a/)에 공개.

### 결과물
- **디자인 기법 9가지**(§4.3, Table 2):
  1. Mechanical Subversion(메커닉 전복)
  2. Input Gestures / Metaphor(입력 제스처·은유)
  3. Feedback Ambiguity(피드백 모호성)
  4. Recontextualization(재맥락화 — 은유적/공간적/기계적)
  5. Narrative Significance(서사적 유의성)
  6. Appropriate Feedback(적정 피드백)
  7. Agency Reduction(행위성 축소)
  8. Sensory Deprivation(감각 박탈)
  9. Conscious Interactions(의식적 상호작용)
- **이론 Expectation Modulation**(§4.4): 플레이어의 기대와 실제 게임 필의 관계가 Resonance(공명) / Dissonance(불협) / Novelty(신규성) / Motif(모티프)라는 4개 매개체를 통해 감정 경험을 낳는다는 모델.

### 검증 방식과 그 한계 — **저자들이 스스로 밝힌 한계 (§5.4 Limitations and Future Work)**

> Even though we recruited participants with experience in designing and developing games, we primarily interviewed them on their game-playing experience rather than their designing experience.

> Additionally, a major portion of our participants self-identified as students and were based in Espoo/Helsinki, Finland at the time of the interviews.

> The discussions were also based on retrospective experiences and given how much game feel relates to the finer design details, interviews with players during or immediately after gameplay sessions may offer richer data.

> Some major game genres like multiplayer games, mobile games and VR games were relatively lacking in our dataset. […] the data was still heavy on narrative single-player experience.

> Our future work aims to test our techniques and theory through design. We hope to see how useful the design techniques are when building emotional experiences in games, whether or not they aid in creating rich player experiences and what major practical hurdles remain for meaningful game design.

즉 **기법 9가지와 이론은 아직 설계 실무에서 검증되지 않았다(future work)**. 추가로 §5.3에서도:

> We only scratch the surface through the design techniques and each of them presents a rich avenue for future exploration. Similarly, the theory needs to be explored further to understand how it connects to other theories of emotions in games.

방법론상의 자잘한 한계도 스스로 적었다(§3.4.2):

> This section of the interviews was scheduled too tightly for line-by-line coding of each interview before the next one, so ad-hoc coding of the most prominent ideas was performed and the text was explored in depth afterwards.

용어 자체의 모호성(§5.2):

> When discussing game feel with participants, only about half of them claimed to be familiar with the term. Even within that subset, there was no strong consensus.

---

## 4. 인용 가능한 내용

### (1) 감각 박탈(Sensory Deprivation) — **손전등으로 시야를 제한하는 우리 게임에 가장 직접적으로 맞는 항목** ⭐

Table 2의 정의:
> **Sensory Deprivation** — Reduction or removal of a specific set of feedback: visual, tactile, aural or otherwise, to emulate the loss of that respective sensation.

§4.3.8 본문 — 《Alan Wake》의 손전등 사례가 우리 게임 구조와 거의 동일하다:

> The torch/light mechanic(M50) in Alan Wake [G31] uses a similar idea but leading to a different player experience. The game world and environment are often very dark (reducing visual feedback), which makes sources of light, like your handheld torch that also acts as a weapon against dark corrupted enemies, feel more powerful and hopeful.

같은 절의 《Alien: Isolation》 사례:

> E.g., Alien Isolation[G10] when using the hand-held motion scanner (M100) to see if the hostile alien is nearby, the rest of the background is blurred, partially depriving the visual feedback to build a sense of unease.

§4.3.8 총론:
> The various visual, aural and tactile feedback that acts as the virtual sensory sources for the player may be removed, fully or partially. This effect may be used diegetically, reflecting the loss of sense(s) for both the avatar and the player. Depending on the use case, sensory deprivation can also be non-diegetic, only affecting the player.

한국어 간접인용 예시:
> Bhatnagar 외(2025)는 시각·청각·촉각 피드백을 부분적으로 제거하는 '감각 박탈(sensory deprivation)'을 감정적 게임 필을 만들어 내는 디자인 기법의 하나로 분류하고, 《Alan Wake》에서 어두운 환경이 시각 피드백을 줄임으로써 손전등이라는 광원 자체를 더 강력하고 희망적인 것으로 느끼게 만든다는 점을 사례로 제시하였다.

> 이러한 관점에서 보면, 손전등으로 시야를 제한하는 설계는 단순한 난이도 조절이 아니라 광원에 정서적 가치를 부여하는 게임 필 기법으로 해석할 수 있다(Bhatnagar et al., 2025).

### (2) 피드백 모호성(Feedback Ambiguity) — 몬스터 탐지 게이지와 연결 가능

Table 2 정의:
> **Feedback Ambiguity** — Intentionally leaving the feedback ambiguous for certain player actions.

§4.3.3:
> E.g., in Inside [G30], there is a scene where the protagonist is being chased by dogs(M51, M52). The player needs to pull off planks blocking an exit, but when doing so, the game doesn't give clear feedback regarding how close the player is to pulling it off. This creates a feeling of tension and panic as the player has to decide whether they commit to the action and risk the dogs catching up or run to safety.

**중요한 단서 — 저자들이 직접 붙인 경고:**
> This design technique should be used consciously and tested well to avoid frustration arising from lack of essential feedback.

한국어 간접인용 예시:
> Bhatnagar 외(2025)는 플레이어 행동에 대한 피드백을 의도적으로 모호하게 남겨 긴장과 공황을 유발하는 기법을 보고하면서, 동시에 필수 피드백의 결여가 좌절로 이어지지 않도록 신중한 테스트가 필요함을 지적하였다.

### (3) 행위성 축소(Agency Reduction) — 허기·갈증 등 생존 스탯 악화 구간에 적용 가능

Table 2 정의:
> **Agency Reduction** — Reduction of player agency to a certain degree to highlight the game feel at the moment holistically or for specific mechanics.

§4.3.7:
> Reducing the player's mechanical agency to a certain degree can highlight the elements of game feel. This technique thus can be seen as a supplement to the other presented techniques by placing them under the spotlight and guiding player attention towards them.

### (4) '좋은 게임 필 = 주이시한 게임'이라는 통념 비판 — 2장 관련연구용

§5.1:
> What seems to have been an unfortunate result of this conflation is the perpetuation of the idea of juicy games having "good game feel" and a lack of juice in a game meaning "lacking game feel" or having "bad game feel" [20]. Our data showed that the perception of game feel being "bad" within participants was more often linked to bugs, lagginess, and lack of cohesion within various game elements.

§5.3 — Swink의 '좋은 게임 필' 원칙을 일부 반박:
> Swink presented principles of "good game feel" [46, p 297]. Our design techniques contradict some principles, for example, Swink suggests that feedback for player actions should be predictable but our techniques of Feedback Ambiguity shows that doing quite the opposite can be desirable depending on the context. […] This is not to say that Swink's principles were wrong, but that they were limited and intentionally "breaking the rules" did lead to enhanced experiences.

한국어 간접인용 예시:
> Bhatnagar 외(2025)는 참가자들이 '나쁜 게임 필'이라고 느낀 경우가 주이스(juice)의 부족보다는 버그, 랙, 게임 요소 간 응집성 결여와 더 자주 결부되어 있었다고 보고하였다.

### (5) 04번(Pichlmair & Johansen)의 서지사항을 확인해 주는 자료로서의 가치

이 논문의 참고문헌 [36]번이 Pichlmair & Johansen 논문의 정식 서지사항을 담고 있다. 04번 메모의 연도 확정 근거가 바로 여기다.

> [36] Martin Pichlmair and Mads Johansen. 2022. Designing Game Feel. A Survey. IEEE Transactions on Games 14, 2 (June 2022), 138–152. https://doi.org/10.1109/TG.2021.3072241 arXiv:2011.09201 [cs].

또한 본문에서 04번 논문을 이렇게 요약하고 있어, 우리가 04번을 소개할 때 참고할 수 있다:

> The survey of game feel research by Pichlmair and Johansen [36] compiled various elements of games feel design including properties like character movement, gravity, screen shake, audio feedback, etc.

---

## 5. 우리 논문에서 쓸 자리

**솔직한 판단부터: 현재 우리 논문은 이 문헌을 인용하고 있지 않으며, 인용하지 않아도 논문은 성립한다.** 이 논문은 "게임 필이 감정에 미치는 영향"을 다루는 질적 연구이고, 우리 논문은 시스템 설계·구현 논문이다. 주제 거리가 있으므로 **억지로 넣을 필요는 없다.** 다만 아래 두 자리는 자연스럽게 들어갈 수 있으므로, 분량이나 관련연구의 폭이 필요하다면 쓸 만하다.

- **3.9절(렌더링·최적화) 또는 3.3절(몬스터 AI·탐지 게이지) — 가장 자연스러운 자리.**
  우리 게임의 "손전등으로 시야가 제한됨"이라는 핵심 설계를, 단순한 난이도 장치가 아니라 **의도된 감각 박탈(sensory deprivation) 기법**으로 규정하는 데 쓸 수 있다. 4절 (1)의 《Alan Wake》 손전등 인용이 우리 게임과 거의 1:1로 대응하므로 근거로서 매우 적절하다.
  - 다만 **URP 사용자 정의 렌더 패스라는 구현 내용 자체의 근거는 될 수 없다.** 어디까지나 "왜 시야를 제한하는가"라는 설계 의도를 설명하는 문단에만 쓸 것.
  - 이 자리에는 폴더 내 다른 문헌(`렌더링_최적화__Game_Studies_-_Dynamic_Lighting_for_Tension_in_Games`)이 조명과 긴장의 관계를 더 직접적으로 다룰 가능성이 높으므로, **두 문헌을 비교한 뒤 더 맞는 쪽을 고르거나 둘 다 쓰는 편이 낫다.**
- **2장 관련연구 — 보조적으로.**
  04번(Pichlmair & Johansen, 2022)으로 게임 필 연구의 기본 틀을 제시한 뒤, "최근에는 게임 필이 만족감·통제감을 넘어 더 넓은 감정 스펙트럼에 어떻게 작용하는지가 연구되고 있다(Bhatnagar et al., 2025)"는 한 문장으로 최신 동향을 덧붙이는 용도. **2025년 논문이라 참고문헌의 최신성을 보여 주는 효과가 있다.**
- **3.4절(전투 시스템) — 부적합.** 이 논문에는 입력 버퍼·캔슬 윈도우·콤보에 관한 내용이 없다. 3.4절 근거로는 04번을 쓸 것.
- **5장 결론 및 한계 — 선택적.** "본 연구는 시스템 구현에 초점을 맞추었고 플레이어의 정서적 반응은 측정하지 못했다. 게임 필과 감정의 관계에 관한 최근 연구(Bhatnagar et al., 2025)가 제시하는 기법들을 준거로 한 후속 평가가 필요하다"는 식의 향후 과제 문장에 쓸 수 있다. 이 쓰임이 가장 정직하고 부담이 적다.

---

## 6. 쓰면 안 되는 주장

- ❌ **"감각 박탈 기법이 플레이어의 긴장감을 높인다는 것이 검증되었다."**
  이 연구는 실험이 아니라 **회고적 인터뷰 기반 질적 연구**다. 효과 크기나 인과관계를 측정하지 않았다. 저자들이 §5.4에서 "retrospective experiences"에 기반했음을 한계로 명시했고, 기법의 유효성 검증은 future work라고 적었다.
- ❌ **"플레이어들은 ~하다고 느낀다" 식의 일반화.**
  참가자 15명, 대부분 핀란드 에스포·헬싱키 거주 학생이며 비확률 표본이다. 저자들이 직접 표본 편향을 인정했다. 반드시 "이 연구의 참가자들은" 또는 "일부 참가자(P5, P8 등)는"처럼 범위를 좁혀 쓸 것.
- ❌ **"멀티플레이·모바일·VR 게임에서도 동일하게 적용된다."**
  §5.4에서 해당 장르가 데이터셋에 거의 없었다고 명시했다. 데이터는 서사 중심 싱글플레이에 편중되어 있다.
- ❌ **"9가지 기법이 감정적 게임 필 디자인의 완전한 분류이다."**
  §5.3에서 "We only scratch the surface through the design techniques"라고 적었다. Table 2에도 "the selection of use case possibilities and examples is not exhaustive"라는 단서가 붙어 있다.
- ❌ **"이 논문이 제시한 기법을 우리 게임에 적용하여 효과를 얻었다."**
  우리 게임은 이 논문을 읽고 설계한 것이 아니며, 정서적 효과를 측정하지도 않았다. "우리 설계가 이 논문이 분류한 ~기법에 해당한다"는 사후적 해석까지가 한계다.
- ❌ **《Alan Wake》나 《Alien: Isolation》의 실제 구현 방식에 대한 기술적 주장.**
  이 논문은 두 게임의 코드나 렌더링 방식을 분석하지 않았다. 참가자 및 저자의 플레이 경험에 기반한 메커닉 기술일 뿐이다.
- ❌ **FDG의 회차 서수(예: "20th International Conference…")를 임의로 붙이는 것.**
  원문이 서수를 밝히지 않았다(※ 원문에서 확인하지 못함). 아래 7절 형식 그대로 쓸 것.

---

## 7. 참고문헌 항목

```
Bhatnagar, P., Laattala, M., Dutta, S., Cole, T., & Hämäläinen, P. (2025). Beyond satisfaction: Game feel design for emotionally impactful experiences. In Proceedings of the International Conference on the Foundations of Digital Games (FDG '25). Association for Computing Machinery. https://doi.org/10.1145/3723498.3723808
```
