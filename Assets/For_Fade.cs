using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
            Debug.Log(23);
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
        StopCoroutine(Fade_out_To_BossRoom());

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
