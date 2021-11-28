using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Teleporter_Boss_Script : MonoBehaviour
{

    /// <summary>
    /// 嬢辞神室推 失層戚税 什滴験闘税 紳杏 発慎杯艦陥.
    /// 食延 左什 穿遂 匂纏 什滴験闘脊艦陥.
    /// 採塊君酔艦 原製企稽 左獣檎 照桔艦陥.
    /// 揮穿拭辞 左什己生稽 戚疑馬澗闇 樟聖 戚疑馬澗闇 限走幻 匂纏聖 展澗惟 焼艦虞
    /// 庚聖伸壱 級嬢亜澗 鯵割戚虞 督銅適戚 級嬢亜走 省聖 森舛脊艦陥.
    /// </summary>





    //public GameObject Scene_Translate_Partical;
    public GameObject Fade_out_in_canvas;//匂纏 纏凶 持奄澗 戚耕走昔汽 戚暗 燈誤亀 繕箭馬檎辞 
                                         //Fade反引 操亜走壱 析採君 DontDestroy杏嬢鎌製けいしけいしけい戚たい酔軒たけしい櫛艦虞


    public GameObject Camera;
    //左什樟生稽 亜艦猿 域紗 朝五虞亜 蒸嬢辞 神嫌亜 蟹澗汽 DontDestroy背操醤拝牛 杯艦陥~



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //庚 伸壱 級嬢亜澗暗 堂嬢爽壱
            DontDestroyOnLoad(Fade_out_in_canvas.transform.parent.gameObject);
            DontDestroyOnLoad(collision.transform.parent.gameObject);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.W))
            {
                For_Fade.FadeOff_To_BossRoom();
                //幻鉦 左什己生稽 庚聖伸壱 級嬢亜澗 督銅適凶庚拭 獣娃戚 杏鍵陥檎 嬢砧趨走澗汽
                //琶推馬惟 渠傾戚研 杏嬢爽室推~
                //益舛亀澗 硝設亨杯獣陥~
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            //transform.GetChild(1).gameObject.SetActive(false); 庚戚虞辞 琶推蒸聖暗 旭焼辞 走頗嬢推~
        }
    }
  
}
