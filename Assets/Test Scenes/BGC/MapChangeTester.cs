using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangeTester : MonoBehaviour
{
    // Start is called before the first frame update
    public static MapChangeTester AbyssMask;
    public  void s_instance()
    {
    if(AbyssMask==null)
    {
            AbyssMask=this;
    }
    else
    {
            Destroy(gameObject);
    }

    }
  
    public void Start()
    {
        s_instance();
    }
    public Animator test;

    // Update is called once per frame
 
}
