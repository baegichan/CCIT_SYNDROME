using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    public GameObject player;
    public GameObject Item;
    public bool IsPlayer;
    public GameObject lht;
    public GameObject Particle;
    public Vector3 Item_Pos;
    public Vector3 Particle_Pos;
    public Animator ani;
    public bool IsUse;

    void Update()
    {
        if (!IsUse && Input.GetKeyDown(KeyCode.F) && IsPlayer && AbyssManager.abyss.Darkfog >= 10)
        {
            IsUse = true;
            AbyssManager.abyss.Darkfog -= 10;
            ani.SetTrigger("Gacha");
        }
    }

    void SpawnItem()
    {
        GameObject gacha = Instantiate(Item, Item_Pos + transform.position, Quaternion.identity);
        GameObject particle = Instantiate(Particle, Particle_Pos + transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.gameObject;
        IsPlayer = true;
        lht.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        IsPlayer = false;
        lht.SetActive(false);
    }
}
