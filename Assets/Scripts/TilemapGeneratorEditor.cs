using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(TilemapGenerator))]
public class TilemapGeneratorEditor : Editor
{
    private TilemapGenerator tilemapGenerator;
    private Vector2 scrollPosition = Vector2.zero;
    void OnEnable() {
        tilemapGenerator = target as TilemapGenerator;
    }

    void OnDisable() {

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

         DrawDefaultInspector();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(500));
        serializedObject.FindProperty("DSLInput").stringValue = EditorGUILayout.TextArea(serializedObject.FindProperty("DSLInput").stringValue, GUILayout.ExpandHeight(true)); 
        EditorGUILayout.EndScrollView();
        if(GUILayout.Button("Generate Tilemap")) {
            tilemapGenerator.Canvas(100, 100);
            tilemapGenerator.Fill(0, 0, 10, 1, Color.blue);
            tilemapGenerator.Fill(0, 1, 10, 1, Color.red);
            tilemapGenerator.Fill(0, 2, 10, 1, Color.green);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
