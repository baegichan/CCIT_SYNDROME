using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Map Data", menuName = "SYNDROME_MAP/Map_data")]
public class MapData : ScriptableObject
{
    public enum Map_Direction
    {
        x,
        y
    }
    [SerializeField]
   GameObject[] Back_Ground_Sprite;
    [SerializeField]
    GameObject Tile_Map;
    [SerializeField]
    GameObject Center;
    
    
    float Width;
    float Height;

    public Map_Direction direction = Map_Direction.x;
    public void Get_center(GameObject center)
    {
        Center = center;
    }
    public float Get_Total_Sprite_Width()
    {
        float total = 0;
        for (int i = 0; i < Back_Ground_Sprite.Length; i++)
        {
           total +=Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width/ Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
            return total;
    }
    public float Get_Total_Sprite_Height()
    {
        float total = 0;
        for (int i = 0; i < Back_Ground_Sprite.Length; i++)
        {
            total += Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.height / Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
        return total;
    }
    public void Batch_map()
    {
        float total = 0;
        switch (direction)
        {
            case Map_Direction.x:
                total = Get_Total_Sprite_Width() / 2;
         
                break;
            case Map_Direction.y:
                total = Get_Total_Sprite_Height() / 2;
           
                break;

        }
        Instantiate(Tile_Map, Vector3.zero, Quaternion.identity, Center.transform);
        for (int i = 0; i < Back_Ground_Sprite.Length; i++)
        {
            switch(direction)
            {
                case Map_Direction.x:
                 
                    Instantiate(Back_Ground_Sprite[i], new Vector3(total - Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity, Center.transform);
                    if (i + 1 < Back_Ground_Sprite.Length)
                    {
                        total -= Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width / Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                    }
                    break;
                case Map_Direction.y:
                  
                    Instantiate(Back_Ground_Sprite[i], new Vector3(0, total - Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.height / (2 * Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0), Quaternion.identity, Center.transform);
                    if (i + 1 < Back_Ground_Sprite.Length)
                    {
                        total -= Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.height/ Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
                    }
                    break;

            }
           // Instantiate(Back_Ground_Sprite[i], new Vector3(total - Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width / (2 * Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit), 0, 0), Quaternion.identity,Center.transform);
            //Back_Ground_Sprite[i].transform.localPosition = new Vector3(total-Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.rect.width/2*Back_Ground_Sprite[i].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit, 0, 0);
           
        } 
    }
    public Sprite Get_Sprite(int index)
    {
        return Back_Ground_Sprite[index].GetComponent<SpriteRenderer>().sprite;
    }
    public int Get_length()
    {
        return Back_Ground_Sprite.Length;
    }
   public void Save_WH()
    {

        if(direction==Map_Direction.x)
        {
            Width = Get_Total_Sprite_Width();
            Height= Back_Ground_Sprite[0].GetComponent<SpriteRenderer>().sprite.rect.height / Back_Ground_Sprite[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        }
        else
        {

            Width = Back_Ground_Sprite[0].GetComponent<SpriteRenderer>().sprite.rect.width / Back_Ground_Sprite[0].GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            Height = Get_Total_Sprite_Height();
        }
      

    }
}
