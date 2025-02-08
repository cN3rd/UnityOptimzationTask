using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FireHazardScriptableObject))]
public class OurCoolInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.HelpBox("Some help box", MessageType.Info);
    }
}
