using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FllowCha : MonoBehaviour
{
    public CinemachineVirtualCameraBase Camera;
    public Char_Parent Parent;

     void Update()
    {
        Camera.Follow = Parent.SelectChar.transform;
    }
}
