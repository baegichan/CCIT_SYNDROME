using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
       Camera.main.GetComponentInChildren<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = GetComponent<PolygonCollider2D>();
    }
}
