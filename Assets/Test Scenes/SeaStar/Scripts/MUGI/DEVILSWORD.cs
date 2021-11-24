using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVILSWORD : MonoBehaviour
{
    public int D;
    public GameObject Event;
    public GameObject YourParent;
    public Vector2 BoxSize;
    public LayerMask monsterlayer;
    public float ShakeT, ShakeF;

    //public GameObject YourParent;

    public void EvilSwordAttack()
    {
        if (YourParent.GetComponent<AbilityManager>().E_Attack_State == true)
        {
            Collider2D[] hitEs = Physics2D.OverlapBoxAll(transform.position, BoxSize, 0, monsterlayer);
            Debug.Log(hitEs.Length);
            foreach (Collider2D Current in hitEs)
            {
                CameraShake.Cam_instance.Shake(ShakeT, ShakeF);
                Current.GetComponent<Character>().Damage(D, YourParent.GetComponent<Char_Parent>().UseApPostion);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, BoxSize);
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    DEVILSWORD_ANI ese = Event.GetComponent<DEVILSWORD_ANI>();
    //    if (YourParent.GetComponent<AbilityManager>().E_Attack_State == true)
    //    {
    //        if (DEVILSWORD_ANI.Attack_int == 0)
    //        {
    //            if (col.tag == "Monster")
    //            {
    //                Current = col.gameObject;
    //                if (ese.hit.Count > 0)
    //                {
    //                    bool tt = true;
    //                    for (int i = 0; i < ese.hit.Count; i++)
    //                    {
    //                        if (ese.hit[i] == Current)
    //                        {
    //                            tt = false;
    //                        }
    //                    }
    //                    if (tt)
    //                    {
    //                        First();
    //                        ese.hit.Add(Current);
    //                    }
    //                }
    //                else
    //                {
    //                    First();
    //                    ese.hit.Add(Current);
    //                }
    //            }
    //        }
    //        if (DEVILSWORD_ANI.Attack_int == 1)
    //        {
    //            if (col.tag == "Monster")
    //            {
    //                CameraShake.Cam_instance.Shake(0.12f, 0.005f);
    //                Current = col.gameObject;
    //                Current.GetComponent<Character>().Damage(D, YourParent.GetComponent<Char_Parent>().UseApPostion);
    //            }
    //        }
    //        if (DEVILSWORD_ANI.Attack_int == 2)
    //        {
    //            if (col.tag == "Monster")
    //            {
    //                CameraShake.Cam_instance.Shake(0.5f, 0.09f);
    //                Current = col.gameObject;
    //                Current.GetComponent<Character>().Damage(D, YourParent.GetComponent<Char_Parent>().UseApPostion);

    //            }
    //        }
    //    }
    //}
}
