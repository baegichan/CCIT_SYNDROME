using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFllow : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera CVC;
    private void OnEnable()
    {
        //CVC.Follow = GetComponent<Transform>();
    }
}
