using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyVIewer : MonoBehaviour
{
    public GameObject Target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Target.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Target.SetActive(false);
        }
    }
}
