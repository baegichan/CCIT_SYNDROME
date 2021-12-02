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
        { " �̷��� �� �� ��� ������ �����ϰ� �־���." + "\r\n" + "Ȧ�α׷����ζ� �޼����� ���� �� �־� �����̱���.�Ƶ��." + "\r\n" + "������ �ȿ� �Ȱ��� ��Ĵ��� ���͵��� �ʸ� �븮�� ���� �Ŷ���."+ "\r\n"+"�׵��� ����ġ�� �׵��� �ɿ��� ���� �������ž�.",
        "���͵��� �ɿ��� ����� ������ ������ �� �־�.�ʵ� Q�� ���� ���� �� �ִܴ�." + "\r\n" + " �� ���� �ʿ�� ����.", " ������ ���͸� ������ ����ġ��" + "\r\n" + " �ʸ� ��ȭ��ų �� �ִ� ���� �Ȱ� �������� ���� �� ����",  "�ʰ� ������ �� �ֵ��� ������ �پ��� �������� ����ִ� �̱� ���� ���ڸ� �ξ���." + "\r\n" + " F�� ���� ������ �Ǵ� �������� ������.������ ���� �ɸ� ������ �ʸ� ��ȭ������ �� �ִ�.",
        "��¼�� �ʿ��� �ʿ��� �������� �Ĵ� ���� ������ ���� �� �� �ְڴ�." + "\r\n" + "������ ������ �ظ� ��ġ�� ��������.","���ʿ� �غ�� �����ҷ� ���ϴ� ��Ż�� ������ W�� ���� �̵��� �� �������� ����� ���....",

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
