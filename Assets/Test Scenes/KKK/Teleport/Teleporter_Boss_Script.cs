using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Teleporter_Boss_Script : MonoBehaviour
{

    /// <summary>
    /// ������� �������� ��ũ��Ʈ�� �°� ȯ���մϴ�.
    /// ���� ���� ���� ��Ż ��ũ��Ʈ�Դϴ�.
    /// �β������ ������� ���ø� �ȵ˴ϴ�.
    /// �������� ���������� �̵��ϴ°� ���� �̵��ϴ°� ������ ��Ż�� Ÿ�°� �ƴ϶�
    /// �������� ���� �����̶� ��ƼŬ�� ���� ���� �����Դϴ�.
    /// </summary>





    //public GameObject Scene_Translate_Partical;
    public GameObject Fade_out_in_canvas;//��Ż Ż�� ����� �̹����ε� �̰� ���� �����ϸ鼭 
                                         //Fadeȿ�� �డ���� �Ϻη� DontDestroy�ɾ���������������������̤����츮�����������϶�


    public GameObject Camera;
    //���������� ���ϱ� ��� ī�޶� ��� ������ ���µ� DontDestroy������ҵ� �մϴ�~



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //�� ���� ���°� Ʋ���ְ�
            DontDestroyOnLoad(Fade_out_in_canvas.transform.parent.gameObject);
            DontDestroyOnLoad(collision.transform.parent.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.W))
            {
                Fade_out();
                //���� ���������� �������� ���� ��ƼŬ������ �ð��� �ɸ��ٸ� ��ο����µ�
                //�ʿ��ϰ� �����̸� �ɾ��ּ���~
                //�������� ���ߵ��սô�~
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(1).gameObject.SetActive(false); ���̶� �ʿ������ ���Ƽ� �������~
        }
    }
  

    void Scene_Change_Boss()
    {
        SceneManager.LoadScene("Boss_Scene");

        //��͵� ������ �ٲ��ֵ��� �ؿ�~
    }

    float Fade_out_in_canvas_Alpha;

    void Fade_out()
    {
        Fade_out_in_canvas_Alpha = Fade_out_in_canvas.GetComponent<Image>().color.a;
        StartCoroutine(Fade_Out());
    }

    IEnumerator Fade_Out()
    {
        while (Fade_out_in_canvas_Alpha < 1.0f)
        {
            Fade_out_in_canvas_Alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            Fade_out_in_canvas.GetComponent<Image>().color = new Color(0, 0, 0, Fade_out_in_canvas_Alpha);
        }

        Scene_Change_Boss();
        StopCoroutine(Fade_Out());
    }
}
