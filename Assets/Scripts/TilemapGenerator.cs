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
    
    public int Seed;

    [HideInInspector]
    public string DSLInput;

    // DSL API
    private Tilemap baseTilemap;
    private Tilemap transparentMap;
    private List<Sprite> texturesWithTransparency;

    public void UpdateTransparentTextureList() {
        foreach(Sprite sprite in Textures) {
            Texture2D tex = sprite.texture;
            Color32[] pixels = tex.GetPixels32();
            foreach(Color32 pixel in pixels) {
                
                if(pixel.a != 255) {
                    texturesWithTransparency.Add(sprite);
                    break;
                }
            }
        }
    }

    public void Canvas(int width, int height) {
        GameObject tilemapGameObject = Instantiate(TilemapPrefab);
        Tilemap[] tilemaps = tilemapGameObject.GetComponentsInChildren<Tilemap>();
        baseTilemap = tilemaps[0].gameObject.name == "BaseTilemap" ? tilemaps[0] : tilemaps[1];
        transparentMap = tilemaps[0].gameObject.name == "TransparentMap" ? tilemaps[0] : tilemaps[1];
    }

    public void Fill(int x, int y, int width, int height, Color32 color32) {
        // Debug.Log("Filling: " + x + " " + y + " " + width + " " + height + " " + color32.ToString());
        Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
        tile.sprite = BaseTileSprite;
        tile.color = color32;
        for(int i = x; i < x + width; i++){
            for(int j = y; j < y + height; j++){
                baseTilemap.SetTile(new Vector3Int(i,j,0), tile);
            }
        }
    }

    public void Fill(int x, int y, int width, int height, Assets.Scripts.AST.Texture texture) {
        // Debug.Log("Filling: " + x + " " + y + " " + width + " " + height + " with texture");
        Tile tile = ScriptableObject.CreateInstance("Tile") as Tile;
        tile.sprite = Textures[texture.GetIndex()];

        Tilemap tilemap = baseTilemap;
        if(texturesWithTransparency.Contains(tile.sprite)) {
            tilemap = transparentMap;
        } 

        for(int i = x; i < x + width; i++){
            for(int j = y; j < y + height; j++){
                tilemap.SetTile(new Vector3Int(i,j,0), tile);
            }
        }
    }
}
