using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drillmonster : MonoBehaviour
{
    //Moveable
    public float Movespeed;
    public float Patrollspeed;
    public float Attackspeed;
    public Transform Groundcheck;

    //Attack
    public float Attackrange;
    public float Akcooltime;
    public float Akcurrenttime;
    public float Damege;
    public Transform Target;

    //Hp
    public float Hp;

    //PlayerCheck
    public bool hit;

    //References
    public Animator anim;

    //Sound
    public AudioClip Attacksound;
    public AudioClip Movesound;
    public AudioClip Findsound;
    public AudioClip Diesound;



    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Patroll()
    {
        
    }

    public void Move()
    {
        
    }

    public void Filp()
    {

    }

    public void Attack()
    {

    }

    public void idle()
    {

    }

    public void Die()
    {

    }
}
