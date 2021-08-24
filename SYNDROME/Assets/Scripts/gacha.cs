using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemGrade { U, R, N }


[System.Serializable]
     public class Item
   {
    public string ItemName;
    public Sprite ItemImage;
    public ItemGrade itemGrade;
    public int weight;
    public Item(Item item)
    {
        this.ItemName = item.ItemName;
        this.ItemImage = item.ItemImage;
        this.itemGrade = item.itemGrade;
        this.weight = item.weight;
    }
   }

