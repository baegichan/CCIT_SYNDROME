using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWorldShop : NPC
{
    public GameObject Enhance_UI;
    public AbilityItem item;
    public Ability[] SellItem;
    public bool[] IsSell;

    void Awake()
    {
        ply = GameObject.FindGameObjectWithTag("Player");
        item = GetComponent<AbilityItem>();
        sellItem();

        Scale = transform.localScale;
        DefaultX = Scale.x;
        FlipX = -Scale.x;

        talk = OpenEngance;
    }

    void Update()
    {
        Flip();
        talkWithPlayer();
    }

    public Char_Parent py;

    void OpenEngance()
    {
        Enhance EH = Enhance_UI.GetComponent<Enhance>();
        py = ply.GetComponent<Char_Parent>();
        EH.CP = py;
        EH.EnhanceNPC = gameObject;
        Enhance_UI.SetActive(true);
        EH.SettingShop();
        EH.SettingAbility();
    }

    void sellItem()
    {
        for(int i = 0; i < SellItem.Length; i++)
        {
            SellItem[i] = Decide_SellItem();
            item.commonList = new List<Ability>();
            item.rareList = new List<Ability>();
            item.uniqueList = new List<Ability>();
            item.DrinkList = new List<Ability>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player") { IsPlayer = true; }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { IsPlayer = false; }
    }

    Ability Decide_SellItem()
    {
        item.AlyakList();
        float SelectItem = Random.Range(0, 100.0f);
        if (SelectItem <= 80)
        {
            if(item.commonList.Count == 0) { Decide_SellItem(); }
            else
            {
                int i = Random.Range(0, item.commonList.Count);
                item.ThisCode = item.commonList[i].AbCode;
            }
        }
        else if (SelectItem > 80 && SelectItem <= 95)
        {
            if (item.rareList.Count == 0) { Decide_SellItem(); }
            else
            {
                int i = Random.Range(0, item.rareList.Count);
                item.ThisCode = item.rareList[i].AbCode;
            }
        }
        else if (SelectItem > 95 && SelectItem <= 100)
        {
            if (item.uniqueList.Count == 0) { Decide_SellItem(); }
            else
            {
                int i = Random.Range(0, item.uniqueList.Count);
                item.ThisCode = item.uniqueList[i].AbCode;
            }
        }

        item.SelectAbility();
        Ability ab = item.me;

        return ab;
    }
}