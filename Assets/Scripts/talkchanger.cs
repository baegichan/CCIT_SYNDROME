using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class talkchanger : MonoBehaviour
{
    public GameObject textcase;
    public bool tutorialstart=false;
    private bool tutorialend = false;
    public GameObject NPC;
    public GameObject text;
    private float alpha = 1;
    public float timer;
    private int text_counter = 0;
    private int intext_counter = 0;
    public string[] text1 =
        { " 이렇게 될 줄 어느 정도는 예상하고 있었어." + "\r\n" + "홀로그램으로라도 메세지를 전할 수 있어 다행이구나.아들아." + "\r\n" + "연구소 안에 안개에 잠식당한 몬스터들이 너를 노리고 있을 거란다."+ "\r\n"+"그들을 물리치면 그들은 심연의 경계로 도망갈거야.",
        "몬스터들은 심연의 경계라는 곳으로 도망갈 수 있어.너도 Q를 눌러 따라갈 수 있단다." + "\r\n" + " 꼭 쫓을 필요는 없어.", " 하지만 몬스터를 완전히 물리치면" + "\r\n" + " 너를 강화시킬 수 있는 검은 안개 에너지를 얻을 수 있지",  "너가 대적할 수 있도록 곳곳에 다양한 아이템이 들어있는 뽑기 기계와 상자를 두었어." + "\r\n" + " F를 눌러 도움이 되는 아이템을 얻으렴.나에게 말을 걸면 언제나 너를 강화시켜줄 수 있다.",
        "어쩌면 너에게 필요한 아이템을 파는 상점 주인을 만날 수 도 있겠다." + "\r\n" + "무서워 하지만 해를 끼치진 않을꺼야.","위쪽에 준비된 연구소로 통하는 포탈이 있으니 W를 눌러 이동할 수 있을꺼야 행운을 빈다....",

    };


  
    public void tutorialstarter()
    {
        if (tutorialstart != true)
        {
            textcase.SetActive(true);
            tutorialstart = true;
            StartCoroutine(textchanger());
        }
    }
    private void Update()
    {
        if (intext_counter == text1[text_counter].Length&&Input.GetKey(KeyCode.F)&& text_counter!=6)
        {
            Debug.Log(text_counter+"text");
            text_counter++;
            intext_counter = 0;
            StartCoroutine(textchanger());
        }
        else if(text_counter==5 && Input.GetKey(KeyCode.F))
        {
            PlayerPrefs.SetFloat("Tuto", 1);
            textcase.SetActive(false);
            tutorialend = true;
        }
        if(tutorialend)
        {
            tutorialending();
        }

    }
    public IEnumerator textchanger()
    {

        yield return new WaitForSeconds(timer);
       if(intext_counter<text1[text_counter].Length)
        {
            text.GetComponent<Text>().text = text1[text_counter].Substring(0, intext_counter);
            intext_counter++;
            StartCoroutine(textchanger());
        }
    
    }
    public void tutorialending()
    {
        NPC.GetComponent<SpriteRenderer>().color = new Color(1, 1,1, alpha - 0.02f);
        alpha -= 0.02f;
        if(alpha<0)
        {
            Destroy(NPC);
        }
    }
}
