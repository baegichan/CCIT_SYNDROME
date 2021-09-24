using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class MapEvent :MonoBehaviour
{
    public enum Event
    {
        None,
        MapLock,
        MonsterSpawn,
    }

    [Range(-50.0f, 50.0f)] public float L_Area;
    [Range(-50.0f, 50.0f)] public float R_Area;
    [Range(-50.0f, 50.0f)] public float T_Area;
    [Range(-50.0f, 50.0f)] public float B_Area;

    public Event EventType;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(L_Area,T_Area), new Vector2(R_Area, T_Area));
        Gizmos.DrawLine(new Vector2(R_Area, T_Area), new Vector2(R_Area, B_Area));
        Gizmos.DrawLine(new Vector2(R_Area, B_Area), new Vector2(L_Area, B_Area));
        Gizmos.DrawLine(new Vector2(L_Area, B_Area), new Vector2(L_Area, T_Area));
    }
}
