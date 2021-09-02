using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability_
{
    public int AbCode;
    public string AbName;
    public int AbType;
    public string AbGrade;
    public int AbPrice;
    public bool IsBuy;
    public bool IsSelect;
    public Sprite AbSprite;
    public bool IsUsing;

    public Ability_(int AbCode, string AbName, int AbType, string AbGrade, int AbPrice, bool IsBuy, bool IsSelect, Sprite AbSprite, bool IsUsing)
    {
        this.AbCode = AbCode;
        this.AbName = AbName;
        this.AbType = AbType;
        this.AbGrade = AbGrade;
        this.AbPrice = AbPrice;
        this.IsBuy = IsBuy;
        this.IsSelect = IsSelect;
        this.AbSprite = AbSprite;
        this.IsUsing = IsUsing;
    }
}
