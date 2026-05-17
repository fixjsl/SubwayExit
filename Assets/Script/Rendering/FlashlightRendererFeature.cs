using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class FlashlightRendererFeature : ScriptableRendererFeature
{
    [SerializeField] private Material passMaterial;
    [SerializeField] private RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;

    private FlashlightPass _pass;

    public override void Create()
    {
        _pass = new FlashlightPass(passMaterial, passEvent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (passMaterial == null) return;
        renderer.EnqueuePass(_pass);
    }

    private class FlashlightPass : ScriptableRenderPass
    {
        private readonly Material _mat;

        public FlashlightPass(Material mat, RenderPassEvent evt)
        {
            _mat = mat;
            renderPassEvent = evt;
            ConfigureInput(ScriptableRenderPassInput.Depth);
        }

        private class PassData { public Material mat; }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (_mat == null) return;

            var resourceData = frameData.Get<UniversalResourceData>();
            if (!resourceData.cameraDepthTexture.IsValid()) return;

            using var builder = renderGraph.AddRasterRenderPass<PassData>("FlashlightPass", out var passData);
            passData.mat = _mat;
            builder.SetRenderAttachment(resourceData.activeColorTexture, 0, AccessFlags.ReadWrite);
            builder.UseTexture(resourceData.cameraDepthTexture, AccessFlags.Read);

            builder.SetRenderFunc(static (PassData data, RasterGraphContext ctx) =>
            {
                ctx.cmd.DrawProcedural(Matrix4x4.identity, data.mat, 0, MeshTopology.Triangles, 3);
            });
        }
    }
}
