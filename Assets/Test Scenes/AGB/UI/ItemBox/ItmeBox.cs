using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItmeBox : MonoBehaviour
{
    bool isOpen = false;
    public Animator ani;

    public GameObject Item;



    // Start is called before the first frame update

    bool isPlayer = false;
    private void Update()
    {
        Debug.Log(isPlayer);
        if (Input.GetKeyDown(KeyCode.F) && !isOpen && isPlayer)
        {

            isOpen = true;
            ani.SetBool("Open", true);
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

        var dd = new Vector3(0, 1, 0);
        Vector3 items = gameObject.transform.position + dd;

        var d = Instantiate(Item, items, Quaternion.identity);
   
        
      
    }
}
