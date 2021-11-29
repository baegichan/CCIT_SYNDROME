using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVILSWORD : MonoBehaviour
{
    public int D;
    public GameObject Event;
    public Char_Parent CP;
    public AbilityManager AM;
    public Vector2 BoxSize;
    public LayerMask monsterlayer;
    public float ShakeT, ShakeF;
    public GameObject HitEffect;

    //public GameObject YourParent;

    public void EvilSwordAttack()
    {
        if (AM.E_Attack_State == true)
        {
            Collider2D[] hitEs = Physics2D.OverlapBoxAll(transform.position, BoxSize, 0, monsterlayer);
            foreach (Collider2D Current in hitEs)
            {
                CameraShake.Cam_instance.Shake(ShakeT, ShakeF);
                Current.GetComponent<Character>().Damage(AM.EvilAP[CP.ActiveAbility.Enhance], CP.UseApPostion, HitEffect);
                Current.GetComponent<Character>().KnuckBack(transform, 2.5f, Current.GetComponent<Character>().IsBoss);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, BoxSize);
    }
}
