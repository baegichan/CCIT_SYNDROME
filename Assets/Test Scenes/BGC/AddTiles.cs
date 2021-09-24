using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTiles : MonoBehaviour
{
    // Start is called before the first frame update
    public MapData MapData;
    public GameObject Editor;
    public GameObject Base_Tile;

    GameObject Potals;
    

    public bool PotalnameCheck(string potalname,bool isdelete)
    {
        if (!isdelete)
        {
            for (int i = 0; i < Potals.transform.childCount; i++)
            {
                if (Potals.transform.GetChild(i).name == potalname)
                {
                    return false;
                }


            }
            return true;
        }
        else
        {
            for (int i = 0; i < Potals.transform.childCount; i++)
            {
                if (Potals.transform.GetChild(i).name == potalname)
                {
                    DestroyImmediate(Potals.transform.GetChild(i).gameObject);
                }


            }
            return false ;
        }
    }
    /// <summary>
    /// 비어있을시 true 아닐시 false
    /// </summary>
    /// <returns></returns>
    public bool PotalsCheck()
    {
        if(Potals==null)
        {
            Potals=(GameObject)Instantiate(Resources.Load("Potals"), this.transform);
            return true;
        }
        else
        {
          
            return false;
        }
    }
    public GameObject GetPotalsroots()
    {
        return Potals;
    }
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
