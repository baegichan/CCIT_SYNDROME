using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourcemanager : MonoBehaviour
{
    public GameObject resourceobj;
    public Text resource;
    private int currentresource=0;
    // Start is called before the first frame update
    void Start()
    {
        resource = resourceobj.GetComponent<Text>();
    }

  public void resourcechange(int current)
    {
        currentresource += current;
        resource.text = currentresource.ToString();
    }
}
