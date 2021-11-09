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
    public int Enhance;
    public int[] Enhance_Cost;
    public int AbPrice;
    public bool IsSelect;
    public Sprite AbIcon;
    public Sprite AbSprite;
    public bool IsUsing;

    public Ability(int AbCode, string AbName, int AbType, string AbGrade, int Enhance, int[] Enhance_Cost, int AbPrice, bool IsSelect,Sprite AbIcon, Sprite AbSprite, bool IsUsing)
    {
        this.AbCode = AbCode;
        this.AbName = AbName;
        this.AbType = AbType;
        this.AbGrade = AbGrade;
        this.Enhance = Enhance;
        this.Enhance_Cost = Enhance_Cost;
        this.AbPrice = AbPrice;
        this.IsSelect = IsSelect;
        this.AbIcon = AbIcon;
        this.AbSprite = AbSprite;
        this.IsUsing = IsUsing;
    }
}
