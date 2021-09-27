using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class MapEvent :MonoBehaviour
{
   

    public enum Event
    {
        None,
        MapLock,
        MonsterSpawn,
    }

    public Event EventType;
    public GameObject[] Events;

}
