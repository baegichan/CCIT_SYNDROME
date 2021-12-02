using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhance : MonoBehaviour
{
    public Char_Parent CP;

    public GameObject Ability_;
    public GameObject Ability_Shop;
    public GameObject Enhance_;
    public GameObject Ability_Enhance;
    public GameObject EnhanceNPC;
    public GameObject[] PageButton;

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

    void Update()
    {
        if (gameObject.activeSelf) { Char_Parent.ShopOn = true; }
    }

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
            Active_Price_Text.enabled = true;

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
            Active_Price_Text.enabled = false;
        }

        if (CP.PassiveAbility.AbSprite != null)
        {
            LevelTextPos(CP.PassiveAbility.AbCode, Passive_Level_Text);

            PassiveEnhance.SetActive(true);
            Passive_Level_Text.enabled = true;
            Passive_Explan_Text.enabled = true;
            Passive_Price_Text.enabled = true;

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
            Passive_Price_Text.enabled = false;
        }
    }

    OtherWorldShop EHNPC;

    public void SettingShop()
    {
        PageButton[0].GetComponent<Image>().sprite = PageButton[0].GetComponent<Button>().spriteState.selectedSprite;

        EHNPC = EnhanceNPC.GetComponent<OtherWorldShop>();
        for(int i = 0; i < EHNPC.SellItem.Length; i++)
        {
            Icon_Image[i].sprite = EHNPC.SellItem[i].AbIcon;
            if(EHNPC.IsSell[i])
            {
                Explan_Text[i].text = "이미 구입했습니다.";
                BuyButton[i].SetActive(false);
                Price_Text[i].enabled = false;
            }
            else
            {
                Explan_Text[i].text = EHNPC.SellItem[i].AbExplan;
                BuyButton[i].SetActive(true);
                Price_Text[i].enabled = true;
            }
            Price_Text[i].text = EHNPC.SellItem[i].AbPrice.ToString();
        }
    }

    public void Exit()
    {
        PageButton[0].GetComponent<Image>().sprite = PageButton[0].GetComponent<Button>().spriteState.disabledSprite;
        PageButton[1].GetComponent<Image>().sprite = PageButton[1].GetComponent<Button>().spriteState.disabledSprite;

        Ability_Enhance.SetActive(false);
        Enhance_.SetActive(false);
        Ability_Shop.SetActive(true);
        Ability_.SetActive(true);
        gameObject.SetActive(false);
        Char_Parent.ShopOn = false;
    }

    public void EnhaceAbility()
    {
        if(CP.ActiveAbility.Enhance < 3 && AbyssManager.abyss.Darkfog >= CP.ActiveAbility.Enhance_Cost[CP.ActiveAbility.Enhance])
        {
            AbyssManager.abyss.Darkfog -= CP.ActiveAbility.Enhance_Cost[CP.ActiveAbility.Enhance];
            CP.ActiveAbility.Enhance++;
            SettingAbility();
        }
    }

    public void EnhacePassive()
    {
        if (CP.PassiveAbility.Enhance < 3 && AbyssManager.abyss.Darkfog >= CP.PassiveAbility.Enhance_Cost[CP.PassiveAbility.Enhance])
        {
            AbyssManager.abyss.Darkfog -= CP.PassiveAbility.Enhance_Cost[CP.PassiveAbility.Enhance];
            CP.PassiveAbility.Enhance++;
            SettingAbility();
        }
    }

    public void OpenShop()
    {
        PageButton[0].GetComponent<Image>().sprite = PageButton[0].GetComponent<Button>().spriteState.selectedSprite;
        PageButton[1].GetComponent<Image>().sprite = PageButton[1].GetComponent<Button>().spriteState.disabledSprite;

        Ability_.SetActive(true);
        Ability_Shop.SetActive(true);
        Enhance_.SetActive(false);
        Ability_Enhance.SetActive(false);
    }

    public void OpenEnhance()
    {
        PageButton[0].GetComponent<Image>().sprite = PageButton[0].GetComponent<Button>().spriteState.disabledSprite;
        PageButton[1].GetComponent<Image>().sprite = PageButton[1].GetComponent<Button>().spriteState.selectedSprite;

        Ability_.SetActive(false);
        Ability_Shop.SetActive(false);
        Enhance_.SetActive(true);
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
        if (AbyssManager.abyss.Darkfog >= EHNPC.SellItem[index].AbPrice)
        {
            AbyssManager.abyss.Darkfog -= EHNPC.SellItem[index].AbPrice;
            if (EHNPC.SellItem[index].AbType == Ability.ABTYPE.Active)
            {
                PlayerSkillUI.skill.Image_Active.sprite = EHNPC.SellItem[index].icon;
                PlayerSkillUI.skill.Image_CoolTime.sprite = EHNPC.SellItem[index].CoolTime;
                EHNPC.py.ActiveAbility = EHNPC.SellItem[index];
                EHNPC.py.SelectAbility();
                EHNPC.py.DecideChar();
                if (EHNPC.SellItem[index].AbCode == 0)
                {
                    EHNPC.py.Ani.SetFloat("AbilityNum",0);
                    EHNPC.py.Ani.SetTrigger("Ability");
                }
            }
            else if (EHNPC.SellItem[index].AbType == Ability.ABTYPE.Passive)
            {
                PlayerSkillUI.skill.Image_Passive.sprite = EHNPC.SellItem[index].icon;
                EHNPC.py.PassiveAbility = EHNPC.SellItem[index];
                CP.UsePassive();
                CP.passive();
            }
            CP.SaveAbilityHistory(EHNPC.SellItem[index]);
            EHNPC.IsSell[index] = true;
            SettingShop();
            SettingAbility();
        }
    }
}
