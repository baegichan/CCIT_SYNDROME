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
        { "ġ��- ġ��-" + "\r\n" + "��- ��- �̾��ϱ��� �Ƶ��" + "\r\n" + "���� �ڻ��� ������ �������� ���߾�...",
        "���� �ڻ縦 ���� �� �ִ� ����� �� �ۿ� ����..." + "\r\n" + "�̴�� �δٰ� ���谡 ���� �Ȱ��� �ڵ����� ���ž�...",
        "���� �ڻ翡�� �¼� �������� ���� �� �ִ�"+ "\r\n"+"�̱��踦 ���� ������ ��ġ�� �ξ���...",
       "���� �Ȱ� �������� �����ϸ�"+ "\r\n"+"�̱��踦 ����� �� �ִܴ�...",
       "���� �Ȱ� �������� ���͸� óġ�ϸ� ���� �� �־�...",
        "���� �Ȱ��� �� ������� �ñ��ϰ�����"+ "\r\n"+"���� �� �����ֱ⿣ �ð��� ����..."+"\r\n"+"ġ��- ġ��...",
        "�ϴ� ���� �ϳ��� �ٲ�.."+"\r\n"+"�����ʿ� ���� ���Ⱑ �����ž�..."
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
