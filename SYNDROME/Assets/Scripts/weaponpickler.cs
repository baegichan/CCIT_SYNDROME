using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponpickler : MonoBehaviour
{
    public Sprite weaponhand;
    public Sprite noweaponhand;
    public GameObject target;
  
        
    public  void pickup()
    {
        target.GetComponent<SpriteRenderer>().sprite = weaponhand;


    }
}
