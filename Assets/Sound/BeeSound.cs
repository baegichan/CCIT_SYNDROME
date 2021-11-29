using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSound : Sound_P
{
    // Start is called before the first frame update
    public override void HitSound()
    {
        SoundManager.OneShot("InsectHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("BeeAtk");
        
    }

    public override void DieSound()
    {
        SoundManager.OneShot("InsectDie");
    }
}
