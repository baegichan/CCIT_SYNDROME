using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTiles : MonoBehaviour
{
    // Start is called before the first frame update
    public MapData MapData;
    public GameObject Editor;
    
    public Vector2 BaseTileSize;
    GameObject Base_Tile;


    GameObject Potals;
    GameObject Event;
    public GameObject PotalObjectCheck(string potalname)
    {
       
            for (int i = 0; i < Potals.transform.childCount; i++)
            {
                if (Potals.transform.GetChild(i).name == potalname)
                {
                return (Potals.transform.GetChild(i).gameObject);
                }


            }
        return null;
    }
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
    public bool EventObjectCheck()
    {
       if(Event==null)
        {
            Event= (GameObject)Instantiate(Resources.Load("Event"), this.transform);
            return true;
        }
       else
        {
            return false;
        }
    }
    public GameObject GetEventObjectCheck()
    {
        return Event;
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
        if (Potals == null)
        {
           Potals=Instantiate((GameObject)Resources.Load("Potals"), transform).gameObject;
        }        
        MapData.SpawnPotal(Potals);

    }
    public void Save_MapData()
    {
        MapData.Save_MapData(Editor.transform.GetChild(0).gameObject);
        if(!PotalnameCheck("LeftPotal",false))
        {
            MapData.Save_Potal(PotalObjectCheck("LeftPotal"),0);
        }
        else
        {
            MapData.DestroyPotal(0);
        }
        if (!PotalnameCheck("RightPotal", false))
        {
            MapData.Save_Potal(PotalObjectCheck("RightPotal"), 1);
        }
        else
        {
            MapData.DestroyPotal(1);
        }
        if (!PotalnameCheck("TopPotal", false))
        {
            MapData.Save_Potal(PotalObjectCheck("TopPotal"), 2);
        }
        else
        {
            MapData.DestroyPotal(2);
        }
        if (!PotalnameCheck("BottomPotal", false))
        {
            MapData.Save_Potal(PotalObjectCheck("BottomPotal"), 3);
        }
        else
        {
            MapData.DestroyPotal(3);
        }
    }
    
}
