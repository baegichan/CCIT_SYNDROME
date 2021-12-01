using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFlash : MonoBehaviour
{
    public SpriteRenderer eye;
    private void OnDisable()
    {
        eye.color = new Color(0,0,0,0);
    }
}
