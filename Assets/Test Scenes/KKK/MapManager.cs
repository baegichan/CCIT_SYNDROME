using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    int Level;

    int map_index;
    int width = 500;//사이 간격

    public GameObject room;
    public GameObject Maps;

    [SerializeField]
    GameObject[,] map;
    [SerializeField]
    int a =0 ,b =0;
    [SerializeField]
    Vector2 Start_Map;
    [SerializeField]
    Vector2 Start_Room_Index;
    [SerializeField]
    Vector2 Boss_Room_Index;
    [SerializeField]
    Vector2 Gacha_Room_Index;
    [SerializeField]
    Vector2 Store_Room_Index;
    
    public Vector2 BOSS_ROOM
    {
        get
        {
            return Boss_Room_Index;
        }
        
    }
    
    
    public void Check_Room_Index(Vector2 One, Vector2 targetroom)
    {
         
        if(One==targetroom)
        {
            //change()
            Check_Room_Index(One, targetroom);
        }
        else
        {
            
        }
    }
    private void Start()
    {
        GameObject room = GameObject.FindGameObjectWithTag("Room");
        Start_Room_Index = new Vector2(Level, Level);
        map = new GameObject[(2 * Level) +1, (2 * Level) + 1]; 
        map_index = (2 * Level + 1) * (2 * Level + 1);
        //map[Level, Level].transform.position = new Vector2(0, 0);
        //Start_Map = map[Level, Level].transform.position;
        if(Level==3)
        {
            Boss_Room_Index = new Vector2(Level + (Random.Range(0, 2) == 0 ? Random.Range(2, Level + 1) : -Random.Range( 2, Level + 1)), Level + (Random.Range(0, 2) == 0 ? +Random.Range(2, Level + 1) : -Random.Range( 2, Level + 1)));
      
        }
        else
        {
            Boss_Room_Index = new Vector2(Level + (Random.Range(0, 2) == 0 ? Random.Range(Level / 2, Level + 1) : -Random.Range(Level / 2, Level + 1)), Level + (Random.Range(0, 2) == 0 ? +Random.Range(Level / 2, Level + 1) : -Random.Range(Level / 2, Level + 1)));

        }
        //Level은 3보다 높아야 함



        make_map();
        Map_Move();
    }
    void make_map()
    {
        for (int i = 0; i < map_index; i++)
        {
            map[a, b] = (GameObject)Instantiate(room, new Vector2(0, 0), Quaternion.identity);
            Map_Move();
            map[a, b].transform.parent = GameObject.Find("Maps").transform;
            map[a, b].name = "room_index    " +"[ " + a + " ]" + "[ " + b + " ]" ;
            b++;
            if(b == ((2 * Level)+1))
            {
                b = 0;
                if (a <= ((2 * Level) - 1))
                {
                    a++;
                    Debug.Log(22);
                }
            }
            
        }
    }
    
    void Map_Move()
    {
        if (a <= Level)
        {
            if (b > Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x + ((b - Level) * width), Start_Map.y + ((Level - a) * width));
            }
            else if (b <= Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x - ((Level - b) * width), Start_Map.y + ((Level - a) * width));
            }
        }
        else if (a > Level)
        {
            if (b > Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x + ((b - Level) * width), Start_Map.y - ((a - Level) * width));
            }
            else if (b <= Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x - ((Level - b) * width), Start_Map.y - ((a - Level) * width));
            }
        }

    }









}
