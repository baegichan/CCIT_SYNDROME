using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSize : MonoBehaviour
{
    float Map_X_size;
    float Map_Y_size;

    public float X_Size
     {
        get { return Map_X_size; }
        set {
        if(Map_X_size<value) 
        {
        Map_X_size = value;
        }
        }
     }
    public float Y_Size
    {
        get { return Map_Y_size; }
        set
        {
            if (Map_Y_size < value)
            {
                Map_Y_size = value;
            }
        }
    }
}
