using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(AddTiles))]
public class Addtilesinspecter : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("�ʵ����͸� ������ �ε����ּ���. \n�̸� �ε�� ������Ʈ���������� �ʱ�ȭ�� �ε����ּ���.", MessageType.Warning);

        AddTiles map = (AddTiles)target;
        GUILayout.Label("");
        if (GUILayout.Button("�ʵ����� ���̺�"))
        {
            map.Save_MapData();
        }


        GUILayout.Label("");
        if (GUILayout.Button("�ʵ����� �ε�"))
        {
            if(map.Check_MapData())
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
            map.Base_Tile = null;
            map.MapData = null;
          
        }
    }
}
