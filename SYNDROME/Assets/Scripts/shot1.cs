using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot1 : MonoBehaviour
{
    public GameObject bullet;
    public GameObject left;
    public GameObject right;
    // Start is called before the first frame update
  
    public void shottingright()
    {
        Instantiate(bullet, left.transform.position, Quaternion.identity);
    }

    public void shottingleft()
    {
        Instantiate(bullet, right.transform.position, Quaternion.identity);
    }
    // Update is called once per frame

}
