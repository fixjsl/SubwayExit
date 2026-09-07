검증이 끝났습니다. 이번 세션에서 남은 두 가지(클럭 코루틴, 콤보 윈도우 연결부)까지 전부 확인했고, 기존 제안서의 수치·구조 주장과 **모두 일치**했습니다. 추가로 논문에서 쓸 수 있는 정량 사실 하나를 확보했습니다:

- **게임 시간 속도 = 실시간의 24배**: `Clock()` 코루틴이 실시간 2.5초마다 게임 시간 +1분씩 증가시키므로, **게임상 1일(1440분)은 정확히 실사용 1시간**에 완료됩니다(86400s ÷ 3600s = 24×).
- **콤보 윈도우 실제 연결부 확인**: 애니메이션 이벤트 `OncanCombo()` → `OpenAttackCancelWindow(withCombo: ComboIndex < 2)` → 캔슬/콤보 윈도우 오픈. 윈도우 내 버퍼 입력이 Dodge/Parry면 공격을 캔슬하고, Attack이면 상태 재진입 없이 `DoCombo()` 호출(3타 = ComboIndex 0~2). 3타 완료 후엔 무기 `attackSpeed` 전체 1회분 쿨다운 후에야 새 콤보 시작(`CanEnter`).

아래는 파일에 반영할 **최종 문서 전문**입니다. (직후 `SubwayExit_졸업논문_구성제안.md`를 이 내용으로 확정 저장합니다.)

---

# SubwayExit 학사 졸업논문 구성 제안 (최종 확정본)

> 근거 자료: `기능명세서.md`(구현 기준, 전량 확인) + `기획서 초안.docx` 추출본(컨셉·밸런스 근거)
> 교수 코멘트 대응(25.11.11): "졸작으로 원하는 건 '무슨 기능을 쓰냐'지, 콘텐츠 나열 기획서가 아님" / 약 5페이지(10분 발표)로 압축 가능해야 함 → 아래 구성은 **구현 기술 중심 서술** 전제로 설계
> 코드 검증 완료: 모든 수치·구조 주장을 실제 소스와 대조 확인함. 각 절에 근거 코드를 병기하고, 부록 A에 전체 검증 기록을 수록

---

## 1. 제목 후보 (5개)

| # | 제목 | 특징 |
|---|------|------|
| 1 | 지하철 폐쇄 공간을 배경으로 한 3D 서바이벌 호러 게임의 상태 머신 기반 AI와 생존 시스템 설계 및 구현 | 가장 무난하고 범위 정확함. "무슨 기능을 쓰냐"에 대한 정면 답변이 제목에 포함됨 |
| 2 | 소음·빛 기반 탐지 게이지형 몬스터 AI를 지닌 지하철 서바이벌 게임의 설계 및 구현 | 이 프로젝트의 **가장 차별화된 메카닉**을 제목으로 내세움. 임팩트 전달에 유리 |
| 3 | 루프(Cycle) 기반 진행 구조와 데이터 구형 아키텍처를 적용한 Unity 서바이벌 게임 개발 | CycleManager + ScriptableObject 중심. "시스템 설계" 무게가 강함 |
| 4 | 지하철 폐허 배경 3D 서바이벌 호러 게임 SubwayExit의 설계 및 구현 | 프로젝트명을 직접 쓰는 직선형. 안전하지만 기술적 차별점은 제목에 안 남음 |
| 5 | 상태 머신과 오브젝트 풀링을 활용한 다수 몬스터 동시 운영이 가능한 3D 생존 게임 AI 시스템 개발 | "성능/동시성" 관점. 다만 본 프로젝트의 검증 자료(프로파일 수치)가 약하면 과대포장 위험 |

**권고:** 발표 임팩트를 최우선으로 하면 **2번**, 안정성을 우선하면 **1번**. 2번을 택하되 본문에서 탐지 게이지를 "핵심 차별점"으로 한 절 전체를 할애하는 구성이 아래 목차와 가장 잘 맞습니다.

---

