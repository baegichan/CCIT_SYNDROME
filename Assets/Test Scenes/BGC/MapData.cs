using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "Map Data", menuName = "SYNDROME_MAP/Map_data")]
public class MapData : ScriptableObject
{
    public enum Map_Direction
    {
        x,
        y
    }
    //[SerializeField]
    //GameObject[] Back_Ground_Sprite;
    
    GameObject Tile_Map;
    [SerializeField]
    GameObject Center;


    public BackGroundSprites[] BG;
    float Width;
    float Height;

    public Map_Direction direction = Map_Direction.x;
    public void Get_center(GameObject center)
    {
        Center = center;
    }
    public float Get_Total_Sprite_Width(int j)
    {
        float total = 0;
        
        for (int i = 0; i < BG[j].BackGround.Length; i++)
        {
           total += BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
            return total;
    }
    public float Get_Total_Sprite_Height(int j)
    {
        float total = 0;
        for (int i = 0; i < BG[j].BackGround.Length; i++)
        {
            total += BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
        return total;
    }
    public void Batch_map()
    {
        if(Tile_Map==null)
        {
            Tile_Map = (GameObject)Resources.Load("Grid");
        }
        GameObject Grid=Instantiate(Tile_Map, Vector3.zero, Quaternion.identity, Center.transform);
        for (int j = 0; j < BG.Length; j++)
        {
            if (BG[j].Tilemap == null)
            {
                BG[j].Tilemap = (GameObject)Resources.Load("Tilemap");
            }
            BG[j].Tilemap = Instantiate(BG[j].Tilemap, Vector3.zero, Quaternion.identity, Grid.transform);
            float total = 0;
            switch (direction)
            {
                case Map_Direction.x:
                    total = Get_Total_Sprite_Width(j);

                    break;
                case Map_Direction.y:
                    total = Get_Total_Sprite_Height(j);

                    break;

            }
            for (int i = 0; i < BG[j].BackGround.Length; i++)
            {
                switch (direction)
                {
                    case Map_Direction.x:
                        
                        GameObject BackGround_x=Instantiate(BG[j].BackGround[i], new Vector3(total - BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity, Center.transform);
                        BackGround_x.GetComponent<SpriteRenderer>().sortingOrder = BG[j].OrderInLayer;
                        

                        int Layer_binary =Convert.ToInt32(Convert.ToString(BG[j].SortingLayer.value,2));
                        int Layer_count = 0;
                        for (int L = 0; L < 32; L++)
                        {
                           
                            if (Layer_binary % 2 == 1)
                            {
                                BackGround_x.layer = Layer_count;
                                break;
                            }
                            Layer_binary = Layer_binary / 10;
                            
                            
                            Layer_count++;
                           // LayerMask Layer = (BG[j].SortingLayer.value);
                           // BackGround_x.layer = Layer;
                        }
                        
                        if (i + 1 < BG[j].BackGround.Length)
                        {
                            total -= BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.width / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                        }
                        break;
                    case Map_Direction.y:

                        GameObject BackGround_y = Instantiate(BG[j].BackGround[i], new Vector3(0, total - BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / (2 * BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0), Quaternion.identity, Center.transform);
                        BackGround_y.GetComponent<SpriteRenderer>().sortingOrder = BG[j].OrderInLayer;
                        BackGround_y.layer = BG[j].SortingLayer.value;
                        if (i + 1 < BG[j].BackGround.Length)
                        {
                            total -= BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.rect.height / BG[j].BackGround[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                        }
                        break;

                }
                // Instantiate(Back_Ground_Sprite[i], new Vector3(total - Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity,Center.transform);
                //Back_Ground_Sprite[i].transform.localPosition = new Vector3(total-Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width/2*Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit, 0, 0);
                
            }
           
        }
    }
    public Sprite Get_Sprite(int index)
    {
        return BG[0].BackGround[index].GetComponent<SpriteRenderer>().sprite;
    }
    public int Get_length(int index)
    {
        return BG[index].BackGround.Length;
    }
   public void Save_WH(int j)
    {

        if(direction==Map_Direction.x)
        {
            Width = Get_Total_Sprite_Width(j);
            Height= BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.rect.height / BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
        else
        {

            Width = BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.rect.width / BG[0].BackGround[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            Height = Get_Total_Sprite_Height(j);
        }
      

    }
}
[Serializable]
public class BackGroundSprites 
{
    [Tooltip("기획변경으로 인해 안쓸듯")]
   public GameObject[] BackGround;
   public GameObject Tilemap;
    [Tooltip("넉넉하게 잡은거임")]
    [Range(-30,-40)]public int OrderInLayer;
    [Tooltip("Please do not chose multiple Layer")]
    public LayerMask SortingLayer;
   


}
[Serializable]
public class Event
{
    public GameObject[] Events;
    


}