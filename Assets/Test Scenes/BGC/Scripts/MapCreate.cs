using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    // Start is called before the first frame update
    public int TestLevel ;
    public GameObject[,] MapArray;
    public DFSRoom DFS;
    public MiniMap Minimap;
    public float distance;
    public int Level
    {
        get { return TestLevel; }
        set { MapArray = new GameObject[Level * 2 + 1, Level * 2 + 1];  TestLevel = value;  }
    }
    void Update()
    {
        
    }
    private void Start()
    {
        Level = 4;
        for(int i =0; i<Level*2+1;i++)
        {
            for(int j = 0;j<Level*2+1;j++)
            {
                SpawnMapObject(i-Level, j-Level);
            }
        }
       
        FirstRoomSetting();
       
        DFS.DFSRoomCheck((int)Level, (int)Level, MapArray);
        Minimap.MiniMapSetting();
    }
    public void FirstRoomSetting()
    {
        MapArray[Level, Level].GetComponent<RoomData>().StartRoomSetting();
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Bottom, MapArray[Level, Level - 1]);
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Top, MapArray[Level, Level + 1]);
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Left, MapArray[Level - 1, Level]);
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Right, MapArray[Level + 1, Level]);
        Instantiate(DFS.Map_Image[MapArray[Level, Level].GetComponent<RoomData>().RoomCode], MapArray[Level, Level].transform);
    }
    public void SpawnMapObject(float x, float y)
    {
        GameObject Test = (GameObject)Instantiate(Resources.Load("Dummy Map"), new Vector2(x * distance, y * distance), Quaternion.identity, transform);
        Test.name = Test.name + "{" + (x+Level) + "}{" + (y + Level) + "}";
        MapArray[(int)x + Level, (int)y + Level] = Test;
    }
    public void SideMapProcess()
    {
        int counter = 0;
        for (int i =0;i<Level*2;i++)
        {
            if(MapArray[i, Level * 2 + 1].GetComponent<RoomData>().IsCreated)
            {
                counter += 1;
            }
        }
    }
}
