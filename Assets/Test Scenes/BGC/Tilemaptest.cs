using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Tilemaptest : MonoBehaviour
{
    public TilemapRenderer TilemapRen = null;
    public Tilemap tilemap;
    TileBase tile;
    //public Sprite 
    void Start()
    {
        
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);


        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                 tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
              
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
