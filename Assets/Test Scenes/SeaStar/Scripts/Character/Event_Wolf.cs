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
    public Vector3[] RightBox, LeftBox;
    public Char_Wolf wolf;
    public GameObject[] HitEffect;
    public Char_Parent CP;
    public AbilityManager AM;
    public GameObject py;
    int AttackInt;
    public Vector3 wolfRange;
    public Vector3 BiteRange;

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

    public Vector3 RH;
    public Vector3 LH;
    void overLap_R()
    {
        RH = new Vector3(RightHand.x * transform.localScale.x + transform.position.x, RightHand.y + transform.position.y);
        R_hit = Physics2D.OverlapBoxAll(RH, RightBox[AttackInt], 0);
    }

    void overLap_L()
    {
        LH = new Vector3(LeftHand.x * transform.localScale.x + transform.position.x, LeftHand.y + transform.position.y);
        L_hit = Physics2D.OverlapBoxAll(LH, LeftBox[AttackInt], 0);
    }

    void WolfAttack_R()
    {
        if (wolf.P_Attack_State == true)
        {
            foreach (Collider2D Current in R_hit)
            {
                if (Current.tag == "Monster")
                {
                    if (!GetComponent<Char_Wolf>().Ani.GetBool("Jump"))
                    {
                        CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                        Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP, GetComponentInParent<Char_Parent>().UseApPostion, HitEffect[AttackInt - 1]);
                        Current.GetComponent<Character>().KnuckBack(transform, 2.5f, Current.GetComponent<Character>().IsBoss);
                    }
                    if (GetComponent<Char_Wolf>().Ani.GetBool("Jump"))
                    {
                        CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                        Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP + 10, GetComponentInParent<Char_Parent>().UseApPostion, HitEffect[AttackInt - 1]);
                    }
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
                    if (!GetComponent<Char_Wolf>().Ani.GetBool("Jump"))
                    {
                        CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                        Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP, GetComponentInParent<Char_Parent>().UseApPostion, HitEffect[AttackInt - 1]);
                        Current.GetComponent<Character>().KnuckBack(transform, 2.5f, Current.GetComponent<Character>().IsBoss);
                    }
                    if (GetComponent<Char_Wolf>().Ani.GetBool("Jump"))
                    {
                        CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                        Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP + 10, GetComponentInParent<Char_Parent>().UseApPostion, HitEffect[AttackInt - 1]);
                    }
                }
            }
        }
    }

    void attackInt()
    {
      if (AttackInt == 3) { AttackInt = 1; }
      else if (AttackInt < 3) { AttackInt++; }
    }

    Vector2 pp;
    void WolfAttack()
    { 
            Debug.Log("rotlqkf0");
            pp = new Vector3(transform.position.x + wolfRange.x * transform.localScale.x, transform.position.y + wolfRange.y);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, BiteRange, 0, AM.TargetLayer);
            for (int i = 0; i < hit.Length; i++)
            {
                Debug.Log("rotlqkf1");
                if (hit[i].tag == "Monster")
                {
                    Debug.Log("rotlqkf");
                    if (hit[i].GetComponent<Character>().Hp_Current < AM.WolfAP[CP.ActiveAbility.Enhance])
                    {
                        Debug.Log("rotlqkf3");
                        CP.Hp_Current++;
                    }
                    hit[i].GetComponent<Character>().Damage(AM.WolfAP[CP.ActiveAbility.Enhance], CP.UseApPostion);
                    hit[i].GetComponent<Character>().KnuckBack(transform, 1.5f, hit[i].GetComponent<Character>().IsBoss);

                }
            }

    }

      void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(pp, BiteRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(LH, LeftHand);
            Gizmos.color = Color.gray;
            Gizmos.DrawWireCube(RH, RightHand);
        }
}