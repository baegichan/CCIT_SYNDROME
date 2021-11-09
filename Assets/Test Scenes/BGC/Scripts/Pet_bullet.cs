using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public Rigidbody2D rigid;
    private void Start()
    {
        
        rigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.forward * Speed;
    }
}
