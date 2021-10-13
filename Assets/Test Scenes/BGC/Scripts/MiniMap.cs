using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    #region FakeData
    /// <summary>
    /// ����ũ �ʵ����� �� �������ϰ� �� �˰��� ���鶧 �� �迭 �Ѱ���ߵ�
    /// ����ü ������ X �� �游 �߰�������Ʈ �ʿ���
    /// ������ǥ�� array  �ΰ� �޾Ƽ� ���
    /// MapEnable�� ���ι޾ƾߵ�
    /// </summary>
    int[,] MapData = new int[,] { {1,1,1,1,1 },{ 0,0,1,0,1},{ 0,1,1,1,1},{ 0,1,1,1,0},{1,1,0,0,0 } };
    bool[,] MapEnable = new bool[,] { { true, true, true, false, false }, { false, false, true, false, false }, { false, true, false, false, true }, { false, false, true, true, false }, { true, false, false, false, false } };
    #endregion
    GameObject[,] WorldMap;
    public int[,] WORLDMAP_DATA
    {
        get {return MapData; }
        set { MapData = value; STAGELEVEL = ((int)Mathf.Sqrt(value.Length) - 1)/2; WorldMap = new GameObject[STAGELEVEL * 2 + 1, STAGELEVEL * 2 + 1]; }
    }
    private void Start()
    {
        transform.parent.GetComponent<Canvas>().worldCamera = Camera.main;
        WORLDMAP_DATA =new int[,] { { 1,1,1,1,1 },{ 0,0,1,0,1},{ 0,1,1,1,1},{ 0,1,1,1,0},{ 1,1,0,0,0 } };
        LoadMiniMap(WORLDMAP_DATA);
        MapUpdate();
        Loaded = true;
    }
    /// <summary>
    /// ������ ���� ���ôٽ��ؾߵ� ������Ƽ���ٶ�
    /// </summary>
    int Level = 2;
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
       //���� KeyManager Ű�� ������
      if ( Input.GetKeyDown(KeyCode.M))
        {
            if(Loaded==false)
            {
                
                LoadMiniMap(WORLDMAP_DATA);
                Loaded = true;
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
        int height= STAGELEVEL * distance;
        int width= -STAGELEVEL * distance;
         for(int i = 0;i< STAGELEVEL * 2+1;i++)
        {
            for (int j = 0; j < STAGELEVEL * 2 + 1; j++)
            {
                if(CheckMapData(LoadMap[i,j]))
                {
                    WorldMap[i,j]=Instantiate(Target,new Vector3(width+j*distance,height),Quaternion.identity, WolrdMap.transform).gameObject;
                    WorldMap[i,j].transform.localScale = (new Vector3(1, 1, 1));
                }                 
            }
           height -= distance;
           width = -STAGELEVEL * distance;
        }
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
