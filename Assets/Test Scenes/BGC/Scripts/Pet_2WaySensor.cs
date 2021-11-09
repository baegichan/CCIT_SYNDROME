using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_2WaySensor : Sensol
{
    void Start()
    {
        Set_Module();
        Detected_Object = new GameObject[2];
    }
    private void FixedUpdate()
    {
        Left_hit = Physics2D.Raycast(transform.position, Vector2.left*Ray_Range);
        if(Left_hit.collider!=null)
        {
            Debug.DrawRay(transform.position, Vector2.left * Ray_Range,Color.red);
            Debug.Log("Ray Online _L");
            if(Left_hit.transform.tag==Wall_Tag_Name)
            {
                Detected_Object[0] = Left_hit.transform.gameObject;
            }
        }
        else
        {
            if (Detected_Object[0] != null)
            {
                Detected_Object[0] = null;
            }
            Debug.DrawRay(transform.position, Vector2.left * Ray_Range, Color.white);
        }
        Right_hit = Physics2D.Raycast(transform.position, Vector2.right * Ray_Range);
        if (Right_hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.right * Ray_Range, Color.red);
            Debug.Log("Ray Online _R");
            if (Right_hit.transform.tag == Wall_Tag_Name)
            {
                Detected_Object[1] = Right_hit.transform.gameObject;
            }
        }
        else
        {
             if(Detected_Object[1] != null)
            {
                Detected_Object[1] = null;
            }
            Debug.DrawRay(transform.position, Vector2.right * Ray_Range, Color.white);
        }

    }
}