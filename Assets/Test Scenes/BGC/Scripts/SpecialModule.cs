using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialModule : Modules
{
    // Start is called before the first frame update
    public void Set_Module()
    {
        if (GetComponent<Module_Set>().SpeacialModule == null)
        {
            GetComponent<Module_Set>().SpeacialModule = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
