using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
public class For_Fade : MonoBehaviour
{/*
    public static For_Fade Fade;

    public static For_Fade fade
    {
        get
        {
            if (Fade == null)
            {
                fade = 
            }
        }
        set
        {

        }
    }
    */
    
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

    public static void Translate_Player(GameObject a)
    {
        a.transform.position = new Vector3(-5, 5, 0);
    }

    

    /// <summary>
    /// 마을로 이동
    /// </summary>
    public static void FadeOff_To_StartRoom()
    {
        if (Fade != null)
        {
            Fade.Fad_out_To_StartRoom();
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
        Invoke("Delay_a_back", 1f);
        StopCoroutine(Fade_out_To_BossRoom());


        Invoke("aaaa", 3f);
        GameObject Canvas1 = GameObject.Find("Player").transform.GetChild(3).gameObject;
        //Canvas1.GetComponent<Canvas>().worldCamera = 
        /*
        //GameObject b = GameObject.Find("Main Camera");
        GameObject Canvas1 = GameObject.Find("Player").transform.GetChild(3).gameObject;
        GameObject Canvas2 = GameObject.Find("Player").transform.GetChild(4).gameObject;


        if (SceneManager.GetActiveScene().name == "Boss_Scene")
            Debug.Log(23);

            a = Camera.main;

        Debug.Log(a.gameObject.name);

        Canvas1.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        Canvas2.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        
        */

    }


    Camera MaskCamera;
    void aaaa()
    {
        if (SceneManager.GetActiveScene().name == "Boss_Scene")
        {
            a = Camera.main;
            MaskCamera = Camera.main.transform.GetChild(2).gameObject.GetComponent<Camera>();
            GameObject Canvas1 = GameObject.Find("Player").transform.GetChild(3).gameObject;
            GameObject Canvas2 = GameObject.Find("Player").transform.GetChild(4).gameObject;
            GameObject Canvas3 = GameObject.Find("Player").transform.GetChild(0).transform.GetChild(6).gameObject;
            GameObject Canvas4 = GameObject.Find("Player").transform.GetChild(0).transform.GetChild(5).gameObject;
            Canvas1.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            Canvas2.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            Canvas3.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            Canvas4.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            Canvas1.GetComponent<Canvas>().worldCamera = a;
            Canvas2.GetComponent<Canvas>().worldCamera = a;
            Canvas3.GetComponent<Canvas>().worldCamera = a;
            Canvas4.GetComponent<Canvas>().worldCamera = MaskCamera;


            MapChangeTester.AbyssMask.test.SetTrigger("Changed");
        }
    }
    public void Delay_a_back()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
    public void Fad_out_To_StartRoom()
    {

        i_alpha = GetComponent<Image>().color.a;
        StartCoroutine(Fade_out_To_StartRoom());
    }
    IEnumerator Fade_out_To_StartRoom()
    {
        while (i_alpha < 1.0f)
        {
            i_alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            GetComponent<Image>().color = new Color(0, 0, 0, i_alpha);
        }
        Use_Scene_Change.Change_Start_Scene();
        StopCoroutine(Fade_out_To_StartRoom());

    }







}
