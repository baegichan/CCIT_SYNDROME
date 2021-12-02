using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlow : MonoBehaviour
{
    // Start is called before the first frame update
    public Char_Parent cha;

    // Update is called once per frame
    void Update()
    {
        transform.position = cha.SelectChar.transform.position;
    }
}
