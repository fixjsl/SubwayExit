using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;

public class FlashlightRendererFeature : ScriptableRendererFeature
{
    [SerializeField] private Material passMaterial;
    // 스프라이트/투명 오브젝트가 모두 그려진 뒤 처리
    [SerializeField] private RenderPassEvent passEvent = RenderPassEvent.AfterRenderingTransparents;
    private FlashlightPass _pass;

    public override void Create() => _pass = new FlashlightPass(passMaterial, passEvent);

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (passMaterial == null) return;
        renderer.EnqueuePass(_pass);
    }

    private class FlashlightPass : ScriptableRenderPass
    {
        private readonly Material _mat;

        private static readonly int BlitTextureID    = Shader.PropertyToID("_BlitTexture");
        private static readonly int BlitScaleBiasID  = Shader.PropertyToID("_BlitScaleBias");
        private static readonly int FlEnabledID     = Shader.PropertyToID("_FlashlightEnabled");
        private static readonly int FlPosID         = Shader.PropertyToID("_FlashlightPos");
        private static readonly int FlDirID         = Shader.PropertyToID("_FlashlightDir");
        private static readonly int FlRangeID       = Shader.PropertyToID("_FlashlightRange");
        private static readonly int FlOuterAngleID  = Shader.PropertyToID("_FlashlightOuterAngle");
        private static readonly int FlInnerAngleID  = Shader.PropertyToID("_FlashlightInnerAngle");
        private static readonly int FlIntensityID   = Shader.PropertyToID("_FlashlightIntensity");
        private static readonly int FlColorID       = Shader.PropertyToID("_FlashlightColor");

        public FlashlightPass(Material mat, RenderPassEvent evt)
        {
            _mat = mat;
            renderPassEvent = evt;
            ConfigureInput(ScriptableRenderPassInput.Color);
        }

        private class PassData
        {
            public Material        mat;
            public TextureHandle   source;
            public float           flEnabled;
            public Vector4         flPos;
            public Vector4         flDir;
            public float           flRange;
            public float           flOuterAngle;
            public float           flInnerAngle;
            public float           flIntensity;
            public Color           flColor;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (_mat == null) return;

            var resourceData = frameData.Get<UniversalResourceData>();
            var cameraData   = frameData.Get<UniversalCameraData>();

            RenderTextureDescriptor desc = cameraData.cameraTargetDescriptor;
            desc.depthBufferBits = 0;

            // 현재 화면 스냅샷 복사
            TextureHandle sourceCopy  = UniversalRenderer.CreateRenderGraphTexture(renderGraph, desc, "FlashlightSourceCopy", false);
            TextureHandle activeColor = resourceData.activeColorTexture;
            renderGraph.AddBlitPass(activeColor, sourceCopy, Vector2.one, Vector2.zero);

            using var builder = renderGraph.AddRasterRenderPass<PassData>("FlashlightPass", out var passData, new ProfilingSampler("FlashlightPass"));
            builder.AllowGlobalStateModification(true);

            passData.mat        = _mat;
            passData.source     = sourceCopy;
            passData.flEnabled  = Shader.GetGlobalFloat("_FlashlightEnabled");
            passData.flPos      = Shader.GetGlobalVector("_FlashlightPos");
            passData.flDir      = Shader.GetGlobalVector("_FlashlightDir");
            passData.flRange      = Shader.GetGlobalFloat("_FlashlightRange");
            passData.flOuterAngle = Shader.GetGlobalFloat("_FlashlightOuterAngle");
            passData.flInnerAngle = Shader.GetGlobalFloat("_FlashlightInnerAngle");
            passData.flIntensity  = Shader.GetGlobalFloat("_FlashlightIntensity");
            passData.flColor    = Shader.GetGlobalColor("_FlashlightColor");

            builder.SetRenderAttachment(activeColor, 0, AccessFlags.Write);
            builder.UseTexture(passData.source, AccessFlags.Read);

            builder.SetRenderFunc(static (PassData data, RasterGraphContext ctx) =>
            {
                data.mat.SetTexture(BlitTextureID,   data.source);
                data.mat.SetVector(BlitScaleBiasID, new Vector4(1f, 1f, 0f, 0f));
                data.mat.SetFloat(FlEnabledID,      data.flEnabled);
                data.mat.SetVector(FlPosID,         data.flPos);
                data.mat.SetVector(FlDirID,         data.flDir);
                data.mat.SetFloat(FlRangeID,        data.flRange);
                data.mat.SetFloat(FlOuterAngleID,   data.flOuterAngle);
                data.mat.SetFloat(FlInnerAngleID,   data.flInnerAngle);
                data.mat.SetFloat(FlIntensityID,    data.flIntensity);
                data.mat.SetColor(FlColorID,        data.flColor);

                ctx.cmd.DrawProcedural(Matrix4x4.identity, data.mat, 0, MeshTopology.Triangles, 3);
            });
        }
    }
}
