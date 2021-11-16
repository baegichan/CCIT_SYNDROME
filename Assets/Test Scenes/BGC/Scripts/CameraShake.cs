using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Cam_instance = null;
    private Vector3 pre_Cam_Vec;
    private float shake_time=0.0f;
    private float shake_power=0.0f;

    void Awake()
    {

   
       if(Cam_instance==null)
       {
            Cam_instance = this;
       }
       else
       {
            Destroy(this);
       }
    }


    private void OnPreRender()
    {
        if(shake_time>0)
        {
            pre_Cam_Vec = Random.insideUnitCircle * shake_power;
            transform.localPosition = transform.localPosition + pre_Cam_Vec ;
        }
    }
    private void OnPostRender()
    {
        if (shake_time > 0)
        {
         
            transform.localPosition = transform.localPosition - pre_Cam_Vec;
            shake_time -= Time.unscaledTime;
        }
    }
    static public void Shake(float time,float power)
    {
        if(Cam_instance==null)
        {
            return;
        }
        Cam_instance.shake_time = time;
        Cam_instance.shake_power = power;
    }
}
