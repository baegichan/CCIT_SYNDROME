using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSRoom : MonoBehaviour
{
    public List<GameObject> RoomList;
    public MapCreate MapcreateSC;
    public GameObject[] Map_Image;
    public int RoomCount=0;
    public int MinRoomNum;
    private void Start()
    {
        RoomList = new List<GameObject>();
    }
    
    public void DFSRoomCheck(int x,int y,GameObject[,] Mapdata)
    {
        RoomCount += 1;
        if (0 < x && x < MapcreateSC.Level * 2 && 0 < y && y < MapcreateSC.Level * 2)
        {
            if (!Mapdata[x, y + 1].GetComponent<RoomData>().IsCreated)
            {
                if ((Mapdata[x, y].GetComponent<RoomData>().RoomCode >> 1 & 0b0001) == 1)
                {
                    Test(x, y+1, Mapdata);
                    Mapdata[x, y + 1].GetComponent<RoomData>().ChangeRoomCode(0b0010);
                    // Instantiate(Map_Image[Mapdata[x, y + 1].GetComponent<RoomData>().RoomCode], Mapdata[x, y + 1].transform);
                    RoomCount += 1;
                    DFSRoomCheck(x, y + 1, Mapdata);
                }
            }
            if (!Mapdata[x + 1, y].GetComponent<RoomData>().IsCreated)
            {
                if ((Mapdata[x, y].GetComponent<RoomData>().RoomCode >> 2 & 0b0001) == 1)
                {
                    Test(x +1, y, Mapdata);
                    Mapdata[x + 1, y].GetComponent<RoomData>().ChangeRoomCode(0b0100);
                    //Instantiate(Map_Image[Mapdata[x + 1, y].GetComponent<RoomData>().RoomCode], Mapdata[x + 1, y].transform);
                    RoomCount+=1;
                    DFSRoomCheck(x + 1, y, Mapdata);
                }
            }
            if (!Mapdata[x, y - 1].GetComponent<RoomData>().IsCreated)
            {
                if ((Mapdata[x, y].GetComponent<RoomData>().RoomCode >> 0 & 0b0001) == 1)
                {
                    Test(x , y-1, Mapdata);
                    Mapdata[x, y - 1].GetComponent<RoomData>().ChangeRoomCode(0b0001);
                    //Instantiate(Map_Image[Mapdata[x, y - 1].GetComponent<RoomData>().RoomCode], Mapdata[x, y - 1].transform);
                    RoomCount += 1;
                    DFSRoomCheck(x, y - 1, Mapdata);
                }
            }
            if (!Mapdata[x - 1, y].GetComponent<RoomData>().IsCreated)
            {
                if ((Mapdata[x, y].GetComponent<RoomData>().RoomCode >> 3 & 0b0001) == 1)
                {
                    Test(x - 1, y, Mapdata);
                    Mapdata[x - 1, y].GetComponent<RoomData>().ChangeRoomCode(0b1000);
                    // Instantiate(Map_Image[Mapdata[x - 1, y].GetComponent<RoomData>().RoomCode], Mapdata[x - 1, y].transform);
                    RoomCount += 1;
                    DFSRoomCheck(x - 1, y, Mapdata);
                }
            }
        }
    }
    public void Test(int x, int y, GameObject[,] Mapdata)
    {
        if (y - 1 >= 0)
        {
            Mapdata[x, y ].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Bottom, Mapdata[x, y-1]);
        }
        if (x - 1 >= 0)
        {
            Mapdata[x , y].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Left, Mapdata[x-1, y]);
        }
        if (x + 1 < MapcreateSC.Level * 2+1)
        {
            Mapdata[x , y].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Right, Mapdata[x+1, y]);
        }
        if (y + 1 < MapcreateSC.Level * 2+1)
        {
            Mapdata[x, y ].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Top, Mapdata[x, y+1]);
        }
        
    }
}
