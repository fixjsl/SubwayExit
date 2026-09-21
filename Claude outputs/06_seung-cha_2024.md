# seung-cha(2024) — Implementation of Input Buffer for Fighting Games

## 1. 서지사항

- **정식 인용**: seung-cha. (2024, January 26). *Implementation of input buffer for fighting games* [Blog post]. My Blog. https://seung-cha.github.io/coding/2024/01/26/fighting-game-input-buffer.html
- **파일**: 블로그/Implementation_of_Input_Buffer_for_Fighting_Games___My_Blog.txt (웹페이지 PDF 출력, 11쪽)
- **종류**: **비학술(개인 기술 블로그)**
- **등급 메모** — ⚠️ **가장 중요한 항목. 반드시 지킬 것.**

  ### ① 참고문헌 목록에 올리면 안 된다. 각주로만 처리한다.

  이 글은 개인 GitHub Pages 블로그(seung-cha.github.io)에 올라온 기술 노트다. 동료심사(peer review)를 전혀 거치지 않았고, 발행 기관·편집 주체·ISSN·DOI가 없다. **한국 대학 학부 졸업논문의 참고문헌 목록(References)에 학술 문헌과 나란히 올리면 심사에서 출처의 무게를 지적받을 소지가 크다.**
  → **처리 방침: 본문 참고문헌 목록에서 제외하고, 해당 내용을 언급하는 지면의 각주(footnote)로만 출처를 밝힌다.** 양식은 아래 4절 (4) 참조.

  ### ② 이 글로는 어떤 학술적 주장도 정당화할 수 없다.

  입력 버퍼가 **필요하다/ 효과가 있다 / 표준적이다**라는 주장의 근거로 쓰면 안 된다. 그 근거는 **04번 Pichlmair & Johansen(2022) §III-A8 Button Caching**에서 가져와야 한다. 이 블로그는 오직 **"구현 방식의 한 가지 실제 사례"**로서만, 그리고 **우리 구현과의 차이를 대조하기 위한 참조점**으로서만 쓴다.

  ### ③ 저자 신원

  글에는 필자명이 핸들 **`seung-cha`**로만 표기되어 있고, 페이지 하단에 `seunghwancha5857@gmail.com`이 병기되어 있다. **본명은 원문에서 확정할 수 없다**(※ 원문에서 확인하지 못함). 이메일 문자열에서 실명을 추정해 적지 말 것. 각주에는 핸들 `seung-cha`를 그대로 쓴다.

  ### ④ 작성일과 접속일

  - 작성일: 글 제목 아래 **`Jan 26, 2024`** 로 명시되어 있다.
  - 접속일: 우리가 가진 PDF의 머리글에 캡처 시각 **`26. 9. 9. 오전 10:18`** 이 찍혀 있으므로 **2026년 9월 9일**에 접속한 것이다. 각주의 접속일자는 이 날짜를 쓴다.

---

## 2. 한 줄 요약

Unity에서 격투게임의 모션 입력(커맨드) 인식을 구현하기 위해, 매 프레임 전체 버튼 상태를 리스트에 쌓아 두는 고정 길이 입력 버퍼와 책임 연쇄(Chain of Responsibility) 패턴 기반의 커맨드 판정기를 만드는 방법을 코드와 함께 설명한 개인 블로그 글이다.

---

## 3. 이 연구가 실제로 한 것

### 연구 방법
- **연구 방법 없음.** 실험도, 측정도, 비교도 없다. 필자 본인의 Unity 구현을 코드 스니펫과 함께 서술한 **튜토리얼 성격의 기술 노트**다.

> In this article, I will be implementing a simple motion input system that is easy to expand in Unity. I will assume you are familiar with the numpad notation.

### 대상과 전제
- 대상: 2D 격투게임의 전형적 입력 — 방향키 4개(상·하·좌·우) + 공격키 3개(약·중·강).
- **60fps 고정 전제**를 명시한다. (→ 아래 4절 (3)의 차이점 4번의 근거)

> The system will receive 4 directional keys (up, down, left, right) and 3 attack keys (light, medium, heavy) of a typical 2D fighting game. Although not every fighting game follows this structure and yours will also likely differ too, implementation should remain the same or similar regardless (unless your game is 4D). **It will run in 60 fps.**

- 표본 규모라 할 것이 없다. **구현 사례 1건**.

### 검증 방식과 그 한계
- **검증 없음.** 성능 측정, 플레이테스트, 다른 구현과의 비교가 전혀 없다. 리포지토리와 데모 영상 링크만 제시한다.

> You can check out the full implementation of input buffer and watch a demonstration video on my repo.

- **필자 스스로 밝힌 한계** (원칙 3에 따라 반드시 기록):

