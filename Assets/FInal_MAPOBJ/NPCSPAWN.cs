using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSPAWN : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NPC;

    private void Start()
    {
        Instantiate(NPC, transform.position, Quaternion.identity,transform.parent);
    }
}
