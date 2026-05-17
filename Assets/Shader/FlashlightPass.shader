Shader "Custom/FlashlightPass"
{
    Properties {}

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off ZTest Always Cull Off Blend Off

        Pass
        {
            Name "FlashlightPass"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float2 _FlashlightPos;        // 플레이어 뷰포트 좌표 (0~1)
            float2 _FlashlightDir;        // 조준 방향 2D 단위벡터
            float  _FlashlightRange;      // 사거리 (뷰포트 스케일)
            float  _FlashlightOuterAngle; // 빔 외각 반각(도)
            float  _FlashlightInnerAngle; // 빔 내각 반각(도)
            float  _FlashlightIntensity;
            float4 _FlashlightColor;
            float  _FlashlightEnabled;
            // x=바닥 뷰포트Y, y=천장 뷰포트Y, z=하이라이트 blur 두께
            float4 _FloorCeilingY;

            static const float EDGE_BLUR   = 0.012; // 내부 하드 엣지 폭
            static const float ARC_FADE    = 0.08;  // 호 끝에서 dark까지 그라데 폭
            static const float ANG_FADE    = 0.45;  // 각도 경계 바깥쪽 그라데 비율 (outerTan 기준)
            static const float BOUND_FADE  = 0.05;  // 바닥/천장 경계 그라데 폭
            static const float DARK_FACTOR = 0.02;
            static const float BEAM_DIM    = 0.05; // 빔 안 원본 색상 밝기 배수 (낮출수록 어두움)

            half4 Frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                float2 uv  = input.texcoord;
                float4 col = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv);

                if (_FlashlightEnabled < 0.5)
                    return col * DARK_FACTOR;

                float floorY = _FloorCeilingY.x;
                float ceilY  = _FloorCeilingY.y;

                // ── 플레이 영역 경계 그라데 마스크 ──────────────────────
                // 바닥 아래/천장 위 → 0 (어둡), 경계 근처 → 서서히 1
                float floorFade = smoothstep(floorY - BOUND_FADE, floorY + BOUND_FADE, uv.y);
                float ceilFade  = smoothstep(ceilY  + BOUND_FADE, ceilY  - BOUND_FADE, uv.y);
                float playMask  = floorFade * ceilFade;

                // 완전히 바깥이면 early return으로 연산 절약
                if (playMask < 0.001)
                    return col * DARK_FACTOR;

                // ── 종횡비 보정 ───────────────────────────────────────────
                float  aspect  = _ScreenParams.x / _ScreenParams.y;
                float2 uvA     = float2(uv.x * aspect, uv.y);
                float2 originA = float2(_FlashlightPos.x * aspect, _FlashlightPos.y);

                // ── 빔 좌표계 분해 ───────────────────────────────────────
                float2 fwd  = normalize(_FlashlightDir + 1e-5);
                float2 side = float2(-fwd.y, fwd.x);
                float2 delta = uvA - originA;
                float  along = dot(delta, fwd);
                float  perp  = abs(dot(delta, side));

                // ── 부채꼴 사거리 마스크 (원호 끝) ──────────────────────
                // ARC_FADE만큼 경계 바깥까지 smoothstep을 연장 → dark_factor까지 서서히 그라데
                float actualDist = length(delta);
                float fwdMask = step(0.0, along)
                              * smoothstep(_FlashlightRange + ARC_FADE, _FlashlightRange - EDGE_BLUR, actualDist);

                // ── 원뿔 각도 마스크 ─────────────────────────────────────
                // outerTan * (1 + ANG_FADE) 까지 smoothstep 연장 → 측면도 서서히 그라데
                float pixelTan  = perp / max(along, 1e-5);
                float outerTan  = tan(radians(max(_FlashlightOuterAngle, 1.0) * 0.5));
                float innerTan  = tan(radians(clamp(_FlashlightInnerAngle, 0.5, _FlashlightOuterAngle - 0.5) * 0.5));
                float fadeOuter = outerTan * (1.0 + ANG_FADE); // 그라데 바깥 경계
                float sideMask  = smoothstep(fadeOuter, innerTan, pixelTan);

                // ── 거리 감쇠 ────────────────────────────────────────────
                float falloff = 1.0 - saturate(actualDist / _FlashlightRange);
                falloff *= falloff;

                float mask = fwdMask * sideMask;

                // ── 바닥/천장 반사 하이라이트 ────────────────────────────
                // 빔의 호(arc)가 floor/ceiling Y선과 만나는 지점에서만 발생:
                //   - uv.y가 해당 Y선에 가까울 것 (smoothstep)
                //   - 빔 원뿔 안쪽일 것 (pixelTan <= outerTan)
                //   - 플레이어 앞쪽일 것 (fwdMask)
                float blur = max(_FloorCeilingY.z, 0.005);
                float inCone   = step(pixelTan, fadeOuter) * fwdMask; // 원뿔+전방 마스크 (그라데 영역 포함)
                float floorHL  = smoothstep(blur, 0.0, abs(uv.y - floorY)) * inCone * falloff;
                float ceilHL   = smoothstep(blur, 0.0, abs(uv.y - ceilY))  * inCone * falloff;
                float surfaceBoost = max(floorHL, ceilHL);

                // ── 최종 합성 ────────────────────────────────────────────
                float3 mainLight    = _FlashlightColor.rgb * _FlashlightIntensity * falloff * mask;
                float3 surfaceLight = _FlashlightColor.rgb * _FlashlightIntensity * surfaceBoost * 1.5;
                // 빔 안도 원본 색상을 BEAM_DIM 배율로 줄임 → 더 어두운 손전등 느낌
                float4 lit          = col * BEAM_DIM + float4(mainLight + surfaceLight, 0.0);
                // playMask로 바닥/천장 경계도 함께 페이드
                float  totalMask    = saturate(mask + surfaceBoost) * playMask;

                return lerp(col * DARK_FACTOR, lit, totalMask);
            }
            ENDHLSL
        }
    }
}
