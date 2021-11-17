using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Tilemaptest : MonoBehaviour
{
    public Tilemap tilemap;
    void Start()
    {
     
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            TileBase tile = allTiles[x];
            if (tile != null)
            {
                Debug.Log("x:" + x + " tile:" + tile.name);
            }
        }
        for (int y = 0; y< bounds.size.y; y++)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
