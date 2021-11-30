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
        { "치직- 치직-" + "\r\n" + "아- 아- 미안하구나 아들아" + "\r\n" + "동료 박사의 세력이 생각보다 강했어...",
        "동료 박사를 막을 수 있는 사람은 너 밖에 없어..." + "\r\n" + "이대로 두다간 세계가 검은 안개로 뒤덮히고 말거야...",
        "동료 박사에게 맞설 아이템을 얻을 수 있는"+ "\r\n"+"뽑기기계를 도시 곳곳에 설치해 두었어...",
       "검은 안개 에너지를 주입하면"+ "\r\n"+"뽑기기계를 사용할 수 있단다...",
       "검은 안개 에너지는 몬스터를 처치하면 얻을 수 있어...",
        "검은 안개가 왜 생겼는지 궁금하겠지만"+ "\r\n"+"지금 다 말해주기엔 시간이 없어..."+"\r\n"+"치직- 치직...",
        "일단 무기 하나를 줄께.."+"\r\n"+"오른쪽에 보면 무기가 있을거야..."
    };
    private void Start()
    {
      
     

    }
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
        if (intext_counter == text1[text_counter].Length&&Input.GetKey(KeyCode.R)&& text_counter!=7)
        {
            Debug.Log(text_counter+"text");
            text_counter++;
            intext_counter = 0;
        //    StartCoroutine(textchanger());
        }
        else if(text_counter==7 && Input.GetKey(KeyCode.R))
        {
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
