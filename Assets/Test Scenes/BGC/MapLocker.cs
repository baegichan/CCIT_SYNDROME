using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLocker : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount>0)
        {
            MapManager.s_Instace.Map_Lock = true;
        }
        else
        {
            MapManager.s_Instace.Map_Lock = false;
        }
    }
}
