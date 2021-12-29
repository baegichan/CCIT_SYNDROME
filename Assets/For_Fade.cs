using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using System;

public class For_Fade : MonoBehaviour
{

    public static List<GameObject> aa = new List<GameObject>();

    public static For_Fade Fade;
    Image fadeimage;
    float i_alpha;



    public void Awake()
    {
        if(Fade==null)
        {
            Fade = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    



    /// <summary>
    /// 던전 Scene으로 이동
    /// </summary>
    public static void FadeOff_To_Dungeon()
    {
        if(Fade!=null)
        {
            Fade.Fad_out_To_Dungeon();

        }
    }
    /// <summary>
    /// 보스 Scene으로 이동
    /// </summary>
    public static void FadeOff_To_BossRoom()
    {
        if (Fade != null)
        {
            Fade.Fad_out_To_BossRoom();
        }
    }

   

    

    /// <summary>
    /// 마을로 이동
    /// </summary>
    public static void FadeOff_To_StartRoom()
    {
        if (Fade != null)
        {
            //Fade.Fad_out_To_StartRoom();
        }
    }












    public void Fad_out_To_Dungeon()
    {
        i_alpha = GetComponent<Image>().color.a;
        StartCoroutine(Fade_Out_To_Dungeon());
    }
    IEnumerator Fade_Out_To_Dungeon()
    {
        while (i_alpha < 1.0f)
        {
            i_alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            GetComponent<Image>().color = new Color(0, 0, 0, i_alpha);
        }

        Use_Scene_Change.Change_Dungeon_Scene();
        StopCoroutine(Fade_Out_To_Dungeon());
    }

    Camera a;
    public void Fad_out_To_BossRoom()
    {
        i_alpha = GetComponent<Image>().color.a;
        StartCoroutine(Fade_out_To_BossRoom());
    }
    IEnumerator Fade_out_To_BossRoom()
    {
        while (i_alpha < 1.0f)
        {
            i_alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            GetComponent<Image>().color = new Color(0, 0, 0, i_alpha);
        }
        Use_Scene_Change.Change_Boss_Scene();
        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss)
        {
            MapChangeTester.AbyssMask.test.SetTrigger("Changed");
        }
        Invoke("Delay_a_back", 3f);
       
        StopCoroutine(Fade_out_To_BossRoom());

        Invoke("Player_Camera_Set", 3f);
        Invoke("aaaa", 3f);
        

    }

    Camera MaskCamera;

    void Player_Camera_Set()
    {
        a = Camera.main;
        GameObject Player1 = GameObject.Find("Player").gameObject;
        Debug.Log(a.name);
        if(a != null)
        Player1.GetComponent<Char_Parent>().Cam = a;

        GameObject Boss = GameObject.Find("Boss_Controll").transform.GetChild(0).gameObject;
        Boss.GetComponent<Boss>().Boss_Active_on = true;
    }


    void aaaa()
    {
            a = Camera.main;
            MaskCamera = Camera.main.transform.GetChild(2).gameObject.GetComponent<Camera>();
        GameObject Canvas3 = GameObject.Find("Player").transform.GetChild(3).gameObject;//Canvas (1)
        GameObject Canvas4 = GameObject.Find("Player").transform.GetChild(4).gameObject;//Canvas
        GameObject Canvas5 = GameObject.Find("Player").transform.GetChild(5).gameObject;
        GameObject Player_UI = GameObject.Find("Player_UI_Manager").gameObject;
            Canvas3.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            Canvas4.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            Player_UI.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            Canvas3.GetComponent<Canvas>().worldCamera = a;
            Canvas4.GetComponent<Canvas>().worldCamera = a;
            MaskCamera = Camera.main.transform.GetChild(2).gameObject.GetComponent<Camera>();
        Canvas5.GetComponent<Canvas>().worldCamera = MaskCamera;
            Player_UI.GetComponent<Canvas>().worldCamera = a;
            
            MIniMapSingleton.Minimap.GetComponentInChildren<CinemachineVirtualCameraBase>().Follow = GameObject.Find("Player").GetComponent<Char_Parent>().SelectChar.transform;

    }

    public void Delay_a_back()
    {
        GameObject a2 = GameObject.Find("2222");

        if(a2 != null)
        a2.gameObject.SetActive(false);
        GetComponent<Image>().color = new Color(0, 0, 0, 0);

        GameObject Player = GameObject.Find("Player");
        Player.GetComponent<Char_Parent>().SelectChar.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GameObject CurrentPlayer = Player.GetComponent<Char_Parent>().SelectChar;
        CurrentPlayer.transform.position = new Vector3(-3.37f, 2f, 0);
    }


    public void Fad_out_To_StartRoom(bool isClear)
    {
        i_alpha = GetComponent<Image>().color.a;
        StartCoroutine(Fade_out_To_StartRoom(isClear));
    }
    int Abyss_10 = 0;
    public GameObject aby;
    IEnumerator Fade_out_To_StartRoom(bool isClear)
    {
        while (i_alpha < 1.0f)
        {
            i_alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            GetComponent<Image>().color = new Color(0, 0, 0, i_alpha);
        }
      

        GameObject Fade = GameObject.Find("fade");
        GameObject ResourceManager1 = GameObject.Find("ResourceManager");
        GameObject AbyssManager1 = GameObject.Find("AbyssManager");
        GameObject SettingManager = GameObject.Find("SettingManager");
        AbyssManager.abyss.GoReal();
        ResourceManager.re.DarkFogSelect(isClear);
     
        for (int i = 0; i< aa.Count; i++)
        {
            if(aa[i] != Fade || aa[i] != ResourceManager1 || aa[i] != SettingManager)
            {
                Destroy(aa[i]);
            }
        }
        Destroy(Fade);
       
        Use_Scene_Change.Change_Start_Scene();
      

    }
   






}
