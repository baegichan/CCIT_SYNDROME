using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundroop : MonoBehaviour
{
    private GameObject[] loop_objects=new GameObject[4];
    private int loop_object_count;
    private Camera Maincamera;
    public float camx;
    // Start is called before the first frame update

    private void Start()
    {
        Maincamera = Camera.main;
        loop_object_count = GameObject.Find("BG_loop").transform.childCount;
        for (int i = 0; i < loop_object_count; i++) 
        { 
            loop_objects[i] = GameObject.Find("BG_loop").transform.GetChild(i).gameObject;
         }

    }
    // Update is called once per frame
    void Update()
    {
        float[] movecheck= Maincamera.GetComponent<Camera_movement>().return_xy();
       if(movecheck[0]!=0)
        {
            for (int i = 0; i < loop_object_count; i++) 
            
            {
               // loop_objects[i].transform.localPosition = new Vector2(loop_objects[i].transform.localPosition.x+movecheck[0]*2,0);
            }
        }
        if (Maincamera.transform.position.x>=loop_objects[2].transform.localPosition.x-loop_objects[1].transform.localPosition.x+camx)
        {
            //  transform.parent.DetachChildren();
            camx = Maincamera.transform.position.x;
            this.transform.DetachChildren();
            for (int i = 1; i < loop_object_count; i++)
            {
                loop_objects[i].transform.SetParent(this.gameObject.transform);
            }
            loop_objects[0].transform.SetParent(this.gameObject.transform);
            loop_objects[3] = loop_objects[0];
            loop_objects[0].transform.localPosition = new Vector3(loop_objects[0].transform.position.x + (loop_objects[1].transform.position.x - loop_objects[0].transform.position.x) * loop_object_count, 0, 0);
            for (int i = 0; i < loop_object_count; i++)
            {
                loop_objects[i] = loop_objects[i + 1];
            }
        }
        if(Maincamera.transform.position.x <= loop_objects[0].transform.localPosition.x - loop_objects[1].transform.localPosition.x + camx)
        {
            camx = Maincamera.transform.position.x;
            this.transform.DetachChildren();
            loop_objects[loop_object_count - 1].transform.SetParent(this.gameObject.transform);
            for (int i = 0; i < loop_object_count-1; i++)
            {
                loop_objects[i].transform.SetParent(this.gameObject.transform);
            }
            
           
            for (int i = loop_object_count; i > 0; i--)
            {
                loop_objects[i] = loop_objects[i-1];
            }
            loop_objects[0] = loop_objects[3];
            loop_objects[0].transform.localPosition = new Vector3(loop_objects[0].transform.position.x - (loop_objects[0].transform.position.x - loop_objects[2].transform.position.x) * loop_object_count, 0, 0);

        }
      //  else if(Maincamera.transform.position.x<)
    }
}
