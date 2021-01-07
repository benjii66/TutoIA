using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IA_WaypointSystem))]
public class IA_WaypointSystemEditor : Editor
{
    IA_WaypointSystem pattern = null;
    SerializedProperty points = null;

    private void OnEnable()
    {
        pattern = (IA_WaypointSystem)target;
        points = serializedObject.FindProperty("waypoints");
    }


    private void OnSceneGUI()
    {
        if (points == null) return;
        serializedObject.Update();
        for (int i = 0; i < points.arraySize; i++)
            points.GetArrayElementAtIndex(i).vector3Value = Handles.DoPositionHandle(points.GetArrayElementAtIndex(i).vector3Value, Quaternion.identity);
        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add point"))
            pattern.AddPoint();
        if (GUILayout.Button("Clear points"))
            pattern.Clear();
    }
}