> I suppose there is no right way to implement input buffer and motion inputs - Every game is different. Some games may not even have complex motion inputs. How you use the data in the buffer is totally up to the designer.

  즉 필자 본인이 **"올바른 구현 방식이란 없으며 전적으로 디자이너의 선택"**이라고 명시했다. 이 문장 하나만으로도 이 글을 "표준적 구현" 근거로 인용할 수 없음이 확정된다.

- 자료구조 선택 근거도 "편의"에 기반한 개인적 판단임을 밝힌다:

> I chose to work with List instead of LinkedList because: random access is O(1). The interface is much more convenient to work with […] It is absolutely fine to use LinkedList to implement input buffer.

- 코드에 정의된 `Consumed` 플래그는 **이 글에서 실제로 사용되지 않는다**고 스스로 적고 있다(이는 아래 4절 (3) 차이점 5번과 직접 연결된다):

> `Consumed` is a flag to indicate that the input is 'consumed'. This is to ensure that no same input causes the same action twice. **This variable is not used in this article** but for more complex system, consider using it.

---

## 4. 인용 가능한 내용

### (1) 입력 버퍼의 자료구조와 폐기 규칙

> Input buffer is data structure behind the scene. It is a contiguous array of fixed size storing input in each frame.

버퍼 길이 상수:
> ```csharp
> public class InputBuffer
> {
>     /// <summary>
>     /// How long the buffer should store input?
>     /// </summary>
>     public const int BUFFERLEN = 60;
>     private List<InputButton[]> buffer;
> }
> ```

매 프레임 갱신과 폐기:
> In each frame we insert a new array of InputButton at the front of the buffer and check if the size exceeds the buffer length. If so, delete the last (oldest) input. We initialise the array with the buttons being released. This way, older inputs slowly decay.

> ```csharp
> public void NewFrame()
> {
>     buffer.Insert(0, new InputButton[InputButton.LEN]);
>     AddButton(InputButton.NEUTRAL, true);
>     ...
>     // Discard the oldest set of input if there is one.
>     if (buffer.Count > BUFFERLEN)
>     {
>         buffer.RemoveAt(buffer.Count - 1);
>     }
> }
> ```

Stack을 쓸 수 없는 이유(우리 구현에서도 큐/리스트를 고른 이유와 통한다):
> Stack will not work because we need to access elements in the middle of the list without popping them.

### (2) 입력 윈도우(tolerance)와 소비(consume)

커맨드마다 입력 유효 구간을 프레임 단위 `tol` 값으로 둔다:
> ```csharp
> public class Command_Fireball : ICommand
> {
>     private int tol = 20; // Input window
> ```

성공 시 버퍼 전체를 비운다:
> ```csharp
>     //Fireball command is casted.
>     Debug.Log("Fireball is casted!");
>     buffer.Clear();  // Wipe out the buffer
>     return true;
> ```

소비가 왜 필요한지에 대한 서술(우리 구현의 문제의식과 동일):
> **Input needs to be consumed**
> Suppose you are implementing a wave dash mechanics (6N23 to perform a special dash). If this is successfully performed, the set of inputs that contributes to wave dash should not be used again. Your character will perform another wave dash if it still exists in the buffer. Another solution to this is to decrease the maximum size of the input buffer. This way, the wave dash input decays naturally when your character is ready to perform another wave dash.

### (3) ⭐ 우리 3.4절 구현과의 대조 — 같은 점과 다른 점

우리 구현: **입력을 큐에 보관 → bufferTime 경과 시 폐기 → 캔슬 윈도우/콤보 윈도우에서 소비.**

#### 같은 점 (설계 사상이 일치하는 부분)

| # | 항목 | 블로그 | 우리 구현 |
|---|---|---|---|
| 1 | **입력을 즉시 버리지 않고 보관했다가 나중에 판정한다** | `List<InputButton[]> buffer`에 매 프레임 적재 | 입력 이벤트를 큐에 적재 |
| 2 | **보관 기간에 상한을 두고 초과분을 폐기한다** | `buffer.Count > BUFFERLEN(60)` 이면 가장 오래된 항목 `RemoveAt` | `bufferTime` 경과 시 폐기 |
| 3 | **한 번 쓴 입력은 소비되어야 한다는 문제의식** | "Input needs to be consumed" — 같은 입력으로 같은 동작이 두 번 나오면 안 됨 | 윈도우에서 소비한 입력은 큐에서 제거 |
| 4 | **유효 판정에 '윈도우'라는 시간 구간 개념을 쓴다** | 커맨드별 `tol = 20` 프레임 입력 윈도우 | 캔슬 윈도우 / 콤보 윈도우 |
| 5 | **임의 접근이 필요해 스택을 쓰지 않는다** | "Stack will not work because we need to access elements in the middle" | 큐/리스트 사용 |

