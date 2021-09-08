using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newMapSystem : MonoBehaviour
{
    /// <summary>
    /// Depth_Lever  => mapsize
    /// 1=> 3x3
    /// 2=> 5x5
    /// 
    /// Depth_lever*2+1 
    /// </summary>
   [HideInInspector]
    public int Depth_Lever;
    [HideInInspector]
    public MapData mapdata;

    private GameObject[,] map;
    private Vector2 Start_Map_Index;
    /// <summary>
    /// 카메라 시야 고정을 위해 각 맵사이즈 계산 해서 넣어야됨
    /// </summary>
    private Vector2[,] Map_xysize;

    private Vector2 Current_map;


    public void Move_Right_Map()
    {
        if (Map_Check(new Vector2(Current_map.x+1, Current_map.y)))
        {
            Map_Move_Event(new Vector2(Current_map.x+1, Current_map.y ));
        }
    }
    public void Move_Left_Map()
    {
        if (Map_Check(new Vector2(Current_map.x-1, Current_map.y )))
        {
            Map_Move_Event(new Vector2(Current_map.x-1, Current_map.y ));
        }
    }
    public void Move_Up_Map()
    {
        if (Map_Check(new Vector2(Current_map.x, Current_map.y +1)))
        {
            Map_Move_Event(new Vector2(Current_map.x, Current_map.y + 1));
        }
    }
    public void Move_Down_Map()
    {
       if( Map_Check(new Vector2(Current_map.x, Current_map.y - 1)))
        {
            
               Map_Move_Event(new Vector2(Current_map.x, Current_map.y - 1));
        }
    }

    public void Map_Move_Event(Vector2 xy)
    {
        
    }
    public bool Map_Check(Vector2 current_mapidx)
    {
        if(map[(int)current_mapidx.x, (int)current_mapidx.y]!=null)
        {
            return false;

        }
        else
        {
            return true;
        }
    }

    public void Set_mapdata(MapData map)
    {

        mapdata = map;

    }


    void Start()
    {

        map = new GameObject[Depth_Lever*2+1, Depth_Lever*2+1];
        Start_Map_Index = new Vector2(Depth_Lever, Depth_Lever);
        //mapdata.Get_center(this.gameObject);
       // mapdata.Batch_map();
    }


   

    // Update is called once per frame
    
}
