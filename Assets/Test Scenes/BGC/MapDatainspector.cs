using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(newMapSystem))]
public class MapDatainspector : Editor
{
    private static Texture2D TextureField(string name, Texture2D texture)
    {
        GUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = 70;
        GUILayout.Label(name, style);
        var result = (Texture2D)EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.EndVertical();
        return result;
    }
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        EditorGUILayout.HelpBox("현재 맵데이터에 등록된 백그라운드 스프라이트 프리뷰", MessageType.Info);
        
        newMapSystem map = (newMapSystem)target;

        
        if (map.mapdata.Get_length() != 0)
        {
            for (int i = 0; i < map.mapdata.Get_length(); i++)
            {
                Sprite tex = map.mapdata.Get_Sprite(i);
                Texture2D myTexture = AssetPreview.GetAssetPreview(tex);
                GUILayout.Label("MapData index [" + i + "] : " + tex.name);
                GUILayout.Label(myTexture);


            }
        }
        else
        {
            GUILayout.Label("백그라운드가 존재하지않습니다. 맵데이터를 확인해주세요.");
        }
        
    }
}
