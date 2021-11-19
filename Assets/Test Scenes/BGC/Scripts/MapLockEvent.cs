using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLockEvent : MapEvent
{

    MapLockEvent()
    {
        base.EventType = MapEvent.Event.MapLock;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MapManager.s_Instace.MapLoack();
        }
    }

}
