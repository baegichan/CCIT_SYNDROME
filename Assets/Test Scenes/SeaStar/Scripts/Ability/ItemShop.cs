using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public List<GameObject> SellItem;

    void Update()
    {
        if(SellItem[0].GetComponent<AbilityItem>().ThisCode == SellItem[1].GetComponent<AbilityItem>().ThisCode)
        {
            Debug.Log("0 : " + SellItem[1].name);
            ChangeItem(SellItem[1]);
        }
        else if (SellItem[1].GetComponent<AbilityItem>().ThisCode == SellItem[2].GetComponent<AbilityItem>().ThisCode || SellItem[0].GetComponent<AbilityItem>().ThisCode == SellItem[2].GetComponent<AbilityItem>().ThisCode)
        {
            Debug.Log("1 : " + SellItem[2].name);
            ChangeItem(SellItem[2]);
        }
    }

    void ChangeItem(GameObject Item)
    {
        Item.GetComponent<AbilityItem>().AlyakList();
        Item.GetComponent<AbilityItem>().dc();
    }
}
