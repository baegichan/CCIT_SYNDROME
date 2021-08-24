using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageevent : MonoBehaviour
{
    public GameObject saviour;

    private void Start()
    {
        saviour = GameObject.Find("saviour");
    }
    private void Update()
    {
        saviour.transform.position = new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z);
    }
    // Start is called before the first frame update
    public void attackevent()
    {
      
        Collider[] colls = Physics.OverlapBox(saviour.transform.position,new Vector3(4,6,10));
        for(int i =0; i<colls.Length;i++)
        {
            Debug.Log(colls[i].name);
            if(colls[i].tag=="enemy")
            {
                
                colls[i].GetComponent<AddForce_>().check();
            }
            else if(colls[i].tag=="Boss")
            {
               
            }
        }
   
    }


}
