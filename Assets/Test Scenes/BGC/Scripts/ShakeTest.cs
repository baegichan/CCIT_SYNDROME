using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        CameraShake.Shake(10000, 10);
    }
}
