using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class capsule : MonoBehaviour
{
    
    public int item_code;
    public GameObject Player;
    public GameObject[] gachaimages;
    public UImanager uimanager;
    public GameObject fill;
    int[] typeindexer = new int[2];
    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        uimanager = GameObject.Find("Gamemanager").GetComponent<UImanager>();
        item_code = Random.Range(0, 10);
        switch(item_code)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
        }
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
     if(uimanager != null)
        {
            if (Input.GetKeyDown(settingmanager.GM.comunication)) 
            {
              // GameObject drug=Instantiate(fill, this.gameObject.transform);
                switch (item_code)
                {
                    case 0:
                        uimanager.setskillui(0);
                        uimanager.ActiveSkill.GetComponent<Image>().sprite=uimanager.Activeskillsprites[0];
                        uimanager.ActiveSkill.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                        uimanager.ActiveSkillcool.GetComponent<Image>().sprite = uimanager.Activeskillcoolsprites[0];
                        uimanager.ActiveSkillcool.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                        //´Á´ë
                        break;
                    case 1:
                        uimanager.setskillui(1);
                        uimanager.ActiveSkill.GetComponent<Image>().sprite = uimanager.Activeskillsprites[1];
                        uimanager.ActiveSkill.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                        //ÆøÅº

                        break;
                    case 2:
                        uimanager.setskillui(1);
                        uimanager.PassiveSkill.GetComponent<Image>().sprite = uimanager.Passiveskillsprites[0];
                        uimanager.PassiveSkillcool.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                        //µ¹Áø
                        break;
                    case 3:
                        uimanager.setskillui(1);
                        uimanager.PassiveSkill.GetComponent<Image>().sprite = uimanager.Passiveskillsprites[1];
                        uimanager.PassiveSkillcool.GetComponent<Image>().color = new Color(255, 255, 255, 255);

                        //³¯°³
                        break;
                    case 4:
                    
                        break;
                    case 5:
                      
                        break;
                    case 6:
                
                    //  break;
                    case 7:
                        break;
                    case 8:
                   
                        //Èú
                        break;
                    case 9:
                        //item2;
                        break;
                }
                // drug.GetComponent<SpriteRenderer>().sprite = setskillui.
                // drug.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite=

            }
        }
    }
}
