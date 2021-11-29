using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSound : Sound_P
{
    public override void HitSound()
    {
        SoundManager.OneShot("HumanHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("Hammer");
    }

    public override void DieSound()
    {
        SoundManager.OneShot("HumanDie");
    }

    // Start is called before the first frame update

}
