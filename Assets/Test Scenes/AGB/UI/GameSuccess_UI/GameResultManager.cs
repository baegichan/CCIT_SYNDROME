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
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_result != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);

    }

    public void ShowResult(bool isGameClear)
    {
        if (isGameClear)
            ClearPanel.SetActive(true);
        else
            FaillPanel.SetActive(true);

        TimeText.text = string.Format("{0}:{1}:{2}", (int)PlayTime / 3600, (int)PlayTime / 60 % 60, (int)PlayTime % 60);
        KillMobText.text = Convert.ToString(countKillMonster);
        KillBossText.text = Convert.ToString(countKillBoss);
        DarkFogText.text = Convert.ToString(AbyssManager.abyss.darkfog);
        float count = Convert.ToSingle(ability.Count) / 3f;
        int num = 1;
        int index = 1;
        for (int i = 0; i < count; i++)
        {
            var d = Instantiate(ItemBox);
            d.transform.SetParent(Content.transform, false);

            for (int j = 1; j <= 3; j++)
            {
                index = j * num;
                if (ability[index] == null)
                    break;
                d.transform.GetChild(j - 1).GetComponent<Image>().sprite = ability[index].ResultIcon;
                d.transform.GetChild(j - 1).gameObject.SetActive(true);
            }

            num++;
        }
    }

    public void Abilty(List<Ability> d)
    {
        ability = d;
    }
}


