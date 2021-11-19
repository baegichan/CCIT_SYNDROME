using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangeTester : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator test;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            test.SetTrigger("Changed");
        }
    }
}
