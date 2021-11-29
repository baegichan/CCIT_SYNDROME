using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BGM : Sound_P
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





 

    public void BossMove()
    {
        SoundManager.OneShot("BossMove");
    }

    public void BossEvasion()
    {
        SoundManager.OneShot("BossEvasion");
    }

    public void GrenadeThrow()
    {
        SoundManager.OneShot("GrenadeThrow");
    }

    public void GrenadeExplosion()
    {
        SoundManager.OneShot("GrenadeExplosion");
    }

    public void Swirl()
    {
        SoundManager.OneShot("Swirl");
    }

    public void Shield()
    {
        SoundManager.OneShot("Shield");
    }

    public void Smoke()
    {
        SoundManager.OneShot("Smoke");
    }

    public override void HitSound()
    {
        SoundManager.OneShot("BossHit");
    }

    public override void AttackSound()
    {
        SoundManager.OneShot("BossAtk");
    }

    public override void DieSound()
    {
        SoundManager.OneShot("BossDie");
    }
}
