using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class MonsterSpawnEvent : MapEvent
{
    // Start is called before the first frame update
    MonsterSpawnEvent()
    {
        base.EventType = MapEvent.Event.MonsterSpawn;
    }

    public Monster_List[] Monters;
   
}
[Serializable]
public class Monster_List
{
    public enum Monster_Type
    {
        None,
        Test_Monster1,
        Test_Monster2,
        Test_Monster3,
        Test_Monster4
    }

    public Monster_Type Monster = Monster_Type.None;
}