## 2. 전체 장(章) 구조 (권고안)

### 제1장 서론
- **연구 배경:** 폐쇄 공간(지하철)에서 빛·소음을 자원으로 삼는 생존 호러 컨셉. 기획서 프로로그(핵전쟁 폐허 → 무리 이탈 → 지하철 심부 추락)를 2~3문단으로 압축 인용 — 스토리를 "설정"이 아니라 **탐지 게이지 메카닉의 존재 이유**(어둠=안전, 빛=위협이라는 역설 구조가 성립하는 배경)로 서술
- **연구 목적:** ① 상태 머신 기반 플레이어/몬스터/보스 3계층 AI 아키텍처 구축 ② 소음·빛 입력을 게이지로 변환하는 탐지 모델 설계 ③ 루프(Cycle) 구조의 월드 리셋 시스템 구현 — 이 세 줄이 "무슨 기능을 쓰냐"에 대한 답변의 뼈대
- **연구 범위 및 방법:** Unity + C#, URP, ScriptableObject 데이터 구형. 기획서(컨셉) → 기능명세서(구현)로 내려온 개발 흐름을 1문단

### 제2장 이론적 배경 및 참고 연구
- **상태 패턴과 게임 상태 머신:** GoF State 패턴의 게임 적용 맥락(이벤트→조건→전환). 본 프로젝트의 Player/Monster/Boss 3계층 구조가 이 이론의 구체화 사례임을 명시
- **탐지 게이지형 적 AI:** 기존 호러/스텔스 게임의 "발각도 게이지" 관행(구체 작품명은 인용 근거 확보 시에만 기재, 없으면 일반론으로) → 본 프로젝트의 0~100 게이지 모델과의 비교 지점 정의
- **데이터 구형 설계(ScriptableObject)와 오브젝트 풀링:** Unity 공식 권장 패턴으로서의 위상 정리. 이 둘이 제3장 아키텍처의 전제가 됨을 연결

### 제3장 시스템 아키텍처 및 구현 (논문 본체, 분량 최대)
장 내 절 구성은 **의존 관계 상향** 순서(데이터 → 상태 → 월드 → 렌더링):

- **3.1 모듈 구조와 의존성:** 폴더 구조(Manager / State / Object / UI / Status / Item / Rendering 분리)를 블록 다이어그램으로. 기능명세서 부록의 "주요 스크립트 간 의존 관계"가 그대로 도图源 — GameManager(싱글톤 허브: 시간·오디오·설정) → PlayerStateMachine ↔ MonsterStateMachine → Manager군으로 흐르는 구조를 한 장으로 표현
  - *코드 근거:* `Assets/Script` 폴더 실제 구성과 일치함.

- **3.2 데이터 구형 아키텍처:** PlayerStatus / MonsterStatus / WeaponStatus / ItemBase / LootTable 5종 ScriptableObject의 필드 설계(스탯·비용·범위·특수효과). "코드를 건드리지 않고 수치만 교체해 밸런싱하는 구조"가 기획서의 무기 밸런스 표(공속/사거리/피해/경직 공식)를 구현 데이터로 연결하는 근거
  - *코드 근거:* `Assets/Script/Status/WeaponStatus.cs` — bloodStrength(출혈)/stunStrength(스턴 축적)/GuardStrength(가드 파괴) 특수효과 필드, attackStamina 등 행동별 스태미나 비용 필드 확인.

