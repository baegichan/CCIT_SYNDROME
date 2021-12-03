using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXE : MonoBehaviour
{
    public int Damage;
    public static int Attack_int;
    public Char_Parent CP;
    public AbilityManager AM;
    public float KnuckBackForce;
    public GameObject HitEffect;
    public Vector3 Point;
    public Vector3 Size;
    public void AxeAttack()
    {
        Vector3 Axe = new Vector3(transform.position.x + Point.x * transform.localScale.x, transform.position.y + Point.y);
        Collider2D[] hitAxe = Physics2D.OverlapBoxAll(Axe, Size, 0);

        if (AbilityManager.A_Attack_State == true)
        {
            foreach (Collider2D Current in hitAxe)
            {
                if (Current.tag == "Monster")
                {
                    if (Attack_int <= 4)
                    {
                        Attack_int++;
                        Fourth(Current);
                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + Point.x * transform.localScale.x, transform.position.y + Point.y), Size);
    }
    private void Fourth(Collider2D col)
    {
        switch (Attack_int)
        {
            case 1:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                col.GetComponent<Character>().Damage(AM.AxeAP[CP.ActiveAbility.Enhance], CP.UseApPostion, HitEffect);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce, col.GetComponent<Character>().IsBoss);
                break;
            case 2:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                col.GetComponent<Character>().Damage(AM.AxeAP[CP.ActiveAbility.Enhance], CP.UseApPostion, HitEffect);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce, col.GetComponent<Character>().IsBoss);
                break;
            case 3:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                col.GetComponent<Character>().Damage(AM.AxeAP[CP.ActiveAbility.Enhance], CP.UseApPostion, HitEffect);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce, col.GetComponent<Character>().IsBoss);
                break;
            case 4:
                Debug.Log("aaaa");
                CameraShake.Cam_instance.Shake(0.12f, 0.8f);
                col.GetComponent<Character>().Damage(AM.AxeAP[CP.ActiveAbility.Enhance] + 20, CP.UseApPostion, HitEffect);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce + 5, col.GetComponent<Character>().IsBoss);
                break;
        }


        if (AbilityManager.A_Attack_State == false)
        {
            if (Attack_int == 5)
            {
                Debug.Log("초기화 현재:" + Attack_int);
                Attack_int = 0;
            }
        }
        if (AbilityManager.A_Attack_State == true)
        {
            if (Attack_int == 5)
            {
                Debug.Log("안초기화:" + Attack_int);
                Attack_int = 0;
            }
        }
    }
}