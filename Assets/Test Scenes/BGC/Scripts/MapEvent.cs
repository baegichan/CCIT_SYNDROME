using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapEvent :MonoBehaviour
{
   

    public enum Event
    {
        None,
        MapLock,
        MonsterSpawn,
        MapLockandMonsterSpawn
    }

    public Event EventType;


}
