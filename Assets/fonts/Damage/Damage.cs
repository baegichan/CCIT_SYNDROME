using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public TextMesh text;

    public void event_0()
    {
        if(text.transform.parent.tag == "Player") { text.GetComponent<TextMesh>().color = Color.red; }
        else if(text.transform.parent.tag == "enemy") { text.GetComponent<TextMesh>().color = Color.white; }
        text.gameObject.transform.parent.SetParent(null);
    }

    public void event_1()
    {
        Destroy(this.gameObject);
    }

    public void event_2()
    {
        text.GetComponent<TextMesh>().color = Color.green;
    }
}
