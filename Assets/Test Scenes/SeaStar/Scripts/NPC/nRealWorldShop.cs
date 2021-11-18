using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nRealWorldShop : NPC
{
    public GameObject RealWorldShop_UI;
    public AbilityItem item;
    public Ability[] SellItem;
    public bool[] IsSell;

    void Start()
    {
        ply = GameObject.FindGameObjectWithTag("Player");
        Enhance_UI = GameObject.Find("Canvas").transform.Find("Enhance_Ability").gameObject;
        item = GetComponent<AbilityItem>();

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

    public Char_Parent py;

    void OpenShop()
    {
        RealWorldShop RS = RealWorldShop_UI.GetComponent<RealWorldShop>();
        py = ply.GetComponent<Char_Parent>();
        //EH.CP = py;
        //EH.EnhanceNPC = gameObject;
        //Enhance_UI.SetActive(true);
        //EH.SettingShop();
        //EH.SettingAbility();
    }
}
