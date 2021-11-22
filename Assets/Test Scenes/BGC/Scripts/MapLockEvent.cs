using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLockEvent : MapEvent
{
    public bool Lockdown=false;
    GameObject Monster=null;
    MapLockEvent()
    {
        base.EventType = MapEvent.Event.MapLock;
    }

    private void Start()
    {
        Monster= transform.parent.Find("NormalMonsterP(Clone)").gameObject;
    }
    private void Update()
    {
        if(Lockdown==true)
        {
           if( Monster.transform.childCount==0)
           {
                Lockdown = false;
                MapManager.s_Instace.MapUnLock();
                Destroy(this.gameObject);
           }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MapManager.s_Instace.MapLock();
            Lockdown = true;
        }
    }

}
