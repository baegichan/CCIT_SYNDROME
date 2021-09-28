using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTiles : MonoBehaviour
{
   
    public MapData MapData;
    public GameObject Editor;
    
    public Vector2 BaseTileSize;
    GameObject Base_Tile;


    GameObject Potals;
    GameObject Event;
    int mapcode=0;
  
   
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
        if (Event == null)
        {
            Event = Instantiate((GameObject)Resources.Load("Event"), transform).gameObject;
        }
        MapData.SpawnEvent(Event);

    }
    public void Save_MapData()
    {
        mapcode = 0;
        MapData.Save_MapData(Editor.transform.GetChild(0).gameObject);
        if(!PotalnameCheck("LeftPotal",false))
        {
            MapData.Save_Potal(PotalObjectCheck("LeftPotal"),0);
            mapcode += 0b0001;
        }
        else
        {
            MapData.DestroyPotal(0);
        }
        if (!PotalnameCheck("RightPotal", false))
        {
            MapData.Save_Potal(PotalObjectCheck("RightPotal"), 1);
            mapcode += 0b0010;
        }
        else
        {
            MapData.DestroyPotal(1);
        }
        if (!PotalnameCheck("TopPotal", false))
        {
            MapData.Save_Potal(PotalObjectCheck("TopPotal"), 2);
            mapcode += 0b0100;
        }
        else
        {
            MapData.DestroyPotal(2);
        }
        if (!PotalnameCheck("BottomPotal", false))
        {
            MapData.Save_Potal(PotalObjectCheck("BottomPotal"), 3);
            mapcode += 0b1000;
        }
        else
        {
            MapData.DestroyPotal(3);
        }
        //배열 초기화 생각해야됨
        MapData.MapDataLengthSet(Event.transform.childCount);
        for (int i = 0; i < Event.transform.childCount; i++)
        {
            MapData.Map_Event[i]=(new Event(Event.transform.GetChild(i).gameObject));
        }
        MapData.Map_Code_Save(mapcode);
    }
    public  void Save_EventData()
    {

        MapData.MapDataLengthSet(Event.transform.childCount);
        for (int j =0; j<MapData.Map_Event.Length;j++)
        {
            MapData.Map_Event[j] = null;
        }
        
        for (int i=0; i<Event.transform.childCount;i++)
        {
            MapData.Map_Event[i]=(new Event(Event.transform.GetChild(i).gameObject));
        }
        
    }
}
