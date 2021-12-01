using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapColOnOff : MonoBehaviour
{
    AbyssManager.AbyssState state;
    // Start is called before the first frame update
    void Start()
    {
        Check();


    }
    void Check()
    {
        if (GetComponent<LayerChecker>().isAbyssLayer)
        {
            state = AbyssManager.AbyssState.Abyss;
        }
        else
        {
            state = AbyssManager.AbyssState.Reality;
        }
        if (AbyssManager.abyss.abyssState == state)
        {

        }
        else
        {
            GetComponent<TilemapCollider2D>().enabled = false;
        }
    }
    private void OnEnable()
    {
        Check();
    }
    // Update is called once per frame
  
}
