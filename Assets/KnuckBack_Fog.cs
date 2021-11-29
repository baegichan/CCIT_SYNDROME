using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckBack_Fog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<Char_Parent>().PlayerKnuckBack(transform, collision.transform, 5, false);
        }
    }
}
