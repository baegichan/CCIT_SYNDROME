using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbyssManager : MonoBehaviour
{
    #region ����


    [Header("InputKey")]
    public char abyssKey = 'p';

    [Header("Abyss Variable")]
    //���� �Ȱ�
    private int blackfog = 0;
    private int maxAbyssGage = 100;
    //��� ������
    private int abyssGage = 100;
    //hp ������
    public int hpGage = 100;

    //��� ������ �پ��� ��

    public int abyssGageConsumption = 5;

    //��� ������ �پ��� �ð�
    public float abyssConsumptionTime = 0.5f;

    //�ɿ� ���� ����
    private bool isAbyssStay = false;
    //�ɿ� �ڷ�ƾ �������� ����
    private bool isAbyssEnd = false;

    private enum AbyssState { Abyss, Reality };

    [Header("Monster")]
    public GameObject Monster1;








    [Header("Temporary")]

    public Text hpText;
    public Text AbyssGageText;
    public Text StateText;
    public Text lifeText;
    public Text fogText;

    AbyssState abyssState = new AbyssState();

    string[] monsterName = new string[50];
    Transform[] monsterpos = new Transform[50];
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        abyssState = AbyssState.Reality;
        hpText.text = Convert.ToString(hpGage);
        AbyssGageText.text = Convert.ToString(AbyssGage);

        StateText.text = "����";

        fogText.text = Convert.ToString(Blackfog);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isAbyssStay == false && isAbyssEnd == false)
            {
                isAbyssStay = true;
                abyssState = AbyssState.Abyss;
                StateText.text = "�ɿ�";
                AbyssMonsterSpawn();
                StartCoroutine("GoAbyss");
            }

            else
            {
                abyssState = AbyssState.Reality;
                StateText.text = "����";
                isAbyssStay = false;
            }

        }
    }

    IEnumerator GoAbyss()
    {
        isAbyssEnd = true;
        while (isAbyssStay)
        {



            if (abyssGage > 0)
                AbyssGage -= abyssGageConsumption;
            else
                hpGage -= abyssGageConsumption;

            if (abyssGage < 0)
                abyssGage = 0;
            if (hpGage < 0)
                hpGage = 0;

            if (hpGage <= 0)
            {
                //��� �ֱ�

                //�ӽ�
                StateText.text = "����";
                lifeText.text = "���";
                ///


                isAbyssStay = false;
                abyssState = AbyssState.Reality;
                break;
            }


            #region �ӽ� �������

            hpText.text = Convert.ToString(hpGage);
            AbyssGageText.text = Convert.ToString(abyssGage);
            if (isAbyssStay == false)
            {
                break;
            }

            #endregion

            yield return new WaitForSeconds(abyssConsumptionTime);


        }
        abyssState = AbyssState.Reality;
        StateText.text = "����";
        isAbyssEnd = false;
    }

    //���� óġ�� or �Һ�� �Լ�

    #region ��� ���� ���� getset 
    public int Blackfog
    {
        get
        {
            return blackfog;
        }

        set
        {
            blackfog = value;
        }
    }

    public int AbyssGage
    {
        get
        {
            return abyssGage;
        }

        set
        {
            abyssGage = value;
        }
    }


    #endregion


    #region �ɿ� ���� ���� �Լ�

    void AbyssMonsterSpawn()
    {
        // ��� ���Խ� ����
        for (int i = 0; i < monsterName.Length; i++)
        {
            if (monsterName[i] == null)
            {
                break;
            }
            else
            {
                if (monsterName[i].Contains("Circle"))
                {
                    Instantiate(Monster1, monsterpos[i]);
                }
            }



        }

    }

    public void AbyssMonsterAdd(string name, Transform pos)
    {
        //������ hp�� ���ݷ� ���ϰ� ���ÿ� ����ֱ�

        for (int i = 0; i < monsterName.Length; i++)
        {
            if (monsterName[i] == null)
            {
                monsterName[i] = name;
                monsterpos[i] = pos;
                break;
            }


        }



    }



    //public void AbyssMonseterKill(int fog, int abyss)
    public void AbyssMonseterKill()
    {

        //AbyssGage += abyss;
        //Blackfog += fog;
        int dd;
        //�ӽ�
        if (abyssState == AbyssState.Abyss)
        {

            dd = AbyssGage + UnityEngine.Random.Range(5, 20);
            if (dd > maxAbyssGage + 1)
            {
                AbyssGage = maxAbyssGage;
            }
            else
            {
                AbyssGage = dd;
            }
            AbyssGageText.text = Convert.ToString(abyssGage);

            Blackfog += UnityEngine.Random.Range(1, 10);
            fogText.text = Convert.ToString(Blackfog);
        }

    }

    #endregion
}
