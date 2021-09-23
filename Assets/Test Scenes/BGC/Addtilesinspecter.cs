using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(AddTiles))]
public class Addtilesinspecter : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("맵데이터를 넣은후 로드해주세요. \n미리 로드된 오브젝트들이있으면 초기화후 로드해주세요.", MessageType.Warning);

        AddTiles map = (AddTiles)target;
        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 세이브"))
        {
            map.Save_MapData();
        }


        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 로드"))
        {
            if(map.Check_MapData())
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
    }
}
