using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSound : Sound_P
{
    // Start is called before the first frame update
    public override void HitSound()
    {
        SoundManager.OneShot("RobotHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("SwordAtk");
    }

    public override void DieSound()
    {
        SoundManager.OneShot("RobotDie");
    }
}
