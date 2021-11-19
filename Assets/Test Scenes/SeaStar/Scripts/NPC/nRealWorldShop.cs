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
        RealWorldShop_UI = GameObject.Find("Canvas").transform.Find("RWS").gameObject;

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

    void OpenShop()
    {
        RealWorldShop RS = RealWorldShop_UI.GetComponent<RealWorldShop>();
        RS.CP = ply.GetComponent<Char_Parent>();
        RealWorldShop_UI.SetActive(true);
        RS.HPItem = item[0];
        RS.APItem = item[1];
        RS.SettingBox();
    }
}
