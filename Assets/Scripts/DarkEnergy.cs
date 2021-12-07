using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergy : MonoBehaviour
{
    public GameObject player;
    private bool teststopper;
    public float speed;
    public GameObject box;


    private void Awake()
    {
        player = Camera.main.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow.gameObject;
    }
    private void Start()
    {
        StartCoroutine(onsokunosonic());
    }
    
    void Update()
    {
        if (teststopper)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), step);
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
            AbyssManager.abyss.Darkfog += Random.Range(80, 141);
            if(box != null) { box.GetComponent<ItmeBox>().destroy(); }
            Destroy(transform.gameObject);
        }
    }
}
