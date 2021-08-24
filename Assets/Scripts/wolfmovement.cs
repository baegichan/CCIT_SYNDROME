using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class wolfmovement : MonoBehaviour
{

    public Animator wolfani;
    public GameObject uimanager;
    public Rigidbody2D wolfrigid;
    public int damage=60;
    public enum wolfstate
    {
        charging,
        idle,
        moving,
        attack,
        die,
        skill
    }
    Vector2 mouse;
    public GameObject gameobject;
    public int HP = (int)PlayerMovement.Hp+30;
    public float speed = 10;
    public wolfstate currentstate;


    
    public GameObject sprite1;
    public GameObject sprite2;
    public float justcool=1;
    public bool coolon=false;
    public void cool()
    {
        sprite1.GetComponent<Image>().fillAmount = 1;
        justcool = 1;
    }
    private float[] axis = new float[2];
    // Update is called once per frame

    private void Start()
    {
        uimanager = GameObject.Find("GameManager");
        wolfani = this.GetComponent<Animator>();
        wolfrigid = this.GetComponent<Rigidbody2D>();
    }
    float charge = 0;
    int chargecount = 0;
    public void attack()
    {
        Collider2D[] Rhit = Physics2D.OverlapBoxAll(this.gameobject.transform.position, new Vector2(6, 2), 0);
        foreach (Collider2D collider in Rhit)
        {

            if (collider.tag == "enemy")
            {

                collider.GetComponent<AddForce_>().check();
                collider.GetComponent<MeleeAttack>().damaged(damage);
                uimanager.GetComponent<Damagetextspawn>().damagespawner(collider.gameObject, damage);



            }

            else if (collider.tag == "Boss")
            {

              
                collider.GetComponent<BossScript>().damaged(damage);
                uimanager.GetComponent<Damagetextspawn>().damagespawner(collider.gameObject, damage);



            }
        }
    }
    public IEnumerator Cool()
    {
        yield return new WaitForSeconds(0.1f);
        justcool -= 0.05f;
        sprite1.GetComponent<Image>().fillAmount = justcool;
        if (justcool > 0)
        {
            StartCoroutine(Cool());
        }
    }
    public void keydown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack_trigger();
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ground_trigger2();
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up *700);
            jump_trigger();
        }
        if (Input.GetMouseButtonDown(1))
        {
          
            charging_trigger();
        }
        if(Input.GetMouseButton(1))
        {
        
            charge = Mathf.Clamp(charge += Time.deltaTime*5,0,10);
            chargecount = (int)charge;
            if (chargecount != 0&&chargecount<=10)
            {
                uimanager.GetComponent<UImanager>().Charge(chargecount);
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            if (chargecount != 0)
            {
                uimanager.GetComponent<UImanager>().DisCharge();
            }
            Wolfrush(chargecount*5);
           
            buttonup_trigger();
            chargecount = 0;
            charge = 0;
            cool();
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            dash_trigger();
        }
       
        
      
        /*
        if (Input.GetKey((KeyCode)settingmanager.GM.left))
        {
            axis[0] = Mathf.Clamp(axis[0] - Time.deltaTime, -1, 1);
            Debug.Log(axis[0] + "돌긴함");
           
            if(axis[0]!=0)
            {
                wolfani.SetBool("move", true);
            }
        }
        else if (Input.GetKey((KeyCode)settingmanager.GM.right))
        {
            axis[0] = Mathf.Clamp(axis[0] + Time.deltaTime, -1, 1);
            if (axis[0] != 0)
            {
                wolfani.SetBool("move", true);
            }
        }

        else if (axis[0]!=0)
        {
            if(axis[0]>0)
            {
               axis[0]= Mathf.Clamp(axis[0] - Time.deltaTime * 3, 0, 1);
            }
            else if(axis[0]<0)
            {
                axis[0] = Mathf.Clamp(axis[0] + Time.deltaTime * 3, -1, 0);
            }
              
        }
        if (axis[0] == 0)
        {
            wolfani.SetBool("move", false);
        }
        if (axis[0] != 0)
        {
            wolfrigid.mov
        }*/
        Vector3 velo;
        float Horizontal = Input.GetAxis("Horizontal");
        //이동 수정필요.
        Vector3 Positon = transform.position;

        Positon.x += Horizontal * Time.deltaTime * speed;

        transform.position = Positon;
        if(Horizontal!=0)
        {
            wolfani.SetBool("move", true);
        }
        else
        {
            wolfani.SetBool("move", false);

        }
    }
    public void Mouseflip()//마우스 방향으로 플립
    {
        if (mouse.x <= 1763650)// 1920x1080 기준 중간지점
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (mouse.x >= 1763650)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void Wolfrush(float power)
    {
        if(this.gameObject.transform.localScale.x<0)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.left* power*1100);
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * power*1100);
        }
    }
    public void move()
    {

        transform.position = new Vector3(this.gameObject.transform.position.x+Time.deltaTime * speed, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

    } 
    private void Update()
    {
       
        mouse = Camera.main.ViewportToScreenPoint(Input.mousePosition);//마우스 포인터 좌표받기
        Mouseflip();
        keydown();
        if (justcool == 1)
        {
            StartCoroutine(Cool());
        }
        switch (currentstate)
        {

            case wolfstate.idle :



                break;
            case wolfstate.moving:



                break;
            case wolfstate.attack:



                break;
            case wolfstate.charging:



                break;
            case wolfstate.die:



                break;


        }
    }


    public void attack_trigger()
    {
        wolfani.SetTrigger("attackcheck");

    }


    public void charging_trigger()
    {
        wolfani.SetTrigger("skill");
    }
    public void buttonup_trigger()
    {
        wolfani.SetTrigger("buttonup");
    }
    public void jump_trigger()
    {
        wolfani.SetTrigger("jump");
    }
    public void dash_trigger()
    {
        wolfani.SetTrigger("dash");
        if (this.transform.localScale.x >0)
        {
           // this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 3700);
        }
        else
        {
           // this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 3700);
        }
    }
    public void ground_trigger()
    {
        wolfani.SetBool("grounded",true);
    }
    public void ground_trigger2()
    {
        wolfani.SetBool("grounded", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Ground")
        {
            ground_trigger();
        }
    }
}
