using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(AddTiles))]
public class Addtilesinspecter : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("설정할 맵데이터 를 넣은후 타일맵설치", MessageType.Info);

        AddTiles map = (AddTiles)target;
        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 세이브"))
        {
            string localPath = "Assets/" + "TileMaps/" + "test" + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            PrefabUtility.SaveAsPrefabAssetAndConnect(map.transform.GetChild(0).gameObject, localPath, InteractionMode.UserAction);
            map.MapData.Save_TileMap(map.transform.GetChild(0).gameObject);
        }


        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 로드"))
        {
            int count = map.Get_EditorOBJ().transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Debug.Log(map.Get_EditorOBJ().transform.GetChild(0).gameObject);
                DestroyImmediate(map.Get_EditorOBJ().transform.GetChild(0).gameObject);
            }
            map.GetComponent<newMapSystem>().mapdata = null;
            //map.Base_Tile = null;


            if (map.Check_MapData())
            {
                map.MapData.Road_Default_TileMap();
                map.MapData.Get_center(map.Get_EditorOBJ());
                map.Load_MapData();
            }
        }


        GUILayout.Label("");
        if (GUILayout.Button("맵데이터 초기화"))
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
