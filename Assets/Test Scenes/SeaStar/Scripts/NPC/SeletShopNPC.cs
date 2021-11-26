using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletShopNPC : MonoBehaviour
{
    public GameObject AbyssShop;
    public GameObject NormalShop;

    bool IsAbyss = false;
    private void Update()
    {

        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss && IsAbyss)
        {
            AbyssShop.SetActive(true);
            NormalShop.SetActive(false);
            IsAbyss = false;
        }
        else if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Reality && !IsAbyss)
        {
            AbyssShop.SetActive(false);
            NormalShop.SetActive(true);
            IsAbyss = true;
        }
    }
    //void ShopSwitch()
    //{
    //    if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss)
    //    {
    //        AbyssShop.SetActive(true);
    //        NormalShop.SetActive(false);
    //    }
    //    else if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Reality)
    //    {
    //        AbyssShop.SetActive(false);
    //        NormalShop.SetActive(true);
    //    }
    //}
}