#### 다른 점 (반드시 논문에 명시해야 할 부분)

| # | 항목 | 블로그 | 우리 구현 |
|---|---|---|---|
| 1 | **목적** | 격투게임의 **모션 입력(커맨드) 인식** — 236, 623 같은 방향키 **시퀀스 패턴 매칭** | **단일 공격 입력의 지연 실행** — 아직 실행할 수 없는 입력을 받아 두었다가 실행 가능해지면 발동 |
| 2 | **버퍼의 성격** | 과거 입력의 **히스토리(기록)**. 패턴을 거슬러 탐색하는 용도 | 실행 대기 중인 **명령 대기열(queue)** |
| 3 | **저장 단위** | **매 프레임** 전체 버튼 상태 스냅샷 배열을 삽입. 입력이 없어도 엔트리가 생기며, 인덱스 자체가 경과 프레임 수를 뜻함 | **입력 이벤트가 발생했을 때만** 엔트리를 큐에 넣음 |
| 4 | **폐기 기준** | **프레임 개수**(BUFFERLEN = 60). 글 서두에서 "It will run in 60 fps"라고 **프레임레이트 고정을 전제**함 | **경과 시간**(`bufferTime`, 초 단위). 프레임레이트가 변동하는 실시간 환경에서도 버퍼 길이가 일정하게 유지됨 |
| 5 | **소비 방식** | 커맨드 성공 시 **`buffer.Clear()`로 버퍼 전체를 비움**. 개별 입력 단위 소비용 `Consumed` 플래그는 **정의만 되어 있고 이 글에서 사용되지 않음** | **소비한 입력 하나만** 큐에서 제거 |
| 6 | **소비 시점을 결정하는 주체** | **매 프레임** `CommandChain.Update()`가 무조건 전체 커맨드를 검사. 캐릭터 상태와의 연동 없음 | **플레이어 상태머신**(3.2절)이 연 **캔슬 윈도우/콤보 윈도우** 구간에서만 소비. 상태가 소비 시점을 게이트함 |
| 7 | **커맨드 간 우선순위** | 책임 연쇄(Chain of Responsibility) 패턴으로 명시적 우선순위 처리(623 > 236 > 236236) | 해당 없음(단일 공격 입력이므로 우선순위 문제가 발생하지 않음) |

**→ 논문에 쓸 한 줄 정리 (간접인용 예시):**
> 격투게임 계열에서 널리 쓰이는 입력 버퍼 구현은 매 프레임 전체 버튼 상태를 고정 길이 배열에 누적해 두고 커맨드 시퀀스를 역방향으로 탐색하는 방식을 취하는 반면,<sup>각주</sup> 본 게임의 입력 버퍼는 입력 이벤트 단위의 큐로 구성되며 프레임 수가 아닌 경과 시간(`bufferTime`)을 기준으로 폐기하고, 상태머신이 개방한 캔슬 윈도우와 콤보 윈도우 구간에서만 개별 입력을 소비한다는 점에서 구조가 다르다.

이렇게 쓰면 (a) 우리 구현이 선행 사례를 의식하고 있음을 보이면서 (b) 차별점을 분명히 하고 (c) 비학술 출처를 각주로만 처리하는 세 목적을 동시에 달성한다.

### (4) 각주 양식 (그대로 복사해 쓸 것)

**국문 논문 각주 양식 (권장):**
```
seung-cha, "Implementation of Input Buffer for Fighting Games", My Blog, 2024. 1. 26.,
https://seung-cha.github.io/coding/2024/01/26/fighting-game-input-buffer.html
(접속일: 2026. 9. 9.)
```

**더 짧게 쓸 경우:**
```
seung-cha, "Implementation of Input Buffer for Fighting Games", My Blog(2024. 1. 26.),
https://seung-cha.github.io/coding/2024/01/26/fighting-game-input-buffer.html (2026. 9. 9. 접속).
```

**영문 각주 양식이 필요할 경우:**
```
seung-cha, "Implementation of Input Buffer for Fighting Games," My Blog, January 26, 2024,
accessed September 9, 2026,
https://seung-cha.github.io/coding/2024/01/26/fighting-game-input-buffer.html.
```

- **URL**: `https://seung-cha.github.io/coding/2024/01/26/fighting-game-input-buffer.html`
  (PDF의 모든 쪽 하단 꼬리말에 이 URL이 찍혀 있어 확인됨)
