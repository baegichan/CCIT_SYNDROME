using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSound : Sound_P
{
    // Start is called before the first frame update
    public override void HitSound()
    {
        SoundManager.OneShot("RobotHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("DrillAtk");
    }

    public override void DieSound()
    {
        SoundManager.OneShot("RobotDie");
    }
}
