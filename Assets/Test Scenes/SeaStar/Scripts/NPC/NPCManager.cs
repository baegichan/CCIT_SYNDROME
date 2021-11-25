using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public enum NPCType { SHOP, GACHA }
    public NPCType Type;
    public GameObject Shop;
    public GameObject Gacha;
    public GameObject AbyssShop;
    public GameObject NormalShop;

    void Awake()
    {
        if(Type == NPCType.SHOP) { Shop.SetActive(true); }
        else { Gacha.SetActive(true); }
    }

    void Update()
    {
        if(AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss)
        {
            if(NormalShop.activeSelf)
            {
                AbyssShop.SetActive(true);
                NormalShop.SetActive(false);
                Debug.Log("어비스 상점");
            }
        }
        else if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Reality)
        {
            if (AbyssShop.activeSelf)
            {
                AbyssShop.SetActive(false);
                NormalShop.SetActive(true);
                Debug.Log("노말 상점");
            }
        }
    }
}
