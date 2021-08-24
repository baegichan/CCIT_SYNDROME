using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{
    public int Map_Code=0;
    public float[] Mapxsize;
    public float[] Mapysize;
    public GameObject[] MapcenterOBJ;
    public GameObject[] Warppoint;
    private GameObject target;
    private float maxcamerax;
    private float mincamerax;
    private float maxcameray;
    private float mincameray; 
    private Vector2 mapcenter;
    private Camera cam;
    private float height;
    private float width;
    public Vector2[] CenterVectors;
    private float x;
    private float y;
    public int Camera_movement_range=1;
   
    // Start is called before the first frame update
    void Start()
    {
        Mappannel.pannel.GetComponent<Animator>().SetTrigger("panneldowntrigger");
        target = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        MapChange(Map_Code);
        target.transform.position = Warppoint[0].transform.position;
        Camera_target_move();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKey(settingmanager.GM.left))
        {
            
           x= Mathf.Clamp(x-Time.deltaTime*3 , -1, 1);
        }
        else if(Input.GetKey(settingmanager.GM.right))
        {
            
           x= Mathf.Clamp(x + Time.deltaTime*3 , -1, 1);
        }
        else
        {
           if(x>0)
            {
                x = Mathf.Clamp(x - Time.deltaTime * 3, 0, 1);
            }
            else 
            {
                x = Mathf.Clamp(x + Time.deltaTime * 3, -1, 0);
            }
        }
        if(Input.GetKey(settingmanager.GM.up))
        {
            
            y = Mathf.Clamp(y + Time.deltaTime*3 , -1, 1);
            Debug.Log(y);
        }
        else if (Input.GetKey(settingmanager.GM.down))
        {
          
             y=  Mathf.Clamp(y - Time.deltaTime*3, -1, 1);
        }
        else
        {
            if (y > 0)
            {
                y = Mathf.Clamp(y - Time.deltaTime * 3, 0, 1);
            }
            else
            {
                y = Mathf.Clamp(y + Time.deltaTime * 3, -1, 0);
            }
        }
        */
      
            Camera_target_move();
        
    }
    public float[] return_xy()
    {
        float[] re = { x, y };
        return re;
    }
    public void MapChange(int new_map)
    {
        

        Map_Code = new_map;
        mapcenter = CenterVectors[Map_Code];
        mincamerax = mapcenter.x - Mapxsize[new_map]/2 + (width / 2);
        mincameray = mapcenter.y - Mapysize[new_map]/2 + (height / 2);
        maxcamerax = mapcenter.x + Mapxsize[new_map]/2 - (width / 2);
        maxcameray = mapcenter.y + Mapysize[new_map]/2 - (height / 2);
        target.transform.position = Warppoint[new_map].transform.position;
        cam.transform.position = new Vector3(Mathf.Clamp(this.gameObject.transform.position.x, mincamerax + width/2, maxcamerax- width/2), Mathf.Clamp(this.gameObject.transform.position.y, mincameray+height/2, maxcameray-height/2), target.transform.position.z -20);
    }
    void Camera_target_move()
    {
       
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(Mathf.Clamp(target.transform.position.x, mincamerax + width / 2, maxcamerax - width / 2), Mathf.Clamp(target.transform.position.y , mincameray+height/2, maxcameray-height/2), target.transform.position.z - 20),1f);
       
    
    
    }
 
}
