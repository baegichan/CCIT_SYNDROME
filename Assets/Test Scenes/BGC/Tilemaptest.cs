using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Tilemaptest : MonoBehaviour
{
    public TilemapRenderer TilemapRen = null;
    public Tilemap tilemap;
    void Start()
    {
        
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);


        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y +  " tile: "+tile.name);
                    Debug.Log( tilemap.GetTile(new Vector3Int(0, 0,0)).name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
            //x ·çÆ¾

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
