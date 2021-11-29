using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuncherSound : Sound_P
{
    public override void HitSound()
    {
        SoundManager.OneShot("RobotHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("SequenceAtk");

    }

    public override void DieSound()
    {
        SoundManager.OneShot("RobotDie");
    }
}
