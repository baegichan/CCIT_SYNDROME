using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoadTest : MonoBehaviour
{
    // Start is called before the first frame update
    public MapData testmap;
    private void Start()
    {
        testmap.Load_MapData(gameObject);
    }

    public void Loading_Map(int mapcode)
    {
        //분별코드
        
        Resources.Load(mapcode+"/");
        testmap.Load_MapData(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
