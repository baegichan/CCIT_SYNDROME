using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    #region FakeData
    /// <summary>
    /// 페이크 맵데이터 맵 프리팹하고 맵 알고리즘 만들때 맵 배열 넘겨줘야됨
    /// 맵전체 데이터 X 들어간 방만 추가업데이트 필요함
    /// 최종목표는 array  두개 받아서 사용
    /// MapEnable도 따로받아야됨
    /// </summary>
    int[,] MapData = new int[,] { {1,1,1,1,1 },{ 0,0,1,0,1},{ 0,1,1,1,1},{ 0,1,1,1,0},{1,1,0,0,0 } };
    bool[,] MapEnable = new bool[,] { { true, true, true, false, false }, { false, false, true, false, false }, { false, true, false, false, true }, { false, false, true, true, false }, { true, false, false, false, false } };
    #endregion
    public MapManager MapManager;
    public MapCreate MapCreateSC;
    private bool minimapCreated;
    GameObject[,] WorldMap;
    public GameObject WolrdMapCanvas;
    public int[,] WORLDMAP_DATA
    {
        get {return MapData; }
        set { MapData = value; STAGELEVEL = ((int)Mathf.Sqrt(value.Length) - 1)/2; WorldMap = new GameObject[MapManager.Level * 2 + 1, MapManager.Level * 2 + 1]; }
    }
    private void Start()
    {
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    
    public void MiniMapSetting()
    {
        WORLDMAP_DATA = new int[(int)Mathf.Sqrt(MapManager.map.Length), (int)Mathf.Sqrt(MapManager.map.Length)];
        MapEnable = new bool[(int)Mathf.Sqrt(MapManager.map.Length), (int)Mathf.Sqrt(MapManager.map.Length)];  
        WorldMap = new GameObject[(int)Mathf.Sqrt(MapManager.map.Length), (int)Mathf.Sqrt(MapManager.map.Length)];
        WorldMapUpdate();
        LoadMiniMap(WORLDMAP_DATA);
        minimapCreated= true;


    }
    public void WorldMapUpdate()
    {
        for(int i = 0; i< Level*2+1;i++)
        {
            for(int j =0; j< Level*2+1;j++)
            {
                WORLDMAP_DATA[i, j] = MapManager.map[i, j].GetComponent<Room_data>().Room_Created?1:0;
                MapEnable[i, j] = MapManager.map[i, j].GetComponent<Room_data>().VisitedRoom;
            }
        }
    }

    public void VisitRoomCheck()
    {
        for (int i = 0; i < Level * 2 + 1; i++)
        {
            for (int j = 0; j < Level * 2 + 1; j++)
            {
                MapEnable[j, i] = MapManager.map[i, j].GetComponent<Room_data>().VisitedRoom; ;
            }
        }
    }

    int Level;
    public  int STAGELEVEL 
    {   
        set { Level = value; }
        get { return Level; }
    }
   [Range(0,30)] public int distance = 15;
    public GameObject Target;
    public GameObject Canvas;
    public GameObject WolrdMap;
    public bool Loaded = false;
    private void Update()
    {
    if(minimapCreated)
        {        //이후 KeyManager 키로 변경요망
            if (Input.GetKeyDown(KeyCode.M))
            {
              
                if (Loaded == false)
                {
                    WolrdMap.transform.localPosition = -WorldMap[(int)MapManager.PCurrent_Room.y, (int)MapManager.PCurrent_Room.x].transform.localPosition;
                    MapUpdate();
                }
                else
                {
                    
                    
                    if(WolrdMapCanvas.activeSelf)
                    {
                        WolrdMapCanvas.SetActive(false);
                    }
                    else
                    {
                        WolrdMapCanvas.SetActive(true);
                    }   
                    WolrdMap.transform.localPosition = -WorldMap[(int)MapManager.PCurrent_Room.y, (int)MapManager.PCurrent_Room.x].transform.localPosition;
                    MapUpdate();
                }
            }
        }
      
      
    }
    public void TimeScaleChanger()
    {
      
    }
    public void CanvasOff()
    {
        WolrdMapCanvas.SetActive(false);
    }
    public void MapLocationUpdate()
    {
        WolrdMap.transform.localPosition = -WorldMap[(int)MapManager.PCurrent_Room.y, (int)MapManager.PCurrent_Room.x].transform.localPosition;
    }
    public void MapUpdate()
    {
        VisitRoomCheck();
        for (int i = 0; i < STAGELEVEL * 2 + 1; i++)
        {
            for (int j = 0; j < STAGELEVEL * 2 + 1; j++)
            {
                if(MapEnable[i, j]==false && WorldMap[i, j] != null)
                {
                    WorldMap[i, j].SetActive(false);
                }
                if(MapEnable[i,j]==true&&WorldMap[i,j]==true)
                {
                    WorldMap[i, j].SetActive(true);
                }             
            }
        }
    }
    public void CurrentOff(Vector2 Index)
    {
        WorldMapSpriteChanger SpChanger = WorldMap[(int)Index.y, (int)Index.x].GetComponent<WorldMapSpriteChanger>();
        SpChanger.CurrentRoomChecker.SetActive(false);

    }
    public void CurrentOn(Vector2 Index)
    {
        WorldMapSpriteChanger SpChanger = WorldMap[(int)Index.y, (int)Index.x].GetComponent<WorldMapSpriteChanger>();
        SpChanger.CurrentRoomChecker.SetActive(true);

    }


    public void LoadMiniMap(int[,] LoadMap)
    {  
        int height= -STAGELEVEL * distance;
        int width= -STAGELEVEL * distance;
         for(int i = 0;i< STAGELEVEL * 2+1;i++)
        {
            for (int j = 0; j < STAGELEVEL * 2 + 1; j++)
            {
                if(CheckMapData(LoadMap[i,j]))
                {
                    WorldMap[j,i]=Instantiate(Target,new Vector3(width,height + j * distance),Quaternion.identity, WolrdMap.transform).gameObject;
                    WorldMap[j,i].transform.localScale = (new Vector3(1, 1, 1));
                    WorldMapSpriteChanger SpChanger =WorldMap[j, i].GetComponent<WorldMapSpriteChanger>();
                   if(!MapManager.map[i, j].GetComponent<Room_data>().Right)
                   {
                        SpChanger.R_Brige.SetActive(false);
                   }
                    if (!MapManager.map[i, j].GetComponent<Room_data>().Left)
                    {
                        SpChanger.L_Brige.SetActive(false);
                    }
                    if (!MapManager.map[i, j].GetComponent<Room_data>().Top)
                    {

                        SpChanger.T_Brige.SetActive(false);
                    }
                    if (!MapManager.map[i, j].GetComponent<Room_data>().Bottom)
                    {
                        SpChanger.B_Brige.SetActive(false);
                    }
                    if(MapManager.map[i, j].GetComponent<Room_data>().Room_Type==Room_data.RoomType.Boss)
                    {
                        SpChanger.Boss.SetActive(true);
                    }
                    if (MapManager.map[i, j].GetComponent<Room_data>().Room_Type == Room_data.RoomType.Crane)
                    {
                        SpChanger.Gacha.SetActive(true);
                    }
                    if (MapManager.map[i, j].GetComponent<Room_data>().Room_Type == Room_data.RoomType.Shop)
                    {
                        SpChanger.Shop.SetActive(true);
                    }
                    if(MapManager.Current_Room == new Vector2(i,j))
                    {
                        SpChanger.CurrentRoomChecker.SetActive(true);
                    }
                }                 
            }
           height = -STAGELEVEL * distance;
           width +=  distance;
        }
        Loaded = true;
        MapUpdate();
    }

   
    public bool CheckMapData(int test)
    {
        if(test == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
