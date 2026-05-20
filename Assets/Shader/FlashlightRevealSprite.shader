// ──────────────────────────────────────────────────────────────────────────────
// FlashlightRevealSprite.shader
// SpriteRenderer 용. 손전등 콘 바깥은 완전 투명, 안쪽은 원본 스프라이트 출력.
// Flashlight.cs 가 매 프레임 SetGlobal 로 보내는 값을 그대로 읽어서 판정하므로
// 별도 C# 없이 머티리얼 교체만 해도 동작한다.
// ──────────────────────────────────────────────────────────────────────────────
Shader "Custom/FlashlightRevealSprite"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color   ("Tint", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"             = "Transparent"
            "IgnoreProjector"   = "True"
            "RenderType"        = "Transparent"
            "PreviewType"       = "Plane"
            "CanUseSpriteAtlas" = "True"
            "RenderPipeline"    = "UniversalPipeline"
        }

        Cull Off       // 스프라이트는 양면 렌더링
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha  // 표준 알파 블렌딩

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // ── Flashlight.cs 가 SetGlobal 로 매 프레임 갱신하는 전역 값 ──────
            float2 _FlashlightPos;        // 손전등 위치 (뷰포트 0~1)
            float2 _FlashlightDir;        // 조준 방향 2D 단위벡터
            float  _FlashlightRange;      // 사거리 (뷰포트 스케일)
            float  _FlashlightOuterAngle; // 빔 외각 반각 (도)
            float  _FlashlightInnerAngle; // 빔 내각 반각 (도) — 중심 하이라이트 경계
            float  _FlashlightEnabled;    // 0=꺼짐, 1=켜짐

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Color;
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
                float4 screenPos  : TEXCOORD1;  // 콘 판정에 쓸 뷰포트 좌표
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
                o.screenPos  = ComputeScreenPos(o.positionCS); // 클립공간 → 뷰포트 준비
                return o;
            }

            // ── 손전등 콘 마스크 계산 ─────────────────────────────────────────
            // FlashlightPass.shader 와 완전히 동일한 수식.
            // 반환값: 0.0(콘 밖/꺼짐) ~ 1.0(콘 정중앙)
            float CalcFlashlightMask(float2 vp)
            {
                // 거리 경계 그라데이션 폭
                static const float ARC_FADE  = 0.04;
                // 거리 경계 안쪽 하드엣지 폭
                static const float EDGE_BLUR = 0.012;
                // 각도 경계 바깥쪽 그라데이션 비율
                static const float ANG_FADE  = 0.45;

                // 화면 종횡비 보정 — X축을 늘려 픽셀이 정사각형처럼 계산되게 함
                float  aspect   = _ScreenParams.x / _ScreenParams.y;
                float2 uvA      = float2(vp.x * aspect, vp.y);
                float2 originA  = float2(_FlashlightPos.x * aspect, _FlashlightPos.y);

                // 손전등 좌표계로 분해
                float2 fwd   = normalize(_FlashlightDir + 1e-5); // 조준 방향
                float2 side  = float2(-fwd.y, fwd.x);            // 수직 방향
                float2 delta = uvA - originA;                     // 픽셀 → 손전등 벡터
                float  along = dot(delta, fwd);   // 조준 방향 성분 (앞=양수)
                float  perp  = abs(dot(delta, side)); // 측면 성분 (절댓값)
                float  dist  = length(delta);

                // 사거리 마스크: 손전등 앞쪽 + 사거리 이내
                float fwdMask  = step(0.0, along)  // 뒤쪽이면 0
                               * smoothstep(_FlashlightRange + ARC_FADE,
                                            _FlashlightRange - EDGE_BLUR, dist);

                // 각도 마스크: pixelTan 이 outerTan 보다 작으면 콘 안쪽
                float pixelTan  = perp / max(along, 1e-5);
                float outerTan  = tan(radians(max(_FlashlightOuterAngle, 1.0) * 0.5));
                float innerTan  = tan(radians(clamp(_FlashlightInnerAngle, 0.5,
                                                    _FlashlightOuterAngle - 0.5) * 0.5));
                float fadeOuter = outerTan * (1.0 + ANG_FADE); // 그라데이션 바깥 경계
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

                // 뷰포트 UV 복원 (ComputeScreenPos 는 w 로 나눠야 실제 0~1 좌표)
                float2 vp        = i.screenPos.xy / i.screenPos.w;
                float  flashMask = CalcFlashlightMask(vp);

                // 원본 알파에 콘 마스크를 곱해 → 콘 밖은 0, 안쪽은 원본 투명도 유지
                col.a *= flashMask;

                return col;
            }
            ENDHLSL
        }
    }
}
