using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    public List<GameObject> SellItem;

    AbilityItem Item_0, Item_1, Item_2;

    void Start()
    {
        Item_0 = SellItem[0].GetComponent<AbilityItem>();
        Item_1 = SellItem[1].GetComponent<AbilityItem>();
        Item_2 = SellItem[2].GetComponent<AbilityItem>();

        ItemOverlapCheck();
    }

    void ChangeItem(GameObject Item)
    {
        Item.GetComponent<AbilityItem>().AlyakList();
    }

    void ItemOverlapCheck()
    {
        if (Item_0.ThisCode == Item_1.ThisCode)
        {
            for(int i = 0; i < Item_1.AbList.Count; i++)
            {
                if(Item_1.AbList[i].AbCode == Item_0.ThisCode) { Item_1.AbList.RemoveAt(i); break; }
            }
            ChangeItem(SellItem[1]);
            ItemOverlapCheck();
        }
        else if (Item_1.ThisCode == Item_2.ThisCode)
        {
            for (int i = 0; i < Item_2.AbList.Count; i++)
            {
                if (Item_2.AbList[i].AbCode == Item_1.ThisCode) { Item_2.AbList.RemoveAt(i); break; }
            }
            ChangeItem(SellItem[2]);
            ItemOverlapCheck();
        }
        else if (Item_0.ThisCode == Item_2.ThisCode)
        {
            for (int i = 0; i < Item_2.AbList.Count; i++)
            {
                if (Item_2.AbList[i].AbCode == Item_0.ThisCode) { Item_2.AbList.RemoveAt(i); break; }
            }
            ChangeItem(SellItem[2]);
            ItemOverlapCheck();
        }
    }
}
