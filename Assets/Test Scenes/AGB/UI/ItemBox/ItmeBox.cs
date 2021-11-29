using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItmeBox : MonoBehaviour
{
    bool isOpen = false;
    public Animation ani;
    // Start is called before the first frame update

    bool isPlayer = false;
    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.F) && !isOpen && isPlayer)
        {
            Debug.Log("¿­·È´ç±ú");
            isOpen = true;
            ani.Play("ItemBox");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") { isPlayer = true; }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { isPlayer = false; }
    }

    void OpenItmeBox()
    {
        
    }
}
