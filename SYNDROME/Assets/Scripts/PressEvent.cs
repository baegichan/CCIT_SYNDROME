using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEvent : MonoBehaviour
{
    public GameObject presskey;
    private GameObject key;
    public float height = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            key = Instantiate(presskey, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + height, this.gameObject.transform.position.z), Quaternion.identity, this.gameObject.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           Destroy(key);
        }
    }
}
