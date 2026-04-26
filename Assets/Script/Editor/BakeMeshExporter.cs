using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
#endif

public class BakeMeshExporter : MonoBehaviour
{
    [ContextMenu("Export Baked Pose to FBX")]
    public void ExportBakedMesh()
    {
#if UNITY_EDITOR
        SkinnedMeshRenderer[] smrs = GetComponentsInChildren<SkinnedMeshRenderer>();
        if (smrs.Length == 0)
        {
            Debug.LogError("No SkinnedMeshRenderer found!");
            return;
        }

        GameObject root = new GameObject(gameObject.name + "_BakedPose");

        foreach (SkinnedMeshRenderer smr in smrs)
        {
            Mesh bakedMesh = new Mesh();
            smr.BakeMesh(bakedMesh);

            GameObject child = new GameObject(smr.gameObject.name);
            child.transform.SetParent(root.transform, false);

            MeshFilter mf = child.AddComponent<MeshFilter>();
            MeshRenderer mr = child.AddComponent<MeshRenderer>();
            mf.mesh = bakedMesh;
            mr.materials = smr.materials;
        }

        string path = "Assets/BakedPose_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".fbx";
        ModelExporter.ExportObject(path, root);

        DestroyImmediate(root);
        AssetDatabase.Refresh();

        Debug.Log($"Exported: {path}");
#endif
    }
}