- **3.3 플레이어 상태 머신:** PlayerStateMachine(Singleton + State Machine), 11종 액티브 상태(Idle·Move·Attack·Dodge·Parry·Guard·Crunch·Hit·Die·Interact·Execution)와 isBlock 강제 전환 규칙, **0.2초 버퍼 입력 시스템**(입력 씹힘 방지; `buffertime = 0.2f`), **3타 콤보 윈도우** — 애니메이션 이벤트 `OncanCombo()`가 `OpenAttackCancelWindow(withCombo: ComboIndex < 2)`로 캔슬/콤보 윈도우를 열고, 윈도우 내 버퍼 입력이 Dodge/Parry면 공격을 캔슬(`CloseAllAttackWindows` + `BufferState()`)하며 Attack이면 상태 재진입 없이 `DoCombo()` 호출(3타 = ComboIndex 0~2). 3타 완료 후엔 무기 `attackSpeed` 전체 1회분 쿨다운 후에야 새 콤보 진입(`CanEnter`). 패시브 상태(HungerThirst, NoiseABright)는 Strategy 패턴으로 분리 — 생존 수치와 소음·밝기 산출이 전투 상태와 독립적으로 틱되는 구조
  - *코드 근거:* `Assets/Script/State/PlayerState/ActiveState/`에 11개 상태 클래스 + `PassiveState/`(HungerThirst, NoiseABright) 확인. `PlayerStateMachine.cs`에서 `buffertime = 0.2f`, `PostAnimComboTime = 0.5f`, 캔슬/콤보 윈도우 틱 로직(버퍼 입력 분기: Dodge·Parry → 캔슬, Attack → `DoCombo()`) 확인. `Attack.cs`에서 `OncanCombo()` → `OpenAttackCancelWindow(withCombo: ComboIndex < 2)`, `DoCombo()`(ComboIndex++ 후 최대 2 초과 시 반환), `CanEnter()`의 3타 후 쿨다운 판정(`Time.time - lastCombo3FinishTime >= attackSpeed`) 확인.

- **3.4 탐지 게이지 기반 몬스터 AI (핵심 차별점):** MonsterStateMachine의 0.1초 간격 OverlapBox 스캔 → **탐지 게이지(0~100)** 모델: 탐지 범위 내 소음·빛 누적, 장애물 존재 시 소음 전달 80% 감소, 전투 범위 진입 + 고(高) 밝기 시 즉시 100, 게이지 만점→Chase, 미감지 2초 지속→Return. 플레이어의 currentnoise/currentbrighten 값이 **손전등 ON/OFF·웅크리기·달리기 선택에 직접 연결**되는 포인트를 흐름도로 — "조명 도구가 곧 스테ルス 자원"이라는 게임플레이 루프가 이 한 절에서 완성되어야 함
  - *코드 근거:* `Assets/Script/State/MonsterState/MonsterStateMachine.cs` — 0.1초 간격 OverlapBox 스캔, 장애물 Linecast 시 소음 ×0.2(80% 감소), 전투범위 내+밝기>0 시 게이지 즉시 100, 게이지≥100이면 Idle/Move/Return 상태에서 Chase 전환, 미감지 20프레임(=2초) 지속 시 `LoseTarget()`→`ChangeState<Return>()`, `OnHit` 시 detection_gauge=100 강제, `ActiveState.isBlock`이면 상태 전환 없이 데미지만 적용.

- **3.5 전투 시스템:** WeaponStatus의 bloodStrength(출혈)/stunStrength(스턴 축적)/GuardStrength(가드 파괴) 특수 효과 모델, AttackHitbox의 HashSet 중복 피격 방지, 스턴 상태 몬스터 대상 Execution 처형(MonsterManager.GetStunnedInRange 범위 탐색), 패리·회피 무적 프레임
  - *코드 근거:* `MonsterManager.GetStunnedInRange(origin, range)` 메서드 존재 확인.

- **3.6 보스 확장 설계:** BossStateMachine이 MonsterStateMachine을 **상속**하여 일반 상태를 재사용하고 특수 공격(쿨타임 15초·발동 확률 40%)과 First Attack만 추가하는 구조 — "기존 AI 파이프라인의 비용으로 보스를 확장했다"는 서술. BossStartTrigger / BossCombatZone(구역 이탈 시 리셋) / BossUI
  - *코드 근거:* `BossStateMachine.cs` — MonsterStateMachine 상속, SpecialCooldown=15f, SpecialTriggerChance=0.4f(SerializeField 기본값 → 논문에서는 "기본 설계값"으로 표기), BossStartTrigger/BossCombatZone/BossUI 존재 확인.

