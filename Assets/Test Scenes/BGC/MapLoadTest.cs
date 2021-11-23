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
            Room_data Saving = Target_Room.GetComponent<Room_data>();
            if (Saving.Room_Created)
            {
                switch(Saving.Room_Type)
                {
                    case Room_data.RoomType.StartRoom:
                        testmap = Mapset.Get_RandomRoom(16);
                        Target_Room.GetComponent<Room_data>().Visit();
                        Debug.Log("Load StartRoom");
                        break;
                    case Room_data.RoomType.Shop:
                        testmap = Mapset.Get_RandomRoom(17);
                        Debug.Log("Load ShopRoom");
                        break;
                    case Room_data.RoomType.Boss:
                        testmap = Mapset.Get_RandomRoom(19);
                        Debug.Log("Load BossMap");
                        break;
                    case Room_data.RoomType.Crane:
                        testmap = Mapset.Get_RandomRoom(18);
                        Debug.Log("Load CraneRoom");
                        break;
                    default:
                        testmap = Mapset.Get_RandomRoom(Target_Room.GetComponent<Room_data>().map_code);
                        Debug.Log("Load NomalRoom");
                        break;
                }
                testmap.Load_MapData(Target_Room);
                testmap.Load_TileCollider(Target_Room);
            }
        }
    }
  
    // Update is called once per frame
}
