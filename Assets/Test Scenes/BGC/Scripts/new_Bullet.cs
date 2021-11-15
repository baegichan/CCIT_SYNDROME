using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public Rigidbody2D rigid;
    public bool Curve;
    public int bulletDMG=0;
    public enum CurveDir
    {
    Top,
    Bottom
    }
    public CurveDir Curvedir;
    public float CurvePower;
    private Vector3 CurveVec;
    [Range(0,100)]public float Bullet_LifeTime;
    public enum Target
    {
    Player,
    Monster
    }
    public Target Bullet_Target = Target.Monster;
    private void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        switch (Curvedir)
        {
            case CurveDir.Top:
                CurveVec = Vector3.up;
                break;
            case CurveDir.Bottom:
                CurveVec = Vector3.down;
                break;
        }

        
    }
    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.right * Speed; 
        if(Curve)
        {
            //transform.rotation =Quaternion.Euler(0,0,transform.rotation.z,);
        }
        Bullet_LifeTime = Mathf.Clamp(Bullet_LifeTime - Time.deltaTime, 0, 100);
        if(Bullet_LifeTime==0)
        {
            Destroy(gameObject);
        }
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Bullet_Target == Target.Player)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Character>().Damage(bulletDMG);
                Destroy(gameObject);
            }
            
        }
        else if(Bullet_Target==Target.Monster)
        {
            if (collision.tag == "enemy")
            {
                collision.GetComponent<Character>().Damage(bulletDMG);
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if(collision.tag=="Ground")
        {
            Destroy(gameObject);
        }
    }
}
