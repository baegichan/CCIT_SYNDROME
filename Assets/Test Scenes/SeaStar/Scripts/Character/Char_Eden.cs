using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Eden : MonoBehaviour
{
    //Test
    public int HP;
    public int DP;
    public Char_Parent CP;
    //
    [Header("공격")]
    public bool P_Attack_State = false;
    public float P_CombatTimer = 5;
    public float P_CombatInt;
    public GameObject HitEffect;
    [Tooltip("플레이어 공격시 앞으로 움직여지는 변수")]
    public float P_AttackMoveInt;
    //
    [Header("대쉬")]
    public float P_DashForce;
    public float P_DashInt = 1;
    public float P_DashTimer = 5;
    int AnimeInt = 1;
    //
    [Header("기본공격 이팩트")]
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    [Header("도끼공격 이팩트")]
    public GameObject A_One;
    public GameObject A_Two;
    public GameObject A_Three;

    [Header("라이트 리스트")]
    public GameObject PharaoLight;
    public GameObject SwordLight;
    public GameObject AxeLight;

    AudioSource AS;
    public Animator Ani;
    Rigidbody2D rigid;
    public LayerMask layerMask;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        Ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        BAI();
        //GroundCheck();
        DownPlatform();
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Char_Parent.ShopOn == false)
        {
            if (CP.Ani.GetBool("Jump") == false)
            {
                Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 0) * (P_AttackMoveInt * 5), ForceMode2D.Impulse);
            }

            Ani.SetTrigger("Attack");
            Ani.SetBool("Combat", true);
            if (CP.Ani.GetBool("Jump") == true) { Ani.SetBool("CanIThis", true); }
            else { Ani.SetBool("CanIThis", false); }
            P_CombatTimer = 5;
            P_CombatInt = 1;
            P_Attack_State = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            P_CombatInt = 0;
        }
        if (P_CombatInt <= 0)
        {
            P_CombatTimer -= Time.deltaTime;
            if (P_CombatTimer <= 0)
            {
                P_Attack_State = false;
                Ani.SetBool("Combat", false);
            }
        }
    }

    public Vector3 SwordPoint;
    public Vector3 Boundary;

    void SwordAttack()
    {
        Vector3 sp = new Vector3(transform.position.x + SwordPoint.x * transform.localScale.x, transform.position.y + SwordPoint.y);
        Collider2D[] hit = Physics2D.OverlapBoxAll(sp, Boundary, 0);

        if (P_Attack_State == true)
        {
            foreach (Collider2D Current in hit)
            {
                if (Current.tag == "Monster")
                {
                    if(!Ani.GetBool("Jump"))
                    {
                        Boundary = new Vector2(2, 1.3f);
                        CameraShake.Cam_instance.CameraShake_Cinemachine(0.1f, 2f);
                        Current.GetComponent<Character>().Damage(CP.AP, CP.UseApPostion, HitEffect);
                        Current.GetComponent<Character>().KnuckBack(transform, 2.5f, Current.GetComponent<Character>().IsBoss);
                    }
                    if(Ani.GetBool("Jump"))
                    {
                        Boundary = new Vector2(3, 1.5f);
                        CameraShake.Cam_instance.CameraShake_Cinemachine(0.1f, 2f);
                        Current.GetComponent<Character>().Damage(CP.AP + 10, CP.UseApPostion, HitEffect);
                        
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + SwordPoint.x * transform.localScale.x, transform.position.y + SwordPoint.y), Boundary);
    }

    void AttackEffect(GameObject Effect)
    {
        if (Effect.activeSelf) { Effect.SetActive(false); }
        Effect.SetActive(true);
    }

    void SwordAttackEffect1() { AttackEffect(One); AS.PlayOneShot(SoundManager.instance.EFXs[0].Audio); }
    void SwordAttackEffect2() { AttackEffect(Two); AS.PlayOneShot(SoundManager.instance.EFXs[1].Audio); }
    void SwordAttackEffect3() { AttackEffect(Three); Two.SetActive(false); AS.PlayOneShot(SoundManager.instance.EFXs[2].Audio); }
    void AxeAttackEffect1() { AttackEffect(A_One); AS.PlayOneShot(SoundManager.instance.EFXs[3].Audio); }
    void AxeAttackEffect2() { AttackEffect(A_Two); AS.PlayOneShot(SoundManager.instance.EFXs[3].Audio); }
    void AxeAttackEffect3() { AttackEffect(A_Three); A_Two.SetActive(false); AS.PlayOneShot(SoundManager.instance.EFXs[3].Audio); }
    void SwordLightOn() { SwordLight.SetActive(true); }
    void SwordLightOff() { SwordLight.SetActive(false); }
    void AxeLightOn() { AxeLight.SetActive(true); }
    void AxeLightOff() { AxeLight.SetActive(false); }
 
    public void Dash()
    {
        if (Ani.GetBool("Jump") == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (P_DashTimer >= 5)
                {
                    AS.PlayOneShot(SoundManager.instance.EFXs[15].Audio);
                    P_DashTimer = 0;
                    Ani.SetBool("Dash", true);
                    Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 0.1f) * P_DashForce * 2);
                    Char_Parent.rigid.velocity = new Vector2(0, 0);
                }
            }
        }
        if (P_DashTimer < 5)
        {
            P_DashTimer += Time.deltaTime;
        }
    }

    //void GroundCheck()
    //{
    //    RaycastHit2D Ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.05f), Vector2.down, CP.RayDistance);
    //    Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.05f), Vector2.down * CP.RayDistance, Color.blue);
    //    if (Ground.collider.gameObject.tag == "Ground")
    //    {
    //        Ani.SetBool("Jump", false);
    //        CP.P_JumpInt = CP.P_MaxJumpInt;
    //    }
    //}

    public void PharaoWandSwitch()
    {
        if (CP.PharaoWand_Senaka.activeSelf) { CP.PharaoWand_Senaka.SetActive(false); }
        else { CP.PharaoWand_Senaka.SetActive(true); }
    }

    public delegate void Active();
    public Active active;
    
    public void UseActive()
    {
        active();
    }
    public void CanIThisOn()
    {
        Ani.SetBool("CanIThis", true);
    }
    public void CanIThisOff()
    {
        Ani.SetBool("CanIThis", false);
    }
    void DashE()
    {
        Ani.SetBool("Dash", false);
    }
    void AttackStart()
    {
        P_Attack_State = true;
    }
    void AttackEnd()
    {
        P_Attack_State = false;
    }
    ///

    /// <전투도끼>
    void A_int1()
    {
        AXE.Attack_int = 0;
    }
    void A_int2()
    {
        AXE.Attack_int = 1;

    }
    void A_int3()
    {
        AXE.Attack_int = 2;
    }
    void A_int4()
    {
        AXE.Attack_int = 3;
    }
    void BattleAxeEvent()
    {
        AbilityManager.A_Attack_State = false;
    }

    void CanMoving()
    {
        CP.Ani.SetBool("CanIThis", true);
    }

    void BattleAxeAttackEvent()
    {
        AbilityManager.A_Attack_State = true;
        CP.Ani.SetBool("CanIThis", false);
    }

    void OnBattleAxeSwith()
    {
        CP.OnBattleAxe();
    }

    void OffBattleAxeSwith()
    {
        CP.OffBattleAxe();
    }

    void BattleAxeintInitalization()
    {
       //BAX.GetComponent<BattleAxeAttack>().Attack_int = 0;
        BattleAxeAttack.Attack_int = 0;
        Char_Parent.Active_Cool = 0f;
    }

    void TestBa()
    {
        if(BattleAxeAttack.Attack_int >= 5)
        {
            BattleAxeAttack.Attack_int = 1;
        }
    }

    void BAI()//Battle Axe Attack Int <-- 기계도끼 애니메이션
    {
       Ani.SetInteger("BAI", BattleAxeAttack.Attack_int);
    }
    
    void AxeAttack()
    {
        CP.BattleAxe.GetComponent<AXE>().AxeAttack();
    }
    void Transformation_Wolf()
    {
        CP.DecideChar();
    }

    void SlowTime()
    {
        Time.timeScale = 0.1f;
    }

    void NormalTime()
    {
        Time.timeScale = 1f;
    }

    void PharaoOff()
    {
        if (PharaoLight.activeSelf) { PharaoLight.SetActive(false); }
        else { PharaoLight.SetActive(true); }
    }

    void Fail()
    {
        GameResultManager.result.Abilty(CP.AbilityHistory);
        GameResultManager.result.ShowResult(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        CP.pf = col.transform.GetComponent<PlatformEffector2D>();
    }

    void DownPlatform()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("다운플랫폼 ");
            CP.pf.colliderMask = layerMask;
            Invoke("AllLayerPlatform", 0.6f);
        }
    }

    void AllLayerPlatform()
    {
        Debug.Log("다운플랫폼 초기화 ");
        CP.pf.colliderMask = Physics.AllLayers;
        CP.pf = null;
    }
}