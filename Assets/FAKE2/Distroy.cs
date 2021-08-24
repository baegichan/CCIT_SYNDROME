using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distroy : MonoBehaviour
{
    public GameObject wolf;
    public GameObject pill;
    public void pillon()
    {
        pill.SetActive(true);
    }
    // Start is called before the first frame update
  public void destroythis()
    {
        Destroy(this.gameObject);
    }
  public void wolfon()
    {
        wolf.SetActive(true);
    }
}
