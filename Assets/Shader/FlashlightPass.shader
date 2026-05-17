Shader "Custom/FlashlightPass"
{
    Properties {}

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        ZWrite Off
        ZTest Always
        Cull Off
        Blend One One

        Pass
        {
            Name "FlashlightPass"

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float3 _FlashlightPos;
            float3 _FlashlightDir;
            float  _FlashlightRange;
            float  _FlashlightOuterAngle;   // Mathf.Cos(outerAngle)
            float  _FlashlightInnerAngle;   // Mathf.Cos(innerAngle)
            float  _FlashlightIntensity;
            float4 _FlashlightColor;
            float  _FlashlightEnabled;
            float  _FlashlightNearRange;

            half4 Frag(Varyings input) : SV_Target
            {
                if (_FlashlightEnabled < 0.5)
                    return half4(1, 0, 0, 1); // DEBUG: 빨강 = 꺼진 상태

                float2 uv    = input.texcoord;
                float  depth = SampleSceneDepth(uv);

                #if UNITY_REVERSED_Z
                    if (depth < 1e-5) return half4(0, 0, 0, 0);
                #else
                    if (depth > 1.0 - 1e-5) return half4(0, 0, 0, 0);
                #endif

                float3 worldPos  = ComputeWorldSpacePosition(uv, depth, UNITY_MATRIX_I_VP);
                float3 toPixel   = worldPos - _FlashlightPos;
                float2 toPixelXY = float2(toPixel.x, toPixel.y);
                float  distXY    = max(length(toPixelXY), 0.001);

                float distFade = saturate(1.0 - distXY / _FlashlightRange);
                if (distFade <= 0) return half4(0, 0, 0, 0);

                float  flashDirLen = length(_FlashlightDir.xy);
                if (flashDirLen < 0.001) return half4(0, 0, 0, 0);
                float2 pixelDirXY = toPixelXY / distXY;
                float2 flashDirXY = _FlashlightDir.xy / flashDirLen;
                float  cosTheta   = dot(pixelDirXY, flashDirXY);
                float  spotFade   = saturate((cosTheta - _FlashlightOuterAngle) /
                                            max(_FlashlightInnerAngle - _FlashlightOuterAngle, 0.001));

                float finalFade = spotFade * distFade * _FlashlightIntensity;
                return half4(_FlashlightColor.rgb * finalFade, 1);
            }
            ENDHLSL
        }
    }
}
