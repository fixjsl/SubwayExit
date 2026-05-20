// ──────────────────────────────────────────────────────────────────────────────
// FlashlightFogDust.shader
// 먼지 / 안개 파티클 · 쿼드 전용.
// 손전등 콘 바깥은 완전 투명, 안쪽은 원본 알파를 그대로 유지.
// _EdgeSoft 로 경계 부드러움을 Inspector 에서 실시간 조절 가능.
// ──────────────────────────────────────────────────────────────────────────────
Shader "Custom/FlashlightFogDust"
{
    Properties
    {
        _MainTex    ("Texture", 2D)     = "white" {}
        _Color      ("Tint", Color)     = (1,1,1,0.5)
        [HDR] _EmissionColor ("Emission", Color) = (0,0,0,0)
        // 0 = 하드 엣지, 1 = 경계가 매우 넓고 부드럽게 페이드
        _EdgeSoft   ("Edge Softness", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            // Transparent+1 : 일반 투명 오브젝트보다 나중에 그려져
            //                  손전등 빔 효과 위에 먼지/안개가 올라옴
            "Queue"           = "Transparent+1"
            "IgnoreProjector" = "True"
            "RenderType"      = "Transparent"
            "RenderPipeline"  = "UniversalPipeline"
        }

        Cull Off   // 파티클은 카메라 방향과 무관하게 양면 렌더링
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha  // 표준 알파 블렌딩

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #pragma multi_compile_particles  // 파티클 버텍스 컬러 스트림 활성화

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // ── Flashlight.cs 가 SetGlobal 로 매 프레임 갱신하는 전역 값 ──────
            float2 _FlashlightPos;        // 손전등 위치 (뷰포트 0~1)
            float2 _FlashlightDir;        // 조준 방향 2D 단위벡터
            float  _FlashlightRange;      // 사거리 (뷰포트 스케일)
            float  _FlashlightOuterAngle; // 빔 외각 반각 (도)
            float  _FlashlightInnerAngle; // 빔 내각 반각 (도)
            float  _FlashlightEnabled;    // 0=꺼짐, 1=켜짐

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Color;
                float4 _EmissionColor;
                float  _EdgeSoft;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color      : COLOR;
                float2 uv         : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color      : COLOR;
                float2 uv         : TEXCOORD0;
                float4 screenPos  : TEXCOORD1;  // 콘 판정용 뷰포트 좌표
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes i)
            {
                Varyings o;
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.positionCS = TransformObjectToHClip(i.positionOS.xyz);
                o.uv         = TRANSFORM_TEX(i.uv, _MainTex);
                o.color      = i.color * _Color;
                o.screenPos  = ComputeScreenPos(o.positionCS);
                return o;
            }

            // ── 손전등 콘 마스크 계산 ─────────────────────────────────────────
            // FlashlightRevealSprite 와 동일한 수식이지만
            // _EdgeSoft 값으로 ARC_FADE / ANG_FADE 를 동적으로 조절한다.
            float CalcFlashlightMask(float2 vp, float edgeSoft)
            {
                // edgeSoft 가 클수록 경계 그라데이션 폭이 넓어짐
                float ARC_FADE  = lerp(0.01, 0.08, edgeSoft); // 사거리 경계 그라데 폭
                float EDGE_BLUR = 0.012;                       // 거리 하드엣지 폭 (고정)
                float ANG_FADE  = lerp(0.2,  0.6,  edgeSoft); // 각도 경계 그라데 비율

                // 화면 종횡비 보정
                float  aspect   = _ScreenParams.x / _ScreenParams.y;
                float2 uvA      = float2(vp.x * aspect, vp.y);
                float2 originA  = float2(_FlashlightPos.x * aspect, _FlashlightPos.y);

                // 손전등 좌표계로 분해
                float2 fwd   = normalize(_FlashlightDir + 1e-5); // 조준 방향
                float2 side  = float2(-fwd.y, fwd.x);            // 수직 방향
                float2 delta = uvA - originA;
                float  along = dot(delta, fwd);   // 조준 방향 성분
                float  perp  = abs(dot(delta, side)); // 측면 성분
                float  dist  = length(delta);

                // 사거리 마스크
                float fwdMask  = step(0.0, along)
                               * smoothstep(_FlashlightRange + ARC_FADE,
                                            _FlashlightRange - EDGE_BLUR, dist);

                // 각도 마스크
                float pixelTan  = perp / max(along, 1e-5);
                float outerTan  = tan(radians(max(_FlashlightOuterAngle, 1.0) * 0.5));
                float innerTan  = tan(radians(clamp(_FlashlightInnerAngle, 0.5,
                                                    _FlashlightOuterAngle - 0.5) * 0.5));
                float fadeOuter = outerTan * (1.0 + ANG_FADE);
                float sideMask  = smoothstep(fadeOuter, innerTan, pixelTan);

                return fwdMask * sideMask;
            }

            half4 frag(Varyings i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv) * i.color;

                // 손전등 꺼짐 → 완전 투명
                if (_FlashlightEnabled < 0.5)
                {
                    col.a = 0.0;
                    return col;
                }

                // 뷰포트 UV 복원
                float2 vp        = i.screenPos.xy / i.screenPos.w;
                float  flashMask = CalcFlashlightMask(vp, _EdgeSoft);

                // 원본 알파에 콘 마스크를 곱해 → 콘 밖은 투명, 안쪽은 파티클 원본 투명도
                col.a *= flashMask;

                return col;
            }
            ENDHLSL
        }
    }
}
