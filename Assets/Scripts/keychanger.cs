using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keychanger : MonoBehaviour
{
    Event keyevent;
    string keyPressed;
    KeyCode newKey;
    public GameObject settinginfo;
    private GameObject currentselectedOB;

    Text keyText;

    public void buttonkeyevent(GameObject button,string keyname)
    {

        StartCoroutine(waitkeydown(button,keyname));
    }
    IEnumerator waitkeydown(GameObject button,string keyname)
    {
        while (true)
        {
           
            button.transform.GetChild(1).gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                button.transform.GetChild(0).GetComponent<Text>().text = "Mouse-L";
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);
                break;
            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {

                button.transform.GetChild(0).GetComponent<Text>().text = "Mouse-R";
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);
                break;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                keyPressed = Input.inputString;
                button.transform.GetChild(0).GetComponent<Text>().text = "LeftShift".ToUpper();
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);

                break;
            }
            else if (Input.GetKey(KeyCode.RightShift))
            {
                keyPressed = Input.inputString;
                button.transform.GetChild(0).GetComponent<Text>().text = "RightShift".ToUpper();
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);

                break;
            }
            else if (Input.GetKey(KeyCode.LeftAlt))
            {
                keyPressed = Input.inputString;
                button.transform.GetChild(0).GetComponent<Text>().text = "LeftAlt".ToUpper();
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);

                break;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                keyPressed = Input.inputString;
                button.transform.GetChild(0).GetComponent<Text>().text = "LeftControl".ToUpper();
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);

                break;
            }
            else if (Input.anyKeyDown)
            {
                keyPressed = Input.inputString;
                button.transform.GetChild(0).GetComponent<Text>().text = keyPressed.ToUpper();
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);

                break;
            }
            if(button.GetComponent<Toggle>().isOn!=true)
            {
                button.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 254, 254, 255);
                button.transform.GetChild(1).gameObject.SetActive(false);
                break;
            }

        yield return null;
        }
    }
   
}
