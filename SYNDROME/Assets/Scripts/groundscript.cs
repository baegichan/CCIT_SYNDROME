using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(7,9, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
