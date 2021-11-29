using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSound : Sound_P
{
    // Start is called before the first frame update
    public override void HitSound()
    {
        SoundManager.OneShot("InsectHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("MothAtk");
    }

    public override void DieSound()
    {
        SoundManager.OneShot("InsectDie");
    }
}
