using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Teleporter_Script : MonoBehaviour
{
    public GameObject Scene_Translate_Partical;//匂纏 纏凶 持奄澗 督銅適
                                               //public GameObject Fade_out_in_canvas;//匂纏 纏凶 持奄澗 戚耕走昔汽 戚暗 燈誤亀 繕箭馬檎辞 
                                               //Fade反引 操亜走壱 析採君 DontDestroy杏嬢鎌製けいしけいしけい戚たい酔軒たけしい櫛艦虞

    public bool aaaaa = false;
    GameObject In_Porter_Unit;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(true);
            In_Porter_Unit = collision.gameObject;
            if (Input.GetKeyDown(KeyCode.W) && aaaaa == false)
            {
                Instantiate(Scene_Translate_Partical, new Vector3(collision.transform.position.x, collision.transform.position.y + 1, collision.transform.position.z), Quaternion.identity);
                aaaaa = true;
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
