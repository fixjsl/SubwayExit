# Koulaxidis & Xinogalos(2022) — Improving Mobile Game Performance with Basic Optimization Techniques in Unity

## 1. 서지사항

- **정식 인용**: Koulaxidis, G., & Xinogalos, S. (2022). Improving mobile game performance with basic optimization techniques in Unity. *Modelling, 3*(2), 201–223. https://doi.org/10.3390/modelling3020014
- **파일**: 렌더링_최적화/modelling-03-00014.pdf
- **종류**: **학술지 정식 논문 (Article)** — MDPI 오픈액세스 저널 *Modelling*, 3권 2호, 201–223쪽
- **서지 세부 (원문에서 확인)**:
  - 문서 머리에 **"Article"** 표기. 정식 논문이다.
  - 저자: Georgios Koulaxidis, Stelios Xinogalos. 두 사람 모두 그리스 University of Macedonia, Department of Applied Informatics (GR-54636 Thessaloniki) 소속. 교신저자 표시(*)가 두 사람 모두에게 붙어 있다.
  - 투고/게재 일정이 원문에 명시: "Received: 20 January 2022 / Accepted: 25 March 2022 / Published: 28 March 2022"
  - DOI: 10.3390/modelling3020014. 쪽 범위 201–223은 원문 하단 인용 정보에서 확인.
  - 키워드: "mobile game performance; optimization techniques; android; low poly; object pooling; blender; unity"
  - 라이선스: CC BY 4.0 (오픈액세스). "Funding: This research received no external funding." / "Conflicts of Interest: The authors declare no conflict of interest."
  - 저자 기여 표기(Author Contributions)상 **실험·구현·분석은 전부 제1저자 G.K. 단독**이고 제2저자 S.X.는 집필·검토·감수(supervision)를 맡았다. 즉 **실질적으로 학생 프로젝트를 지도교수가 감수해 논문화한 형태**로 보인다.
- **등급 메모**:
  - 정식 학술지 논문이므로 **출처의 무게 자체는 문제없다.** 학부 졸업논문에서 측정 방법의 근거로 인용하기에 적절한 급이다.
  - 다만 **MDPI 저널**이고 *Modelling*은 비교적 신생 저널(2022년 기준 3권)이다. 심사에서 저널의 무게를 따질 사람이 있다면 지적될 수 있으나, 오픈액세스·CC BY·DOI·쪽수가 모두 갖춰져 있어 인용 자체에는 문제가 없다.
  - **정말 주의해야 할 것은 저널 등급이 아니라 이 논문의 내용 범위다** (6번 참조). 게임 1종·기기 1대·비교 2버전, 통계 분석 없음, 사용자 실험 없음.

## 2. 한 줄 요약

Unity와 Blender로 안드로이드용 3D 슈터 게임을 만들고, 저폴리 모델·정점 병합·텍스처/머티리얼(컬러 팔레트)·오클루전 컬링·오브젝트 풀링이라는 다섯 가지 기본 최적화 기법을 **한꺼번에 적용한 버전과 적용하지 않은 버전** 두 개를 제작해, 초당 프레임 수(FPS)·배치 수·삼각형/폴리곤 수라는 세 지표를 Unity 프로파일러로 비교한 사례 연구다. PC에서는 두 버전의 차이가 작았으나 실제 모바일 기기에서는 최적화 버전 50~60 FPS, 비최적화 버전 20~30 FPS로 뚜렷하게 갈렸다.

## 3. 이 연구가 실제로 한 것

### 연구 방법

- **연구 질문이 원문에 한 문장으로 명시되어 있다.**

  > "What is the impact of basic optimization techniques on 3D mobile game performance?"

