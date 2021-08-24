using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHPUI : MonoBehaviour
{
    //private ·Î ¹Ù²ã¾ßµÊ
    public int playerhp;
    private GameObject[] hpsprites =new GameObject[13];
    public GameObject[] hpbase2;
    public int potion=50;
    public Text potionui;
    public enum HPbase
    {
        HP100,
        HP130
    };

    public HPbase hpbase;
    // Start is called before the first frame update
    void Start()
    {
        
        playerhp = PlayerPrefs.GetInt("PlayerHP");
        change_hpbase(hpbase);
    }
    private void Update()
    {
        //Å×½ºÆ®¿ë
        hppotion();



    }
    public void hppotion()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            potion -= 1;
            damaged(-50);
            potionui.text = potion.ToString();
            this.gameObject.GetComponent<Damagetextspawn>().heal(50);
        }
    }
    /// <summary>
    /// 1=100base 2=130base
    /// </summary>
    /// <param name="mode"></param>
   void change_hpmode(int mode)
    {
        switch(mode)
        {
            case 1:
                hpbase = HPbase.HP100;
                change_hpbase(hpbase);
                break;
            case 2:
                hpbase = HPbase.HP130;
                change_hpbase(hpbase);
                break;
        }
    }
    public void damaged(int dmg)
    {
        playerhp= Mathf.Clamp(playerhp -=dmg,0,130);
        for (int j = 0; j < playerhp / 13; j++)
        {
            hpsprites[j].SetActive(true);
        }
        for (int k = playerhp / 13; k < hpbase2[1].transform.childCount; k++)
        {
            hpsprites[k].SetActive(false);
        }

    }
    void change_hpbase(HPbase HPbase)
    {
        playerhp = PlayerPrefs.GetInt("PlayerHP");
        switch (HPbase)
        {
            case HPbase.HP100:
                hpbase2[0].SetActive(true);
                hpbase2[1].SetActive(false);
                for (int i = 0; i< hpbase2[0].transform.childCount;i++)
                {
                    
                    hpsprites[i] = hpbase2[0].transform.GetChild(i).gameObject;


                }
                for(int j =0; j<playerhp/10;j++)
                {
                    hpsprites[j].SetActive(true);
                }
                for (int k = playerhp / 10; k < hpbase2[0].transform.childCount; k++)
                {
                    hpsprites[k].SetActive(false);
                }
                break;
            case HPbase.HP130:
                hpbase2[1].SetActive(true);
                hpbase2[0].SetActive(false);
                for (int i = 0; i < hpbase2[1].transform.childCount; i++)
                {
                    hpsprites[i] = hpbase2[1].transform.GetChild(i).gameObject;


                }
                for (int j = 0; j < playerhp /10; j++)
                {
                    hpsprites[j].SetActive(true);
                }
                for(int k= playerhp / 10; k< hpbase2[1].transform.childCount;k++)
                {
                    hpsprites[k].SetActive(false);
                }
                break;
        
        }
    }
 
}
