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
                       // testmap = Mapset.Get_RandomRoom(16);
                        testmap = Mapset.Get_RandomSpecialRoom(Target_Room.GetComponent<Room_data>().map_code);
                        Target_Room.GetComponent<Room_data>().Visit();
                       // Target_Room.transform.Find("NPC").GetComponent<NPCManager>().Setting(Room_data.RoomType.StartRoom);

                        Debug.Log("Load StartRoom");
                        break;
                    case Room_data.RoomType.Shop:
                       // testmap = Mapset.Get_RandomRoom(17);
                        testmap = Mapset.Get_RandomSpecialRoom(Target_Room.GetComponent<Room_data>().map_code);
                       // Target_Room.transform.Find("NPC").GetComponent<NPCManager>().Setting(Room_data.RoomType.Shop);
                        Debug.Log("Load ShopRoom");
                        break;
                    case Room_data.RoomType.Boss:
                       // testmap = Mapset.Get_RandomRoom(19);
                        testmap = Mapset.Get_RandomSpecialRoom(Target_Room.GetComponent<Room_data>().map_code);
                       // Target_Room.transform.Find("NPC").GetComponent<NPCManager>().Setting(Room_data.RoomType.Boss);
                        Debug.Log("Load BossMap");
                        break;
                    case Room_data.RoomType.Crane:
                        //testmap = Mapset.Get_RandomRoom(18);
                        testmap = Mapset.Get_RandomSpecialRoom(Target_Room.GetComponent<Room_data>().map_code);
                       // Target_Room.transform.Find("NPC").GetComponent<NPCManager>().Setting(Room_data.RoomType.Crane);
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