- **설계**: 동일한 게임의 두 버전(최적화 / 비최적화)을 직접 제작하고 프로파일러 데이터를 비교하는 **단일 사례 비교(A/B 구현 비교)**. 통계적 가설 검정이나 반복 측정은 없다.
- **적용한 최적화 기법 — 원문 2장에 다섯 개가 절 단위로 나열된다. (이것이 의뢰에서 정확히 정리하라고 한 항목이다.)**

  원문 서론의 요약 문장:
  > "The basic optimization techniques investigated collectively include low poly models, merge vertices (as a means of achieving low poly models), textures and materials, occlusion culling and object pooling."

  | 절 | 기법 | 원문이 설명하는 내용 |
  |---|---|---|
  | 2.1 | **Low Poly Models** (저폴리 모델) | 폴리곤 수가 많을수록 사실적이지만 렌더링 비용이 커진다. GPU는 삼각형만 렌더링할 수 있으므로 모든 폴리곤은 삼각형으로 변환된다. Unity 문서(2017년판)를 인용해 **모바일에서는 메시당 폴리곤 300~1500개** 범위를 권고. Blender의 **decimate 모디파이어**로 기존 모델의 면 수를 줄일 수 있으나, **처음부터 저폴리로 설계하는 편이 더 효율적**이다(ratio 값이 작으면 모델이 깨질 수 있음). |
  | 2.2 | **Merge Vertices** (정점 병합) | Blender의 **'merge by distance'** 옵션. 거리값 0으로 두면 완전히 겹친 정점만 병합되어, 복사·붙여넣기 과정에서 옛 모델 위에 새 모델이 겹쳐 남는 문제를 정리한다. 거리값을 0보다 크게 주면 멀리 떨어진 정점까지 병합되어 decimate 모디파이어와 비슷한 결과가 된다. **저폴리 모델을 달성하는 수단**으로 분류된다. |
  | 2.3 | **Textures and Materials** | 메시는 형태를, 머티리얼은 표면을 기술한다. 이 연구의 게임은 **컬러 팔레트(color pallet) 기법**을 썼다 — Blender에서 모델의 UV 맵을 생성하고, 팔레트 이미지를 만들어 각 면을 원하는 색 위치에 정렬한 뒤, 모델과 팔레트 .png를 함께 Unity로 가져와 머티리얼로 지정한다. 모델마다 팔레트를 따로 둘 수도, 게임 전체에 하나만 둘 수도 있다. **색을 저장하면서 자원을 아끼는 방법**으로 제시된다. |
  | 2.4 | **Occlusion Culling** (오클루전 컬링) | Unity 카메라는 기본적으로 시야각 안의 모든 오브젝트를 벽 뒤에 가려진 것까지 그린다. 오클루전 데이터를 'Baking'하면 가려진 부분을 무시한다. 오브젝트를 **occluder static 또는 occludee static**으로 분류해야 베이킹 대상이 된다. **동적 오브젝트는 occluder 역할은 할 수 있으나 occludee는 될 수 없고**, 포함시키려면 각 렌더러 컴포넌트에서 dynamic occlusion 옵션을 켜야 한다. 결과적으로 **프레임당 그려지는 지오메트리가 줄어든다.** |
  | 2.5 | **Object Pooling** (오브젝트 풀링) | `instantiate(...)`/`destroy(...)`를 반복 호출하면 가비지 컬렉터가 자주 작동하고, 대상이 많고 폴리곤 수가 높으면 **CPU 사용량 증가와 힙 단편화(heap fragmentation)** 문제가 생길 수 있다. 미리 인스턴스를 다수 생성해 리스트 같은 자료구조에 넣어 두고, 필요할 때 활성화·용도가 끝나면 비활성화해 재사용한다. **이미 생성되어 비활성 상태로 씬에 존재하므로 렌더에 필요한 자원이 적다.** 문제점도 명시: 풀 크기를 잘못 잡으면 필요한 수보다 적거나 지나치게 많아진다. |

  원문 결론부의 재진술:
  > "The basic optimization techniques investigated collectively included low poly models, merge vertices, textures and materials, occlusion culling and object pooling."

  **주의**: 원문 결론은 이 다섯 기법 중 **오클루전 컬링만이 Unity에 의존하는 기법**이고 나머지는 어떤 3D 그래픽 소프트웨어로도 같은 결과를 낼 수 있다고 밝힌다. 또 오클루전 컬링도 Unreal, Turbulenz, Sio2, Shiva, Ogre3D 등 여러 엔진이 공통으로 제공한다고 적는다.

- **측정 지표 — 세 개다.** (원문 3장 방법론에서 불릿으로 나열)
  > "For the comparison between the two versions of the game, the following metrics were used for measuring and evaluating performance [21]:
  > • Frames per second (FPS);
  > • Batches;
  > • Triangles/polygons."

- **측정 도구 (3.4절)**: 주 도구는 **Unity에 내장된 프로파일러(Unity Profiler)**. 두 번째 도구는 **FPS를 화면에 출력하는 스크립트**다. 원문이 이 스크립트가 왜 필요한지를 명시한다 — "the profiler has this kind of functionality, it can only monitor the game running on the computer. So, the measurement of the game's FPS, on mobile devices, is being done via a script that calculates frames per second over an interval."

- **측정 절차 (3장 + 5장)**: 실험은 **세 단계**로 구성된다.
  1. **PC — 게임 시작 시점**: 메뉴 씬에서 본 씬으로 전환하며 모든 오브젝트가 로드되는 큰 스파이크 프레임의 데이터를 두 버전에서 비교.
  2. **PC — 게임플레이 중**: 900프레임 집합에서 **FPS가 가장 낮은 스냅샷 1개**와 **FPS가 60에 가까운 스냅샷 1개**를 뽑아 두 버전을 비교. 각각을 렌더링 데이터와 메모리 데이터로 나누어 표로 제시.
  3. **실제 모바일 기기 — 게임플레이 중 FPS 카운트**.
