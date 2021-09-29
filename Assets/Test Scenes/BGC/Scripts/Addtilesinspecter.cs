using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(AddTiles))]
public class Addtilesinspecter : Editor
{
    // Start is called before the first frame update
    int selected = 0;
    int Eventselected = 0;
    int Monsterselected = 0;
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("�ʵ����͸� ������ �ε����ּ���. \n�̸� �ε�� ������Ʈ���������� �ʱ�ȭ�� �ε����ּ���.", MessageType.Info);

        AddTiles map = (AddTiles)target;
        GUILayout.Label("");
        if (GUILayout.Button("�ʵ����� ���̺�"))
        {
            map.Save_MapData();
        }


        GUILayout.Label("");
        if (GUILayout.Button("�ʵ����� �ε�"))
        {
            if (map.Check_MapData())
            {
                map.MapData.Get_center(map.Get_EditorOBJ());
                map.Load_MapData();
            }
        }


        GUILayout.Label("");
        if (GUILayout.Button("������ �ʱ�ȭ"))
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
        EditorGUILayout.HelpBox("��Ż �̵� �����ؾߵ�", MessageType.Info);

        GUILayout.Label("��Ż�߰�");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("LeftPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("LeftPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "LeftPotal";
            }

        }
        if (GUILayout.Button(new GUIContent("RightPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("RightPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "RightPotal";
            }
        }
        if (GUILayout.Button(new GUIContent("TopPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("TopPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "TopPotal";
            }
        }
        if (GUILayout.Button(new GUIContent("BottomPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("BottomPotal", false))
            {
                GameObject potal = Instantiate((GameObject)Resources.Load("Potal"), map.GetPotalsroots().transform);
                potal.name = "BottomPotal";
            }
            //���� �����߰�
        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("��Ż����");
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

            //���� �����߰�
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("");
        EditorGUILayout.HelpBox("���� �̺�Ʈ �����ؾߵ�", MessageType.Info);

        string[] options = new string[]
        {
         "None","�����&����", "���� ����" , "�����+���ͽ���",
        };
        selected = EditorGUILayout.Popup("�̺�Ʈ ���", selected, options);
        if (GUILayout.Button("�̺�Ʈ �߰�"))
        {
            map.EventObjectCheck();
            switch (selected)
            {
                case 1:
                    GameObject Eventtest1 = (GameObject)Instantiate(Resources.Load("DefaultEvent"), map.GetEventObjectCheck().transform);
                    Eventtest1.name = "Lock";
                    Eventtest1.AddComponent<MapLockEvent>().EventType = MapEvent.Event.MapLock; ;
                    break;
                case 2:
                    GameObject Eventtest2 = (GameObject)Instantiate(Resources.Load("DefaultEvent"), map.GetEventObjectCheck().transform);
                    Eventtest2.name = "Spawn";
                    Eventtest2.AddComponent<MonsterSpawnEvent>().EventType = MapEvent.Event.MonsterSpawn;

                    break;
                case 3:
                    GameObject Eventtest3 = (GameObject)Instantiate(Resources.Load("DefaultEvent"), map.GetEventObjectCheck().transform);
                    Eventtest3.name = "LockandSpawn"; ;
                    Eventtest3.AddComponent<MonsterSpawnEvent>().EventType = MapEvent.Event.MapLockandMonsterSpawn;

                    break;
            }

        }
        if (GUILayout.Button("�̺�Ʈ ����"))
        {
            map.Save_EventData();

        }
        if (GUILayout.Button("�̺�Ʈ �ʱ�ȭ"))
        {

            if (map.GetEventObjectCheck() != null)
            {
                int Count = map.GetEventObjectCheck().transform.childCount;
                for (int i = 0; i < Count; i++)
                {

                    DestroyImmediate(map.GetEventObjectCheck().transform.GetChild(0).gameObject);
                    //map.MapData.Map_Event[i].DestroyEvent();
                    //map.MapData.Map_Event = null;
                }

            }
        }

        GUILayout.Label("");
        EditorGUILayout.HelpBox("���� �߰� �̿ϼ�", MessageType.Warning);



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



      
        Eventselected = EditorGUILayout.Popup("�̺�Ʈ ���", Eventselected, EventLists);
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
                Monsterselected = EditorGUILayout.Popup("���� ����", Monsterselected, MonsterList);

                GUILayout.Label("��ġ ����");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(new GUIContent("���� ����"), GUILayout.Width(80)))
                {



                }

                EditorGUILayout.EndHorizontal();
                GUILayout.Label("");

                GUILayout.Label("��ġ ����");
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(new GUIContent("���� ����"), GUILayout.Width(80)))
                {



                }
                if (GUILayout.Button(new GUIContent("���� ����"), GUILayout.Width(80)))
                {



                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Label("");
            }
        }
    }
}
