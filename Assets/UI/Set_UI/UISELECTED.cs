using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISELECTED : MonoBehaviour
{
    public bool issizechager;
    public AudioSource audio;
    public AudioClip clip;
    // Start is called before the first frame update
    public void isonchanged()
    {
        audio.PlayOneShot(clip);
        if (this.gameObject.GetComponent<Toggle>().isOn)
        {
            this.gameObject.GetComponent<Image>().color = new Color(0, 254, 254);
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255);

        }
    }
    private void Start()
    {
        chabge();
    }
    public void chabge()
    {
        if (issizechager)
        {
            if (this.gameObject.GetComponent<Toggle>().isOn)
            {
                ColorBlock cb = this.gameObject.GetComponent<Toggle>().colors;
                cb.normalColor = new Color(0, 254, 254);
                this.gameObject.GetComponent<Toggle>().colors = cb;
            }
            else
            {
                ColorBlock cb = this.gameObject.GetComponent<Toggle>().colors;
                cb.normalColor = new Color(255, 255, 255);
                this.gameObject.GetComponent<Toggle>().colors = cb;
            }
        }
    }
}
