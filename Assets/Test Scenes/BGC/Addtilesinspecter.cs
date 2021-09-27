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
            map.Base_Tile = null;
            map.MapData = null;

        }
        GUILayout.Label("");
        EditorGUILayout.HelpBox("��Ż �̵� �̱��� �𸣴°������� ����", MessageType.Info);

        GUILayout.Label("��Ż�߰�");
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
        EditorGUILayout.HelpBox("�̺�Ʈ �߰� �̿ϼ�", MessageType.Warning);
        
        string[] options = new string[]
        {
         "None","�����&����", "���� ����",
        };
        selected = EditorGUILayout.Popup("�̺�Ʈ ���", selected, options);
        if (GUILayout.Button("�̺�Ʈ �߰�"))
        {
            switch (selected)
            {
                case 1:
                    break;
                case 2:
                    break;
            }

        }
        if (GUILayout.Button("�̺�Ʈ ����"))
        {
           

        }
        if (GUILayout.Button("�̺�Ʈ �ʱ�ȭ"))
        {


        }

        GUILayout.Label("");
        EditorGUILayout.HelpBox("���� �߰� �̿ϼ�", MessageType.Warning);

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
