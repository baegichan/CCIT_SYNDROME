using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gachatem : MonoBehaviour
{
    public float startForce = 10f;
    Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        /*
        if (Player.EatPill == true)
        {
            Destroy(this.gameObject);
        }
        */
    }
}
