using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter_Script : MonoBehaviour
{
    public GameObject Scene_Translate_Partical;//��Ż Ż�� ����� ��ƼŬ
    //public GameObject Fade_out_in_canvas;//��Ż Ż�� ����� �̹����ε� �̰� ���� �����ϸ鼭 
                                         //Fadeȿ�� �డ���� �Ϻη� DontDestroy�ɾ���������������������̤����츮�����������϶�


    GameObject In_Porter_Unit;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            In_Porter_Unit = collision.gameObject;
            if (Input.GetKeyDown(KeyCode.W))
            {
                Instantiate(Scene_Translate_Partical, new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z), Quaternion.identity);
                Invoke("Set_OFF",0.3f);
                
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void Set_OFF()
    {
        if (In_Porter_Unit != null)
        {
            In_Porter_Unit.gameObject.SetActive(false);
            In_Porter_Unit = null;
        }
        For_Fade.FadeOff_To_Dungeon();
    }
    
}
