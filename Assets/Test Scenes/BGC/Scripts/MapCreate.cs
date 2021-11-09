using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    // Start is called before the first frame update
    public int TestLevel ;
    public GameObject[,] MapArray;
    public DFSRoom DFS;
    public GameObject BossImage;
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
       
        Starting();
        SetBossRoom();


        //test code
        for (int i = 0; i < Level * 2 + 1; i++)
        {
            for (int j = 0; j < Level * 2 + 1; j++)
            {
             if( MapArray[i,j].GetComponent<RoomData>().Cur_Roomtype ==RoomData.RoomType.Boss )
                {
                    Instantiate(BossImage, MapArray[i, j].transform.position, Quaternion.identity, MapArray[i, j].transform);
                }
            }
        }


        Minimap.MiniMapSetting();
    }
    public void Starting()
    {
        DFS.DFSRoomCheck((int)Level, (int)Level, MapArray);
        for (int i = 0; i < Level * 2 + 1; i++)
        {
            if (MapArray[i, Level*2].GetComponent<RoomData>().CheckRoomCode() == false)
            {
                MapArray[i, Level * 2].GetComponent<RoomData>().AutoCunnecting();
            }
            if (MapArray[0, i].GetComponent<RoomData>().CheckRoomCode() == false)
            {
                MapArray[0, i].GetComponent<RoomData>().AutoCunnecting();
            }
            if (MapArray[Level * 2 , i].GetComponent<RoomData>().CheckRoomCode() == false)
            {
                MapArray[Level * 2 , i].GetComponent<RoomData>().AutoCunnecting();
            }
            if (MapArray[i,0].GetComponent<RoomData>().CheckRoomCode() == false)
            {
                MapArray[i,0].GetComponent<RoomData>().AutoCunnecting();
            }
        }
        //실적용시 제외해야됨
        if (DFS.MinRoomNum<DFS.RoomCount)
        {
            for (int i = 0; i < Level * 2 + 1; i++)
            {
                for (int j = 0; j < Level * 2 + 1; j++)
                {
                    //실적용시 맵오브젝트 로드
                    Instantiate(DFS.Map_Image[MapArray[i, j].GetComponent<RoomData>().RoomCode], MapArray[i, j].transform.position, Quaternion.identity,MapArray[i,j].transform);
                }
            }
            return;
        }
        else
        {
            for(int i=0;i<Level*2+1;i++)
            {
                for(int j =0; j<Level*2+1;j++)
                {
                    if(j!=Level&&i!=Level)
                    {
                        MapArray[i, j].GetComponent<RoomData>().RoomClear();
                    }
                }
            }
            DFS.RoomCount = 0;
            Starting();
        }
    }
    public void FirstRoomSetting()
    {
        MapArray[Level, Level].GetComponent<RoomData>().StartRoomSetting();
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Bottom, MapArray[Level, Level - 1]);
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Top, MapArray[Level, Level + 1]);
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Left, MapArray[Level - 1, Level]);
        MapArray[Level, Level].GetComponent<RoomData>().SetArroundRoom(RoomData.Roomdir.Right, MapArray[Level + 1, Level]);

        //실적용시 맵오브젝트 로드
        Instantiate(DFS.Map_Image[MapArray[Level, Level].GetComponent<RoomData>().RoomCode], MapArray[Level, Level].transform);
    }
    public void SpawnMapObject(float x, float y)
    {
        GameObject Test = (GameObject)Instantiate(Resources.Load("Dummy Map"), new Vector2(x * distance, y * distance), Quaternion.identity, transform);
        Test.name = Test.name + "{" + (x+Level) + "}{" + (y + Level) + "}";
        MapArray[(int)x + Level, (int)y + Level] = Test;
    }
    public void SetBossRoom()
    {
        List<GameObject> BoosRoomCandidate = new List<GameObject>();
        
        for (int i = 0; i < Level * 2 + 1; i++)
        {
            for (int j = 0; j < Level * 2 + 1; j++)
            {
                if((Level/2>=i|| Level / 2 +Level <= i) &&( Level / 2 >= j || Level / 2 + Level <= j))
                {
                    if(MapArray[i,j].GetComponent<RoomData>().Cur_Roomtype == RoomData.RoomType.Nomal && MapArray[i, j].GetComponent<RoomData>().IsCreated)
                    {
                        BoosRoomCandidate.Add(MapArray[i, j]);
                        Debug.Log("Set candidate x:" + i + "   y:" + j);
                    } 
                }
            }
        }
        BoosRoomCandidate[Random.Range(0, BoosRoomCandidate.Count)].GetComponent<RoomData>().ChangeRoomType(RoomData.RoomType.Boss);
    }
    public void SideMapProcess()
    {
        int counter = 0;
        for (int i =0;i<Level*2+1;i++)
        {
            if(MapArray[i, Level * 2 + 1].GetComponent<RoomData>().IsCreated)
            {
                if (i != Level * 2 + 1)
                {
                    if (MapArray[i + 1, Level * 2 + 1].GetComponent<RoomData>().IsCreated)
                    {
                    }
                    counter += 1;
                    if (counter == 2)
                    {
                        MapArray[i - 1, Level * 2 + 1].GetComponent<RoomData>().SetRoomCode(0b0101);
                        MapArray[i, Level * 2 + 1].GetComponent<RoomData>().SetRoomCode(0b1010);

                    }
                }
            }
        }
    }
}
