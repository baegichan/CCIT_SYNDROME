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


    GameObject Player_For_DonDestroy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //�� ���� ���°� Ʋ���ְ�
            DontDestroyOnLoad(Fade_out_in_canvas.transform.parent.gameObject);
            DontDestroyOnLoad(collision.transform.parent.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            Player_For_DonDestroy = collision.transform.parent.gameObject;
            
           
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                DontDestroyOnLoad(Player_For_DonDestroy);
                For_Fade.FadeOff_To_BossRoom();
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
            Player_For_DonDestroy = null;
            //transform.GetChild(1).gameObject.SetActive(false); ���̶� �ʿ������ ���Ƽ� �������~
        }
    }
  
}
