using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class buttondownmanager : MonoBehaviour
{
    private GameObject SETTING;
    private GameObject MANAGER;
    public int screensizex;
    public int screensizey;
    public bool mode;
    public static int savedx = 1920;
    public static int savedy = 1080;
    public static bool screenmode = true;
    public AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update

    private void Start()
    {


        //MANAGER = GameObject.Find("Settingmanager");
        //SETTING = MANAGER.GetComponent<keychanger>().settinginfo;
        MANAGER = GameObject.Find("SettingManager");
        SETTING = MANAGER.transform.GetChild(0).gameObject;

    }
    public void cancelbutton()
    {
        //audio.PlayOneShot(clip);
        MANAGER.GetComponent<settingmanager>().cancelsetkey();
        SETTING.SetActive(false);

    }

    public void checkbutton()
    {
        //audio.PlayOneShot(clip);
        MANAGER.GetComponent<settingmanager>().setkey();
        SETTING.SetActive(false);
    }
    public void screensize()
    {
        audio.PlayOneShot(clip);
        savedx = screensizex;

        savedy = screensizey;
        Screen.SetResolution(savedx, savedy, screenmode);
    }
    public void screenmodechange()
    {
        screenmode = mode;
        if (screenmode)
        {
            Screen.fullScreen = false;
            Screen.SetResolution(savedx, savedy, screenmode);
        }
        else
        {
            Screen.fullScreen = true;
            Screen.SetResolution(savedx, savedy, screenmode);
        }
    }
    public void exitbutton()
    {


    }
    public void setting()
    {
        SETTING.SetActive(true);
    }
    public void GameQuit()
    {

        Application.Quit();
    }

}
