using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(AddTiles))]
public class Addtilesinspecter : Editor
{
    // Start is called before the first frame update
    int selected = 0;
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("맵데이터를 넣은후 로드해주세요. \n미리 로드된 오브젝트들이있으면 초기화후 로드해주세요.", MessageType.Info);

        AddTiles map = (AddTiles)target;
        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 세이브"))
        {
            map.Save_MapData();
        }


        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 로드"))
        {
            if (map.Check_MapData())
            {
                map.MapData.Get_center(map.Get_EditorOBJ());
                map.Load_MapData();
            }
        }


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
            map.Base_Tile = null;
            map.MapData = null;

        }
        GUILayout.Label("");
        EditorGUILayout.HelpBox("포탈 이동 미구현 모르는거있으면 질문", MessageType.Info);

        GUILayout.Label("포탈추가");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("LeftPotal"), GUILayout.Width(80)))
        {
            map.PotalsCheck();
            if (map.PotalnameCheck("LeftPotal",false))
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
        EditorGUILayout.HelpBox("이벤트 추가 미완성", MessageType.Warning);
        
        string[] options = new string[]
        {
         "None","방잠김&열림", "몬스터 스폰",
        };
        selected = EditorGUILayout.Popup("이벤트 목록", selected, options);
        if (GUILayout.Button("이벤트 추가"))
        {
            switch (selected)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

        }
        if (GUILayout.Button("이벤트 저장"))
        {
           

        }
        if (GUILayout.Button("이벤트 초기화"))
        {


        }

        GUILayout.Label("");
        EditorGUILayout.HelpBox("몬스터 추가 미완성", MessageType.Warning);

        GUILayout.Label("위치 고정");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("몬스터 고정"), GUILayout.Width(80)))
        {

        }
   
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("");

        GUILayout.Label("위치 랜덤");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("몬스터 랜덤"), GUILayout.Width(80)))
        {

        }
        if (GUILayout.Button(new GUIContent("몬스터 고정"), GUILayout.Width(80)))
        {

        }
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("");
   
    }
}
