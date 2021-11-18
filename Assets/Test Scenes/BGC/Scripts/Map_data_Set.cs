using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Map set", menuName = "SYNDROME_MAP/Map_set")]
public class Map_data_Set : ScriptableObject
{
    // Start is called before the first frame update
    public Maps[] Maps = null;
    
    public MapData Get_RandomRoom(int code)
    {
        return Maps[code].MapData[Random.Range(0,Maps[code].MapData.Length)];
    }
}
[System.Serializable]
public class Maps
{
    public int MapCode = 0;
    [SerializeField]
    public MapData[] MapData;
}