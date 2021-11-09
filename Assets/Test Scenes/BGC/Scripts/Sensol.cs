using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensol : Modules
{
    public string Wall_Tag_Name;
    public string Target_Name;
    public float Ray_Range;
    public GameObject[] Detected_Object;
    public void Set_Module()
    {
        if (GetComponent<Module_Set>().SensolModule == null)
        {
            GetComponent<Module_Set>().SensolModule = this;
        }
        else
        {
            Destroy(this);
        }
    }
    protected RaycastHit2D Left_hit;
    protected RaycastHit2D Right_hit;
    protected RaycastHit2D Top_hit;
    protected RaycastHit2D Bottom_hit;

    protected RaycastHit2D TL_hit;
    protected RaycastHit2D TR_hit;
    protected RaycastHit2D BL_hit;
    protected RaycastHit2D BR_hit;
}
