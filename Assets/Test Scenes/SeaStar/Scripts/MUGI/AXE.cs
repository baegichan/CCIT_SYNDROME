using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXE : MonoBehaviour
{
    public int Damage;
    public static int Attack_int;
    public GameObject YourParent;
    public float KnuckBackForce;

    public void AxeAttack()
    {
        Collider2D[] hitAxe = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.8f, 1), 0);

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
        Gizmos.DrawWireCube(transform.position, new Vector2(1.8f, 1));
    }
    private void Fourth(Collider2D col)
    {
        switch (Attack_int)
        {
            case 1:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                col.GetComponent<Character>().Damage(Damage, YourParent.GetComponent<Char_Parent>().UseApPostion);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce);
                break;
            case 2:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                col.GetComponent<Character>().Damage(Damage, YourParent.GetComponent<Char_Parent>().UseApPostion);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce);
                break;
            case 3:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                col.GetComponent<Character>().Damage(Damage, YourParent.GetComponent<Char_Parent>().UseApPostion);
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce);
                break;
            case 4:
                Debug.Log("aaaa");
                CameraShake.Cam_instance.Shake(0.12f, 0.8f);
                col.GetComponent<Character>().Damage(Damage + 60, YourParent.GetComponent<Char_Parent>().UseApPostion);//더미 플러스값 언제든 변경가능
                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce + 5);
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