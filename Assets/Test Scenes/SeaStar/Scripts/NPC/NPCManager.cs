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

    }
}
