using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergy : MonoBehaviour
{
    public GameObject player;
    private bool teststopper;
    public float speed;
    public GameObject gamemanager;
    private void Start()
    {
        StartCoroutine(onsokunosonic());
        gamemanager = GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        if (teststopper)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y+2, player.transform.position.z), step);
        }
    }
    
      IEnumerator onsokunosonic()
    {

        yield return new WaitForSeconds(2);
        teststopper = true;
    }

void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.GetComponent<PlayerMovement>().YaminoDarkEnergy += 20;
            gamemanager.GetComponent<resourcemanager>().resourcechange(20);
            Destroy(transform.gameObject);
        }

    }

}
