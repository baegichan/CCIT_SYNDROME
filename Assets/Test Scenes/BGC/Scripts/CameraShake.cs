using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Cam_instance = null;
    public static GameObject Target;
    private CinemachineVirtualCamera Cam;
    private Vector3 pre_Cam_Vec;
    private float shake_time = 0.0f;
    private float shake_power = 0.0f;
    private bool shaking;
    CinemachineBasicMultiChannelPerlin perlin;
    void Awake()
    {


        if (Cam_instance == null)
        {
            Cam_instance = this;
            Target = gameObject;
            Cam= GetComponent<CinemachineVirtualCamera>();
            perlin= Cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Destroy(this);
        }

    }
    private void FixedUpdate()
    {

    }
    Vector3 PrePos;
    public IEnumerator Shaking(float time, float power)
    {
        if (shaking == true)
        {
            transform.localPosition = PrePos;
            shaking = false;
            StartCoroutine(Shaking(time, power));
        }
        else
        {
            shaking = true;
            PrePos = transform.localPosition;
            float elapsed = 0.0f;
            while (elapsed < time)
            {
                float x = Random.Range(-1f, 1f) * power;
                float y = Random.Range(-1f, 1f) * power;
                transform.localPosition = new Vector3(x, y, PrePos.z);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = PrePos;
            shaking = false;
        }


    }
    private void OnPreRender()
    {
        if (shake_time > 0)
        {
            pre_Cam_Vec = Random.insideUnitCircle * shake_power;
            Target.transform.localPosition = Target.transform.localPosition + pre_Cam_Vec;
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
    public void Shake(float time, float power)
    {
        //if(Cam_instance==null)
        //{
        //   return;
        // }

        StartCoroutine(Shaking(time, power));
    }


    public  void CameraShake_Cinemachine(float time, float power)
    {
       
        perlin.m_AmplitudeGain = power;
        shake_time = time;
    }

    private void Update()
    {
        if(shake_time>0)
        {
            shake_time -= Time.deltaTime;
            if(shake_time<0)
            {
                perlin.m_AmplitudeGain = 0;
            }
        }
    }
}
