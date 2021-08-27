using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> AbList = new List<Ability>();
    
    public void Ability_A()
    {
        Debug.Log("A");
    }

    public void Ability_B()
    {
        Debug.Log("B");
    }

    public void Ability_C()
    {
        Debug.Log("C");
    }
}
