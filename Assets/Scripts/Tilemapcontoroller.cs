using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Tilemapcontoroller : MonoBehaviour
{
    public GameObject defaultspriteobj;
    private GameObject spawnsprite;
    // Start is called before the first frame update
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        /*
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    //defaultspriteobj.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                  //  spawnsprite = Instantiate(defaultspriteobj, this.gameObject.transform, this.gameObject.transform);
                   // defaultspriteobj.layer = 6;
                
                }
                else
                {
                    
                }
            }
        }

        */
    }

    // Update is called once per frame
 
}
