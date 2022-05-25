using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public GameObject TilemapPrefab;
    public Sprite BaseTileSprite;

    [HideInInspector]
    public string DSLInput;

    // DSL API
    private Tilemap tilemap;

    public void Canvas(int width, int height) {
        GameObject tilemapGameObject = Instantiate(TilemapPrefab);
        tilemap = tilemapGameObject.GetComponentInChildren<Tilemap>();
    }

    public void Fill(int x, int y, int width, int height, Color32 color32) {
        Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
        tile.sprite = BaseTileSprite;
        tile.color = color32;
        for(int i = x; i < x + width; i++){
            for(int j = y; j < y + height; j++){
                tilemap.SetTile(new Vector3Int(i,j,0), tile);
            }
        }

    }
}