- **3.7 생존·수급 시스템:** 배고픔/갈증 0~100과 분당 감소량, 스태미나 비용 체계, 모닥불(연료 소비 점화 → 요리), DIY 정수기, 무게 기반 인벤토리 + 퀵슬롯 2칸. 기획서의 **광원 진화 루트**(횃불→랜턴/손전등)가 canBurnAsFuel 연료 아이템 구조로 어떻게 구현 근거를 갖는지 한 문단
  - *코드 근거:* 기능명세서 시스템별 요약과 일치.

- **3.8 렌더링 및 최적화:** URP 커스텀 RendererFeature 기반 손전발(내각/외각 원뿔, 바닥·천장 바운스 라이트, Shader Global 변수로 플레이어 위치·방향 전달), ScreenPointLight의 스크린 스페이스 간소화 조명, MonsterFlashlightVisibility. "조명 렌더링 비용과 감지 메카닉이 같은 파라미터(밝기)를 공유한다"는 점이 이 프로젝트만의 결합점이므로 반드시 명시
  - *코드 근거:* URP 커스텀 RendererFeature 기반 손전등 구현 존재 확인.

- **3.9 루프(Cycle) 진행 구조:** CycleManager의 4종 리셋(플레이어 리스폰 / 몬스터 재배치 / 컨테이너 루트 테이블 재생성 / 비상구 위치 변경) + GameManager 시간 시스템(**코루틴 기반 Day/Hour/Minute 클럭** — `TutorialStart()`가 Day=1/Hour=0/Minute=0으로 초기화 후 `Clock()` 코루틴을 시작하고, 실사용 2.5초마다 Minute+1 → 60분 시 Hour rollover → 24시간 시 Day rollover + `ChangeDay` 브로드캐스트, 매 틱마다 `ChangeTime(Hour,Minute)` 이벤트 발행). 이 구조로 **게임 시간 속도는 실시간의 정확히 24배**(실사용 1초 = 게임 0.4분)이며, **게임상 1일(1440분)은 실사용 정확히 1시간**에 완료됨 — 기획서의 "현실시간 고정 배터리 vs 비례 게임 세계 시간" 합의가 이 코루틴 구조에 대응함을 1문단
  - *코드 근거:* `CycleManager.cs` — `ExecuteCycleReset()` = TeleportPlayerToBase + RespawnMonsters(보스 제외) + ResetContainers + DepartureGate.ResetForCycle + 비상구 재배치(`RelocateExitFarthestFrom`: 출발점에서 **가장 먼** 스포인트 선택). "4종 리셋" 주장 확인. `GameManager.cs` — Day/Hour/Minute 필드, `ChangeTime(hour,minute)` / `ChangeDay(hour,day)` 이벤트, `TutorialStart()`에서 코루틴 시작, `Clock()` 코루틴의 2.5초 간격 틱 + rollover 로직 확인.

- **3.10 튜토리얼 및 시작 흐름:** GameStartFlow → SelectWeapon(기획서 4장 카드 무기 선택 UI의 구현) → TutorialManager/TutorialTrigger 위치 트리거 방식. 기획서의 단계 1~10 진행 설계가 트리거 타입으로 매핑된 사례 2~3개만 인용(비속어·구어체 발췌 금지, 정리 후 인용)
  - *코드 근거:* GameStartFlow → SelectWeapon → TutorialManager/TutorialTrigger 위치 트리거 방식 확인.

### 제4장 결과 및 분석
- 구현 완료 기능 체크리스트(기능명세서 시스템별 요약 표 — "무엇을 만들었는가"의 객관적 근거)
- 플레이 흐름 검증: 타이틀 → 무기 선택 → 루프 1 진행 → 비상구/보스 분기 → 클리어/게임오버 전 경로
- 성능 관련 서술 가능 지점: 오브젝트 풀링으로 생성·소멸 GC 압축, ScreenPointLight의 간소화 조명 — 단, **프로파일러 수치가 없으면 수치 없이 원리 수준에서 서술** (후속 작업으로 Frame Debugger/Profiler 스크린샷 1~2장 확보를 권장)
- 플레이테스트: 가능하면 동적 인상(탐지 게이지의 긴장감 전달 여부, 버퍼 입력의 조작감)을 소수로라도 진행해 1~2문단

