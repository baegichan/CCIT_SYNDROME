using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameResultManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Fade_out_in_canvas;
    float Fade_out_in_canvas_Alpha;

    private int countKillMonster;
    private int countKillBoss;

    public float playTime;

    List<Ability> ability;

    [SerializeField]
    GameObject GameResultBox;
    [SerializeField]
    GameObject ClearPanel;
    [SerializeField]
    GameObject FaillPanel;



    [Header("Effect")]
    [SerializeField]
    GameObject EffectPanel;

    GameObject EffectResult;
    GameObject EffectBack;
    GameObject textImage;



    [Header("Text")]
    [SerializeField]
    Text TimeText;
    [SerializeField]
    Text KillMobText;
    [SerializeField]
    Text KillBossText;
    [SerializeField]
    Text DarkFogText;

    [SerializeField]
    GameObject ItemBox;

    [SerializeField]
    GameObject Content;

    [Header("Effect")]
    bool allClear = false;
    bool clear1 = false;
    bool clear2 = false;
    float times;

    [Header("ResultManager")]
    bool isSkip = false;
    bool isTimeText = false;
    bool isMonsterText = false;
    bool isBoosText = false;
    bool isDarkFogText = false;

    float timeText = 0;
    int kilMonster = 0;
    int killBoss = 0;
    int dakrfog = 0;
    int endTime;
    bool IsClear;
    public void Go_Back()
    {
        Fade_out_in_canvas_Alpha = Fade_out_in_canvas.GetComponent<Image>().color.a;
        StartCoroutine(Fade_Out());
    }

    void Fade_out()
    {
        Fade_out_in_canvas_Alpha = Fade_out_in_canvas.GetComponent<Image>().color.a;
        StartCoroutine(Fade_Out());
    }

    void Load_Back_Scene()
    {
        SceneManager.LoadScene("main2");
    }
    IEnumerator Fade_Out()
    {
        while (Fade_out_in_canvas_Alpha < 1.0f)
        {
            Fade_out_in_canvas_Alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            Fade_out_in_canvas.GetComponent<Image>().color = new Color(0, 0, 0, Fade_out_in_canvas_Alpha);
        }
        Destroy(Player);
        Load_Back_Scene();
        StopCoroutine(Fade_Out());
    }



    private static GameResultManager _result;
    public static GameResultManager result
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_result)
            {
                _result = FindObjectOfType(typeof(GameResultManager)) as GameResultManager;

                if (_result == null)
                    Debug.Log("no Singleton obj");
            }
            return _result;
        }
    }
    private void Update()
    {
        playTime += Time.deltaTime;

        if (allClear)
        {
            if (clear1)
            {
                times += Time.deltaTime * 5;
                EffectBack.transform.localScale = new Vector3(1, times, 1);
                if (EffectBack.transform.localScale.y > 1.1f)
                {
                    textImage.SetActive(true);
                    clear1 = false;
                    StartCoroutine(Timer(1));

                }
            }
            if (clear2)
            {
                times += Time.deltaTime * 4000;
                EffectBack.transform.localPosition = new Vector3(-times, 0, 0);
                if (EffectBack.transform.localPosition.x < -2000f)
                {
                    clear2 = false;
                    allClear = false;
                    textImage.SetActive(false);
                    EffectManager(false);
                    Result();
                }
            }
        }

        //if (isTextclear)
        //{
        //    if (isTimeText)
        //    {

        //        if (endTime >= timeText)
        //            timeText++;
        //        else
        //        {
        //            isTimeText = false;
        //            isMonsterText = true;
        //            timeText = 0;
        //        }

        //        TimeText.text = string.Format("{0}:{1}:{2}", (int)playTime / 3600, (int)playTime / 60 % 60, (int)playTime % 60);
        //    }
        //    if (isMonsterText)
        //    {
        //        KillMobText.text = Convert.ToString(kilMonster);
        //        if(kilMonster != countKillMonster)
        //        kilMonster++;
        //        else
        //        {
        //            isMonsterText = false;
        //            isBoosText = true;
        //            kilMonster = 0;
        //        }
        //    }
        //    if (isBoosText)
        //    {
        //        KillBossText.text = Convert.ToString(killBoss);
        //        if (killBoss != countKillBoss)
        //            killBoss++;
        //        else
        //        {
        //            isBoosText = false;
        //            isDarkFogText =  true;;
        //            killBoss = 0;

        //        }
        //    }
        //    if (isDarkFogText)
        //    {
        //        DarkFogText.text = Convert.ToString(killBoss);
        //        if (dakrfog != AbyssManager.abyss.darkfog)
        //            dakrfog++;
        //        else
        //        {
        //            isDarkFogText = false;
        //            isTextclear = false;
        //            dakrfog = 0;

        //        }
        //    }

        //}


    }


    private void Awake()
    {

        if (_result == null)
        {
            _result = this;

        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_result != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);

    }

    public void ShowResult(bool isGameClear)
    {
        endTime = Convert.ToInt32(playTime);
        Debug.Log(endTime);
        if (isGameClear)
            EffectResult = EffectPanel.transform.GetChild(0).gameObject; // 성공
        else
            EffectResult = EffectPanel.transform.GetChild(1).gameObject; // 실패

        EffectBack = EffectResult.transform.GetChild(0).gameObject;
        textImage = EffectBack.transform.GetChild(0).gameObject;
        EffectBack.transform.localScale = new Vector3(1, 0, 1);
        EffectBack.transform.localPosition = new Vector3(0, 0, 0);
        times = 0;
        EffectManager(true);
        IsClear = isGameClear;
        allClear = true;
        clear1 = true;

    }


    private void Result()
    {
        DarkFogText.text = "0";
        KillBossText.text = "0";
        KillMobText.text = "0";
        TimeText.text = "00:00:00";
        if (IsClear)
            ClearPanel.SetActive(true);
        else
            FaillPanel.SetActive(true);
        GameResultBox.SetActive(true);

        //TimeText.text = string.Format("{0}:{1}:{2}", (int)playTime / 3600, (int)playTime / 60 % 60, (int)playTime % 60);
        //KillMobText.text = Convert.ToString(countKillMonster);
        //KillBossText.text = Convert.ToString(countKillBoss);
        //DarkFogText.text = Convert.ToString(AbyssManager.abyss.darkfog);
        //try
        //{
        //    float count = Convert.ToSingle(ability.Count) / 3f;
        //    int num = 1;
        //    int index = 0;
        //    for (int i = 0; i < count; i++)
        //    {

        //        var d = Instantiate(ItemBox);
        //        d.transform.SetParent(Content.transform, false);

        //        for (int j = 0; j < 3; j++)
        //        {


        //            if (ability[index] == null)
        //                break;


        //            d.transform.GetChild(j).GetComponent<Image>().sprite = ability[index].ResultIcon;
        //            d.transform.GetChild(j).gameObject.SetActive(true);


        //            index++;
        //            Debug.Log(index);
        //        }
        //    }
        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.Message);

        //}


        //isTextclear = true;
        isTimeText = true;

        StartCoroutine(ResultEffct());
    }

    void EffectManager(bool Isbool)
    {
        EffectPanel.SetActive(Isbool);
        EffectResult.SetActive(Isbool);
        EffectBack.SetActive(Isbool);
    }
    float cuntTime = 0.05f;
    IEnumerator ResultEffct()
    {

        if (isTimeText)
        {

            while (true)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    isSkip = true;
                    isTimeText = false;
                    break;
                }
                if (endTime != timeText)
                {
                    if (1000f < endTime - timeText)
                        timeText += 500;
                    if (500f < endTime - timeText)
                        timeText += 250;
                    else if (100f < endTime - timeText)
                        timeText += 100;
                    else if (50f < endTime - timeText)
                        timeText += 20;
                    else if (25f < endTime - timeText)
                        timeText += 10;
                    else
                        timeText++;
                }

                else
                {
                    isTimeText = false;
                    isMonsterText = true;
                    timeText = 0;
                    Debug.Log("시간끝");
                    break;
                }
                Debug.Log(endTime);
                TimeText.text = string.Format("{0}:{1}:{2}", (int)timeText / 3600, (int)timeText / 60 % 60, (int)timeText % 60);
                yield return new WaitForSeconds(cuntTime);
            }

        }
        if (isMonsterText)
        {

            while (true)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    isSkip = true;
                    isMonsterText = false;
                    break;
                }
                KillMobText.text = Convert.ToString(kilMonster);
                if (kilMonster != countKillMonster)
                {

                    if (100f < countKillMonster - kilMonster)
                        kilMonster += 30;
                    else if (50f < countKillMonster - kilMonster)
                        kilMonster += 20;
                    else if (25f < countKillMonster - kilMonster)
                        kilMonster += 10;
                    else
                        kilMonster++;
                }

                else
                {
                    isMonsterText = false;
                    isBoosText = true;
                    kilMonster = 0;
                    Debug.Log("몮스터");
                    break;
                }
                yield return new WaitForSeconds(cuntTime);
            }

        }
        if (isBoosText)
        {

            while (true)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    isSkip = true;
                    isBoosText = false;
                    break;
                }
                KillBossText.text = Convert.ToString(killBoss);
                if (killBoss != countKillBoss)
                    killBoss++;
                else
                {
                    isBoosText = false;
                    isDarkFogText = true; ;
                    killBoss = 0;
                    Debug.Log("보스");
                    break;
                }
                yield return new WaitForSeconds(cuntTime);
            }

        }
        if (isDarkFogText)
        {

            while (true)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    isSkip = true;
                    isDarkFogText = false;
                    break;
                }
                DarkFogText.text = Convert.ToString(dakrfog);
                if (dakrfog != AbyssManager.abyss.Darkfog)
                {
                    if (1000f < AbyssManager.abyss.Darkfog - dakrfog)
                        dakrfog += 500;
                    if (500f < AbyssManager.abyss.Darkfog - dakrfog)
                        dakrfog += 250;
                    else if (100f < AbyssManager.abyss.Darkfog - dakrfog)
                        dakrfog += 100;
                    else if (50f < AbyssManager.abyss.Darkfog - dakrfog)
                        dakrfog += 20;
                    else if (25f < AbyssManager.abyss.Darkfog - dakrfog)
                        dakrfog += 10;
                    else
                        dakrfog++;
                }

                else
                {
                    isDarkFogText = false;

                    dakrfog = 0;
                    Debug.Log("daa");
                    break;
                }
                yield return new WaitForSeconds(cuntTime);
            }
        }
        float skiptime = 0.5f;
        try
        {
            float count = Convert.ToSingle(ability.Count) / 3f;
            int num = 1;
            int index = 0;
            for (int i = 0; i < count; i++)
            {

                var d = Instantiate(ItemBox);
                d.transform.SetParent(Content.transform, false);

                for (int j = 0; j < 3; j++)
                {
                    if (Input.GetKey(KeyCode.Mouse0) || isSkip)
                    {
                        skiptime = 0;
                        TimeText.text = string.Format("{0}:{1}:{2}", (int)endTime / 3600, (int)endTime / 60 % 60, (int)endTime % 60);
                        KillMobText.text = Convert.ToString(countKillMonster);
                        KillBossText.text = Convert.ToString(countKillBoss);
                        DarkFogText.text = Convert.ToString(AbyssManager.abyss.Darkfog);
                        isSkip = false;
                    }


                    if (ability[index] == null)
                        break;


                    d.transform.GetChild(j).GetComponent<Image>().sprite = ability[index].ResultIcon;
                    d.transform.GetChild(j).gameObject.SetActive(true);

                    yield return new WaitForSeconds(skiptime);
                    index++;
                    Debug.Log(index);
                }
            }
        }
        finally
        {
            if (Input.GetKey(KeyCode.Mouse0) || isSkip)
            {
                skiptime = 0;
                TimeText.text = string.Format("{0}:{1}:{2}", (int)endTime / 3600, (int)endTime / 60 % 60, (int)endTime % 60);
                KillMobText.text = Convert.ToString(countKillMonster);
                KillBossText.text = Convert.ToString(countKillBoss);
                DarkFogText.text = Convert.ToString(AbyssManager.abyss.Darkfog);
                isSkip = false;
            }
        }
        isSkip = false;
    }
    IEnumerator Timer(int i)
    {
        yield return new WaitForSeconds(2f);
        times = 0;
        clear2 = true;

    }
    #region 변수 
    public void Abilty(List<Ability> d)
    {
        ability = d;
    }



    public void PlayTimerReset()
    {
        playTime = 0;
    }
    public int CountKillMonster
    {
        set
        {
            countKillMonster = value;

        }
        get
        {
            return countKillMonster;
        }
    }

    public int CountKillBoss
    {
        set
        {
            countKillBoss = value;

        }
        get
        {
            return countKillBoss;
        }
    }
    #endregion

}

