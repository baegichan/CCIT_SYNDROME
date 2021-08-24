using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class keymatcher : MonoBehaviour
{

    GameObject settingmanager;
    public keychanger keychanger;
    public enum keyname
    {
        nomalattack,
        skillattack,
        dash,
        jump,
        up,
        down,
        left,
        right,
        item1,
        item2,
        skillchange,
        comu
    }
    public keyname key;
    // Start is called before the first frame update
    private void Awake()
    {
        settingmanager = GameObject.Find("SETTINGCANVAS").gameObject;
        updateui();

    }
    public void keysave()
    {
        int outint;
        bool numcheck = int.TryParse(this.gameObject.transform.GetChild(0).GetComponent<Text>().text, out outint);
        if (numcheck)
        {
            PlayerPrefs.SetString(key.ToString().ToLower(), "Alpha" + this.gameObject.transform.GetChild(0).GetComponent<Text>().text);
        }
        else
        {
            PlayerPrefs.SetString(key.ToString().ToLower(), this.gameObject.transform.GetChild(0).GetComponent<Text>().text);
        }
    }

    public void updateui()
    {

        if (PlayerPrefs.GetString(key.ToString()).Remove(PlayerPrefs.GetString(key.ToString()).Length - 1).ToLower() == "alpha")
        {
            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(key.ToString().ToLower()).Remove(0, 5);
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetString(key.ToString().ToLower());
        }
    }
    public void changerclick()
    {

        keychanger.buttonkeyevent(this.gameObject, key.ToString().ToLower());

    }
}