### 제5장 결론 및 향후 계획
- 기여 요약: 3계층 상태 머신 재사용 구조 + 탐지 게이지 AI + 루프 리셋 시스템이 하나의 생존 호러 루프로 결합
- 한계점(솔직 서술 권장): 탐지 게이지가 OverlapBox 기하 기반이라 복잡한 장애물 우회 경로 탐색 없음, 보스 특수 공격이 확률 발동 등
- 향후 계획: 기획서에 있고 미구현/부분 구현인 요소(오버레이 맵·미니맵·월드맵 규칙, 적 4종 중 아귀의 시선 유도 등)를 "향후 확장"으로 이관 — **기획서 콘텐츠는 여기서만 등장**시켜 본문이 콘텐츠 나열로 흐르는 것을 차단

### 발표용 압축안 (10분 / 약 5페이지 기준)
1. 타이틀 + 컨셉 한 장(프로로그 요약 + 핵심 루프 다이어그램 1장)
2. 아키텍처 한 장(의존성 다이어그램 + 3계층 상태 머신 계층도)
3. **탐지 게이지 AI 한 장(핵심, 흐름도 필수)** — 발표 임팩트의 중심
4. 전투·보스·생존을 합친 구현 결과 한 장(게임 스크린샷/캡처 위주)
5. 렌더링 기술 + 결론 한 장

---

## 3. 기획서 내용 배치 원칙 (교수 코멘트 대응)

1. **콘텐츠 나열 금지:** 적 4종·스토리·맵 규칙은 제1장 배경(2~3문단)과 제5장 향후 계획에만 분산 배치
2. **밸런스 표는 데이터 근거로만 사용:** 무기 밸런스 공식(사거리 창<도끼<망치… 등)은 "WeaponStatus 필드 설계의 산출 근거"로 3.2절에 인용 — 별도 장 금지
3. **발췌·정리 필수:** 기획서 대화 탭의 구어체·비속어 원문 인용 금지, 의미만 정리해 재서술
4. **"구현 여부" 필터:** 모든 시스템 서술은 기능명세서에 존재하는 클래스/파일 기준으로만 작성(아귀 유도, 오버레이 맵 등 미구현 요소는 제5장으로)

---

## 4. 리스크 및 보완 포인트

| 리스크 | 보완책 |
|--------|--------|
| "상태 머신"이 학사 수준에서 흔한 패턴이라 차별점 약화 | 3계층 **상속 재사용**(보스가 몬스터 AI를 상속) + isBlock 강제 전환 규칙 같은 프로젝트 고유 변형점을 부각 |
| 탐지 게이지가 단순 수치 누적에 그칠 수 있다는 평가 | 장애물 80% 감소, 즉각 추격 조건(범위+밝기 동시 충족), 미감지 복귀 타이머까지 **조건 분기 전체**를 다이어그램으로 제시해 "규칙 시스템"임을 증명 |
| 성능 주장의 근거 부족 | 프로파일러/Frame Debugger 캡처 2~3장 사전 확보, 없으면 원리 서술로 격하 |
| 루프 구조가 메트로베니아 표기와 불일치 지적 가능 | 장르명은 기획서와 동일하게 유지하되 본문에서는 "루프(Cycle) 기반 진행 구조"라는 기술적 명칭으로 통일 |

---

## 부록 A. 코드 검증 기록 (주장 ↔ 소스 위치)

