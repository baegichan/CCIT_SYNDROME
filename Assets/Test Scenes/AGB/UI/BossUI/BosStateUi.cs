using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BosStateUi : MonoBehaviour
{
    int maxHp;
    int hp;


    int shield;
    int maxShield;
    [SerializeField]
    private Image HpBar;

    [SerializeField]
    private Image ShieldBar;

    public int Hp
    {
        set
        {
            hp = value;
            HpBar.fillAmount = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);
        }
        get
        {
           return hp;
        }
    }
    public int MaxHp
    {
        set
        {
            maxHp = value;
            hp = maxHp;
        }
    }

    public int Shield
    {
        set
        {
            shield = value;
            ShieldBar.fillAmount = Convert.ToSingle(shield) / Convert.ToSingle(maxShield);

        }
        get
        {
            return shield;
        }
    }
    public int MaxShield
    {
        set
        {
            maxShield = value;
            shield = maxShield;
        }
    }
}
