using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mappannel : MonoBehaviour
{
    public Sprite[] mappannel;
    public static GameObject pannel;
    private GameObject darkpannel;
    private void Start()
    {
        pannel = this.gameObject;
        darkpannel = pannel.transform.GetChild(0).gameObject;
    }

    public void panneldownevent(int spritenum)
    {
        pannel.GetComponent<Image>().sprite = mappannel[spritenum];
        pannel.GetComponent<Animator>().SetTrigger("panneldowntrigger");
    }

}
