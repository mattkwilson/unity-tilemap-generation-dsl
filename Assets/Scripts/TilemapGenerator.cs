using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.AST;

public class TilemapGenerator : MonoBehaviour
{
    public GameObject TilemapPrefab;
    public Sprite BaseTileSprite;
    
    public List<Sprite> Textures;
    public int TextureSize;

    [HideInInspector]
    public string DSLInput;

    // DSL API
    private Tilemap tilemap;

    public void Canvas(int width, int height) {
        GameObject tilemapGameObject = Instantiate(TilemapPrefab);
        tilemap = tilemapGameObject.GetComponentInChildren<Tilemap>();
    }

    public void Fill(int x, int y, int width, int height, Color32 color32) {
        Debug.Log("Filling: " + x + " " + y + " " + width + " " + height + " " + color32.ToString());
        Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
        tile.sprite = BaseTileSprite;
        tile.color = color32;
        for(int i = x; i < x + width; i++){
            for(int j = y; j < y + height; j++){
                tilemap.SetTile(new Vector3Int(i,j,0), tile);
            }
        }
    }

    public void Fill(int x, int y, int width, int height, Assets.Scripts.AST.Texture texture) {
        Debug.Log("Filling: " + x + " " + y + " " + width + " " + height + " with texture");
        Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
        tile.sprite = Textures[texture.GetIndex()];
        for(int i = x; i < x + width; i++){
            for(int j = y; j < y + height; j++){
                tilemap.SetTile(new Vector3Int(i,j,0), tile);
            }
        }
    }
}
