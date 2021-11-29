using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
     public void Setting(Room_data.RoomType RoomType)
     {
        switch(RoomType)
        {
            case Room_data.RoomType.Shop:
                Shop.SetActive(true);
                break;
            case Room_data.RoomType.Crane:
                Gacha.SetActive(true);
                break;
            case Room_data.RoomType.StartRoom:
                StartRoom.SetActive(true);
                break;
            case Room_data.RoomType.Boss:
                BossRoom.SetActive(true);

                break;

        }

     }
    
    public enum NPCType { SHOP, GACHA }
    public NPCType Type;
    public GameObject Shop;
    public GameObject Gacha;
    public GameObject StartRoom;
    public GameObject BossRoom;

    //긴급코드
    private void Start()
    {
        Room_data Target = transform.parent.parent.gameObject.GetComponent<Room_data>();

        
        Setting(Target.Room_Type);
    }
}
