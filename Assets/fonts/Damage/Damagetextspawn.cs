using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagetextspawn : MonoBehaviour
{
    public GameObject damage;
    // Start is called before the first frame update
  
 public void damagespawner(GameObject target,int AKF)
    {

        GameObject target_damaged =Instantiate(damage, target.transform.position,Quaternion.identity,target.transform).transform.GetChild(0).gameObject;
        if (target.tag == "enemy")
        {
            target_damaged.GetComponent<SpawnDamage>().setATF2(AKF);
        }
        else if (target.tag =="Player")
        {
            target_damaged.GetComponent<SpawnDamage>().setATF1(AKF);
        }
        else if (target.tag == "Boss")
        {
            target_damaged.GetComponent<SpawnDamage>().setATF2(AKF);s
        }
       
    }

    public void heal(int heal)
    {
        GameObject target_damaged = Instantiate(damage, GameObject.Find("Player").transform.position, Quaternion.identity, GameObject.Find("Player").transform).transform.GetChild(0).gameObject;
        target_damaged.GetComponent<SpawnDamage>().setHEal(heal);
    }
}
