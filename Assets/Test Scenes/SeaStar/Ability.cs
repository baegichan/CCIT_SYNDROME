using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public int AbCode;
    public string AbName;
    public int AbPrice;
    public Sprite AbSprite;
    public bool IsUsing;

    public Ability(int AbCode, string AbName, int AbPrice, Sprite AbSprite, bool IsUsing)
    {
        this.AbCode = AbCode;
        this.AbName = AbName;
        this.AbPrice = AbPrice;
        this.AbSprite = AbSprite;
        this.IsUsing = IsUsing;
    }
}
