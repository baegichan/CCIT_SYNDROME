using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Layers : MonoBehaviour
{

    public List<GameObject> AbyssLayer= new List<GameObject>();
    public List<GameObject> NomalLayer = new List<GameObject>();

    private void OnEnable()
    {
    /*
        if(AbyssManager.abyss.abyssState== AbyssManager.AbyssState.Abyss)
        {
            foreach (GameObject layer in AbyssLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFiledObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = true;
                }
            }
            foreach (GameObject layer in NomalLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFiledObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
        }
        else
        {
            foreach (GameObject layer in AbyssLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent <LayerChecker>().isFiledObject!=true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
            foreach (GameObject layer in NomalLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFiledObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = true;
                }
            }

        }*/
    }
}
