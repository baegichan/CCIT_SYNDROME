using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Use_Scene_Change : MonoBehaviour
{
    public static Use_Scene_Change Scene_Change;


    public void Awake()
    {
        if (Scene_Change == null)
        {
            Scene_Change = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Change_Dungeon_Scene()//마을에서 던전으로 가는 함수
    {
        SceneManager.LoadScene("InCha 2");
    }

    public static void Change_Boss_Scene()//보스 맵 씬으로 이동
    {
        SceneManager.LoadScene("Boss_Scene");
    }

    public static void Change_Start_Scene()//공략 실패시 마을로 돌아가는 함수
    {
        SceneManager.LoadScene("Startpoint");
    }
}
