using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fake : MonoBehaviour
{

    public GameObject player;
    public GameObject wolf;
    public Animator chaani1;
    public Animator chaani2;
    public Vector3 position = new Vector3(17,207.5f,40);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.gameObject.transform.position==new Vector3(17,207.5f,40))
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("moveoff");
           
        }
    }
}
