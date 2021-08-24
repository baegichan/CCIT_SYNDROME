using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolfdrug : MonoBehaviour
{
    public GameObject eden;
    public GameObject wolf;
    public GameObject player;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("겹치긴함");
            if (Input.GetKeyDown((KeyCode)settingmanager.GM.comunication))
            {
                eden.SetActive(false);
                wolf.SetActive(true);

            Destroy(this.gameObject);
            }
        }
    }
}
