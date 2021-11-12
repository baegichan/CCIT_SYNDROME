using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensol : Modules
{

    // 재사용성 너무 낮음 흠...
    public string Wall_Tag_Name;
    public string Ground_Tag_Name;
    public string Target_Name;


    //Test
    public string First_Tag;
    public string Second_Tag;
    public string Third_Tag;


    public float Ray_Range;
    public GameObject[] Detected_Object;
    public enum RayDirection 
    {
    
    Left,
    Right,
    Top,
    Bottom
    }

    public Vector2 RayDirToVec2(RayDirection raydir)
    {
        switch (raydir)
        {
            case RayDirection.Left:
                return Vector2.left;
         
            case RayDirection.Right:
                return Vector2.right;
           
            case RayDirection.Top:
                return Vector2.up;
              
            case RayDirection.Bottom:
                return Vector2.down;
                
        }
        return Vector2.zero;

    }
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
    protected RaycastHit2D first_hit;
    protected RaycastHit2D second_hit;
    protected RaycastHit2D third_hit;
    #region 수정필요할수도있음
    protected RaycastHit2D Left_hit;
    protected RaycastHit2D Right_hit;
    protected RaycastHit2D Top_hit;
    protected RaycastHit2D Bottom_hit;

    protected RaycastHit2D TL_hit;
    protected RaycastHit2D TR_hit;
    protected RaycastHit2D BL_hit;
    protected RaycastHit2D BR_hit;
    #endregion
}
