#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class BearAnimationPostprocessor : AssetPostprocessor
{
    void OnPostprocessAnimation(GameObject root, AnimationClip clip)
    {
        if (!assetPath.Contains("mBear")) return;

        var bindings = AnimationUtility.GetCurveBindings(clip);
        foreach (var binding in bindings)
        {
            // 루트 오브젝트(path가 비어있는)의 스케일 커브만 제거
            if (binding.path == "" && binding.propertyName.StartsWith("m_LocalScale"))
                AnimationUtility.SetEditorCurve(clip, binding, null);
        }
    }
}
#endif
