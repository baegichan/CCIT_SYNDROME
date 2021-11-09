using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModule : Modules
{
    // Start is called before the first frame update
    public void Set_Module()
    {
        if (GetComponent<Module_Set>().AttackModule == null)
        {
            GetComponent<Module_Set>().AttackModule = this;
        }
        else
        {
            Destroy(this);
        }

    }

    public abstract void Attack();
}
