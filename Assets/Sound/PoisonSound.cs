using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSound : Sound_P
{
    // Start is called before the first frame update
    public override void HitSound()
    {
        SoundManager.OneShot("HumanHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("Poison");
    }

    public override void DieSound()
    {
        SoundManager.OneShot("HumanDie");
    }
}
