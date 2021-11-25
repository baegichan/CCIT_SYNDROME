using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public enum NPCType { SHOP, GACHA }
    public NPCType Type;
    public GameObject Shop;
    public GameObject Gacha;

    void Awake()
    {
        if(Type == NPCType.SHOP) { Shop.SetActive(true); }
        else { Gacha.SetActive(true); }
    }
}
