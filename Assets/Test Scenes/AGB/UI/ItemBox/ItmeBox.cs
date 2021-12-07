using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItmeBox : MonoBehaviour
{
    bool isOpen = false;
    public Animator ani;

    public GameObject Item;
    public GameObject DarkEnergy;



    // Start is called before the first frame update

    bool isPlayer = false;
    private void Update()
    {
        //if (isPlayer && !isOpen)
        //{
        //    ani.SetBool("Open", true);
        //    OpenItmeBox();
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && !isOpen) { ani.SetBool("Open", true); }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { isPlayer = false; }
    }

    void OpenItmeBox()
    {
        isOpen = true;
        int i = Random.Range(0, 2);
        if(i == 0)
        {
            var dd = new Vector3(0.1f, 0.5f, 0);
            Vector3 items = gameObject.transform.position + dd;

            var d = Instantiate(Item, items, Quaternion.identity);
            d.GetComponent<AbilityItem>().Box = this.gameObject;
        }
        else
        {
            var dd = new Vector3(0.1f, 0.5f, 0);
            Vector3 items = gameObject.transform.position + dd;

            var d = Instantiate(DarkEnergy, items, Quaternion.identity);
            d.GetComponent<DarkEnergy>().box = this.gameObject;
        }
    }

    public void destroy()
    {
        Destroy(gameObject, 2f);
    }
}
