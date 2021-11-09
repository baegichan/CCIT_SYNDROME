using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AbyssManager : MonoBehaviour
{
    #region 변수


    [Header("InputKey")]
    public char abyssKey = 'q';

    [Header("Abyss Variable")]
    //검은 안개
    private int darkfog = 0;
    public int maxAbyssGage = 100;
    //어비스 게이지
    public int abyssGage = 100;
    //hp 게이지
    public int hpGage = 30;

    //어비스 게이지 줄어드는 값

    public int abyssGageConsumption = 1;

    //어비스 게이지 줄어드는 시간
    public float abyssConsumptionTime = 1f;

    //심연 유지 여부
    private bool isAbyssStay = false;
    //심연 코루틴 끝났는지 여부
    private bool isAbyssEnd = true;

    public enum AbyssState { Reality, Abyss };


    [Header("MonsterBox")]
    public GameObject RealBox;
    public GameObject AbyssBox;

    [Header("AbyssMonsterPrefab")]
    public GameObject[] AbyssMonsterPrefab;

    [Header("CameraLayer")]
    public Camera MainCamera;
    public int realLayer;
    public int abyssLayer;









    [Header("Temporary")]

    public Text hpText;
    public Text AbyssGageText;
    public Text StateText;
    public Text lifeText;
    public Text fogText;

    public AbyssState abyssState = new AbyssState();
    //몬스터

    int[] monsterId = new int[50];
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
        //보이게
        MainCamera.cullingMask = 1 << 11;
        //안보이게 하기
        MainCamera.cullingMask = ~(1 << 12);

        #region 임시 삭제요망
        hpText.text = Convert.ToString(hpGage);
        AbyssGageText.text = Convert.ToString(AbyssGage);

        StateText.text = "메인";

        fogText.text = Convert.ToString(Darkfog);


        #endregion
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
                #region 임시 삭제요망
                hpText.text = Convert.ToString(hpGage);
                StateText.text = "메인";
                lifeText.text = "사망";
                #endregion
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
        #region 임시 삭제요망
        StateText.text = "메인";
        #endregion
        isAbyssEnd = true;
    }


    #region 심연의 경계 변경 함수

    /// <summary>
    /// 심연 이동 함수
    /// </summary>
    void GoAbyss()
    {
        //보이게하기
        MainCamera.cullingMask = 1 << abyssLayer;
        //안보이게 하기
        MainCamera.cullingMask = ~(1 << realLayer);

        isAbyssStay = true;
        abyssState = AbyssState.Abyss; // enum에 상태값 입력
        StateText.text = "심연"; // 임시
        AbyssMonsterSpawn(); // 심연 몬스터가 있을경우 실행
        AbyssBox.SetActive(true);// 심연 몬스터 그룹  활성화
        RealBox.SetActive(false);// 현실 몬스터 그룹 비활성화
        StartCoroutine("AbyssResource"); // 심연 리소스 소모 코루틴 실행
    }

    /// <summary>
    /// 현실 이동 함수
    /// </summary>
    void GoReal()
    {
        //보이게하기
        MainCamera.cullingMask = 1 << realLayer;
        //안보이게 하기
        MainCamera.cullingMask = ~(1 << abyssLayer);
        AbyssBox.SetActive(false);// 현실 몬스터 그룹  활성화
        RealBox.SetActive(true);  // 심연 몬스터 그룹 비활성화
        abyssState = AbyssState.Reality;
        StateText.text = "메인";
        isAbyssStay = false;
    }

    #endregion
    //몬스터 처치시 or 소비시 함수

    #region 어비스 관련 변수 getset 
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


    #region 심연 몬스터 관련 함수

    void AbyssMonsterSpawn()
    {

        for (int i = 0; i < monsterId.Length; i++)
        {
            if (monsterId[i] == 0 && i != 0)
            {
                monsterId = new int[50];
                break;
            }
            else
            {
                var d = Instantiate(AbyssMonsterPrefab[monsterId[i]], monsterPos[i], Quaternion.identity);
                d.transform.SetParent(AbyssBox.transform, false);
            }
        }
    }

    public void AbyssMonsterAdd(int id, Vector3 pos)
    {
        //들어오면 hp값 공격력 더하고 스택에 집어넣기
        for (int i = 0; i < monsterId.Length; i++)
        {
            if (monsterId[i] == 0)
            {
                monsterId[i] = id;
                monsterPos[i] = pos;
                if (i != 0)
                    break;
            }
        }
    }



    #endregion

    #region 리소스 관리


    /// <summary>
    /// 어둠 안개 게이지 획득
    /// </summary>
    /// 
    public void GetDarkFog(int gage)
    {
        Darkfog += gage;
        #region 임시 삭제요망
        fogText.text = Convert.ToString(Darkfog);
        #endregion
    }

    /// <summary>
    /// 심연게이지 습득
    /// </summary>
    /// <param Gage="int값 게이지 추가할 넣어줄것"></param>
    public void GetAbyssGage(int gage)
    {
        int ex = abyssGage + gage;
        if (ex > maxAbyssGage)
            abyssGage = maxAbyssGage;
        else
            abyssGage += gage;
        #region 임시 삭제요망
        AbyssGageText.text = Convert.ToString(abyssGage);
        #endregion

    }

    #endregion

}
