using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    public GameObject player;
    public GameObject Item;
    public bool IsPlayer;
    public GameObject Light;
    public Animator ani;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayer && player.GetComponentInParent<Char_Parent>().P_Money >= 10)
        {
            player.GetComponentInParent<Char_Parent>().P_Money -= 10;
            ani.SetTrigger("Gacha");
        }
    }

    void Anime_0()
    {

    }

    void Anime_1()
    {

    }

    void Anime_2()
    {

    }

    void Anime_3()
    {
        GameObject gacha = Instantiate(Item, transform.position, Quaternion.identity);
        gacha.GetComponent<AbilityItem>().IsBuy = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = col.gameObject;
        IsPlayer = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        IsPlayer = false;
    }
}
