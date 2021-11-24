using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public int AbCode;
    public string AbName;
    public enum ABTYPE { Active, Passive, APDrink, HPDrink }
    public ABTYPE AbType;
    public enum ABGRADE { Common, Rare, Unique, Drink}
    public ABGRADE AbGrade;
    public int Enhance;
    public int[] Enhance_Cost;
    public int AbPrice;
    public bool IsSelect;
    public Sprite AbIcon;
    public Sprite AbSprite;
    public Sprite ResultIcon;
    public Sprite icon;
    public bool IsUsing;
    [TextArea]
    public string AbExplan;
    
    public Ability(int AbCode, string AbName, ABTYPE AbType, ABGRADE AbGrade, int Enhance, int[] Enhance_Cost, int AbPrice, bool IsSelect,Sprite AbIcon, Sprite AbSprite, Sprite ResultIcon, Sprite icon, bool IsUsing, string AbExplan)
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
        this.ResultIcon = ResultIcon;
        this.icon = icon;
        this.IsUsing = IsUsing;
        this.AbExplan = AbExplan;
    }
}
