using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Attack : AttackModule
{
    // Start is called before the first frame update
    public int AttackCycle = 1;
    public float Cycle_Cooltime;
    public BulletInfo[] BulletsInfo;


    private void Start()
    {
        CycleAttack();
    }
    public void CycleAttack()
    {
        for (int i = 0; i < AttackCycle; i++)
        {
            Invoke("Attack", Cycle_Cooltime * i);
        }      
    }
    public override void Attack()
    {
    if(Active)
    {
            if (BulletsInfo != null)
            {           
                foreach (BulletInfo a in BulletsInfo)
                {
                    StartCoroutine(Spawn_Bullet(a));
                }
            }
        }  
    }

    public IEnumerator Spawn_Bullet(BulletInfo BulletsInfo)
    {
        yield return new WaitForSeconds(BulletsInfo.time);
        GameObject bullet = Instantiate(BulletsInfo.bullet,transform.position,Quaternion.Euler(0,0,0 + BulletsInfo.Angle ));
        bullet.GetComponent<new_Bullet>().Speed = BulletsInfo.Speed;
    }
  
}
[System.Serializable]
public class BulletInfo
{ 
    public GameObject bullet;
    [Header("0=to Monster (CW)")]
    public float Angle;
    public float Speed;
    [Header("공격시작할때까지의 시간")]
    public float time;
}

#region 망한 구역
/*
public abstract class Pet_Attack_Sys : MonoBehaviour
{

    public abstract void Attack();
    

}
public class Instance_bullet : MonoBehaviour
{
   public Instance_bullet(GameObject Bullet) { }
   public Instance_bullet() { }
    public void BulletSpawn(float angle,float speed)
    {
        Debug.Log("Online");
    }

}
public class Deco : Pet_Attack_Sys
{
    
    
    public Deco(Pet_Attack_Sys Before, float angle, float speed) { Before_Attack = Before; Speed = speed;Angle = angle; }
    public override void Attack()
    {
       
    }
    public void Test_Attack()
    {
        if (Before_Attack != null)
        {
            Before_Attack.Attack();
        }
    }
   protected  Pet_Attack_Sys Before_Attack;
    protected float Speed;
    protected float Angle;
}

public class DefaultAttack : Pet_Attack_Sys
{
    
    public override void Attack()
    {
        Debug.Log("Defalut Attack");
    }
}

public class SideAttack : Deco
{
    public SideAttack(Pet_Attack_Sys Before, float angle, float speed) : base(Before, angle, speed) { }
    public override void Attack()
    {
        Debug.Log("Aditional Attack(side)");
        //Bullet_Spawner.BulletSpawn(Angle,Speed);
        Test_Attack();
    }
   
}
*/
#endregion