using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(AddTiles))]
public class Addtilesinspecter : Editor
{
    // Start is called before the first frame update
    int selected = 0;
    int Eventselected = 0;
    int Monsterselected = 0;
    private GameObject Tilecol;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("맵데이터를 넣은후 로드해주세요. \n미리 로드된 오브젝트들이있으면 초기화후 로드해주세요.", MessageType.Info);
        AddTiles map = (AddTiles)target;
        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 세이브"))
        {
            map.Save_MapData();
            EditorUtility.SetDirty(map);
        }
        if (GUILayout.Button("맵데이터 로드"))
        {
            if (map.Check_MapData())
            {
                map.MapData.Get_center(map.Get_EditorOBJ());
                map.Load_MapData();
            }
        }
        GUILayout.Label("");
        GUILayout.Label("");
        if (GUILayout.Button("에디터 초기화"))
        {
            int count = map.Editor.transform.childCount;
            Debug.Log(count);
            for (int i = 0; i < count; i++)
            {
                Debug.Log(map.Editor.transform.GetChild(0).gameObject);
                DestroyImmediate(map.Editor.transform.GetChild(0).gameObject);
            }
            // map.Base_Tile = null;
            map.MapData = null;

        }






        GUILayout.Label("");
        if (GUILayout.Button("카메라 범위 컬라이더 스폰"))
        {
          if (map.MapData != null)
          {
           if(GameObject.Find("Grid(Clone)")!=null)
           {
                   map.tileSet = map.MapData.Load_DefaultTileCollider(map.Editor);
           }
          }
        }

        if (GUILayout.Button("몬스터오브젝트 스폰"))
        {
            if (map.GetEventObjectCheck() != null)
            {
                
                map.MapData.Load_AbyssMonsterParent(map.GetEventObjectCheck());
                map.MapData.Load_NormalMonsterParent(map.GetEventObjectCheck());
                
            }
        }
        GUILayout.Label("");
        EditorGUILayout.HelpBox("포탈 이름 및 이동방향 설치시 확인 요망", MessageType.Info);

