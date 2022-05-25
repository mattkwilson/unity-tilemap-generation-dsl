using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
            GameObject tilemapGameObject = Instantiate(tilemapGenerator.TilemapPrefab);
            Tilemap tilemap = tilemapGameObject.GetComponentInChildren<Tilemap>();
            Tile tileBlue = ScriptableObject.CreateInstance("Tile") as Tile;
            Tile tileRed = ScriptableObject.CreateInstance("Tile") as Tile;
            Tile tileGreen = ScriptableObject.CreateInstance("Tile") as Tile;
            tileBlue.sprite = tilemapGenerator.BaseTileSprite;
            tileRed.sprite = tilemapGenerator.BaseTileSprite;
            tileGreen.sprite = tilemapGenerator.BaseTileSprite;
            tileBlue.color = Color.blue;
            tileRed.color = Color.red;
            tileGreen.color = Color.green;
            for(int i = 0; i < 10; i++ ) {
                tilemap.SetTile(new Vector3Int(i,0,0), tileBlue);
                tilemap.SetTile(new Vector3Int(i,1,0), tileRed);
                tilemap.SetTile(new Vector3Int(i,2,0), tileGreen);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
