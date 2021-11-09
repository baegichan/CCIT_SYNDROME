using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    public GameObject player;
    public GameObject Item;
    public bool IsPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsPlayer && player.GetComponentInParent<TestTest>().P_Money >= 10)
        {
            player.GetComponentInParent<TestTest>().P_Money -= 10;
            GameObject gacha = Instantiate(Item, transform.position, Quaternion.identity);
            gacha.GetComponent<AbilityItem>().IsBuy = true;
        }
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
