using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Go_Back : MonoBehaviour
{


    /// <summary>
    /// �ȳ��ϼ��� �̰��� �������� ���Ͱ� ������ ������ ���ư��� ��ũ��Ʈ�Դϴٴٴ٤��ݴ��
    /// </summary>

    GameObject Player;
    GameObject Fade_out_in_canvas;
    float Fade_out_in_canvas_Alpha;

    public void Back_Start_Scene()
    {
        Player = GameObject.Find("Player");
        Fade_out_in_canvas = GameObject.Find("Fade_out_in_canvas");

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











}
