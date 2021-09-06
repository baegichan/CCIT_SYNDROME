using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> AbList = new List<Ability>();
    public GameObject py;
    public GameObject Bomber;
    float WereWolf_Gauge = 0;

    public void Werewolf()
    {
        Debug.Log("´Á´ë´Ù! ¹«¼·Âî!!");

        if (Input.GetMouseButton(1))
        {
            WereWolf_Gauge += 1 * Time.deltaTime;
            Debug.Log("ÃæÀüÁß,,,,,");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            WereWolf_Gauge = 0;
            Debug.Log("Àú µ¹ ¸Í Áø !!!!!!@!@!@!!@!");
            Debug.Log("Àú µ¹ ¸Í Áø !!!@!@!@!!@!");
            Debug.Log("Àú µ¹ ¸Í Áø !!@!@@!@!!@!@!@!!@!");
            Debug.Log("Àú µ¹ ¸Í Áø @!@!!@!");
            Debug.Log("Àú µ¹ ¸Í Áø !@@@@@@@@!@!@!!@!");
        }

    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("ÆÄ¶ó¿À,,,,,,,");
        }
    }

    public void BomberMan()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("ÆøÅºÆøÅº");
            Vector3 pp = py.transform.position;
            GameObject Boom = Instantiate(Bomber, pp, Quaternion.identity);
            Boom.gameObject.name = "Bomb";
            Rigidbody2D rg = Boom.GetComponent<Rigidbody2D>();
            rg.AddForce(Vector3.up * 100000 * Time.deltaTime);
        }
    }

    public void Ability_D()
    {
        Debug.Log("D");
    }

    public void Ability_E()
    {
        Debug.Log("E");
    }

    public void Ability_F()
    {
        Debug.Log("F");
    }
    public void Double_Jump()
    {
        Debug.Log("G");
    }
}