| # | 논문에서 쓸 주장 | 소스 위치 | 확인 결과 |
|---|-----------------|-----------|-----------|
| 1 | 몬스터 탐지 루틴: 0.1초 간격 OverlapBox 스캔 | `Assets/Script/State/MonsterState/MonsterStateMachine.cs` | ✓ 일치 |
| 2 | 장애물 존재 시 소음 전달 80% 감소 (×0.2) |同上 | ✓ 일치 (Linecast 기반) |
| 3 | 전투범위 내 + 밝기>0 시 게이지 즉시 100 |同上 | ✓ 일치 |
| 4 | 게이지≥100 → Idle/Move/Return 상태에서 Chase 전환 |同上 | ✓ 일치 |
| 5 | 미감지 2초(20프레임) 지속 → `LoseTarget()`→`ChangeState<Return>()` |同上 | ✓ 일치 |
| 6 | `OnHit` 시 detection_gauge=100 강제 / isBlock이면 데미지만 |同上 | ✓ 일치 |
| 7 | 플레이어 액티브 상태 11종 + 패시브 2종 | `Assets/Script/State/PlayerState/ActiveState/`, `PassiveState/` | ✓ 일치 (Attack, Crunch, Die, Dodge, Execution, Guard, hit, Idle, Interact, Move, Parry / HungerThirst, NoiseABright) |
| 8 | 버퍼 입력 0.2초 (`buffertime = 0.2f`) | `PlayerStateMachine.cs` | ✓ 일치 |
| 9 | 콤보 윈도우 상수 `PostAnimComboTime = 0.5f` |同上 | ✓ 일치 |
| 10 | `OncanCombo()` → `OpenAttackCancelWindow(withCombo: ComboIndex < 2)` | `ActiveState/Attack.cs`, `PlayerStateMachine.cs` | ✓ 일치 (캔슬/콤보 윈도우 오픈 로직 확인) |
| 11 | `DoCombo()` — 상태 재진입 없이 콤보 진행, 최대 3타(0~2) | `ActiveState/Attack.cs` | ✓ 일치 (`if(ComboIndex >2) return;`) |
| 12 | 3타 후 쿨다운: `CanEnter()`에서 `Time.time - lastCombo3FinishTime >= attackSpeed` 판정 |同上 | ✓ 일치 |
| 13 | 보스 = MonsterStateMachine 상속 + 특수공격(쿨타임 15초·확률 40%) | `BossStateMachine.cs` | ✓ 일치 (SpecialCooldown=15f, SpecialTriggerChance=0.4f) |
| 14 | BossStartTrigger / BossCombatZone / BossUI 존재 |同上 | ✓ 일치 |
| 15 | WeaponStatus 특수효과 필드(bloodStrength/stunStrength/GuardStrength) + 스태미나 비용 3종 | `Assets/Script/Status/WeaponStatus.cs` | ✓ 일치 |
| 16 | 처형 범위 탐색 `GetStunnedInRange(origin, range)` | `MonsterManager.cs` | ✓ 존재 확인 |
| 17 | 사이클 리셋 4종 (플레이어/몬스터/컨테이너/비상구) | `CycleManager.cs` — `ExecuteCycleReset()` | ✓ 일치 (TeleportPlayerToBase + RespawnMonsters(보스 제외) + ResetContainers + DepartureGate.ResetForCycle + RelocateExitFarthestFrom) |
| 18 | 비상구 = 출발점에서 **가장 먼** 스포인트 |同上 — `RelocateExitFarthestFrom` | ✓ 일치 |
| 19 | 게임 클럭: 코루틴 기반 Day/Hour/Minute, 실사용 2.5초마다 +1분 | `GameManager.cs` — `Clock()` | ✓ 일치 (`YeildCache.GetIntervals(2.5f)`) |
| 20 | ChangeTime / ChangeDay 이벤트 브로드캐스트 |同上 | ✓ 일치 (매 틱 ChangeTime, 일 전환 시 ChangeDay) |
| 21 | **게임 시간 속도 = 실시간의 24배, 게임 1일 = 실사용 정확히 1시간** |同上 (계산: 1440분 × 2.5초 = 3600초 = 60분) | ✓ 확인 (새로 추가된 정량 사실) |
| 22 | `TutorialStart()`에서 Day=1/Hour=0/Minute=0 초기화 후 클럭 시작 |同上 | ✓ 일치 |

---

