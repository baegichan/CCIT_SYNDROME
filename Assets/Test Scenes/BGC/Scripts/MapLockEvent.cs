using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLockEvent : MapEvent
{
    MapLockEvent()
    {
        base.EventType = MapEvent.Event.MapLock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
