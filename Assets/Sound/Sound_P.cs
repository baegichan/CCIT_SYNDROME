using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sound_P : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void HitSound();
    public abstract void AttackSound();
    public abstract void DieSound();
   
}
