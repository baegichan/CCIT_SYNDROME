using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AbyssManager : MonoBehaviour
{
    #region ����


    [Header("InputKey")]
    public char abyssKey = 'p';

    [Header("Abyss Variable")]
    //���� �Ȱ�
    private int darkfog = 0;
    public int maxAbyssGage = 100;
    //��� ������
    public int abyssGage = 100;
    //hp ������
    public int hpGage = 30;

    //��� ������ �پ��� ��

    public int abyssGageConsumption = 5;

    //��� ������ �پ��� �ð�
    public float abyssConsumptionTime = 0.5f;

    //�ɿ� ���� ����
    private bool isAbyssStay = false;
    //�ɿ� �ڷ�ƾ �������� ����
    private bool isAbyssEnd = true;

    public enum AbyssState { Reality, Abyss };


    [Header("MonsterBox")]
    public GameObject RealBox;
    public GameObject AbyssBox;

    [Header("Monster")]
    public GameObject Monster1;








    [Header("Temporary")]

    public Text hpText;
    public Text AbyssGageText;
    public Text StateText;
    public Text lifeText;
    public Text fogText;

    public AbyssState abyssState = new AbyssState();
    //����

    string[] monsterName = new string[50];
    Vector3[] monsterPos = new Vector3[50];

    #endregion

    private void Awake()
    {
        RealBox = GameObject.Find("RealMonsterBox").transform.gameObject;
        AbyssBox = GameObject.Find("AbyssMonsterBox").transform.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        AbyssBox.SetActive(false);
        abyssState = AbyssState.Reality;
        hpText.text = Convert.ToString(hpGage);
        AbyssGageText.text = Convert.ToString(AbyssGage);

        StateText.text = "����";

        fogText.text = Convert.ToString(Darkfog);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isAbyssStay == false && isAbyssEnd == true)
                GoAbyss();
            else
                GoReal();

        }
    }

    IEnumerator AbyssResource()
    {
        isAbyssEnd = false;
        int exint;
        while (isAbyssStay)
        {
            exint = abyssGage - abyssGageConsumption;
            if (exint > 0)
                AbyssGage -= abyssGageConsumption;
            else
            {
                AbyssGage = 0;
                hpGage -= abyssGageConsumption;
            }

            if (hpGage <= 0)
            {
                if (hpGage < 0)
                    hpGage = 0;
                //�ӽ�
                hpText.text = Convert.ToString(hpGage);
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
        isAbyssEnd = true;
    }


    #region �ɿ��� ��� ���� �Լ�

    /// <summary>
    /// �ɿ� �̵� �Լ�
    /// </summary>
    void GoAbyss()
    {
        isAbyssStay = true;
        abyssState = AbyssState.Abyss; // enum�� ���°� �Է�
        StateText.text = "�ɿ�"; // �ӽ�
        AbyssMonsterSpawn(); // �ɿ� ���Ͱ� ������� ����
        AbyssBox.SetActive(true);// �ɿ� ���� �׷�  Ȱ��ȭ
        RealBox.SetActive(false);// ���� ���� �׷� ��Ȱ��ȭ
        StartCoroutine("AbyssResource"); // �ɿ� ���ҽ� �Ҹ� �ڷ�ƾ ����
    }

    /// <summary>
    /// ���� �̵� �Լ�
    /// </summary>
    void GoReal()
    {
        AbyssBox.SetActive(false);// ���� ���� �׷�  Ȱ��ȭ
        RealBox.SetActive(true);  // �ɿ� ���� �׷� ��Ȱ��ȭ
        abyssState = AbyssState.Reality;
        StateText.text = "����";
        isAbyssStay = false;
    }

    #endregion
    //���� óġ�� or �Һ�� �Լ�

    #region ��� ���� ���� getset 
    public int Darkfog
    {
        get
        {
            return darkfog;
        }

        set
        {
            darkfog = value;
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
        for (int i = 0; i < monsterName.Length; i++)
        {
            if (monsterName[i] == null)
            {
                monsterName = new string[50];
                break;
            }
            else
            {
                if (monsterName[i].Contains("Monster"))
                {
                    var d = Instantiate(Monster1, monsterPos[i], Quaternion.identity);
                    d.transform.SetParent(AbyssBox.transform, false);
                }
            }
        }
    }

    public void AbyssMonsterAdd(string name, Vector3 pos)
    {
        //������ hp�� ���ݷ� ���ϰ� ���ÿ� ����ֱ�
        for (int i = 0; i < monsterName.Length; i++)
        {
            if (monsterName[i] == null)
            {
                monsterName[i] = name;
                monsterPos[i] = pos;
                break;
            }
        }
    }



    //public void AbyssMonseterKill(int fog, int abyss)
    /// <summary>
    /// ��� �Ȱ� ������ ȹ��
    /// </summary>
    /// 
    public void GetDarkFog(int gage)
    {
        Darkfog += gage;
        fogText.text = Convert.ToString(Darkfog);
    }

    /// <summary>
    /// �ɿ������� ����
    /// </summary>
    /// <param Gage="int�� ������ �߰��� �־��ٰ�"></param>
    public void GetAbyssGage(int gage)
    {
        int ex = abyssGage + gage;
        if (ex > maxAbyssGage)
            abyssGage = maxAbyssGage;
        else
            abyssGage += gage;
        AbyssGageText.text = Convert.ToString(abyssGage);


    }




    #endregion
}
