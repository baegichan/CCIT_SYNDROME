using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class imagechage : MonoBehaviour
{
    public bool t;
    public GameObject attack;
    
    // Start is called before the first frame update
    

    private void OnDisable()
    {
        t = false;
    }

    private void OnEnable()
    {
        t = true;
    }
}
