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
        Editor = GameObject.Find("MapEditer");
        return Editor;
    }

    string Text;

    public string TEXT
    {
        get { return Text; }
        set { Text = value; }

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
        if (transform.childCount != 0)
        {

        }
        MapData.Batch_map();
        this.GetComponent<newMapSystem>().Set_mapdata(this.MapData);
    }
}
