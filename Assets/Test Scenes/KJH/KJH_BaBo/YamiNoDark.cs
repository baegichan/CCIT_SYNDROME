using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YamiNoDark : MonoBehaviour
{
    bool isPlayer = false;
    bool isOpen = false;
    public GameObject Effect;
    Vector3 EffectPosition = new Vector2(0, -0.6f);

    void Awake()
    {
        GameObject effect = Instantiate(Effect, transform.position + EffectPosition, Quaternion.identity);
        effect.transform.parent = transform;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !isOpen && isPlayer)
        {
            isOpen = true;
            AbyssManager.abyss.Darkfog += 50;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") { isPlayer = true; }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { isPlayer = false; }
    }
}
