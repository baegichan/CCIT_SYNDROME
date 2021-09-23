using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTiles : MonoBehaviour
{
    // Start is called before the first frame update
    public MapData MapData;
    public GameObject Editor;
    public GameObject Base_Tile;
    public GameObject Get_EditorOBJ()
    {
        return Editor;
    }

   public  bool Check_MapData()
    {
        if(MapData!=null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Load_MapData()
    {
        MapData.Batch_map();
    }
    public void Save_MapData()
    {
        MapData.Save_MapData(Editor.transform.GetChild(0).gameObject);
    }
}
