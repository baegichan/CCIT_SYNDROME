using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taking : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D col)
    {


        if (col.tag == "Player")
        {
          
            if (Input.GetKey(KeyCode.F) && PlayerPrefs.GetFloat("Tuto")!=1)
            {

               GetComponent<talkchanger>().tutorialstarter();
            }

        }
    }
}