using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_3WaySensor : Sensol
{
    //With direction
    public RayDirection First_ray;
    public RayDirection Second_ray;
    public RayDirection Third_ray;
    Vector2 First_ray_Dir;
    Vector2 Second_ray_Dir;
    Vector2 Third_ray_Dir;
    void Start()
    {
        Set_Module();
        First_ray_Dir = RayDirToVec2(First_ray);
        Second_ray_Dir = RayDirToVec2(Second_ray);
        Third_ray_Dir = RayDirToVec2(Third_ray);
        Detected_Object = new GameObject[3];
    }
    private void FixedUpdate()
    {
        first_hit = Physics2D.Raycast(transform.position, First_ray_Dir, Ray_Range);
        if (first_hit.collider != null)
        {
            Debug.DrawRay(transform.position, First_ray_Dir * Ray_Range, Color.red);
            Debug.Log("Ray Online _L");
            if (first_hit.transform.tag == Wall_Tag_Name)
            {
                Detected_Object[0] = first_hit.transform.gameObject;
            }
        }
        else
        {
            if (Detected_Object[0] != null)
            {
                Detected_Object[0] = null;
            }
            Debug.DrawRay(transform.position, First_ray_Dir * Ray_Range, Color.white);
        }
        second_hit = Physics2D.Raycast(transform.position, Second_ray_Dir, Ray_Range);
        if (second_hit.collider != null)
        {
            Debug.DrawRay(transform.position, Second_ray_Dir * Ray_Range, Color.red);
            Debug.Log("Ray Online _R");
            if (second_hit.transform.tag == Wall_Tag_Name)
            {
                Detected_Object[1] = second_hit.transform.gameObject;
            }
        }
        else
        {
            if (Detected_Object[1] != null)
            {
                Detected_Object[1] = null;
            }
            Debug.DrawRay(transform.position, Second_ray_Dir * Ray_Range, Color.white);
        }

        third_hit = Physics2D.Raycast(transform.position, Third_ray_Dir, Ray_Range);
        if (third_hit.collider != null)
        {
            Debug.DrawRay(transform.position, Third_ray_Dir * Ray_Range, Color.red);
            Debug.Log("Ray Online _R");
            if (third_hit.transform.tag == Wall_Tag_Name)
            {
                Detected_Object[2] = third_hit.transform.gameObject;
            }
        }
        else
        {
            if (Detected_Object[2] != null)
            {
                Detected_Object[2] = null;
            }
            Debug.DrawRay(transform.position, Third_ray_Dir * Ray_Range, Color.white);
        }

    }
}