- **측정 환경 조건 (원문 5장 서두에 명시)**: 맵에 동시에 존재할 수 있는 적의 최대 수는 **100**, 적은 **0.5초마다 출현**. 데이터 기록은 **모바일 폰에 원격 연결된 컴퓨터**에서 이루어졌고 **프로파일러는 최근 900프레임**을 표시했다. 원문이 직접 밝히기를:
  > "Those two factors need to be mentioned, because they affected the data on the profiler."

### 분석·실험 대상과 표본 규모 (숫자를 정확히)

- **게임 1종** — Unity로 제작한 **안드로이드용 단순 3D 슈터**. 모델 대부분은 Blender로 제작.
- **비교 버전 2개** — 최적화 버전 / 비최적화 버전.
- **PC 측정 스냅샷 3개** — 로딩 1개 + 게임플레이 2개(최저 FPS / 60 근접). 각 스냅샷을 렌더링·메모리 두 범주로 나누어 표 6개(Table 2~7) 제시.
- **모바일 기기 1대** — **Xiaomi Mi Note 10 Lite** (Table 8): CPU Qualcomm SDM730 Snapdragon Octa-core MAX 2.2 GHz, RAM 6 GB, 저장공간 64 GB, 화면 6.47인치, 배터리 Li-Po 5260 mAh.
- **참가자(사용자 실험) 0명.** 플레이어 대상 실험·설문·인터뷰는 **일절 없다.** "user experience"라는 표현은 저자들이 FPS 수치로부터 추론한 것이지 측정한 것이 아니다.
- **통계 분석 없음.** 반복 측정·평균·분산·유의성 검정 어느 것도 보고되지 않는다. 스냅샷 단일 프레임의 값을 표로 나란히 놓고 비교한다.
- ※ 원문에서 확인하지 못함 — PC 쪽 하드웨어 사양(모바일 기기 사양만 Table 8에 있고 컴퓨터 사양표는 없다), Unity 버전 번호, Blender 버전 번호, 측정 반복 횟수.

### 검증 방식과 그 한계

- **검증 방식**: 자체 제작한 두 버전의 프로파일러 수치를 직접 비교. 외부 검증·재현·제3자 평가 없음.
- **저자들이 스스로 밝힌 한계** — 원문에서 확인된 것만 적는다.
  - **규모가 작다.** 초록에 직접 쓰여 있다:
    > "Even though the game is not large in scale, the optimized version achieves a better user experience and needs less resources in order to run smoothly. This means that in larger and more complex video games these optimizations could have a bigger impact on the performance of the final product."

    → **큰 게임에서 효과가 더 클 것이라는 부분은 저자들의 추측("could have")이지 이 연구가 보인 결과가 아니다.**
  - **기법을 묶어서 적용했으므로 개별 기여를 분리하지 못한다.** 향후 과제에 명시:
    > "Future work could investigate the impact of each one of the examined optimization techniques on mobile game performance in isolation, while further optimization techniques could be investigated, such as code optimization and reducing unnecessary physics."

    → **"오클루전 컬링이 X% 기여했다" 같은 주장은 이 논문으로 절대 할 수 없다.**
  - **다른 연구와 직접 비교할 수 없다고 스스로 밝힌다.**
    > "Although several studies on optimization techniques in mobile games have been carried out, their results cannot be directly compared with the results of our study."
  - **기기 의존성을 2장 서두에서 스스로 경고한다.**
    > "the mobile devices that are currently on market do not all have the same capabilities [7]. ... Therefore, the developer should choose from the beginning the range of devices which s/he wants the game to run. This is also the case for studies on the effects of various optimization techniques, such as the ones summarized in the previous section, which always define the devices that were utilized in the experiments."

    → **저자들 스스로 "최적화 연구는 사용한 기기를 반드시 밝혀야 한다"고 적는다. 즉 수치는 그 기기에 묶인다.**
  - **다루지 않은 성능 영역을 향후 과제로 열거**: loading speed, frame rate consistency, UI element loading, power consumption, thermal stress.
  - **프로파일러 자체가 측정에 개입한다는 점을 인지하고 있다.** 메모리 표의 Profiler(Used)/Profiler(Reserved) 항목에 대해 "the profiler variable was ignored because in real life scenarios it will not affect the game"라고 적는다. 즉 **프로파일러 오버헤드가 포함된 수치를 놓고 일부 항목을 수동으로 배제한 것**이다.

## 4. 인용 가능한 내용

### (1) 세 지표와 세 성능 측면의 대응 관계 — **의뢰에서 확인하라고 한 핵심 항목**

**결론부터: 우리 논문이 쓰고 있는 "각각 대응시켜"는 원문에 없는 대응이다. 원문은 세 지표를 묶어서(collectively) 세 측면을 평가한다.**

원문에 이 관계가 나오는 대목은 세 군데이고, 세 군데 모두 **일대일 짝짓기가 아니라 집합 대 집합**으로 쓰여 있다.

