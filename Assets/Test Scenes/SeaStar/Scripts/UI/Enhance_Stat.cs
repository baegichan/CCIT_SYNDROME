using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhance_Stat : MonoBehaviour
{
    public GameObject player;
    public Char_Parent py;

    [Header("체력 강화 UI")]
    public Text T_Level_Health;
    public Text T_Effect_Health;
    public Text T_Cost_Health;
    int[] Cost_Health = { 1, 2, 3, 4, 5};

    [Header("공격력 강화 UI")]
    public Text T_Level_Strength;
    public Text T_Effect_Strength;
    public Text T_Cost_Strength;
    int[] Cost_Strength = { 1, 2, 3, 4, 5 };

    [Header("스피드 강화 UI")]
    public Text T_Level_Speed;
    public Text T_Effect_Speed;
    public Text T_Cost_Speed;
    int[] Cost_Speed = { 1, 2, 3, 4, 5 };

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        T_Level_Health.text = "LV " + py.Enhance_Health;
        T_Level_Strength.text = "LV " + py.Enhance_Strength;
        T_Level_Speed.text = "LV " + py.Enhance_Speed;

        if (py.Enhance_Health < 5) { T_Cost_Health.text = Cost_Health[py.Enhance_Health].ToString(); }
        else { T_Cost_Health.text = "Max"; }

        if (py.Enhance_Strength < 5) { T_Cost_Strength.text = Cost_Strength[py.Enhance_Strength].ToString(); }
        else { T_Cost_Strength.text = "Max"; }

        if (py.Enhance_Speed < 5) { T_Cost_Speed.text = Cost_Speed[py.Enhance_Speed].ToString(); }
        else { T_Cost_Speed.text = "Max"; }

        T_Cost_Strength.text = Cost_Strength[py.Enhance_Strength].ToString();
        T_Cost_Speed.text = Cost_Speed[py.Enhance_Speed].ToString();
    }

    public void Enhance_Health()
    {
        if(py.P_Money > Cost_Health[py.Enhance_Health])
        {
            if (py.Enhance_Health < 5)
            {
                py.Enhance_Health++;
                py.UpdateStat();
                py.P_Money -= Cost_Health[py.Enhance_Health];
            }
        }
    }

    public void Enhance_Strength()
    {
        if (py.P_Money > Cost_Strength[py.Enhance_Strength])
        {
            if (py.Enhance_Strength < 5)
            {
                py.Enhance_Strength++;
                py.UpdateStat();
                py.P_Money -= Cost_Strength[py.Enhance_Strength];
            }
        }
    }

    public void Enhance_Speed()
    {
        if (py.P_Money > Cost_Speed[py.Enhance_Speed])
        {
            if (py.Enhance_Speed < 5)
            {
                py.Enhance_Speed++;
                py.UpdateStat();
                py.P_Money -= Cost_Speed[py.Enhance_Speed];
            }
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
