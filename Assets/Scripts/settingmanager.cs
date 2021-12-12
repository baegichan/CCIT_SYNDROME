using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class settingmanager : MonoBehaviour
{
    public static settingmanager SM;
    public GameObject CAN;
    public GameObject ExitPanel;
    public GameObject[] button_ar;
    public GameObject[] volume_ar;
    public GameObject CANVAS;

    public GameObject Box;
    public GameObject ApplyBtn;
  


    public AudioMixer masterMixer;
    public AudioSource audio;
    public AudioClip clip;

    bool isok = false;
    // Start is called before the first frame update
    public KeyCode jump { get; set; }
    public KeyCode up { get; set; }
    public KeyCode down { get; set; }
    public KeyCode left { get; set; }
    public KeyCode dash { get; set; }
    public KeyCode right { get; set; }
    public KeyCode item1 { get; set; }

    public KeyCode item2 { get; set; }
    public KeyCode skillchange { get; set; }
    public KeyCode comunication { get; set; }
    public KeyCode nomalattack { get; set; }
    public KeyCode skillattack { get; set; }
    public KeyCode abyss { get; set; }
    //void Awake()
    //{
    //    PlayerPrefs.DeleteAll();
    //    defalutsetter();

    //    update_keycode();


    //    if (GM == null)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        GM = this;
    //    }
    //    else if (GM != this)
    //    {
    //        Destroy(gameObject);
    //    }
    //    if (CAN == null)
    //    {


    //        CAN = CANVAS;
    //        DontDestroyOnLoad(CAN);

    //    }
    //    else if (CAN != this)
    //    {
    //        Destroy(CAN);
    //    }



    //}

    private static settingmanager _state;

 
    public static settingmanager GM
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_state)
            {
                _state = FindObjectOfType(typeof(settingmanager)) as settingmanager;

                if (_state == null)
                    Debug.Log("no Singleton obj");
            }
            return _state;
        }
    }
    private void instance()
    {
    if(SM==null)
    {
            SM = this;
    }
    else
    {
            Destroy(gameObject);
    }
    }

    private void Awake()
    {


        instance();
        //PlayerPrefs.DeleteAll();
        defalutsetter();

        update_keycode();
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
      
        update_keycode();
    }



    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CAN.gameObject.activeSelf)
            {
                cancelsetkey();
                CAN.gameObject.SetActive(false);
                ExitPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {

                CAN.gameObject.SetActive(true);
                Time.timeScale = 0;
            }

        }
        if (SceneManager.GetActiveScene().name != "InCha 2" && !isok)
        {
            Box.SetActive(false);
            ApplyBtn.SetActive(true);
            isok = true;
        }
        else if((SceneManager.GetActiveScene().name == "InCha 2" || SceneManager.GetActiveScene().name == "Boss_Scene") && isok)
        {
            Box.SetActive(true);
            ApplyBtn.SetActive(false);
            isok = false;
        }
    }
    public void update_keycode()
    {
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump"));
        up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("up"));
        down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("down"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("left"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("right"));
        item1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("item1"));
        item2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("item2"));
        comunication = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("comu"));
        skillchange = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillchange"));
        nomalattack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("nomalattack"));
        dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dash"));
        skillattack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillattack"));
        abyss = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("abyss"));
        volume_ar[0].GetComponent<Slider>().value = PlayerPrefs.GetFloat("BGMvolume");
        volume_ar[1].GetComponent<Slider>().value = PlayerPrefs.GetFloat("EFFECTvolume");

    }
    public void setkey()
    {

        for (int i = 0; i < button_ar.Length; i++)
        {
            button_ar[i].GetComponent<keymatcher>().keysave();
            button_ar[i].GetComponent<keymatcher>().updateui();
        }

        update_keycode();
    }
    public void cancelsetkey()
    {
        for (int i = 0; i < button_ar.Length; i++)
        {
            button_ar[i].GetComponent<keymatcher>().updateui();

        }
    }

    public void defalutsetter()
    {
        if (PlayerPrefs.GetFloat("BGMvolume").ToString() == "0")
        {
            PlayerPrefs.SetFloat("BGMvolume", 5);
            volume_ar[0].GetComponent<Slider>().value = 5;
        }
        if (PlayerPrefs.GetFloat("EFFECTvolume").ToString() == "0")
        {
            PlayerPrefs.SetFloat("EFFECTvolume", 5);
            volume_ar[1].GetComponent<Slider>().value = 5;
        }
        if (PlayerPrefs.GetString("jump").ToString() == "")
        {
            PlayerPrefs.SetString("jump", "Space".ToString());
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jump", "Space").ToString());
        }
        if (PlayerPrefs.GetString("dash").ToString() == "")
        {
            PlayerPrefs.SetString("dash", "LeftShift");
            dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("dash", "LeftShift").ToString());
        }
        if (PlayerPrefs.GetString("right").ToString() == "")
        {
            PlayerPrefs.SetString("right", "D");
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("right", "D").ToString());
        }
        if (PlayerPrefs.GetString("left").ToString() == "")
        {
            PlayerPrefs.SetString("left", "A");
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("left", "A").ToString());
        }
        if (PlayerPrefs.GetString("up").ToString() == "")
        {
            PlayerPrefs.SetString("up", "W");
            up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("up", "W").ToString());
        }
        if (PlayerPrefs.GetString("down").ToString() == "")
        {
            PlayerPrefs.SetString("down", "S");
            down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("down", "S").ToString());
        }
        if (PlayerPrefs.GetString("item1").ToString() == "")
        {
            PlayerPrefs.SetString("item1", "Alpha1");
            item1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("item1", "1").ToString());
        }
        if (PlayerPrefs.GetString("item2").ToString() == "")
        {
            PlayerPrefs.SetString("item2", "Alpha2");
            item2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("item2", "2").ToString());
        }
        if (PlayerPrefs.GetString("skillchange").ToString() == "")
        {
            PlayerPrefs.SetString("skillchange", "E");
            skillchange = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillchange", "E").ToString());
        }
        if (PlayerPrefs.GetString("comu").ToString() == "")
        {
            PlayerPrefs.SetString("comu", "R");
            comunication = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("comu", "Z").ToString());
        }
        if (PlayerPrefs.GetString("nomalattack").ToString() == "")
        {
            PlayerPrefs.SetString("nomalattack", "Mouse0");
            nomalattack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("nomalattack", "Mouse0").ToString());
        }
        if (PlayerPrefs.GetString("skillattack").ToString() == "")
        {
            PlayerPrefs.SetString("skillattack", "Mouse1");
            skillattack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("skillattack", "Mouse1").ToString());
        }
        if (PlayerPrefs.GetString("abyss").ToString() == "")
        {
            PlayerPrefs.SetString("abyss", "Q");
            abyss = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("abyss", "Q").ToString());
        }
        if (PlayerPrefs.GetInt("PlayerHP").ToString() == "0")
        {
            PlayerPrefs.SetInt("PlayerHP", 100);

        }
    }


    public void volumechanger(GameObject name)
    {
        PlayerPrefs.SetFloat(name.name, name.GetComponent<Slider>().value);
        //  gameObject.transform.Find("BGM").gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGM")/10; ;
    }

    public void BGMvolumechanger(GameObject name)
    {
        SoundManager.BgmVol = name.GetComponent<Slider>().value;
        //if (CANVAS.activeSelf) { audio.PlayOneShot(clip); }

        //if (name.GetComponent<Slider>().value == 0)
        //{

        //    //masterMixer.SetFloat("BGM", -80f);
        //}
        //else
        //{
        //    float sound = -40 + (name.GetComponent<Slider>().value * 4);

        //    masterMixer.SetFloat("BGM", sound);
        //}

        PlayerPrefs.SetFloat(name.name, name.GetComponent<Slider>().value);
    }

    public void SFXvolumechanger(GameObject name)
    {

        SoundManager.EffVol = name.GetComponent<Slider>().value;
        //if (CANVAS.activeSelf) { audio.PlayOneShot(clip); }

        //if (name.GetComponent<Slider>().value == 0)
        //{
        //    masterMixer.SetFloat("SFX", -80f);
        //}
        //else
        //{
        //    float sound = -40 + (name.GetComponent<Slider>().value * 4);
        //    masterMixer.SetFloat("SFX", sound);
        //}

        PlayerPrefs.SetFloat(name.name, name.GetComponent<Slider>().value);
    }




    #region 안기범 수정
    public void GoHomes()
    {
        For_Fade.Fade.Fad_out_To_StartRoom(false);
        AbyssManager.abyss.GoReal();
        Time.timeScale = 1;

    }

    #endregion
}
