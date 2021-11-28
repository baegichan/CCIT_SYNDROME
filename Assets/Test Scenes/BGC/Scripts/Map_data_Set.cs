using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Map set", menuName = "SYNDROME_MAP/Map_set")]
public class Map_data_Set : ScriptableObject
{
    // Start is called before the first frame update
    public Maps[] Maps = null;
    public Maps[] SpecialRoom = null;
    public Maps[] StartRoom = null;
    public Maps[] BossRoom = null;
    public MapData Get_RandomRoom(int code)
    {
        return Maps[code].MapData[Random.Range(0,Maps[code].MapData.Length)];
    }

    public MapData Get_RandomBossRoom(int code)
    {
        return BossRoom[code].MapData[Random.Range(0, BossRoom[code].MapData.Length)];
    }

    public MapData Get_RandomStartRoom(int code)
    {
        return StartRoom[code].MapData[Random.Range(0, StartRoom[code].MapData.Length)];
    }

    public MapData Get_RandomSpecialRoom(int code)
    {
        return SpecialRoom[code].MapData[Random.Range(0, SpecialRoom[code].MapData.Length)];
    }
}
[System.Serializable]
public class Maps
{
    public int MapCode = 0;
    [SerializeField]
    public MapData[] MapData;
}