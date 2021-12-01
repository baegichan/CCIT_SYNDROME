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

    public static void Change_Dungeon_Scene()//�������� �������� ���� �Լ�
    {
        SceneManager.LoadScene("InCha 2");
    }

    public static void Change_Boss_Scene()//���� �� ������ �̵�
    {
        SceneManager.LoadScene("Boss_Scene");
    }

    public static void Change_Start_Scene()//���� ���н� ������ ���ư��� �Լ�
    {
        SceneManager.LoadScene("Startpoint");
    }
}
