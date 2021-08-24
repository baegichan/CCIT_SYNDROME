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
            GameObject gamemaster= GameObject.Find("GameManager");
            if (gamemaster != null && Input.GetKey(settingmanager.GM.comunication))
            {

                gamemaster.GetComponent<talkchanger>().tutorialstarter();
            }

        }
    }
}