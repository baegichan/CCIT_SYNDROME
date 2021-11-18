using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealWorldShop : MonoBehaviour
{
    [Header("Shop Page")]
    public GameObject PetShop;
    public GameObject PillShop;
    public GameObject PetShopButton;
    public GameObject PillShopButton;

    [Header("HP Item Box")]
    public Ability HPItem;
    public GameObject HPItemBuy;
    public Image HPItem_Icon_Image;
    public Text HPItem_Explan_Text;
    public Text HPItem_Price_Text;

    [Header("AP Item Box")]
    public Ability APItem;
    public GameObject APItemBuy;
    public Image APItem_Icon_Image;
    public Text APItem_Explan_Text;
    public Text APItem_Price_Text;

    public void SettingBox()
    {
        PetShopButton.GetComponent<Image>().sprite = PetShopButton.GetComponent<Toggle>().spriteState.selectedSprite;

        HPItem_Icon_Image.sprite = HPItem.AbIcon;
        HPItem_Explan_Text.text = HPItem.AbExplan;
        HPItem_Price_Text.text = HPItem.AbPrice.ToString();

        APItem_Icon_Image.sprite = APItem.AbIcon;
        APItem_Explan_Text.text = APItem.AbExplan;
        APItem_Price_Text.text = APItem.AbPrice.ToString();
    }

    public void OpenPet_Box()
    {
        PetShopButton.GetComponent<Image>().sprite = PetShopButton.GetComponent<Toggle>().spriteState.selectedSprite;
        PillShopButton.GetComponent<Image>().sprite = PillShopButton.GetComponent<Toggle>().spriteState.disabledSprite;

        PetShop.SetActive(true);
        PillShop.SetActive(false);
    }

    public void OpenPill_Box()
    {
        PillShopButton.GetComponent<Image>().sprite = PillShopButton.GetComponent<Toggle>().spriteState.selectedSprite;
        PetShopButton.GetComponent<Image>().sprite = PetShopButton.GetComponent<Toggle>().spriteState.disabledSprite;

        PetShop.SetActive(false);
        PillShop.SetActive(true);
    }

    public Char_Parent CP;

    public void BuyHP()
    {
        if(CP.P_Money >= HPItem.AbPrice)
        {
            CP.P_Money -= HPItem.AbPrice;
            CP.MulYakInt++;
        }
    }

    public void BuyAP()
    {
        if (CP.P_Money >= APItem.AbPrice)
        {
            CP.P_Money -= APItem.AbPrice;
            CP.AlYakInt++;
        }
    }

    public void close()
    {
        PetShop.SetActive(true);
        PillShop.SetActive(false);
        PetShopButton.GetComponent<Image>().sprite = PetShopButton.GetComponent<Toggle>().spriteState.disabledSprite;
        PillShopButton.GetComponent<Image>().sprite = PillShopButton.GetComponent<Toggle>().spriteState.disabledSprite;
        gameObject.SetActive(false);
    }
}
