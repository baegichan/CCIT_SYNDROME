using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Wolf : MonoBehaviour
{
    public GameObject Attack_Event_0;
    public GameObject Attack_Event_1;
    public GameObject Attack_Event_2;
    public GameObject Attack_Event_3;
    public GameObject Attack_Event_4;
    public GameObject Attack_Event_5;
    public GameObject Skill_Effect;
    public Vector3 RightHand, LeftHand;
    public Vector3 RightBox, LeftBox;
    public Char_Wolf wolf;
    public GameObject[] HitEffect;
    int AttackInt;

    void CanMove()
    {
        transform.GetComponent<Char_Wolf>().Ani.SetBool("CanIThis", true);
    }

    void SpawnEffect(GameObject Effect)
    {
        if (Effect.activeSelf) { Effect.SetActive(false); }
        Effect.SetActive(true);
    }

    void Hit_1() { SpawnEffect(Attack_Event_0); }

    void Hit_2() { SpawnEffect(Attack_Event_1); }

    void Hit_3() { SpawnEffect(Attack_Event_2); }

    void Hit_4() { SpawnEffect(Attack_Event_3); }

    void Hit_5() { SpawnEffect(Attack_Event_4); }

    void Hit_6() { SpawnEffect(Attack_Event_5); }

    void Skill() { SpawnEffect(Skill_Effect); }
   
    public Collider2D[] L_hit;
    public Collider2D[] R_hit;

    Vector3 RH;
    Vector3 LH;
    void overLap_R()
    {
        RH = new Vector3(RightHand.x * transform.localScale.x + transform.position.x, RightHand.y + transform.position.y);
        R_hit = Physics2D.OverlapBoxAll(RH, RightBox, 0);
    }

    void overLap_L()
    {
        LH = new Vector3(LeftHand.x * transform.localScale.x + transform.position.x, LeftHand.y + transform.position.y);
        L_hit = Physics2D.OverlapBoxAll(LH, LeftBox, 0);
    }

    void WolfAttack_R()
    {
        if (wolf.P_Attack_State == true)
        {
            foreach (Collider2D Current in R_hit)
            {
                if (Current.tag == "Monster")
                {
                    CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                    Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP, GetComponentInParent<Char_Parent>().UseApPostion, HitEffect[AttackInt-1]);
                    Current.GetComponent<Character>().KnuckBack(transform, 5, Current.GetComponent<Character>().IsBoss);
                }
            }
        }
    }

    void WolfAttack_L()
    {
        if (wolf.P_Attack_State == true)
        {
            foreach (Collider2D Current in L_hit)
            {
                if (Current.tag == "Monster")
                {
                    CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                    Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP, GetComponentInParent<Char_Parent>().UseApPostion, HitEffect[AttackInt-1]);
                    Current.GetComponent<Character>().KnuckBack(transform, 5, Current.GetComponent<Character>().IsBoss);
                }
            }
        }
    }

    void attackInt()
    {
        if(AttackInt == 3) { AttackInt = 1; }
        else if(AttackInt < 3) { AttackInt++; }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(RightHand.x * transform.localScale.x + transform.position.x, RightHand.y + transform.position.y), RightBox);
        Gizmos.DrawWireCube(new Vector3(LeftHand.x * transform.localScale.x + transform.position.x, LeftHand.y + transform.position.y), LeftBox);
    }
}
