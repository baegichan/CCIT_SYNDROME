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
    public MapCreate MapCreateSC;
    GameObject[,] WorldMap;
    public int[,] WORLDMAP_DATA
    {
        get {return MapData; }
        set { MapData = value; STAGELEVEL = ((int)Mathf.Sqrt(value.Length) - 1)/2; WorldMap = new GameObject[MapCreateSC.Level * 2 + 1, MapCreateSC.Level * 2 + 1]; }
    }
    private void Start()
    {
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
       
    }
    
    public void MiniMapSetting()
    {
        WORLDMAP_DATA = new int[(int)Mathf.Sqrt(MapCreateSC.MapArray.Length), (int)Mathf.Sqrt(MapCreateSC.MapArray.Length)];
        Debug.Log("Worldmap clear" + WORLDMAP_DATA.Length);
        MapEnable = new bool[(int)Mathf.Sqrt(MapCreateSC.MapArray.Length), (int)Mathf.Sqrt(MapCreateSC.MapArray.Length)];
        Debug.Log("MapEnable clear" + MapEnable.Length);
        WorldMap = new GameObject[(int)Mathf.Sqrt(MapCreateSC.MapArray.Length), (int)Mathf.Sqrt(MapCreateSC.MapArray.Length)];
        WorldMapUpdate();
        LoadMiniMap(WORLDMAP_DATA);
        Debug.Log(WORLDMAP_DATA);
    }
    public void WorldMapUpdate()
    {
        for(int i = 0; i< Level*2;i++)
        {
            for(int j =0; j< Level*2;j++)
            {
                WORLDMAP_DATA[i, j] = MapCreateSC.MapArray[i, j].GetComponent<RoomData>().IsCreated?1:0;
                MapEnable[i, j] = MapCreateSC.MapArray[i, j].GetComponent<RoomData>().VisitedRoom;
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
       //이후 KeyManager 키로 변경요망
      if ( Input.GetKeyDown(KeyCode.M))
        {
            if(Loaded==false)
            {
                WolrdMap.transform.localPosition = new Vector3(0, 0, 0);
                MapUpdate();
            }
             else
            {
                WolrdMap.transform.localPosition = new Vector3(0, 0, 0);
                MapUpdate();
            }
        }
    }
    public void MapUpdate()
    {

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
                    WorldMap[i, j].SetActive(WorldMap[i,j].activeSelf?false:true);
                }             
            }
        }
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
