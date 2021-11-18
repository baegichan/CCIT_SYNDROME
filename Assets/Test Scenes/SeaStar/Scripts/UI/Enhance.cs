using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhance : MonoBehaviour
{
    public Char_Parent CP;

    public GameObject Ability_Shop;
    public GameObject Ability_Enhance;
    public GameObject EnhanceNPC;


    [Header("Sell Items")]
    public Image[] Icon_Image;
    public Text[] Explan_Text;
    public Text[] Price_Text;
    public Text[] Type_Text;
    public GameObject[] BuyButton;

    [Header("Active_Ability")]
    public GameObject ActiveBox;
    public GameObject ActiveEnhance;
    public Image Active_Icon_Image;
    public Text Active_Level_Text;
    public Text Active_Explan_Text;
    public Text Active_Price_Text;

    [Header("Passive_Ability")]
    public GameObject PassiveBox;
    public GameObject PassiveEnhance;
    public Image Passive_Icon_Image;
    public Text Passive_Level_Text;
    public Text Passive_Explan_Text;
    public Text Passive_Price_Text;

    [Header("오브젝트 위치")]
    public Vector2[] BoxPosition;
    public float[] LevelTextPosition;

    void LevelTextPos(int AbilityNum, Text LevelText)
    {
        switch(AbilityNum)
        {
            case 0:
                LevelText.transform.localPosition = new Vector3(LevelTextPosition[0], LevelText.transform.localPosition.y, LevelText.transform.localPosition.z);
                break;
            case 1:
                LevelText.transform.localPosition = new Vector3(LevelTextPosition[1], LevelText.transform.localPosition.y, LevelText.transform.localPosition.z);
                break;
            case 4:
                LevelText.transform.localPosition = new Vector3(LevelTextPosition[4], LevelText.transform.localPosition.y, LevelText.transform.localPosition.z);
                break;
            case 5:
                LevelText.transform.localPosition = new Vector3(LevelTextPosition[3], LevelText.transform.localPosition.y, LevelText.transform.localPosition.z);
                break;
            case 6:
                LevelText.transform.localPosition = new Vector3(LevelTextPosition[2], LevelText.transform.localPosition.y, LevelText.transform.localPosition.z);
                break;
        }
    }

    public void SettingAbility()
    {
        if (CP.ActiveAbility.AbSprite != null)
        {
            ActiveBox.transform.localPosition = BoxPosition[0];
            PassiveBox.transform.localPosition = BoxPosition[1];
            LevelTextPos(CP.ActiveAbility.AbCode, Active_Level_Text);

            ActiveEnhance.SetActive(true);
            Active_Level_Text.enabled = true;
            Active_Explan_Text.enabled = true;

            Active_Icon_Image.sprite = CP.ActiveAbility.AbIcon;
            Active_Level_Text.text = "LV." + CP.ActiveAbility.Enhance.ToString();
            Active_Explan_Text.text = CP.ActiveAbility.AbExplan;
            if (CP.ActiveAbility.Enhance < 3) { Active_Price_Text.text = CP.ActiveAbility.Enhance_Cost[CP.ActiveAbility.Enhance].ToString(); }
            else { Active_Price_Text.text = "MAX"; }
        }
        else
        {
            ActiveBox.transform.localPosition = BoxPosition[1];
            PassiveBox.transform.localPosition = BoxPosition[0];

            ActiveEnhance.SetActive(false);
            Active_Level_Text.enabled = false;
            Active_Explan_Text.enabled = false;
        }

        if (CP.PassiveAbility.AbSprite != null)
        {
            LevelTextPos(CP.PassiveAbility.AbCode, Passive_Level_Text);

            PassiveEnhance.SetActive(true);
            Passive_Level_Text.enabled = true;
            Passive_Explan_Text.enabled = true;

            Passive_Icon_Image.sprite = CP.PassiveAbility.AbIcon;
            Passive_Level_Text.text = "LV." + CP.PassiveAbility.Enhance.ToString();
            Passive_Explan_Text.text = CP.PassiveAbility.AbExplan;
            if (CP.PassiveAbility.Enhance < 3) { Passive_Price_Text.text = CP.PassiveAbility.Enhance_Cost[CP.ActiveAbility.Enhance].ToString(); }
            else { Passive_Price_Text.text = "MAX"; }
        }
        else
        {
            PassiveEnhance.SetActive(false);
            Passive_Level_Text.enabled = false;
            Passive_Explan_Text.enabled = false;
        }
    }

    OtherWorldShop EHNPC;

    public void SettingShop()
    {
        EHNPC = EnhanceNPC.GetComponent<OtherWorldShop>();
        for(int i = 0; i < EHNPC.SellItem.Length; i++)
        {
            Icon_Image[i].sprite = EHNPC.SellItem[i].AbIcon;
            if(EHNPC.IsSell[i])
            {
                Explan_Text[i].text = "이미 구입했습니다.";
                BuyButton[i].SetActive(false);
            }
            else
            {
                Explan_Text[i].text = EHNPC.SellItem[i].AbExplan;
                BuyButton[i].SetActive(true);
            }
            Price_Text[i].text = EHNPC.SellItem[i].AbPrice.ToString();
            //Type_Text[i].text = (EHNPC.SellItem[i].AbType == Ability.ABTYPE.Active) ? "A" : "P";
        }
    }

    public void Exit()
    {
        Ability_Enhance.SetActive(false);
        Ability_Shop.SetActive(true);
        gameObject.SetActive(false);
    }

    public void EnhaceAbility()
    {
        if(CP.ActiveAbility.Enhance < 3 && CP.P_Money >= CP.ActiveAbility.Enhance_Cost[CP.ActiveAbility.Enhance])
        {
            CP.P_Money -= CP.ActiveAbility.Enhance_Cost[CP.ActiveAbility.Enhance];
            CP.ActiveAbility.Enhance++;
            SettingAbility();
        }
    }

    public void EnhacePassive()
    {
        if (CP.PassiveAbility.Enhance < 3 && CP.P_Money >= CP.PassiveAbility.Enhance_Cost[CP.PassiveAbility.Enhance])
        {
            CP.P_Money -= CP.PassiveAbility.Enhance_Cost[CP.PassiveAbility.Enhance];
            CP.PassiveAbility.Enhance++;
            SettingAbility();
        }
    }

    public void OpenShop()
    {
        Ability_Shop.SetActive(true);
        Ability_Enhance.SetActive(false);
    }

    public void OpenEnhance()
    {
        Ability_Shop.SetActive(false);
        Ability_Enhance.SetActive(true);
        SettingAbility();
    }

    public void BuyItem_0()
    {
        BuyItem(0);
    }
    public void BuyItem_1()
    {
        BuyItem(1);
    }
    public void BuyItem_2()
    {
        BuyItem(2);
    }

    void BuyItem(int index)
    {
        if (CP.P_Money >= EHNPC.SellItem[index].AbPrice)
        {
            CP.P_Money -= EHNPC.SellItem[index].AbPrice;
            if (EHNPC.SellItem[index].AbType == Ability.ABTYPE.Active)
            {
                EHNPC.py.ActiveAbility = EHNPC.SellItem[index];
                EHNPC.py.SelectAbility();
                EHNPC.py.DecideChar();
            }
            else if (EHNPC.SellItem[index].AbType == Ability.ABTYPE.Passive)
            {
                EHNPC.py.PassiveAbility = EHNPC.SellItem[index];
                CP.UsePassive();
                CP.passive();
            }
            EHNPC.IsSell[index] = true;
            SettingShop();
            SettingAbility();
        }
    }
}
