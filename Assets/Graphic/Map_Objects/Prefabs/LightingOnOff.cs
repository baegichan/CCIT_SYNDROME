using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingOnOff : MonoBehaviour
{
    // Start is called before the first frame update


    public AbyssManager.AbyssState CurrnetDimention;
   public GameObject[] Lights =null;
    // Update is called once per frame
    void Update()
    {
        if(AbyssManager.abyss.abyssState== CurrnetDimention)
        {
            foreach(GameObject a in Lights)
            {
                a.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject a in Lights)
            {
                a.SetActive(false);
            }
        }
    }
}