**(가) 초록**
> "To measure how the techniques affected the two versions of the game, the values of frames per second, batches and triangles/polygons were taken under consideration and used as metrics for game performance in terms of CPU usage, rendering (GPU usage) and memory usage."

**(나) 서론 (연구 질문 바로 뒤)**
> "The basic optimization techniques investigated collectively include low poly models, merge vertices (as a means of achieving low poly models), textures and materials, occlusion culling and object pooling. These techniques aim to optimize the value of frames per second, batches and triangles/polygons in order to improve game performance in terms of CPU usage, rendering (GPU usage) and memory usage."

**(다) 3장 방법론, 지표 나열 직후**
> "Improvements to their values were expected to result in better game performance in CPU usage, rendering and memory."

**세 문장 어디에도 "FPS는 CPU에, 배치는 렌더링에, 삼각형은 메모리에"라는 짝짓기가 없다.** 문장 구조는 전부 "이 세 지표를 (묶어서) → 저 세 측면의 성능을 개선/평가한다"이다.

**더 결정적으로, 본문의 실제 측정 구조가 일대일 대응을 정면으로 부정한다.**

- **배치(Batches)와 삼각형(Triangles)은 둘 다 "렌더링 데이터" 표(Table 2, 4, 5)에 함께 들어 있다.** 두 지표 모두 렌더링 항목이다. 삼각형이 메모리 표(Table 3, 6, 7)에 나타나는 일은 **한 번도 없다.**
- **메모리 표의 변수는 완전히 다른 것들이다**: Total Used Memory, GC Used, Gfx Used, Audio Used, Video Used, Profiler(Used), Total Reserved Memory, System Used Memory, Textures, Meshes, Materials, Animation Clips, Asset Count, Game Object Count, Scene Object Count, Object Count, GC Allocation in Frame.
- **FPS는 CPU 사용량 다이어그램에서 읽는 값으로 쓰이지만**(표 제목이 전부 "...in CPU usage diagram"), 동시에 **모바일 단계에서는 유일한 종합 지표**로 쓰인다. CPU 전용 지표가 아니다.
- **삼각형/폴리곤에 대해 원문 3.3절은 세 자원 모두를 언급한다.**
  > "However, a fairly realistic model with many polygons requires greater processing power from the GPU and CPU in order for it to be displayed. There are also bigger requirements in the field of memory usage for a complicated model mesh."

  → 삼각형 수는 **GPU·CPU·메모리 셋 다**에 영향을 준다고 쓰여 있다. 메모리 전담 지표가 아니다.
- **배치에 대해서도 3.2절은 세 자원을 모두 언급한다.**
  > "models that have the same materials can be combined in a batch in order to be rendered together [25]. As a result it is possible to minimize runtime memory consumption, along with CPU and GPU workloads [26]."

  → 배치 감소는 **메모리·CPU·GPU 모두**를 줄인다고 쓰여 있다. 렌더링 전담 지표가 아니다.

**판정: 우리 논문의 현재 문장 "초당 프레임 수(FPS), 배치(Batch) 수, 삼각형 수의 세 지표를 각각 CPU 부하와 렌더링 부하, 메모리 사용량에 대응시켜 측정하였다"는 원문을 잘못 옮긴 것이다. 반드시 고쳐야 한다.**

**고쳐 쓸 문장 (원문에 부합하는 형태):**
> Koulaxidis와 Xinogalos(2022)는 초당 프레임 수(FPS), 배치(Batch) 수, 삼각형/폴리곤 수의 세 지표를 함께 사용하여 CPU 사용량·렌더링(GPU) 부하·메모리 사용량의 세 측면에서 모바일 게임 성능을 평가하였다.

**혹은 더 안전하게, 대응 관계를 언급하지 않고:**
> Koulaxidis와 Xinogalos(2022)는 Unity 모바일 게임의 최적화 효과를 초당 프레임 수(FPS), 배치(Batch) 수, 삼각형/폴리곤 수의 세 지표로 측정하였다. 본 연구도 같은 세 지표를 측정 항목으로 삼는다.

### (2) 각 지표가 무엇을 뜻하는지 — 3.1~3.3절 정의

**FPS (3.1절)**
> "Frames per second (FPS) refers to the number of images that are updated every second on the screen. Each image represents a frame [22]. The fast image projection gives the illusion of movement to the human eye. Thus, the larger the number of frames or images projection per second, the smoother the movement is [21]."

원문에 수식 (1)이 하나 있다:
> `1000/target FPS = time per frame (ms)` (수식 1)

FPS가 왜 대표 지표인가:
> "FPS is one of the most common metrics for evaluating the performance of video games, because it affects the end user's game performance as well [23]. A low number of frames means that it is quite difficult for someone to play the game, while a higher number means that a game runs smoothly. In other words, the rate at which the frames are rendered can impact the player's performance affecting the overall experience."

