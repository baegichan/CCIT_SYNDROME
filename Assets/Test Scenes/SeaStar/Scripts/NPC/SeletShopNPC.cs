using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeletShopNPC : MonoBehaviour
{
    public GameObject AbyssShop;
    public GameObject NormalShop;

    private void OnEnable()
    {
        if(AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss) { }
    }
}
