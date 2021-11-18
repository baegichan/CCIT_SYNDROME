using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_NPC : NPC
{
    public GameObject Enhance_Stat_UI;

    void Start()
    {
        ply = GameObject.FindGameObjectWithTag("Player");
        Enhance_Stat_UI = GameObject.Find("Canvas").transform.Find("Enhance_Stat").gameObject;

        Scale = transform.localScale;
        DefaultX = Scale.x;
        FlipX = -Scale.x;

        talk = OpenEnhance;
    }

    void Update()
    {
        Flip();
        talkWithPlayer();
    }

    void OpenEnhance()
    {
        Enhance_Stat ESH = Enhance_Stat_UI.GetComponent<Enhance_Stat>();
        Char_Parent py = ply.GetComponent<Char_Parent>();
        ESH.player = ply;
        ESH.py = py;

        Enhance_Stat_UI.SetActive(true);
    }
}