**Batches (3.2절)**
> "Any object that the user wants to be visible on the scene of his/her game is sent by Unity to the GPU in order to design it, using the selected graphics API for each platform [25]. This action is known as draw call [25]. ... However, in cases where there are a high number of models, these calls can put a lot of strain on the GPU, especially on mobile devices. The solution to this problem is given through reducing the batches [26]. More specifically, models that have the same materials can be combined in a batch in order to be rendered together [25]. As a result it is possible to minimize runtime memory consumption, along with CPU and GPU workloads [26]. Essentially, batches are a way to find out and fix any abuse of the aforementioned modeling materials."

**Triangles/Polygons (3.3절)**
> "Each model used for game development purposes consists of triangles/polygons. The greater the number of polygons we encounter in an object, the more realistically it is represented [13,14]. However, a fairly realistic model with many polygons requires greater processing power from the GPU and CPU in order for it to be displayed. There are also bigger requirements in the field of memory usage for a complicated model mesh. So, one way to optimize a video game model is to use as few polygons as possible to represent it [11]. ... Furthermore, because GPUs can only render triangles [13], it is necessary for all the polygons of a model to be converted into triangles in order for the render process to be accomplished."

한국어 간접인용 예시:
> 배치는 동일한 머티리얼을 사용하는 모델들을 묶어 한 번에 렌더링하는 단위로, 드로우 콜로 인한 GPU 부담을 줄이기 위한 지표다(Koulaxidis & Xinogalos, 2022).

### (3) 실제 측정 결과 — PC에서는 차이가 작고 모바일에서 갈렸다

**이것이 이 논문의 가장 쓸모 있는 실질적 발견이다.**

PC 로딩 단계에 대해:
> "Initially, the two versions of the game were tested on a personal computer, where we recorded a snapshot with the lowest FPS value during the beginning of each game version. The results of the two versions were quite close, with the optimized version accomplishing better results for all the components (CPU usage, rendering and memory)."

게임플레이 단계에 대해:
> "In terms of rendering, the optimized version gave slightly better results for most of the variables in both cases."

모바일 기기에 대해:
> "Lastly, we could not talk about mobile games without testing our game on an actual mobile device. This device was a Xiaomi Mi Note Lite 10 with the specifications mentioned in Table 8. For this part of the experiment, we compared the two versions during runtime. The differences in the FPS for the two versions were very clear in the mobile environment. From the beginning until the end of the game, the FPS for the optimized version ranged between the values of 50 and 60. This means that the user experience was smooth all the time. However, the non-optimized version could not keep up. The value of FPS when there was some action on the map was between 20 and 30, which caused the game to lag at some points."

**모바일 기기의 구체적 수치 (원문 5.3절에서 확인)**

| 상황 | 최적화 버전 | 비최적화 버전 |
|---|---|---|
| 게임 시작 직후 | 59.96 FPS (Figure 14) | 52.13 FPS (Figure 17) |
| 적 19마리 제거 후 | — | **24.58 FPS** (Figure 18) |
| 적 52마리 제거 후 | 56.45 FPS (Figure 15) | **22.15 FPS** (Figure 19) |
| 적 72마리 제거 후 | 50 FPS 밑으로 떨어지지 않음 (Figure 16) | — |
| 적 75마리 제거 후 | — | **24.52 FPS** (Figure 20) |

원문이 비최적화 버전에 대해 덧붙이는 관찰:
> "The game in the non-optimized version came back to 50–60 FPS when there were a few or no enemies on the map."

한국어 간접인용 예시:
> Koulaxidis와 Xinogalos(2022)는 같은 게임의 최적화 버전과 비최적화 버전을 비교한 결과, PC에서는 두 버전의 차이가 작았으나 실제 모바일 기기에서는 각각 50~60 FPS와 20~30 FPS로 뚜렷하게 갈렸다고 보고하였다.

### (4) 삼각형 수 감소의 구체적 규모 — Table 1

원문 Table 1(Triangle count for some of the models in improved and non-improved versions of the game):

| Model | Improved (Triangles) | Non-Improved (Triangles) |
|---|---|---|
| Barrack | 808 | 6584 |
| Tree | 2190 | 5640 |
| Water tank | 4400 | 6088 |
| Sand | 2512 | **130,050** |
| Enemy | 3296 | 25,585 |

프레임 단위 삼각형 총수는 다음과 같이 차이가 났다(Table 2, 4, 5).

| 시점 | 최적화 | 비최적화 |
|---|---|---|
| 로딩 프레임 | 175.4 k | 734.8 k |
| 게임플레이 최저 FPS 프레임 | 490.3 k | 3.5 M |
| 게임플레이 60 FPS 근접 프레임 | 574.7 k | 3.4 M |

배치 수는 차이가 훨씬 작았다.

