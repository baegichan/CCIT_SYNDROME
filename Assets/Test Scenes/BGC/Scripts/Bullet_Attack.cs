using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Attack : AttackModule
{
    // Start is called before the first frame update
    public int AttackCycle = 1;
    public float Cycle_Cooltime;
    public bool Attack_CycleStart=false;
   
    public AttackType Attack_Type=AttackType.Attack;
    public BulletInfo[] BulletsInfo;
    public GameObject target;
   
    public enum AttackType
    {
        Cycle,
        Cycle_Target,
        Attack,
        Attack_Target
    }


    #region 테스트존
    private void Start()
    {
   
        if (Attack_CycleStart){
            switch (Attack_Type)
            {
                case AttackType.Attack:
                    Attack();

                    break;
                case AttackType.Attack_Target:
                    Attack(target);
              
                    break;
                case AttackType.Cycle:
                    CycleAttack();
               
                    break;
                case AttackType.Cycle_Target:
                    CycleAttack(target);
                  
                    break;
        
             

            }
           

        }
        //CycleAttack();   //n Cycle Attack Non Target
        //CycleAttack(target); //n Cycle Attack to Target
        //Attack( target); //1Cycle Attack to Target
        //Attack();   //1Cycle Attack Non Target

    }
    #endregion


    public void CycleAttack()
    {
        for (int i = 0; i < AttackCycle; i++)
        {
            Invoke("Attack", Cycle_Cooltime * i);
        }     
        
    }
    public void CycleAttack(GameObject Target)
    {
        for (int i = 0; i < AttackCycle; i++)
        {
            Attack(target, Cycle_Cooltime*i);
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
    public void Attack(GameObject target)
    {
        if (Active)
        {
            if (BulletsInfo != null)
            {
                foreach (BulletInfo a in BulletsInfo)
                {
                    StartCoroutine(Spawn_Bullet(a, target));
                }
            }
        }
    }
    public void Attack(GameObject target,float time)
    {
        if (Active)
        {
            if (BulletsInfo != null)
            {
                foreach (BulletInfo a in BulletsInfo)
                {
                    StartCoroutine(Spawn_Bullet(a, target,time));
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
    public IEnumerator Spawn_Bullet(BulletInfo BulletsInfo,GameObject target)
    {
        yield return new WaitForSeconds(BulletsInfo.time);
        GameObject bullet = Instantiate(BulletsInfo.bullet, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * 180 / Mathf.PI + BulletsInfo.Angle));
        bullet.GetComponent<new_Bullet>().Speed = BulletsInfo.Speed;
    }
    public IEnumerator Spawn_Bullet(BulletInfo BulletsInfo, GameObject target, float time)
    {
        yield return new WaitForSeconds(BulletsInfo.time+time);
        GameObject bullet = Instantiate(BulletsInfo.bullet, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * 180 / Mathf.PI + BulletsInfo.Angle));
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