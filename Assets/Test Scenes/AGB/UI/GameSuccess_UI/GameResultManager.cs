using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int countKillMonster;
    private int countKillBoss;

    public float PlayTime;

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
        PlayTime += Time.deltaTime;
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
        if (isGameClear)
            ClearPanel.SetActive(true);
        else
            FaillPanel.SetActive(true);
        GameResultBox.SetActive(true);

        TimeText.text = string.Format("{0}:{1}:{2}", (int)PlayTime / 3600, (int)PlayTime / 60 % 60, (int)PlayTime % 60);
        KillMobText.text = Convert.ToString(countKillMonster);
        KillBossText.text = Convert.ToString(countKillBoss);
        DarkFogText.text = Convert.ToString(AbyssManager.abyss.darkfog);
        float count = Convert.ToSingle(ability.Count) / 3f;
        int num = 1;
        int index = 0;
        for (int i = 0; i < count; i++)
        {

            var d = Instantiate(ItemBox);
            d.transform.SetParent(Content.transform, false);

            for (int j = 0; j < 3; j++)
            {


                if (ability[index] == null)
                    break;


                d.transform.GetChild(j).GetComponent<Image>().sprite = ability[index].ResultIcon;
                d.transform.GetChild(j).gameObject.SetActive(true);


                index++;
                Debug.Log(index);
            }
            Debug.Log("ddd");

        }
        //StartCoroutine(Effect(isGameClear, string.Format("{0}:{1}:{2}", (int)PlayTime / 3600, (int)PlayTime / 60 % 60, (int)PlayTime % 60)));
   
      
    }
      float times;
    IEnumerator Effect(bool isGameClear, string time)
    {
        bool isEnd = true;
        if (isGameClear)
            EffectResult = EffectPanel.transform.GetChild(0).gameObject; // 성공
        else 
            EffectResult = EffectPanel.transform.GetChild(1).gameObject; // 실패

        EffectBack = EffectResult.transform.GetChild(0).gameObject;
        textImage = EffectResult.transform.GetChild(1).gameObject;
        EffectBack.transform.localScale = new Vector3(1, 1, 1);
        EffectBack.transform.localPosition = new Vector3(0, 0, 0);
        var vecScale = Vector3.up;
      
        while (EffectBack.transform.localScale.y < 1f)
        {
            times += Time.deltaTime;
            EffectBack.transform.localScale = new Vector3(1, times, 1);
        }

        while (EffectBack.transform.localScale.y < 1f)
        {
            times += Time.deltaTime;
            EffectBack.transform.localScale = new Vector3(1, times, 1);
        }


        yield return null;
        if (isGameClear)
            ClearPanel.SetActive(true);
        else
            FaillPanel.SetActive(true);
        GameResultBox.SetActive(true);

        TimeText.text = string.Format("{0}:{1}:{2}", (int)PlayTime / 3600, (int)PlayTime / 60 % 60, (int)PlayTime % 60);
        KillMobText.text = Convert.ToString(countKillMonster);
        KillBossText.text = Convert.ToString(countKillBoss);
        DarkFogText.text = Convert.ToString(AbyssManager.abyss.darkfog);
        float count = Convert.ToSingle(ability.Count) / 3f;
        int num = 1;
        int index = 0;
        for (int i = 0; i < count; i++)
        {

            var d = Instantiate(ItemBox);
            d.transform.SetParent(Content.transform, false);

            for (int j = 0; j < 3; j++)
            {


                if (ability[index] == null)
                    break;


                d.transform.GetChild(j).GetComponent<Image>().sprite = ability[index].ResultIcon;
                d.transform.GetChild(j).gameObject.SetActive(true);


                index++;
                Debug.Log(index);
            }
            Debug.Log("ddd");

        }

    }

    #region 변수 
    public void Abilty(List<Ability> d)
    {
        ability = d;
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


