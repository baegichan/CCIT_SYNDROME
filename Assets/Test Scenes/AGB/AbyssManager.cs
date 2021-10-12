using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbyssManager : MonoBehaviour
{
    #region 변수


    [Header("InputKey")]
    public char abyssKey = 'p';

    [Header("Abyss Variable")]
    //검은 안개
    private int blackfog = 0;
    private int maxAbyssGage = 100;
    //어비스 게이지
    private int abyssGage = 100;
    //hp 게이지
    public int hpGage = 100;

    //어비스 게이지 줄어드는 값

    public int abyssGageConsumption = 5;

    //어비스 게이지 줄어드는 시간
    public float abyssConsumptionTime = 0.5f;

    //심연 유지 여부
    private bool isAbyssStay = false;
    //심연 코루틴 끝났는지 여부
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

        StateText.text = "메인";

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
                StateText.text = "심연";
                AbyssMonsterSpawn();
                StartCoroutine("GoAbyss");
            }

            else
            {
                abyssState = AbyssState.Reality;
                StateText.text = "메인";
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
                //사망 넣기

                //임시
                StateText.text = "메인";
                lifeText.text = "사망";
                ///


                isAbyssStay = false;
                abyssState = AbyssState.Reality;
                break;
            }


            #region 임시 삭제요망

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
        StateText.text = "메인";
        isAbyssEnd = false;
    }

    //몬스터 처치시 or 소비시 함수

    #region 어비스 관련 변수 getset 
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


    #region 심연 몬스터 관련 함수

    void AbyssMonsterSpawn()
    {
        // 어비스 진입시 실행
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
        //들어오면 hp값 공격력 더하고 스택에 집어넣기

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
        //임시
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
