using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Layers : MonoBehaviour
{

    public List<GameObject> AbyssLayer= new List<GameObject>();
    public List<GameObject> NomalLayer = new List<GameObject>();


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
           StartCoroutine( Change());
        }
    }
    public IEnumerator Change()
    {
        yield return new WaitForSeconds(0.5f);
        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss)
        {
            foreach (GameObject layer in AbyssLayer)

                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = true;
                }
            foreach (GameObject layer in NomalLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
        }


        else
        {
            foreach (GameObject layer in AbyssLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
            foreach (GameObject layer in NomalLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = true;
                }
            }

        }
    }
    private void OnEnable()
    {
        
        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss)
        {
            foreach (GameObject layer in AbyssLayer)

                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = true;
                }
            foreach (GameObject layer in NomalLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
        }
     
        
        else
        {
            foreach (GameObject layer in AbyssLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent <LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = false;
                }
            }
            foreach (GameObject layer in NomalLayer)
            {
                if (layer.GetComponent<TilemapCollider2D>() != null && layer.GetComponent<LayerChecker>().isFieldObject != true)
                {
                    layer.GetComponent<TilemapCollider2D>().enabled = true;
                }
            }

        }
    }
}
