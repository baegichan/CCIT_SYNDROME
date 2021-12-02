using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraFllow : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnEnable()
    {
        Camera.main.transform.Find("CM vcam1").GetComponent<CinemachineVirtualCameraBase>().Follow =transform;
        MIniMapSingleton.Minimap.GetComponentInChildren<CinemachineVirtualCameraBase>().Follow = transform;
    }
}