| 시점 | 최적화 배치 | 비최적화 배치 |
|---|---|---|
| 로딩 프레임 | 103 | 109 |
| 게임플레이 최저 FPS 프레임 | 224 | 246 |
| 게임플레이 60 FPS 근접 프레임 | 249 | 253 |

**주목할 점**: 원문 스스로 배치 차이를 "slightly better"라고만 표현한다.
> "it is obvious that in the optimized version there was a smaller number of set pass calls, draw calls and batches, which means that there was a slightly better grouping of objects."

**즉 이 연구에서 실제로 극적으로 갈린 지표는 삼각형 수와 (모바일의) FPS이고, 배치 수의 차이는 작았다.** 우리 논문이 세 지표를 나란히 놓을 때 이 비대칭을 알고 있어야 한다.

### (5) 최적화 버전이 오히려 나빴던 항목들 — **정직하게 알고 있어야 할 대목**

원문은 모든 지표가 개선된 것이 아니라는 점을 숨기지 않는다.

- **GC 사용 메모리**: 게임플레이 최저 FPS 프레임에서 최적화 19.0 MB vs 비최적화 17.2 MB. 원문의 설명:
  > "The GC usage went up to 19 MB for the optimized version. For this specific version, GC tends not to be called so often, but when it does it uses more memory."

  결론부에서도:
  > "There were also parts such as GC that used less memory size for the non-optimized version than the optimized version, because there were fewer calls on GC to clear memory space."
- **Total Reserved Memory (로딩 시점)**: 원문이 직접 "the non-optimized version having the best value"라고 적는다.
- **오브젝트 수**: 최적화 버전의 Game Object / Scene Object / Object Count가 훨씬 **크다**(예: 로딩 시점 최적화 19,753 vs 비최적화 8,385). 이유는 오브젝트 풀링이다 — 적 100마리를 시작 시점에 전부 생성해 비활성 상태로 두기 때문.
  > "Basically, all the enemies (100 in total) were being created and disabled at the start of the game. So, they were still counted as game objects in the scene."
- **동적 배칭(Dynamic Batching)** 값은 최저 FPS 프레임에서 비최적화 쪽이 더 낮았다. 원문의 해명:
  > "Dynamic batching variables seemed to have lower values for the non-optimized version, and the reason behind that could be that in the optimized version there were more types of different objects being rendered."

  → "could be"라는 표현에 주목. **저자들도 확정하지 못한 추측이다.**
- **Shadow Casters**: 로딩 시점에 최적화 89 vs 비최적화 78로 최적화 쪽이 더 많았다.

### (6) FPS 목표값에 대한 서술 — 우리 4장에 직접 쓸 수 있는 근거

> "After 19 eliminations, the FPS value fell to 24.58 (Figure 18), which means that the game was not smooth any more. The FPS limits that can make a game playable or not are determined by the game type. Some games might be playable with 20–30 FPS, but for a game with a faster-paced gameplay it might have a negative effect on user reflexes. This is true, at least, for the newer games that are optimized for 60 FPS during release."

**이 대목이 중요하다. 저자들은 "몇 FPS 이상이어야 한다"는 보편 기준을 제시하지 않고, 장르에 따라 다르다고 명시한다.** 우리 논문이 목표 FPS를 정할 때 이 문장을 근거로 "장르 특성상 ~를 목표로 삼았다"고 쓸 수 있다.

한국어 간접인용 예시:
> 게임을 플레이 가능하게 하는 FPS의 하한은 게임 유형에 따라 달라지며, 20~30 FPS로도 플레이 가능한 게임이 있는 반면 빠른 템포의 게임에서는 플레이어의 반응에 부정적 영향을 줄 수 있다(Koulaxidis & Xinogalos, 2022).

### (7) 이 연구가 스스로 밝힌 기여의 성격 — "비전문가용 참조 자료"

> "This article aims to contribute to the research on optimization of mobile games performance by investigating basic optimization techniques for 3D mobile games. This is considered important since, nowadays, the stakeholders include people coming from various fields, as noted in [12]. For example, teachers and students design and implement mobile games for usage as learning aids for their classes or in the context of course/thesis projects, respectively. It is of vital importance to provide guidelines for applying basic optimization techniques that have a proven improvement in game performance and can be applied by people that are not experts in mobile game development."

> "In contrast with previous studies, we investigated the impact of a number of basic optimization techniques on a 3D mobile game collectively. These techniques are among the most well-known ones and are simple enough to be applied even by non-experts in the field of game development. The work presented can be utilized as a reference for applying the investigated optimization techniques in a straightforward manner, but also for replicating the study."

**이 논문은 스스로를 "학생·교사 등 비전문가가 참조할 수 있는 기본 기법 안내"로 자리매김한다.** 학부 졸업논문이 측정 방법의 선례로 인용하기에는 오히려 아주 잘 맞는 성격이다. 다만 **"이 연구가 기법의 효과를 증명했다"는 식으로 인용하면 안 된다**(6번 참조).

