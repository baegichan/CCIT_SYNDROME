using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_8WaySensor : Sensol
{

    private float Sqrt_range;
    float testx;
    float testy;
    // Start is called before the first frame update
    void Start()
    {
        Set_Module();
        Detected_Object = new GameObject[8];
        
        testx = Mathf.Sin(Mathf.Deg2Rad * 45) * Ray_Range;
        testy = Mathf.Cos(Mathf.Deg2Rad * 45) * Ray_Range;
        Sqrt_range = testy;
    }

    // Update is called once per frame
   
    private void FixedUpdate()
    {
        if (Active)
        {

            Left_hit = Physics2D.Raycast(transform.position, Vector2.left, Ray_Range);
            if (Left_hit.collider != null)
            {
                Debug.DrawRay(transform.position, Vector2.left * Ray_Range, Color.red);
                Debug.Log("Ray Online _L");
                if (Left_hit.transform.tag == Wall_Tag_Name)
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
            Right_hit = Physics2D.Raycast(transform.position, Vector2.right, Ray_Range);
            if (Right_hit.collider != null)
            {
                Debug.DrawRay(transform.position, Vector2.right * Ray_Range, Color.red);
                if (Right_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[1] = Right_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[1] != null)
                {
                    Detected_Object[1] = null;
                }
                Debug.DrawRay(transform.position, Vector2.right * Ray_Range, Color.white);
            }

            Top_hit = Physics2D.Raycast(transform.position, Vector2.up, Ray_Range);
            if (Top_hit.collider != null)
            {
                Debug.DrawRay(transform.position, Vector2.up * Ray_Range, Color.red);

                if (Top_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[2] = Top_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[2] != null)
                {
                    Detected_Object[2] = null;
                }
                Debug.DrawRay(transform.position, Vector2.up * Ray_Range, Color.white);
            }

            Bottom_hit = Physics2D.Raycast(transform.position, Vector2.down, Ray_Range);
            if (Bottom_hit.collider != null)
            {
                Debug.DrawRay(transform.position, Vector2.down * Ray_Range, Color.red);

                if (Bottom_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[3] = Bottom_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[3] != null)
                {
                    Detected_Object[3] = null;
                }
                Debug.DrawRay(transform.position, Vector2.down * Ray_Range, Color.white);
            }

            TL_hit = Physics2D.Raycast(transform.position, new Vector2(-1, 1), Sqrt_range);
            if (TL_hit.collider != null)
            {
                Debug.DrawRay(transform.position, new Vector2(-1, 1)* Sqrt_range, Color.red);
                if (TL_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[4] = TL_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[4] != null)
                {
                    Detected_Object[4] = null;
                }
                Debug.DrawRay(transform.position, new Vector2(-1, 1)* Sqrt_range, Color.white);
            }
            TR_hit = Physics2D.Raycast(transform.position, new Vector2(1, 1), Sqrt_range);
            if (TR_hit.collider != null)
            {
                Debug.DrawRay(transform.position, new Vector2(1, 1) * Sqrt_range, Color.red);
                Debug.Log("Ray Online _L");
                if (TR_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[5] = TR_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[5] != null)
                {
                    Detected_Object[5] = null;
                }
                Debug.DrawRay(transform.position, new Vector2(1, 1) * Sqrt_range, Color.white);
            }


            BL_hit = Physics2D.Raycast(transform.position, new Vector2(-1, -1), Sqrt_range);
            if (BL_hit.collider != null)
            {
                Debug.DrawRay(transform.position, new Vector2(-1, -1) * Sqrt_range, Color.red);
                if (BL_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[6] = BL_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[6] != null)
                {
                    Detected_Object[6] = null;
                }
                Debug.DrawRay(transform.position, new Vector2(-1, -1) * Sqrt_range, Color.white);
            }

            BR_hit = Physics2D.Raycast(transform.position, new Vector2(1, -1), Sqrt_range);
            if (BR_hit.collider != null)
            {
                Debug.DrawRay(transform.position, new Vector2(1, -1) * Sqrt_range, Color.red);

                if (BR_hit.transform.tag == Wall_Tag_Name)
                {
                    Detected_Object[7] = BR_hit.transform.gameObject;
                }
            }
            else
            {
                if (Detected_Object[7] != null)
                {
                    Detected_Object[7] = null;
                }
                Debug.DrawRay(transform.position, new Vector2(1, -1) * Sqrt_range, Color.white);
            }

        }
    }
}
