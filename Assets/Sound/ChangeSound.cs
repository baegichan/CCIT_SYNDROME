using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSound : Sound_P
{
    public override void AttackSound()
    {
        throw new System.NotImplementedException();
    }

    public override void DieSound()
    {
        throw new System.NotImplementedException();
    }

    public override void HitSound()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    public void AbyssChangeSound()
    {
        SoundManager.OneShot("WorldTransfer");
    }
}