## 5. 우리 논문에서 쓸 자리

- **3.9절(렌더링·최적화) — 최적화 기법 목록의 선례.**
  우리가 적용한 기법이 이 다섯 가지와 겹치는 것이 있다면(예: 오클루전 컬링, 오브젝트 풀링, 저폴리 모델), "모바일 게임 최적화 연구에서 기본 기법으로 다루어진 것들"이라는 맥락을 줄 수 있다. 예:
  > Koulaxidis와 Xinogalos(2022)는 Unity 모바일 게임에서 저폴리 모델, 정점 병합, 텍스처·머티리얼 관리, 오클루전 컬링, 오브젝트 풀링을 기본 최적화 기법으로 제시하였다. 본 연구도 이 가운데 ○○과 ○○을 적용하였다.

  **단, 우리가 적용하지 않은 기법을 "적용했다"는 식으로 목록을 부풀리면 안 된다.**

- **3.9절 / 4장(성능 측정) — 측정 지표 선정의 근거.** 이것이 이 문헌의 주된 용도다.
  > 본 연구의 성능 측정은 Koulaxidis와 Xinogalos(2022)를 따라 초당 프레임 수(FPS), 배치 수, 삼각형 수를 지표로 삼았다.

  **반드시 4-(1)의 고친 문장 형태로 쓸 것. 현재 초안의 "각각 대응시켜"는 삭제해야 한다.**

- **4장 — 측정 절차 설계의 선례.**
  이 논문의 절차(로딩 시점 / 최저 FPS 프레임 / 목표 FPS 근접 프레임의 세 시점을 프로파일러 스냅샷으로 비교)는 우리 4장이 그대로 참조할 만한 구조다. 우리가 같은 방식을 쓴다면 근거로 인용할 수 있다.
  > 측정 시점은 Koulaxidis와 Xinogalos(2022)를 참고하여 로딩 구간과 게임플레이 구간으로 나누고, 게임플레이 구간에서는 FPS가 가장 낮은 프레임을 기준으로 비교하였다.

- **4장 — Unity 프로파일러 사용의 근거.**
  > 성능 데이터는 Unity에 내장된 프로파일러로 수집하였다. 이는 Koulaxidis와 Xinogalos(2022)가 동일 엔진 환경에서 사용한 도구와 같다.

- **4장 또는 5장 — 목표 FPS 설정의 근거.** 4-(6) 참조. 장르에 따라 기준이 달라진다는 진술을 근거로 우리 목표치를 정당화할 수 있다.

- **5장(한계) — 우리 측정의 한계를 밝힐 때 참조.**
  이 논문도 기법을 묶어 적용해 개별 기여를 분리하지 못했다고 스스로 밝힌다. 우리 논문도 같은 한계를 가진다면 선행 연구도 동일한 제약을 안고 있다는 점을 적을 수 있다(면죄부로 쓰지 말고, 한계 기술의 정확성을 위해).

## 6. 쓰면 안 되는 주장

### (1) **수치를 빌려 올 수 없다 — 플랫폼과 장르가 다르다. 이것이 가장 중요하다.**

- **대상이 안드로이드 모바일 기기용 3D 슈터**다. 측정 기기는 **Xiaomi Mi Note 10 Lite 한 대**(Snapdragon SDM730, RAM 6 GB)다.
- **우리 게임 Metro Escape는 PC용 2.5차원 횡스크롤 게임**이다. 플랫폼(모바일 ↔ PC), 시점 구조(3D 일인칭/삼인칭 슈터 ↔ 2.5D 횡스크롤), 렌더링 파이프라인(우리는 URP 사용자 정의 렌더 패스), 부하 구조(우리는 화면 공간 조명과 손전등 시야 제한이 주 부하)가 전부 다르다.
- 따라서 **"50~60 FPS", "20~30 FPS", "175.4 k 삼각형", "배치 103개" 같은 수치는 어떤 형태로도 우리 게임의 기준선이나 비교 대상이 될 수 없다.** 저자들 자신이 최적화 연구는 사용 기기를 반드시 명시해야 한다고 2장 서두에 적었다.
- **인용할 수 있는 것은 "무엇을 지표로 삼았는가"라는 방법론이지, "얼마가 나왔는가"라는 수치가 아니다.**

### (2) 세 지표와 세 성능 측면의 일대일 대응을 이 문헌에 귀속시킬 수 없다

4-(1)에서 상세히 확인했다. 원문은 세 지표를 **묶어서** 세 측면을 평가한다. 배치와 삼각형은 둘 다 렌더링 데이터 표에 들어 있고, 삼각형은 메모리 표에 나오지 않는다. 배치는 CPU·GPU·메모리 모두에, 삼각형은 GPU·CPU·메모리 모두에 영향을 준다고 원문이 직접 쓴다.