- **접속일**: `2026. 9. 9.` (PDF 머리글의 캡처 시각 `26. 9. 9. 오전 10:18` 기준)
  - 제출 직전에 링크가 살아 있는지 다시 확인하고, 그날 날짜로 갱신하는 편이 더 안전하다.
- 학과 양식이 접속일 표기를 요구하지 않는다면 생략 가능하지만, **웹 자료는 접속일을 함께 적는 것이 일반적이다.**

---

## 5. 우리 논문에서 쓸 자리

- **3.4절(전투 시스템) — 각주로만.** 입력 버퍼 구현을 설명하는 문단에서, 우리 방식이 격투게임 계열의 전형적 구현과 어떻게 다른지 한 문장으로 대조할 때. 4절 (3)의 표와 간접인용 예시를 그대로 활용하면 된다.
- **3.2절(플레이어 상태머신·입력 버퍼) — 각주로만.** 입력 버퍼를 상태머신과 묶어 설명하는 자리라면 여기가 더 적합할 수 있다. 특히 **차이점 6번(소비 시점을 상태머신이 게이트한다)**은 3.2절의 핵심 논지와 직결된다.
- **위 두 자리 중 한 곳에만 넣을 것.** 비학술 출처를 여러 번 각주로 반복하면 오히려 눈에 띈다.

**함께 배치할 것 (중요):** 이 각주는 반드시 **04번 Pichlmair & Johansen(2022)의 학술적 근거 문장 뒤에** 따라붙어야 한다. 순서는 다음과 같이 잡는다.

1. (본문·학술 인용) 입력 버퍼링은 플레이어의 의도대로 게임이 반응하게 하는 지원(support) 기법으로 분류된다 **(Pichlmair & Johansen, 2022)**.
2. (본문) 본 게임은 이를 …방식으로 구현하였다.
3. (본문 + **각주**) 격투게임 계열의 전형적 구현과는 …점에서 다르다.<sup>각주 = 이 블로그</sup>

---

## 6. 쓰면 안 되는 주장

- ❌ **참고문헌 목록(References)에 등재하는 것.** 1절 등급 메모 ①. 각주로만 처리한다.
- ❌ **"입력 버퍼는 …하게 구현하는 것이 일반적이다/표준이다."**
  필자 본인이 "there is no right way to implement input buffer and motion inputs - Every game is different"라고 명시적으로 부정했다. 구현 사례 1건으로 '일반적'이라고 말할 수 없다.
- ❌ **"입력 버퍼가 조작감을 개선한다"의 근거로 쓰는 것.**
  이 글에는 측정도, 실험도, 플레이테스트도 없다. 이 주장의 근거는 04번 Pichlmair & Johansen(2022) §III-A8이다.
- ❌ **"버퍼 길이는 60프레임(1초)이 적절하다."**
  BUFFERLEN = 60은 필자가 근거 없이 고른 값이며, 타당성 검토가 없다. 우리 `bufferTime` 값의 근거로 쓰면 안 된다. 수치 근거가 필요하면 04번의 Mario 1–2프레임 / Braid 0.23초를 쓰되, 그것도 재인용임을 밝혀야 한다(04번 메모 6절 참조).
- ❌ **"이 글의 구현을 참고하여 본 게임의 입력 버퍼를 설계하였다."**
  사실이 아니라면 쓰지 말 것. 사후 대조를 위해 찾은 자료라면 "…와 대조하면" 식의 비교 서술에 그쳐야 한다.
- ❌ **필자의 본명을 적는 것.** 원문에는 핸들 `seung-cha`만 있다(※ 본명은 원문에서 확인하지 못함). 이메일 주소에서 실명을 유추해 적지 말 것. 또한 **이메일 주소 자체를 각주에 쓰지 말 것.**
- ❌ **"격투게임은 모두 이런 방식으로 입력을 처리한다."**
  필자는 오히려 게임마다 다르다는 점을 강조한다: "Different fighting games implement the system differently hence each game feels different to play."
- ⚠️ **링크 소멸 위험.** 개인 GitHub Pages는 언제든 사라질 수 있다. 제출 전 접속 확인은 필수이며, 가능하면 Internet Archive(web.archive.org) 스냅샷을 떠 두고 각주에 보조 URL로 병기하는 것이 안전하다.

---

## 7. 참고문헌 항목

⚠️ **참고문헌 목록에는 넣지 않는다.** 아래는 지도교수가 목록 등재를 명시적으로 지시한 경우에 한해 쓸 예비 형식이며, 기본 방침은 4절 (4)의 **각주 처리**다.

```
seung-cha. (2024, January 26). Implementation of input buffer for fighting games [Blog post]. My Blog. https://seung-cha.github.io/coding/2024/01/26/fighting-game-input-buffer.html
```
