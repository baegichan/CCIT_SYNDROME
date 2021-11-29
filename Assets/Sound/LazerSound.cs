using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSound : Sound_P
{
    public override void HitSound()
    {
        SoundManager.OneShot("RobotHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("LazerAtk");

    }

    public override void DieSound()
    {
        SoundManager.OneShot("RobotDie");
    }

    // Start is called before the first frame update
  
}
