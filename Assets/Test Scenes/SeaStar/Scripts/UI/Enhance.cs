using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enhance : MonoBehaviour
{
    public GameObject[] Lock;
    public List<Ability> ability;
    public GameObject player;

    public Ability SelectAbility;

    string Active_Name, Active_Grade, Active_Effect;
    string Passive_Name, Passive_Grade, Passive_Effect;


    [Header("Active_Ability")]
    public Text Active_Name_Text;
    public Text Active_Grade_Text;
    public Text Active_Effect_Text;
    public RawImage Active_Image;

    [Header("Passive_Ability")]
    public Text Passive_Name_Text;
    public Text Passive_Grade_Text;
    public Text Passive_Effect_Text;
    public RawImage Passive_Image;

    void Update()
    {
        SwitchAbility();
        SettingAbility();
    }

    void SettingAbility()
    {
        if (ability[0].AbSprite != null)
        {
            Lock[0].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Lock[0].transform.GetChild(0).gameObject.SetActive(false);
            Active_Name = ability[0].AbName;
            Active_Grade = ability[0].Enhance + "  ->  " + (ability[0].Enhance + 1);
            EH_Ability();

            Active_Name_Text.text = Active_Name;
            Active_Grade_Text.text = Active_Grade;
            Active_Effect_Text.text = Active_Effect;
            Active_Image.GetComponent<RawImage>().texture = ability[0].AbIcon.texture;
        }
        if (ability[1].AbSprite != null)
        {
            Lock[1].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            Lock[1].transform.GetChild(0).gameObject.SetActive(false);
            Passive_Name = ability[1].AbName;
            Passive_Grade = ability[1].Enhance + "  ->  " + (ability[1].Enhance + 1);
            EH_Passive();

            Passive_Name_Text.text = Passive_Name;
            Passive_Grade_Text.text = Passive_Grade;
            Passive_Effect_Text.text = Passive_Effect;
            Passive_Image.GetComponent<RawImage>().texture = ability[1].AbIcon.texture;
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void selectAbility()
    {
        switch(gameObject.name)
        {
            case "Lock_0":
                SelectAbility = ability[0];
                break;
            case "Lock_1":
                SelectAbility = ability[1];
                break;
        }
    }

    delegate void enhance();
    enhance EH_Ability;
    enhance EH_Passive;
    public AbilityManager am;

    void SwitchAbility()
    {
        switch(ability[0].AbCode)
        {
            case 0:
                EH_Ability = Enhance_Werewolf;
                Debug.Log("0");
                break;
            case 1:
                EH_Ability = Enhance_Pharaoh;
                Debug.Log("1");
                break;
            case 2:
                EH_Ability = Enhance_Boom;
                Debug.Log("2");
                break;
            case 3:
                EH_Ability = Enhance_StoneMan;
                Debug.Log("4");
                break;
        }

        switch (ability[1].AbCode)
        {
            case 6:
                EH_Passive = Enhance_Doublejump;
                Debug.Log("3");
                break;
        }
    }

    TestTest tt;

    public void EnhaceAbility()
    {
        //플레이어의 재화가 충분하다면
        //재화 -= SelectAbility.EnhanceCost[SelectAbility.Enhance];
        //SelectAbility.Enhance++;
        ability[0].Enhance++;
        //돈 부족하면 재화 글자 색 뻘겋게
    }

    void Enhance_Boom()
    {
        tt = player.GetComponent<TestTest>();
        Active_Effect = "";
        Active_Effect += "공격력: " + am.BoomAP[tt.ActiveAbility.Enhance] + " -> " + am.BoomAP[tt.ActiveAbility.Enhance + 1] + "\n";
    }

    void Enhance_StoneMan()
    {
        tt = player.GetComponent<TestTest>();
        Active_Effect = "";
        Active_Effect += "방어력: " + tt.SelectChar.GetComponent<Char_RockMan>().DP[tt.ActiveAbility.Enhance] + " -> " + tt.SelectChar.GetComponent<Char_RockMan>().DP[tt.ActiveAbility.Enhance + 1] + "\n";
        player.GetComponent<TestTest>().UpdateStat();
    }

    void Enhance_Werewolf()
    {
        tt = player.GetComponent<TestTest>();
        Active_Effect = "";
        Active_Effect += "공격력: " + am.WolfAP[tt.ActiveAbility.Enhance] + " -> " + am.WolfAP[tt.ActiveAbility.Enhance + 1] + "\n";
        Active_Effect += "체력: " + tt.SelectChar.GetComponent<Char_Wolf>().HP[tt.ActiveAbility.Enhance] + " -> " + tt.SelectChar.GetComponent<Char_Wolf>().HP[tt.ActiveAbility.Enhance + 1] + "\n";
        player.GetComponent<TestTest>().UpdateStat();
    }

    void Enhance_Pharaoh()
    {
        tt = player.GetComponent<TestTest>();
        Active_Effect = "";
        Active_Effect += "공격력: " + am.ParaoAP[tt.ActiveAbility.Enhance] + " -> " + am.ParaoAP[tt.ActiveAbility.Enhance + 1] + "\n";
    }

    void Enhance_Doublejump()
    {

    }
}
