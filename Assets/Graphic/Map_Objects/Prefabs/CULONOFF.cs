using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CULONOFF : MonoBehaviour
{
    public AbyssManager.AbyssState CurrnetDimention;
    // Start is called before the first frame update

    BoxCollider2D currentCol;
    private void Start()
    {
        currentCol = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (AbyssManager.abyss.abyssState == CurrnetDimention)
        {

            currentCol.enabled=true;
            
        }
        else
        {

            currentCol.enabled = false;
            
        }
    }
}
