using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhance_Stat : MonoBehaviour
{
    public GameObject player;
    public Char_Parent py;

    [Header("ü�� ��ȭ UI")]
    public Image Level_Health;
    public Sprite[] Level_Health_Image;
    public Text T_Cost_Health;
    int[] Cost_Health = { 100, 150, 200, 250, 300};
    public GameObject Health_Button;

    [Header("���ݷ� ��ȭ UI")]
    public Image Level_Strength;
    public Sprite[] Level_Strength_Image;
    public Text T_Cost_Strength;
    int[] Cost_Strength = { 100, 200, 300, 400, 500 };
    public GameObject Strength_Button;

    [Header("���ǵ� ��ȭ UI")]
    public Image Level_Speed;
    public Sprite[] Level_Speed_Image;
    public Text T_Cost_Speed;
    int[] Cost_Speed = { 50, 100, 150, 200, 250 };
    public GameObject Speed_Button;

    public void UpdateText()
    {
        Level_Health.sprite = Level_Health_Image[py.Enhance_Health];
        Level_Strength.sprite = Level_Strength_Image[py.Enhance_Strength];
        Level_Speed.sprite = Level_Speed_Image[py.Enhance_Speed];

        if (py.Enhance_Health < 6)
            T_Cost_Health.text = Cost_Health[py.Enhance_Health].ToString();
        else
        {
            T_Cost_Health.text = "Max";
            Health_Button.GetComponent<Image>().sprite = Health_Button.GetComponent<Button>().spriteState.disabledSprite;
            Health_Button.GetComponent<Button>().enabled = false;
        }

        if (py.Enhance_Strength < 6)
            T_Cost_Strength.text = Cost_Strength[py.Enhance_Strength].ToString();
        else
        {
            T_Cost_Strength.text = "Max";
            Strength_Button.GetComponent<Image>().sprite = Strength_Button.GetComponent<Button>().spriteState.disabledSprite;
            Strength_Button.GetComponent<Button>().enabled = false;
        }

        if (py.Enhance_Speed < 6)
            T_Cost_Speed.text = Cost_Speed[py.Enhance_Speed].ToString();
        else
        {
            T_Cost_Speed.text = "Max";
            Speed_Button.GetComponent<Image>().sprite = Speed_Button.GetComponent<Button>().spriteState.disabledSprite;
            Speed_Button.GetComponent<Button>().enabled = false;
        }
    }

    public void Enhance_Health()
    {
        if (py.Enhance_Health < 6)
        {
            if (AbyssManager.abyss.Darkfog > Cost_Health[py.Enhance_Health])
            {
                AbyssManager.abyss.Darkfog -= Cost_Health[py.Enhance_Health];
                py.Enhance_Health++;
                py.Hp_Current += 10;
                py.UpdateStat();
                py.Save_StateEnhance();
                UpdateText();
            }
        }
    }
    
    public void Enhance_Strength()
    {
        if (py.Enhance_Strength < 6)
        {
            if (AbyssManager.abyss.Darkfog > Cost_Strength[py.Enhance_Strength])
            {
                AbyssManager.abyss.Darkfog -= Cost_Strength[py.Enhance_Strength];
                py.Enhance_Strength++;
                py.UpdateStat();
                py.Save_StateEnhance();
                UpdateText();
            }
        }
    }
    
    public void Enhance_Speed()
    {
        if (py.Enhance_Speed < 6)
        {
            if (AbyssManager.abyss.Darkfog > Cost_Speed[py.Enhance_Speed])
            {
                AbyssManager.abyss.Darkfog -= Cost_Speed[py.Enhance_Speed];
                py.Enhance_Speed++;
                py.UpdateStat();
                py.Save_StateEnhance();
                UpdateText();
            }
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
}
