using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nRealWorldShop : NPC
{
    public GameObject RealWorldShop_UI;
    public Ability[] item;

    void Awake()
    {
        ply = GameObject.FindGameObjectWithTag("Player");

        Scale = transform.localScale;
        DefaultX = Scale.x;
        FlipX = -Scale.x;

        talk = OpenShop;
    }

    void Update()
    {
        Flip();
        talkWithPlayer();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") { IsPlayer = true; }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { IsPlayer = false; }
    }

    void OpenShop()
    {
        RealWorldShop RS = RealWorldShop_UI.GetComponent<RealWorldShop>();
        RS.CP = ply.GetComponent<Char_Parent>();
        Char_Parent.ShopOn = true;
        RealWorldShop_UI.SetActive(true);
        RS.HPItem = item[0];
        RS.APItem = item[1];
        RS.SettingBox();
    }
}
