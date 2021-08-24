using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRAP1RANGE : MonoBehaviour
{
    GameObject trap;
    
    void Start()
    {
        trap = this.gameObject.transform.parent.gameObject;
    }
    // Start is called before the first frame update    
    void OnTriggerStay2D(Collider2D col)
    {


        if(col.tag=="Player")
        {
            
            trap.GetComponent<TRAP1>().trapon = true;

        }

    }



}