        GUILayout.Label("포탈추가");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("LeftPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("LeftPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "LeftPotal";
               // potal.GetComponent<PotalEvent>().PotalType = Potals.PotalType.L;
            }

        }
        if (GUILayout.Button(new GUIContent("RightPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("RightPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "RightPotal";
              //  potal.GetComponent<PotalEvent>().PotalType = Potals.PotalType.R;
            }
        }
        if (GUILayout.Button(new GUIContent("TopPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("TopPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "TopPotal";
             //   potal.GetComponent<PotalEvent>().PotalType = Potals.PotalType.T;
            }
        }
        if (GUILayout.Button(new GUIContent("BottomPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("BottomPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "BottomPotal";
            //    potal.GetComponent<PotalEvent>().PotalType = Potals.PotalType.B;
            }
            //유형 변경추가
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("포탈제거");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("LeftPotal"), GUILayout.Width(80)))
        {
            map.PotalnameCheck("LeftPotal", true);

        }
        if (GUILayout.Button(new GUIContent("RightPotal"), GUILayout.Width(80)))
        {

            map.PotalnameCheck("RightPotal", true);

        }
        if (GUILayout.Button(new GUIContent("TopPotal"), GUILayout.Width(80)))
        {
            map.PotalnameCheck("TopPotal", true);

        }
        if (GUILayout.Button(new GUIContent("BottomPotal"), GUILayout.Width(80)))
        {

            map.PotalnameCheck("BottomPotal", true);

            //유형 변경추가
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("");
        EditorGUILayout.HelpBox("이벤트 범위 확인요망", MessageType.Info);

        string[] options = new string[]
        {
         "None","방잠김&열림", "몬스터 스폰" , "방잠김+몬스터스폰",
        };
        selected = EditorGUILayout.Popup("이벤트 목록", selected, options);
        if (GUILayout.Button("이벤트 추가"))
        {
            map.EventObjectCheck();
            switch (selected)
            {
                case 1:
                    GameObject Eventtest1 = (GameObject)Instantiate(Resources.Load("DefaultEvent"), map.GetEventObjectCheck().transform);
                    Eventtest1.name = "Lock";
                    Eventtest1.AddComponent<MapLockEvent>().EventType = MapEvent.Event.MapLock; 
                    break;
                case 2:
                    GameObject Eventtest2 = (GameObject)Instantiate(Resources.Load("DefaultEvent"), map.GetEventObjectCheck().transform);
                    Eventtest2.name = "Spawn";
                    Eventtest2.AddComponent<MonsterSpawnEvent>().EventType = MapEvent.Event.MonsterSpawn;

                    break;
                case 3:
                    GameObject Eventtest3 = (GameObject)Instantiate(Resources.Load("DefaultEvent"), map.GetEventObjectCheck().transform);
                    Eventtest3.name = "LockandSpawn"; ;
                    Eventtest3.AddComponent<MonsterSpawnEvent>().EventType = MapEvent.Event.MonsterSpawn;
                    Eventtest3.AddComponent<MapLockEvent>().EventType = MapEvent.Event.MapLock;

                    break;
            }

        }
        if (GUILayout.Button("이벤트 저장"))
        {
            map.Save_EventData();

        }
        if (GUILayout.Button("이벤트 초기화"))
        {

            if (map.GetEventObjectCheck() != null)
            {
                int Count = map.GetEventObjectCheck().transform.childCount;
                for (int i = 0; i < Count; i++)
                {

                    DestroyImmediate(map.GetEventObjectCheck().transform.GetChild(0).gameObject);
                   // map.MapData.Map_Event[i].DestroyEvent();
                    map.MapData.Map_Event = null;
                }

            }
        }

        GUILayout.Label("");
        EditorGUILayout.HelpBox("현재 몬스터스폰 이벤트에 약간의 에러가있는걸 확인\n세이브시에 잘저장되어있는지 확인요망", MessageType.Warning);



        string[] EventLists;
        if (map.MapData != null)
        {
            if (map.MapData.Map_Event != null&& map.MapData.Map_Event.transform.childCount>0)
            {
                EventLists = new string[map.MapData.Map_Event.transform.childCount];
                for (int i = 0; i < map.MapData.Map_Event.transform.childCount; i++)
                {
                    EventLists[i] = map.MapData.Map_Event.transform.GetChild(i).name;
                }
            }
            else
            {
                EventLists = new string[] { "None" };
            }
        }
        else
        {
            EventLists = new string[] { "None" };
        }



      
        Eventselected = EditorGUILayout.Popup("등록된 이벤트 목록", Eventselected, EventLists);
        string[] MonsterList;
        
        if (EventLists!= null)
        {
            if (EventLists[Eventselected] == "Spawn" || EventLists[Eventselected] == "LockandSpawn")
            {
                if (map.MapData != null)
                {
                    if (map.MapData.MonsterSet != null)
                    {
                        MonsterList = new string[map.MapData.MonsterSet.Monster.Length];
                        for (int i = 0; i < map.MapData.MonsterSet.Monster.Length; i++)
                        {
                            MonsterList[i] = map.MapData.MonsterSet.Monster[i].name;
                        }

                    }
                    else
                    {
                        MonsterList = new string[] { "None" };
                    }
                }
                else
                {
                    MonsterList = new string[] { "None" };
                }
                Monsterselected = EditorGUILayout.Popup("몬스터 종류", Monsterselected, MonsterList);
                if (GUILayout.Button(new GUIContent("몬스터 삭제"), GUILayout.Width(160)))
                {
                  for(int i =0; i<map.GetEventObjectCheck().transform.GetChild(Eventselected).childCount;i++)
                    {
                        if (map.GetEventObjectCheck().transform.GetChild(Eventselected).GetChild(i).name == MonsterList[Monsterselected])
                         {
                            DestroyImmediate(map.GetEventObjectCheck().transform.GetChild(Eventselected).GetChild(i).gameObject);
                            break;
                         }
                    }
                    

                }
                GUILayout.Label("위치 고정");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(new GUIContent("몬스터 소환"), GUILayout.Width(160)))
                {
                    Instantiate(Resources.Load("MonsterTest"), map.GetEventObjectCheck().transform.GetChild(Eventselected)).name = MonsterList[Monsterselected];


                }

                EditorGUILayout.EndHorizontal();
                GUILayout.Label("");
                EditorGUILayout.HelpBox("위치 랜덤에대해서는 추후 예정", MessageType.Warning);
                GUILayout.Label("위치 랜덤");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(new GUIContent("몬스터 랜덤"), GUILayout.Width(80)))
                {

                    Instantiate(Resources.Load("MonsterTest"), map.GetEventObjectCheck().transform.GetChild(Eventselected)).name = MonsterList[Monsterselected];

                }
                if (GUILayout.Button(new GUIContent("몬스터 고정"), GUILayout.Width(80)))
                {

                    Instantiate(Resources.Load("MonsterTest"), map.GetEventObjectCheck().transform.GetChild(Eventselected)).name = MonsterList[Monsterselected];

                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Label("");
            }
        }
    }
}
