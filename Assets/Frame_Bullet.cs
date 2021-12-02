using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_Bullet : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D rigid;
    public bool Curve;
    public int bulletDMG = 10;
    public bool DestroyBullet = false;
    /*
    public enum CurveDir
    {
    Top,
    Bottom
    }
    public CurveDir Curvedir;
    public float CurvePower;
    private Vector3 CurveVec;
    */
    [Range(0, 100)] public float Bullet_LifeTime;
   
    private void Start()
    {

        rigid = GetComponent<Rigidbody2D>();

        /*
        switch (Curvedir)
        {
            case CurveDir.Top:
                CurveVec = Vector3.up;
                break;
            case CurveDir.Bottom:
                CurveVec = Vector3.down;
                break;
        }
        */
        Invoke("Destroy_Bullet", Bullet_LifeTime);


    }
    // Update is called once per frame

    void Update()
    {
        rigid.velocity = transform.right * Speed;
        /*
        if(Curve)
        {
            //transform.rotation =Quaternion.Euler(0,0,transform.rotation.z,);
        }
        */
        Bullet_LifeTime = Mathf.Clamp(Bullet_LifeTime - Time.deltaTime, 0, 100);
        if (Bullet_LifeTime == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Destroy_Bullet()
    {
        if (DestroyBullet)
        {

            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            if (collision.tag == "Player")
            {
                collision.transform.parent.GetComponent<Character>().Damage(bulletDMG);
                collision.GetComponentInParent<Character>().PlayerKnuckBack(transform, collision.transform, 2, false);
                Destroy(gameObject);
            }
        
        if (collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
  
    }
}
