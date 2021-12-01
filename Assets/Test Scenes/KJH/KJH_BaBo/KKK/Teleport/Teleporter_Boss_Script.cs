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





    public GameObject Camera;
    //���������� ���ϱ� ��� ī�޶� ��� ������ ���µ� DontDestroy������ҵ� �մϴ�~


    GameObject Player_For_DonDestroy;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            
            //�� ���� ���°� Ʋ���ְ�
           
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);


            

            Player_For_DonDestroy = collision.transform.parent.gameObject;



            if (Input.GetKeyDown(KeyCode.W))
            {
                DontDestroyOnLoad(Player_For_DonDestroy);
                For_Fade.FadeOff_To_BossRoom();
                //For_Fade.Translate_Player(collision.gameObject) ;
                StartCoroutine(aaa(collision.gameObject));
            }

        }
    }
    IEnumerator aaa(GameObject a)
    {
        yield return new WaitForSeconds(0.5f);
        For_Fade.Translate_Player(a.gameObject);

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