**"삼각형 수를 메모리 사용량에 대응시켜 측정하였다"는 서술은 원문에 없으며, 원문의 실제 측정 구조와 어긋난다. 반드시 수정할 것.**

### (3) "이 기법들이 성능을 향상시킨다는 것이 입증되었다"고 쓸 수 없다

- **게임 1종, 기기 1대, 버전 2개, 스냅샷 3개, 반복 측정 없음, 통계 검정 없음.** 사례 보고이지 효과 검증이 아니다.
- 다섯 기법을 **한꺼번에 적용**했으므로 어느 기법이 얼마나 기여했는지 알 수 없다. 저자들이 이를 향후 과제로 남겨 두었다.
- 따라서 **"오클루전 컬링은 렌더링 부하를 N% 줄인다", "오브젝트 풀링은 GC 부담을 줄인다는 것이 실증되었다"** 같은 문장은 이 문헌으로 쓸 수 없다. 특히 **오브젝트 풀링에 대해서는 이 연구에서 GC 사용 메모리가 최적화 버전에서 오히려 더 컸다**(4-(5) 참조).

### (4) 사용자 경험·플레이어 반응에 대한 주장을 할 수 없다

- **참가자 0명이다.** 플레이 테스트, 설문, 인터뷰, 생체 측정 어느 것도 없다.
- 원문의 "better user experience", "smooth user experience"는 **FPS 수치에서 저자들이 추론한 표현**이지 측정된 결과가 아니다.
- **"최적화가 플레이어 만족도를 높인다", "몰입을 개선한다" 같은 주장에 이 문헌을 붙이면 안 된다.**

### (5) "큰 게임일수록 효과가 크다"고 단정할 수 없다

초록의 해당 문장은 "these optimizations **could** have a bigger impact"이다. **저자들의 추측이며, 이 연구는 큰 게임을 측정하지 않았다.** 우리 논문에서 단정형으로 옮기면 안 된다.

### (6) 이 문헌을 URP·셰이더·사용자 정의 렌더 패스의 근거로 쓸 수 없다

- 이 논문이 다루는 것은 **모델 폴리곤 수, 정점 병합, 컬러 팔레트 텍스처, 오클루전 컬링, 오브젝트 풀링**이다.
- **셰이더 작성, 렌더 파이프라인 구성, 커스텀 렌더 패스, 화면 공간 조명, 광원 처리에 대한 내용은 전혀 없다.** 우리 3.9절의 URP 사용자 정의 렌더 패스 구현 부분에 이 문헌을 근거로 붙이면 잘못된 인용이다.
- 조명 관련 내용도 없다(그림자 캐스터 수치가 표에 나오지만 논의는 없다).

### (7) Unity의 권고 수치를 이 논문의 발견인 것처럼 쓸 수 없다

"모바일에서는 메시당 폴리곤 300~1500개"는 **Unity 공식 문서(2017년판, 참고문헌 [14])의 권고를 이 논문이 인용한 것**이다. 게다가 원문 스스로 단서를 단다:
> "However, those numbers might be incorrect depending on the number of models that are inside a game scene."

우리 논문이 이 수치를 쓰려면 **Unity 문서를 직접 인용**하거나, 재인용임을 밝혀야 한다. 어느 쪽이든 우리 게임은 PC용이므로 이 권고 자체가 적용 대상이 아니다.

### (8) 다른 최적화 연구와 비교하는 서술에 쓸 수 없다

저자들이 명시적으로 "their results cannot be directly compared with the results of our study"라고 적었다. 이 논문의 수치를 다른 연구의 수치와 나란히 놓는 서술은 저자 본인의 경고에 반한다.

### (9) ※ 원문에서 확인하지 못한 것 — 확인 없이 적지 말 것

- PC 측정 환경의 하드웨어 사양 (CPU, GPU, RAM) — **표가 없다.**
- Unity 버전, Blender 버전 — 본문에 버전 번호가 없다. (참고문헌에 인용된 Unity 문서는 2017·2019.3·2020.1·520 판본이 섞여 있다.)
- 측정 반복 횟수, 측정 시각, 배터리·발열 상태 — 기록되지 않았다.
- 게임의 이름, 전체 오브젝트 수, 맵 크기 — 명시되지 않았다.

## 7. 참고문헌 항목

```
Koulaxidis, G., & Xinogalos, S. (2022). Improving mobile game performance with basic optimization techniques in Unity. Modelling, 3(2), 201–223. https://doi.org/10.3390/modelling3020014
```

(한국어 논문 양식으로 적을 경우 예시)

```
Koulaxidis, G. and Xinogalos, S., "Improving Mobile Game Performance with Basic Optimization Techniques in Unity," Modelling, Vol. 3, No. 2, pp. 201-223, 2022.
```
