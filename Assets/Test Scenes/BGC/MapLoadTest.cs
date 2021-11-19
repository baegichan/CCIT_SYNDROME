using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoadTest : MonoBehaviour
{
    
    
     MapData testmap;
    public Map_data_Set Mapset;
     GameObject Target_Room;
     
  
    public void Starting_Setting()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Target_Room = transform.GetChild(i).gameObject;
            if (Target_Room.GetComponent<Room_data>().Room_Created)
            {
                testmap = Mapset.Get_RandomRoom(Target_Room.GetComponent<Room_data>().map_code);
                testmap.Load_MapData(Target_Room);
            }
        }
    }
  
    // Update is called once per frame
}
