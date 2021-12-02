using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIniMapSingleton : MonoBehaviour
{
    public static GameObject Minimap;
    void Awake()
    {
        if (Minimap == null)
        {
            Minimap = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
  
 
}
