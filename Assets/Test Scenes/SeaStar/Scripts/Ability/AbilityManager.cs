using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Camera cam;
    public GameObject py;
    public float WereWolf_Gauge = 0;
    Rigidbody2D rg;
    public LayerMask TargetLayer;
    public int RockAP;
    public float[] DoubleJumpP = { 5, 10, 15, 20 };
    public int[] ParaoAP = { 20, 25, 30, 35 };
    public int[] BoomAP = { 10, 13, 16, 20 };
    public int[] WolfAP = { 3, 4, 5, 6 };
    public int[] AxeAP = { 15, 18, 21, 23 };
    public int[] EvilAP = { 6, 9, 12, 15 };
    public int[] DarkSmokeAP = { 10, 15, 20, 30 };
    public int[] DarkSmokeExplosionAP = { 10, 13, 16, 20 };
    public int[] DarkSmokeMinus = { 50, 75, 100, 125 };
    public int[] BombAP = { 10, 15, 20, 30 };
    public GameObject PharaoHitEffect;
    public AudioSource AS;
    //마검
    public bool E_Attack_State = false;
    //
    //전투도끼
    public int A_Int;
    public static bool A_Attack_State = false;
    //

    public GameObject PharaoEffect;
    public Animator EA;
    public Char_Parent CP;

    void Start()
    {
        AS = GetComponent<AudioSource>();
    }

     void Update()
    {
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        EdenArm.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Werewolf()
    {
        if (Input.GetMouseButtonDown(1) && !CP.Ani.GetBool("Jump") && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            PlayerSkillUI.skill.CoolTime = Char_Parent.Active_Cool_Max;
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            CP.Ani.SetBool("CanIThis", false);
        }
    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            PlayerSkillUI.skill.CoolTime = Char_Parent.Active_Cool_Max;
            Char_Parent.Active_Cool = 0f;
            CP.PharaoWandSwitch();
            py.GetComponent<Char_Eden>().active = Pharao;
            CP.Ani.SetTrigger("Ability");
            CP.Ani.SetBool("Combat", true);
        }
    }

    public void Pharao()
    {
        Vector2 pp = py.transform.position;
        Vector2 ppp = new Vector2(py.transform.position.x, py.transform.position.y + 2f);

        Instantiate(PharaoEffect, ppp, Quaternion.identity);
        Physics2D.queriesStartInColliders = false;
        Collider2D[] MonsterCol = Physics2D.OverlapBoxAll(pp, new Vector2(7, 7), 0, TargetLayer);
        for (int i = 0; i < MonsterCol.Length; i++)
        {
            if (MonsterCol[i].tag == "Monster")
            {
                AS.PlayOneShot(SoundManager.instance.EFXs[4].Audio);
                MonsterCol[i].transform.GetComponent<Character>().Damage(ParaoAP[CP.ActiveAbility.Enhance], CP.UseApPostion, PharaoHitEffect);
                GetComponent<Character>().KnuckBack(transform, 0.8f, MonsterCol[i].transform.GetComponent<Character>().IsBoss);
            }
        }
    }

    public float ShieldCool;
    public void Ability_D()
    {
        if (ShieldCool > 0) { ShieldCool -= Time.deltaTime; }
        else if (ShieldCool <= 0) { CP.Shield = 0; }

        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            Vector2 pp = py.transform.position + new Vector3(0.5f, 0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "Monster")
                {
                    ShieldCool = 4;
                    if (i < 4)
                    {
                        CP.Shield += 10;
                    }
                    hit[i].GetComponent<Character>().Damage(RockAP, CP.UseApPostion);
                }
            }
        }
    }

    public void BattleAxe()
    {
        CP.Ani.SetInteger("AbilityNum", 4);
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            A_Attack();
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            py.GetComponent<Char_Eden>().P_CombatInt = 0;
            py.GetComponent<Char_Eden>().P_CombatTimer = 5;
        }
    }
    public void A_Attack()
    {
        CP.Ani.SetTrigger("Ability");
        CP.Ani.SetBool("CanIThis", false);
        CP.Ani.SetBool("Combat", true);
    }

    public void Ability_E()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EvilSword_Attack();
            if (Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
            {
                py.GetComponent<Char_Eden>().P_CombatInt = 0;
                EA.SetTrigger("First");
                EA.SetTrigger("Attack");
            }
            else if (Char_Parent.Active_Cool < Char_Parent.Active_Cool_Max)
            {
                EA.SetTrigger("Attack");
            }
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            py.GetComponent<Char_Eden>().P_CombatInt = 0;
            py.GetComponent<Char_Eden>().P_CombatTimer = 5;
        }
    }

    public void EvilSword_Attack()
    {
        CP.Ani.SetTrigger("Ability");
        CP.Ani.SetInteger("AbilityNum", 5);
        CP.Ani.SetBool("Combat", true);
    }
    public void Evilst()
    {
        CP.Ani.SetBool("CanIThis", false);
    }

    public void EvilRe()
    {
        CP.Ani.SetBool("CanIThis", true);
    }

    public GameObject Bomb;
    public float Bomb_Damage;
    public float Bomb_Speed;
    Vector2 pp;
    Vector2 CursorPos;
    Vector2 Dire;

    public void BomberMan()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            if (CP.SelectChar.transform.localScale.x == 1)
            {
                if (CP.Mouse.x <= CP.PlayerPosition.x)
                {
                    CP.Ani.SetBool("Turn", true);
                    CP.SelectChar.transform.localScale = new Vector3(CP.SelectChar.transform.localScale.x * -1, 1, 1);
                }
            }
            else
            {
                if (CP.Mouse.x > CP.PlayerPosition.x)
                {
                    CP.Ani.SetBool("Turn", true);
                    CP.SelectChar.transform.localScale = new Vector3(CP.SelectChar.transform.localScale.x * -1, 1, 1);
                }
            }

            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            pp = py.transform.position;
            CursorPos = Input.mousePosition;
            CursorPos = cam.ScreenToWorldPoint(CursorPos);
            Dire = CursorPos - pp;
            Dire = Dire.normalized;
        }
    }

    public void ThrowBomb()
    {
        GameObject Boom = Instantiate(Bomb, EdenArm.position, Quaternion.identity);
        Boom.gameObject.name = "Bomb";
        Rigidbody2D rg = Boom.GetComponent<Rigidbody2D>();
        rg.AddForce(Dire * Bomb_Speed * Time.deltaTime);
        BombObject BO = Boom.GetComponent<BombObject>();
        BO.parent = Char_Parent.ply.SelectChar;
        BO.AP = BombAP[CP.ActiveAbility.Enhance];
        CP.Ani.SetBool("Turn", false);
    }

    public GameObject DarkFog_Ball;
    public Transform EdenArm;
    public float B_Damage;
    public float B_Speed;
    public float[] Angle;
    public float MouseAngle;
    public Transform RightHand;
    public Vector2 MouseP;
    public Vector2 Point;
    public Vector2 Dir;
    public static bool isShoot = false;
    //검은안개능력
    public void BlackSmoke()
    {
        if(CP.Ani.GetBool("Jump") == false)
        {
            if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max && Char_Parent.ply.Ani.GetInteger("AbilityNum") == 9)
            {
                isShoot = true;
                CP.Ani.SetBool("CanIThis", false);
                if (CP.Ani.GetBool("Move")) { CP.Ani.SetBool("Move", false); }
                Char_Parent.Active_Cool = 0f;
                CP.Ani.SetInteger("AbilityNum", 9);
                MouseP = cam.ScreenToWorldPoint(Input.mousePosition);
                Dir = MouseP - Point;
                Dir = Dir.normalized;
                MouseAngle = Vector2.Angle(Vector2.up, MouseP);
                MousePosition();
            }
        }
    }
   
    public void ShootFog()
    {
        if(AbyssManager.abyss.Darkfog >= DarkSmokeMinus[Char_Parent.ply.ActiveAbility.Enhance])
        {
            isShoot = true;
            RightHand.localScale = new Vector3(1, 1, 1);
            Point = RightHand.position;
            GameObject BB = Instantiate(DarkFog_Ball, Point, Quaternion.identity);
            AbyssManager.abyss.Darkfog -= DarkSmokeMinus[Char_Parent.ply.ActiveAbility.Enhance];
            BB.GetComponent<Smoke_>().Dir = Dir;
            BB.GetComponent<Smoke_>().PP = Point;
            CP.Ani.SetBool("Combat", true);
        }
    }

    void MousePosition()
    {
        if (!CP.Ani.GetBool("CanIThis"))
        {
            if (MouseAngle <= Angle[0])
            {
                CP.Ani.SetTrigger("High");
                Height();
            }
            else if (MouseAngle > Angle[0] && MouseAngle <= Angle[1])
            {
                CP.Ani.SetTrigger("Middle");
                Height();
            }
            else if (MouseAngle > Angle[1] && MouseAngle <= Angle[2])
            {
                CP.Ani.SetTrigger("Low");
                Height();
            }
        }
    }

    void Height()
    {
        if (CP.SelectChar.transform.localScale.x == -1)
        {
            if (CP.Mouse.x <= CP.PlayerPosition.x)
            {
                CP.Ani.SetBool("Turn", false);
                CP.Ani.SetTrigger("Ability");
            }
            else if (CP.Mouse.x > CP.PlayerPosition.x)
            {
                CP.Ani.SetBool("Turn", true);
                CP.Ani.SetTrigger("Ability");
            }
        }
        else
        {
            if (CP.Mouse.x <= CP.PlayerPosition.x)
            {
                CP.Ani.SetBool("Turn", true);
                CP.Ani.SetTrigger("Ability");
            }
            else if (CP.Mouse.x > CP.PlayerPosition.x)
            {
                CP.Ani.SetBool("Turn", false);
                CP.Ani.SetTrigger("Ability");
            }
        }
    }

    public void Double_Jump()
    {

    }

    public void Change_Jump_int()
    {
        //pp = py.GetComponent<PlayerM_>();
        //if (pp.PassiveAbility.AbCode == 6) { pp.P_MaxJumpInt = 2; }
        //else { pp.P_MaxJumpInt = 1; }
    }
}
