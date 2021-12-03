using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_NPC : NPC
{
    public GameObject Enhance_Stat_UI;

    void Start()
    {
        Scale = transform.localScale;
        DefaultX = Scale.x;
        FlipX = -Scale.x;

        talk = OpenEnhance;
    }

    void Update()
    {
        Flip();
        if(PlayerPrefs.GetFloat("Tuto")==1)
        {
            talkWithPlayer();
        }
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") { IsPlayer = true; }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { IsPlayer = false; }
    }

    public Char_Parent py;

    void OpenEnhance()
    {
        Enhance_Stat ESH = Enhance_Stat_UI.GetComponent<Enhance_Stat>();
        py = ply.GetComponentInParent<Char_Parent>();
        ESH.player = ply;
        ESH.py = py;

        Char_Parent.ShopOn = true;
        Enhance_Stat_UI.SetActive(true);
        Enhance_Stat_UI.GetComponent<Enhance_Stat>().UpdateText();
    }
}
