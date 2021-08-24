using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGforDD : MonoBehaviour
{

    public float cooltime;
    public int damage;
    public GameObject GMG;
    public float cooltime_save;
    public PlayerHPUI HP;
    // Start is called before the first frame update
    void Start()
    {
        GMG = GameObject.Find("GameManager");
        HP = GMG.GetComponent<PlayerHPUI>();
    }
    //
    // Update is called once per frame
    void Update()
    {
        cooltime = Mathf.Clamp(cooltime -= Time.deltaTime, 0, cooltime_save);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player"&&cooltime==0)
        {
            HP.damaged(damage);
            GMG.GetComponent<Damagetextspawn>().damagespawner(collision.gameObject, damage);
            cooltime = cooltime_save;

        }
    }
}
