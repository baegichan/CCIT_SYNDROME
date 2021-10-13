using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
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
    public float AbWeight;

    public Ability(int AbCode, string AbName, int AbType, string AbGrade, int AbPrice, bool IsBuy, bool IsSelect, Sprite AbSprite, bool IsUsing, float AbWeight)
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
        this.AbWeight = AbWeight;
    }
}
