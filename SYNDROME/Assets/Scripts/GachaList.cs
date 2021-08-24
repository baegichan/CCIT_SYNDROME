using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GachaList
{
    public string name;
    public GameObject gachaPf;
    
    public GachaList(GachaList List)
    {
        this.name = List.name;
        this.gachaPf = List.gachaPf;
    }
}
