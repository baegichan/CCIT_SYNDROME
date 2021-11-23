using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssChange : MonoBehaviour
{
    // Start is called before the first frame update
    
   public void Goabyss()
    {
        AbyssManager.abyss.GoAbyss();
            
    }
    public void Goreal()
    {
        AbyssManager.abyss.GoReal();
    }
}
