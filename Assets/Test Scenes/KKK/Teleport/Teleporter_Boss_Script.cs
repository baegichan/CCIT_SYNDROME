using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Teleporter_Boss_Script : MonoBehaviour
{

    /// <summary>
    /// 어서오세요 성준이의 스크립트의 온걸 환영합니다.
    /// 여긴 보스 전용 포탈 스크립트입니다.
    /// 부끄러우니 마음대로 보시면 안됩니다.
    /// 던전에서 보스맵으로 이동하는건 씬을 이동하는건 맞지만 포탈을 타는게 아니라
    /// 문을열고 들어가는 개념이라 파티클이 들어가지 않을 예정입니다.
    /// </summary>





    //public GameObject Scene_Translate_Partical;
    public GameObject Fade_out_in_canvas;//포탈 탈때 생기는 이미지인데 이거 투명도 조절하면서 
                                         //Fade효과 줘가지고 일부러 DontDestroy걸어놨음ㅁㄴㅇㅁㄴㅇㅁㄴ이ㅏㄴ우리ㅏㅁㅇㄴ랑니라


    public GameObject Camera;
    //보스씬으로 가니까 계속 카메라가 없어서 오류가 나는데 DontDestroy해줘야할듯 합니다~



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //문 열고 들어가는거 틀어주고
            DontDestroyOnLoad(Fade_out_in_canvas.transform.parent.gameObject);
            DontDestroyOnLoad(collision.transform.parent.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.W))
            {
                Fade_out();
                //만약 보스맵으로 문을열고 들어가는 파티클때문에 시간이 걸린다면 어두워지는데
                //필요하게 딜레이를 걸어주세요~
                //그정도는 알잘딱합시다~
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(1).gameObject.SetActive(false); 문이라서 필요없을거 같아서 지웠어요~
        }
    }
  

    void Scene_Change_Boss()
    {
        SceneManager.LoadScene("Boss_Scene");

        //요것두 보스로 바꿔주도록 해요~
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
