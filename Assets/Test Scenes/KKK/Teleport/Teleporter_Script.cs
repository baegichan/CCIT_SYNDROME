using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter_Script : MonoBehaviour
{
    public GameObject Scene_Translate_Partical;//포탈 탈때 생기는 파티클
    //public GameObject Fade_out_in_canvas;//포탈 탈때 생기는 이미지인데 이거 투명도 조절하면서 
                                         //Fade효과 줘가지고 일부러 DontDestroy걸어놨음ㅁㄴㅇㅁㄴㅇㅁㄴ이ㅏㄴ우리ㅏㅁㅇㄴ랑니라


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
    //}

    //void Scene_Change()
    //{
    //    SceneManager.LoadScene("main2");

    //    //던전 Scene로 바꿔야하겟죠?
    //}

    //float Fade_out_in_canvas_Alpha;
    //void Fad_out()
    //{
    //    Fade_out_in_canvas_Alpha = Fade_out_in_canvas.GetComponent<Image>().color.a;
    //    StartCoroutine(Fade_Out());
    //}

    //Fade_out_in_canvas요걸 ui의 레이어 중에서도 맨위에 놓으면 될거 같습니다요
    //IEnumerator Fade_Out()
    //{
    //    while (Fade_out_in_canvas_Alpha < 1.0f)
    //    {
    //        Fade_out_in_canvas_Alpha += 0.01f;
    //        yield return new WaitForSeconds(0.01f);
    //        Fade_out_in_canvas.GetComponent<Image>().color = new Color(0, 0, 0, Fade_out_in_canvas_Alpha);
    //    }
        
    //    Scene_Change();
    //    StopCoroutine(Fade_Out());
    //}



}
