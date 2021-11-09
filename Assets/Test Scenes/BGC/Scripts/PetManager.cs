using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{

    
    public static PetManager Pet_Manager;
    [SerializeField]
    public PET CurrentPet;
    public void PMInstance()
    {
        if (Pet_Manager == null)
        {
            Pet_Manager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

  
    private void Start()
    {
        PMInstance();
    }

}