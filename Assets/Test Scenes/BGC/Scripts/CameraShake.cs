using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Cam_instance = null;
    public static GameObject Target;
    private Vector3 pre_Cam_Vec;
    private float shake_time=0.0f;
    private float shake_power=0.0f;

    void Awake()
    {
        
   
       if(Cam_instance==null)
       {
            Cam_instance = this;
            Target = gameObject;
       }
       else
       {
            Destroy(this);
       }
    
    }
    private void FixedUpdate()
    {
        
    }
  
    public IEnumerator Shaking(float time, float power)
    {
        Vector3 PrePos = transform.localPosition;
        float elapsed = 0.0f;
        while(elapsed<time)
        {
            float x = Random.Range(-1f, 1f) * power;
            float y = Random.Range(-1f, 1f) * power;
            transform.localPosition = new Vector3(x, y, PrePos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }


        transform.localPosition = PrePos;

    }
    private void OnPreRender()
    {
        if(shake_time>0)
        {
            pre_Cam_Vec = Random.insideUnitCircle * shake_power;
            Target.transform.localPosition = Target.transform.localPosition + pre_Cam_Vec ;
        }
    }

   
    private void OnPostRender()
    {
        if (shake_time > 0)
        {

            Target.transform.localPosition = Target.transform.localPosition - pre_Cam_Vec;
            //shake_time -= Time.De;
        }
    }
     public void Shake(float time,float power)
    {
        //if(Cam_instance==null)
        //{
         //   return;
       // }

        StartCoroutine(Shaking(time, power));
    }
}
