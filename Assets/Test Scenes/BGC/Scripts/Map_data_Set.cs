using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Map set", menuName = "SYNDROME_MAP/Map_set")]
public class Map_data_Set : ScriptableObject
{
    // Start is called before the first frame update
    public Maps[] Maps = null;
    
}
[System.Serializable]
public class Maps
{
    public int MapCode = 0;
    [SerializeField]
    public MapData[] MapData;
}