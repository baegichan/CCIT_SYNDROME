using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager s_Instace;
    public bool Map_Lock = false;
    public MiniMap Minimap;

   
    public void instace()
    {
        if (s_Instace == null)
        {
            s_Instace = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public Vector2 Current_Room = new Vector2(0, 0);
    public Vector2 PCurrent_Room
    {
        get { return Current_Room; }
        set
        {
            if (Current_Room != value)
            {

                MapOn(value);
                MapOff(Current_Room);
                Current_Room = value;
            }
        ;
        }
    }
    public void MapLock()
    {
        Map_Lock = true;
    }
    public void MapUnLock()
    {
        Map_Lock = false;
    }
    public void MapAllOff()
    {
        for (int i = 0; i < Maps.transform.childCount; i++)
        {
            Maps.transform.GetChild(i).gameObject.SetActive(false);

        }
    }
    public int currentindexreturner()
    {
        return (int)PCurrent_Room.x * (Level * 2 + 1) + (int)PCurrent_Room.y;
    }
    public void MapOn(Vector2 Mapindex)
    {
        Maps.transform.GetChild((int)Mapindex.x * (Level * 2 + 1) + (int)Mapindex.y).gameObject.SetActive(true);

    }
    public void MapOff(Vector2 Mapindex)
    {
        Maps.transform.GetChild((int)Mapindex.x * (Level * 2 + 1) + (int)Mapindex.y).gameObject.SetActive(false);

    }
    public void PotalMove(Potals.PotalType potalType)
    {
        switch (potalType)
        {
            case Potals.PotalType.L:
                Current_Room = new Vector2(Current_Room.x, Current_Room.y);
                //이동구현
                break;
            case Potals.PotalType.R:
                Current_Room = new Vector2(Current_Room.x, Current_Room.y);
                //이동구현
                break;
            case Potals.PotalType.T:
                Current_Room = new Vector2(Current_Room.x, Current_Room.y);
                //이동구현
                break;
            case Potals.PotalType.B:
                Current_Room = new Vector2(Current_Room.x, Current_Room.y);
                //이동구현
                break;

        }

    }
    [SerializeField]
    public int Level;
    int map_index;
    [Header("간격 default = 500")]
    public int width = 500;//사이 간격

    public GameObject room;
    public GameObject Maps;


    [SerializeField]
   public GameObject[,] map;
    [SerializeField]
    int a = 0, b = 0;
    [SerializeField]
    Vector2 Start_Map;
    [SerializeField]
    Vector2 Start_Room_Index;
    [SerializeField]
    Vector2 Boss_Room_Index;
    [SerializeField]
    Vector2 Gacha_Room_Index;
    [SerializeField]
    Vector2 Store_Room_Index;

    public Vector2 BOSS_ROOM
    {
        get
        {
            return Boss_Room_Index;
        }

    }
    public void Set_AllRoom()
    {
        if (RoomList[Random.Range(0, RoomList.Count)].Room_Type == Room_data.RoomType.Nomal)
        {

        }
        else
        {

        }
    }
    public void StartRoom()
    {
        map[Level, Level].GetComponent<Room_data>().Room_Type = Room_data.RoomType.StartRoom;

    }
    public void Check_Room_Index(Vector2 One, Vector2 targetroom)
    {

        if (One == targetroom)
        {
            //change()
            Check_Room_Index(One, targetroom);
        }
        else
        {

        }
    }
    private List<Room_data> RoomList = new List<Room_data>();
    private List<Room_data> BossRoomList = new List<Room_data>();
    public void AllCreatedRoom()
    {

        for (int i = 0; i < Level * 2 + 1; i++)
        {
            for (int j = 0; j < Level * 2 + 1; j++)
            {
                if (map[i, j].GetComponent<Room_data>().Room_Created)
                {

                    RoomList.Add(map[i, j].GetComponent<Room_data>());


                }

            }

        }
        for (int i = 0; i < Level * 2 + 1; i++)
        {
            for (int j = 0; j < Level * 2 + 1; j++)
            {

                if (map[i, j].GetComponent<Room_data>().Room_Created)
                {
                    if (Level % 2 == 1)
                    {
                        if ((i <= (Level / 2 + 1) || i >= (Level + (Level / 2 + 1))) && (j <= (Level / 2 + 1) || j >= (Level + (Level / 2 + 1))))
                        {

                            BossRoomList.Add(map[i, j].GetComponent<Room_data>());
                        }

                    }
                    else
                    {
                        if ((i <= (Level / 2) || i >= (Level + (Level / 2))) && (j <= (Level / 2) || j >= (Level + (Level / 2))))
                        {

                            BossRoomList.Add(map[i, j].GetComponent<Room_data>());
                        }
                    }

                }
            }

        }

        BossRoomList[Random.Range(0, BossRoomList.Count)].Room_Type = Room_data.RoomType.Boss;
        int CraneLimit = Random.Range(1, Level);
        int ShopLimit = Random.Range(0, Level - 1);
        for (int i = 0; i < ShopLimit; i++)
        {
            int RanShop = Random.Range(0, RoomList.Count);
            if (RoomList[RanShop].Room_Type != Room_data.RoomType.Boss)
            {
                RoomList[RanShop].Room_Type = Room_data.RoomType.Shop;
            }


        }

        for (int i = 0; i < CraneLimit; i++)
        {

            int RanCrane = Random.Range(0, RoomList.Count);
            if (RoomList[RanCrane].Room_Type != Room_data.RoomType.Boss)
            {
                RoomList[RanCrane].Room_Type = Room_data.RoomType.Crane;
            }
        }

        map[Level, Level].GetComponent<Room_data>().Room_Type = Room_data.RoomType.StartRoom;

    }
    private void Awake()
    {
        instace();
        GameObject room = GameObject.FindGameObjectWithTag("Room");
        Start_Room_Index = new Vector2(Level, Level);
        map = new GameObject[(2 * Level) + 1, (2 * Level) + 1];
        map_index = (2 * Level + 1) * (2 * Level + 1);
        //map[Level, Level].transform.position = new Vector2(0, 0);
        //Start_Map = map[Level, Level].transform.position;
        make_map();
        Map_Move();
        bbb();
    }
    private void Start()
    {


        StartCoroutine(test());
    
       
    }
    public IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        AllCreatedRoom();
        Maps.GetComponent<MapLoadTest>().Starting_Setting();
        MapAllOff();
        PCurrent_Room = new Vector2(Level, Level);
        Minimap.MiniMapSetting();
    }
    void bbb()//맵 최소 개수 
    {
        Check_Map_Code(Level, Level);
        aaa();
    }
    void aaa()//맵 최소 개수
    {
        if (map_count < ((2 * Level) + 1) * ((2 * Level) + 1) / 2)
        {
            Debug.Log(22);
            int abc = (2 * Level) - 1;
            for(int i = 0; i < abc + 2; i++)
            {
                for(int a = 0; a < abc + 2; a++)
                {
                    map[i, a].GetComponent<Room_data>().Room_Created = false;
                    map[i, a].GetComponent<Room_data>().Top = false;
                    map[i, a].GetComponent<Room_data>().Right = false;
                    map[i, a].GetComponent<Room_data>().Bottom = false;
                    map[i, a].GetComponent<Room_data>().Left = false;
                    map[i, a].GetComponent<Room_data>().map_code = 0;

                }
            }
            bbb();
        }
        else
        {
            //
        }

    }
    void make_map()
    {
        for (int i = 0; i < map_index; i++)
        {
            map[a, b] = (GameObject)Instantiate(room, new Vector2(0, 0), Quaternion.identity);
            Map_Move();
            map[a, b].transform.parent = GameObject.Find("Maps").transform;
            map[a, b].name = "room_index    " + "[ " + a + " ]" + "[ " + b + " ]";
            map[a, b].GetComponent<Room_data>().Map_index = new Vector2(a, b);
            b++;
            if (b == ((2 * Level) + 1))
            {
                b = 0;
                if (a <= ((2 * Level) - 1))
                {
                    a++;
                }
            }

        }
    }

    void Map_Move()
    {
        if (a <= Level)
        {
            if (b > Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x - ((Level - a) * width), Start_Map.y + ((b - Level) * width));
            }
            else if (b <= Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x - ((Level - a) * width), Start_Map.y - ((Level - b) * width));
            }
        }
        else if (a > Level)
        {
            if (b > Level)
            {
                map[a, b].transform.position = new Vector2(Start_Map.x + ((a - Level) * width), Start_Map.y + ((b - Level) * width));
            }
            else if (b <= Level)    
            {
                map[a, b].transform.position = new Vector2(Start_Map.x + ((a - Level) * width), Start_Map.y - ((Level - b) * width));
            }
        }
    }

    public bool Check_Map_Code(int x, int y)
    {
        map[x, y].GetComponent<Room_data>().SetMapData();
        map[x, y].GetComponent<Room_data>().Room_Created = true;




        CreateRoomRogic(x, y);



        return true;
    }
    [SerializeField]
    int map_count;

    public void CreateRoomRogic(int a, int b)
    {
        //내부
        if (a >= 1 && a <= ((2 * Level) - 1) && b >= 1 && b <= ((2 * Level) - 1))
        {
            if (map[a, b].GetComponent<Room_data>().Check_Top_Connect() && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
            {
                map_count++;
                map[a, b + 1].GetComponent<Room_data>().Room_Created = true;
                Dir_Check(a, b + 1);
                Map_Code_C(a, b + 1);
                CreateRoomRogic(a, b + 1);
            }
            if (map[a, b].GetComponent<Room_data>().Check_Right_Connect() && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
            {
                map_count++;
                map[a + 1, b].GetComponent<Room_data>().Room_Created = true;
                Dir_Check(a + 1, b);
                Map_Code_C(a + 1, b);
                CreateRoomRogic(a + 1, b);
            }
            if (map[a, b].GetComponent<Room_data>().Check_Bottom_Connect() && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
            {
                map_count++;
                map[a, b - 1].GetComponent<Room_data>().Room_Created = true;
                Dir_Check(a, b - 1);
                Map_Code_C(a, b - 1);
                CreateRoomRogic(a, b - 1);
            }
            if (map[a, b].GetComponent<Room_data>().Check_Left_Connect() && map[a - 1, b].GetComponent<Room_data>().Room_Created == false)
            {
                map_count++;
                map[a - 1, b].GetComponent<Room_data>().Room_Created = true;
                Dir_Check(a - 1, b);
                Map_Code_C(a - 1, b);
                CreateRoomRogic(a - 1, b);
            }
        }
        else
        {

            //모서리
            if (b == 2 * Level && a != 0 && a != 2 * Level)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Right_Connect() && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a + 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a + 1, b);
                    Map_Code_C(a + 1, b);
                    CreateRoomRogic(a + 1, b);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Bottom_Connect() && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b - 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b - 1);
                    Map_Code_C(a, b - 1);
                    CreateRoomRogic(a, b - 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Left_Connect() && map[a - 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a - 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a - 1, b);
                    Map_Code_C(a - 1, b);
                    CreateRoomRogic(a - 1, b);
                }
            }
            if (b == 0 && a != 0 && a != 2 * Level)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Top_Connect() && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b + 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b + 1);
                    Map_Code_C(a, b + 1);
                    CreateRoomRogic(a, b + 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Right_Connect() && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a + 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a + 1, b);
                    Map_Code_C(a + 1, b);
                    CreateRoomRogic(a + 1, b);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Left_Connect() && map[a - 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a - 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a - 1, b);
                    Map_Code_C(a - 1, b);
                    CreateRoomRogic(a - 1, b);
                }
            }
            if (a == 2 * Level && b != 0 && b != 2 * Level)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Top_Connect() && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b + 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b + 1);
                    Map_Code_C(a, b + 1);
                    CreateRoomRogic(a, b + 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Bottom_Connect() && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b - 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b - 1);
                    Map_Code_C(a, b - 1);
                    CreateRoomRogic(a, b - 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Left_Connect() && map[a - 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a - 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a - 1, b);
                    Map_Code_C(a - 1, b);
                    CreateRoomRogic(a - 1, b);
                }
            }
            if (a == 0 && b != 0 && b != 2 * Level)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Top_Connect() && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b + 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b + 1);
                    Map_Code_C(a, b + 1);
                    CreateRoomRogic(a, b + 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Right_Connect() && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a + 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a + 1, b);
                    Map_Code_C(a + 1, b);
                    CreateRoomRogic(a + 1, b);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Bottom_Connect() && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b - 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b - 1);
                    Map_Code_C(a, b - 1);
                    CreateRoomRogic(a, b - 1);
                }
            }

            //꼭지점
            if (a == 2 * Level && b == 2 * Level)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Bottom_Connect() && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b - 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b - 1);
                    Map_Code_C(a, b - 1);
                    CreateRoomRogic(a, b - 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Left_Connect() && map[a - 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a - 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a - 1, b);
                    Map_Code_C(a - 1, b);
                    CreateRoomRogic(a - 1, b);
                }
            }
            if (a == 2 * Level && b == 0)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Top_Connect() && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b + 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b + 1);
                    Map_Code_C(a, b + 1);
                    CreateRoomRogic(a, b + 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Left_Connect() && map[a - 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a - 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a - 1, b);
                    Map_Code_C(a - 1, b);
                    CreateRoomRogic(a - 1, b);
                }
            }
            if (a == 0 && b == 2 * Level)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Right_Connect() && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a + 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a + 1, b);
                    Map_Code_C(a + 1, b);
                    CreateRoomRogic(a + 1, b);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Bottom_Connect() && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b - 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b - 1);
                    Map_Code_C(a, b - 1);
                    CreateRoomRogic(a, b - 1);
                }
            }
            if (a == 0 && b == 0)
            {
                if (map[a, b].GetComponent<Room_data>().Check_Top_Connect() && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a, b + 1].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a, b + 1);
                    Map_Code_C(a, b + 1);
                    CreateRoomRogic(a, b + 1);
                }
                if (map[a, b].GetComponent<Room_data>().Check_Right_Connect() && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    map_count++;
                    map[a + 1, b].GetComponent<Room_data>().Room_Created = true;
                    Dir_Check(a + 1, b);
                    Map_Code_C(a + 1, b);
                    CreateRoomRogic(a + 1, b);
                }
            }
        }
    }

    void Dir_Check(int a, int b)
    {
        //내부
        if (a >= 1 && a <= ((2 * Level) - 1) && b >= 1 && b <= ((2 * Level) - 1))
        {
            if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Check_Bottom_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Top = true;

                }
            }
            if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Check_Left_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Right = true;
                }
            }
            if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b - 1].GetComponent<Room_data>().Check_Top_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Bottom = true;

                }
            }
            if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Check_Right_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Left = true;

                }
            }
        }
        //사이드
        if (b == 2 * Level && a != 0 && a != 2 * Level)
        {
            if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Check_Left_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Right = true;
                }
            }
            if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b - 1].GetComponent<Room_data>().Check_Top_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Bottom = true;

                }
            }
            if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Check_Right_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Left = true;

                }
            }
        }
        if (b == 0 && a != 0 && a != 2 * Level)
        {
            if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Check_Bottom_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Top = true;

                }
            }
            if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Check_Left_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Right = true;
                }
            }
            if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Check_Right_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Left = true;

                }
            }
        }
        if (a == 2 * Level && b != 0 && b != 2 * Level)
        {
            if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Check_Bottom_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Top = true;

                }
            }
            if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b - 1].GetComponent<Room_data>().Check_Top_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Bottom = true;

                }
            }
            if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Check_Right_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Left = true;

                }
            }
        }
        if (a == 0 && b != 0 && b != 2 * Level)
        {
            if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Check_Bottom_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Top = true;

                }
            }
            if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Check_Left_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Right = true;
                }
            }
            if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b - 1].GetComponent<Room_data>().Check_Top_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Bottom = true;

                }
            }
        }
        //모서리
        if (a == 2 * Level && b == 2 * Level)
        {
            if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b - 1].GetComponent<Room_data>().Check_Top_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Bottom = true;

                }
            }
            if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Check_Right_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Left = true;

                }
            }
        }
        if (a == 2 * Level && b == 0)
        {
            if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Check_Bottom_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Top = true;

                }
            }
            if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Check_Right_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Left = true;

                }
            }
        }
        if (a == 0 && b == 2 * Level)
        {
            if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Check_Left_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Right = true;
                }
            }
            if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b - 1].GetComponent<Room_data>().Check_Top_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Bottom = true;

                }
            }
        }
        if (a == 0 && b == 0)
        {
            if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
            {

                if (map[a, b + 1].GetComponent<Room_data>().Check_Bottom_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Top = true;

                }
            }
            if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Check_Left_Connect())
                {
                    map[a, b].GetComponent<Room_data>().Right = true;

                }
            }
        }



    }

    void Map_Code_C(int a, int b)
    {
        if (a >= 1 && a <= ((2 * Level) - 1) && b >= 1 && b <= ((2 * Level) - 1))
        {
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
                map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 15;
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
                map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 11;

                }
                else
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
                map[a, b].GetComponent<Room_data>().Left == true && map[a, b].GetComponent<Room_data>().Bottom == false)
            {
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 12;

                }
                else
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Left == true &&
               map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Right == false)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 13;

                }
                else
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Left == true &&
               map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Top == false)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 14;

                }
                else
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
              map[a, b].GetComponent<Room_data>().Bottom == false && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 5;

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }


                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
              map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 6;
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }


                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
             map[a, b].GetComponent<Room_data>().Bottom == false && map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 7;
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }

                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }

            }
            else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
             map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 8;
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;

                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
             map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == false)
                {
                    if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                    }
                    else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                    {
                        int aa = Random.Range(0, 2);
                        if (aa == 0)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 8;
                        }
                        else if (aa == 1)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 11;
                            map[a, b].GetComponent<Room_data>().Top = true;
                            map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        }

                    }
                    else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                    {
                        int aa = Random.Range(0, 2);
                        if (aa == 0)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 8;
                        }
                        else if (aa == 1)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 14;
                            map[a, b].GetComponent<Room_data>().Left = true;
                            map[a - 1, b].GetComponent<Room_data>().Right = true;

                        }
                    }
                    else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                    {
                        int aa = Random.Range(0, 4);
                        if (aa == 0)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 8;
                        }
                        else if (aa == 1)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 11;
                            map[a, b].GetComponent<Room_data>().Top = true;
                            map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        }
                        else if (aa == 2)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 14;
                            map[a, b].GetComponent<Room_data>().Left = true;
                            map[a - 1, b].GetComponent<Room_data>().Right = true;
                        }
                        else if (aa == 3)
                        {
                            map[a, b].GetComponent<Room_data>().map_code = 15;
                            map[a, b].GetComponent<Room_data>().Top = true;
                            map[a, b].GetComponent<Room_data>().Left = true;
                            map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                            map[a - 1, b].GetComponent<Room_data>().Right = true;
                        }
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
             map[a, b].GetComponent<Room_data>().Bottom == false && map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 9;
                }
                else if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }

                }
                else if (map[a, b + 1].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;

                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                else if (map[a, b + 1].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == false &&
            map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 10;
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }

                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
            map[a, b].GetComponent<Room_data>().Bottom == false && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == false &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == true &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == false &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == true &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == false &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == true &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == true &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 1;
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == false &&
                    map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 8);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 4)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 5)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Left = true;

                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 6)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 7)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
            map[a, b].GetComponent<Room_data>().Bottom == false && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                   map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                   map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Left = true;

                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;

                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 2;
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {

                    int aa = Random.Range(0, 8);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;

                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 4)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 5)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Left = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 6)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 7)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;


                    }
                }


            }
            else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == false &&
            map[a, b].GetComponent<Room_data>().Bottom == true && map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                   map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 8);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 4)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 5)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 6)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Left = true;

                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 7)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }

                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 3;
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == false &&
            map[a, b].GetComponent<Room_data>().Bottom == false && map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                   map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a + 1, b].GetComponent<Room_data>().Right = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 8);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {

                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;

                    }
                    else if (aa == 4)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 5)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 6)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 7)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 15;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;

                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                }
                else if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true &&
                  map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 4;
                }
            }
        }


        //사이드 변
        if (b == 2 * Level && a != 0 && a != 2 * Level)
        {
            if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Bottom == true &&
                map[a, b].GetComponent<Room_data>().Left == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 14;
            }
            else if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Bottom == false &&
                map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 2;
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {

                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;

                    }
                }

            }
            else if (map[a, b].GetComponent<Room_data>().Right == false && map[a, b].GetComponent<Room_data>().Bottom == true &&
                map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 3;
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }

            }
            else if (map[a, b].GetComponent<Room_data>().Right == false && map[a, b].GetComponent<Room_data>().Bottom == false &&
                map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 4;
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;

                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {

                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 14;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Bottom == true &&
                map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 8;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 14;
                    map[a, b].GetComponent<Room_data>().Left = true;
                    map[a - 1, b].GetComponent<Room_data>().Right = true;
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Bottom == false &&
                map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 9;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 14;
                    map[a, b].GetComponent<Room_data>().Bottom = true;
                    map[a, b - 1].GetComponent<Room_data>().Top = true;
                }
            }
            else if (map[a, b].GetComponent<Room_data>().Right == false && map[a, b].GetComponent<Room_data>().Bottom == true &&
                map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 10;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 14;
                    map[a, b].GetComponent<Room_data>().Right = true;
                    map[a + 1, b].GetComponent<Room_data>().Left = true;
                }
            }
        }
        if (b == 0 && a != 0 && a != 2 * Level)
        {
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
                map[a, b].GetComponent<Room_data>().Left == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 12;
            }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
               map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 1;
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    else
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    else
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                else if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }

            }
            if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
               map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 2;
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == false &&
               map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 4;
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 9;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 12;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                }

                }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
               map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 5;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 12;
                    map[a, b].GetComponent<Room_data>().Left = true;
                    map[a - 1, b].GetComponent<Room_data>().Right = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
               map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 7;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 12;
                    map[a, b].GetComponent<Room_data>().Right = true;
                    map[a + 1, b].GetComponent<Room_data>().Left = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
               map[a, b].GetComponent<Room_data>().Left == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 9;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 12;
                    map[a, b].GetComponent<Room_data>().Top = true;
                    map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                }
            }
        }
        if (a == 2 * Level && b != 0 && b != 2 * Level)
        {
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Bottom == true &&
                map[a, b].GetComponent<Room_data>().Left == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 13;
            }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Bottom == false &&
              map[a, b].GetComponent<Room_data>().Left == false)
            {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 1;
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    else if(aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;

                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if(aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if(aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                }
                if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Bottom == true &&
             map[a, b].GetComponent<Room_data>().Left == false)
                {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 3;
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b - 1].GetComponent<Room_data>().Bottom = true;

                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                }
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a - 1, b].GetComponent<Room_data>().Right = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
            }
                if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Bottom == false &&
                 map[a, b].GetComponent<Room_data>().Left == true)
                {
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 4;
                }
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                }
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == false && map[a, b + 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                        map[a, b].GetComponent<Room_data>().map_code = 4;
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 7;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 10;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 13;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
            }
                if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Bottom == true &&
                 map[a, b].GetComponent<Room_data>().Left == false)
                {
                if (map[a - 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 6;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 13;
                    map[a, b].GetComponent<Room_data>().Left = true;
                    map[a - 1, b].GetComponent<Room_data>().Right = true;
                }
            }
                if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Bottom == false &&
                 map[a, b].GetComponent<Room_data>().Left == true)
                {
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 7;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 13;
                    map[a, b].GetComponent<Room_data>().Bottom = true;
                    map[a, b - 1].GetComponent<Room_data>().Top = true;
                }
            }
                if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Bottom == true &&
                map[a, b].GetComponent<Room_data>().Left == true)
            { 
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 10;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 13;
                    map[a, b].GetComponent<Room_data>().Top = true;
                    map[a, b + 1].GetComponent<Room_data>().Top = true;
                }
            }

            }
        if (a == 0 && b != 0 && b != 2 * Level)
             {
                   if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
                      map[a, b].GetComponent<Room_data>().Bottom == true)
                   {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                   }

            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
               map[a, b].GetComponent<Room_data>().Bottom == false)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 1;
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b + 1].GetComponent<Room_data>().Top = true;
                    }
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 1;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if(aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b + 1].GetComponent<Room_data>().Top = true;
                    }
                    else if(aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b + 1].GetComponent<Room_data>().Top = true;
                    }
                }
            }
        
            if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
                      map[a, b].GetComponent<Room_data>().Bottom == false)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 2;
                }
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                }
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == false && map[a, b - 1].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 2;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 5;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if(aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                    }
                    else if(aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Bottom = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b - 1].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == false &&
                     map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 3;
                }
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == true && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                }
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    int aa = Random.Range(0, 1);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                }
                if (map[a, b + 1].GetComponent<Room_data>().Room_Created == false && map[a + 1, b].GetComponent<Room_data>().Room_Created == false)
                {
                    int aa = Random.Range(0, 4);
                    if (aa == 0)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 3;
                    }
                    else if (aa == 1)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 6;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                    }
                    else if (aa == 2)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 8;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                    }
                    else if (aa == 3)
                    {
                        map[a, b].GetComponent<Room_data>().map_code = 11;
                        map[a, b].GetComponent<Room_data>().Right = true;
                        map[a, b].GetComponent<Room_data>().Top = true;
                        map[a + 1, b].GetComponent<Room_data>().Left = true;
                        map[a, b + 1].GetComponent<Room_data>().Bottom = true;

                    }
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true &&
                     map[a, b].GetComponent<Room_data>().Bottom == false)
            {
                if(map[a,b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 5;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 11;
                    map[a, b].GetComponent<Room_data>().Bottom = true;
                    map[a, b - 1].GetComponent<Room_data>().Top = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false &&
                     map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                if (map[a + 1, b].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 6;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 11;
                    map[a, b].GetComponent<Room_data>().Right = true;
                    map[a + 1, b].GetComponent<Room_data>().Left = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Top == false && map[a, b].GetComponent<Room_data>().Right == true &&
                     map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                if (map[a, b - 1].GetComponent<Room_data>().Room_Created == true)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 8;
                }
                else
                {
                    map[a, b].GetComponent<Room_data>().map_code = 11;
                    map[a, b].GetComponent<Room_data>().Top = true;
                    map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                }
            }


        }


        //사이드 정점
            if (a == 2 * Level && b == 2 * Level)
            {
            if(map[a,b].GetComponent<Room_data>().Left == true && map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 10;
            }
            if (map[a, b].GetComponent<Room_data>().Left == true && map[a, b].GetComponent<Room_data>().Bottom == false)
            {
                int aa = Random.Range(0, 1);
                if(aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 4;
                }
                else if(aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 10;
                    map[a, b].GetComponent<Room_data>().Bottom = true;
                    map[a, b - 1].GetComponent<Room_data>().Top = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Left == false && map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 3;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 10;
                    map[a, b].GetComponent<Room_data>().Left = true;
                    map[a - 1, b].GetComponent<Room_data>().Right = true;
                }
            }
           
        }
            if (a == 2 * Level && b == 0)
            {
            if (map[a, b].GetComponent<Room_data>().Left == true && map[a, b].GetComponent<Room_data>().Top == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 7;
            }
            if (map[a, b].GetComponent<Room_data>().Left == true && map[a, b].GetComponent<Room_data>().Top == false)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 4;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 7;
                    map[a, b].GetComponent<Room_data>().Top = true;
                    map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Left == false && map[a, b].GetComponent<Room_data>().Top == true)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 1;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 7;
                    map[a, b].GetComponent<Room_data>().Left = true;
                    map[a - 1, b].GetComponent<Room_data>().Right = true;
                }
            }
        }
            if (a == 0 && b == 2 * Level)
            {
            if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 8;
            }
            if (map[a, b].GetComponent<Room_data>().Right == true && map[a, b].GetComponent<Room_data>().Bottom == false)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 2;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 8;
                    map[a, b].GetComponent<Room_data>().Bottom = true;
                    map[a, b - 1].GetComponent<Room_data>().Top = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Left == false && map[a, b].GetComponent<Room_data>().Bottom == true)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 3;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 8;
                    map[a, b].GetComponent<Room_data>().Right = true;
                    map[a + 1, b].GetComponent<Room_data>().Left = true;
                }
            }
        }
            if (a == 0 && b == 0)
            {
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == true)
            {
                map[a, b].GetComponent<Room_data>().map_code = 5;
            }
            if (map[a, b].GetComponent<Room_data>().Top == true && map[a, b].GetComponent<Room_data>().Right == false)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 1;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 5;
                    map[a, b].GetComponent<Room_data>().Right = true;
                    map[a + 1, b].GetComponent<Room_data>().Bottom = true;
                }
            }
            if (map[a, b].GetComponent<Room_data>().Left == false && map[a, b].GetComponent<Room_data>().Right == true)
            {
                int aa = Random.Range(0, 1);
                if (aa == 0)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 2;
                }
                else if (aa == 1)
                {
                    map[a, b].GetComponent<Room_data>().map_code = 5;
                    map[a, b].GetComponent<Room_data>().Top = true;
                    map[a, b + 1].GetComponent<Room_data>().Bottom = true;
                }
            }
        }

        
        }
        public int Check_room_code(int x, int y)
        {
            return map[x, y].GetComponent<Room_data>().map_code;

        }


    } 
