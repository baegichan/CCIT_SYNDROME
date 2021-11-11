using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveModule : Modules
{

    protected GameObject Player;
    protected SpriteRenderer Pet_Sprite;
    protected Rigidbody2D Rigid;
    public void Set_Module()
    {
        if (GetComponent<Module_Set>().MoveModule == null)
        {
            GetComponent<Module_Set>().MoveModule = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public abstract void Flip(GameObject target);
    public abstract void Move();
    public abstract void Jump();
}
