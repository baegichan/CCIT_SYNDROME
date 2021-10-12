using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MELEEGROUND : MonoBehaviour
 {
    //prameter
    public float speed;
    public float patrolSpeed;
    public float atkCooltime = 4;
    public float atkDelay;

    //refernce
    public Transform player;
    public Animator anim;
    public Vector2 first;
    public Vector2 boxSize;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform playerCheck;
    public Transform boxpos; 
    public Vector2 direction;
    public float distance;

    //turn state
    public bool filp;
    public bool patroll;
    public bool trace;
    public bool Targeton = false;
    //2D sight

    [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; //시야각을 담은 변수
    [SerializeField] private float m_viewRadius = 1f;  //탐지 범위
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; //회전 값

    [SerializeField] private LayerMask m_viewTargetMask; //타겟레이어
    [SerializeField] private LayerMask m_viewObstacleMask; //시야를 가로막는 오브젝트 레이어


    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float m_horizontalViewHalfAngle = 0f;

    private void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
    }

    void Start()
    {
        filp = true;
        patroll = true;
        trace = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (patroll == true)
        {
            Patroll();
        }
        //PlayerCheck();
        first = transform.position;
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;

    }

    public void Direction____monster(float target, float baseobj)//이름 다시 설정 필요
    {
        if (target < baseobj)
            anim.SetFloat("Direction", -1);
        else
            anim.SetFloat("Direction", 1);
    }

    public void ___Attack()//이름 다시 설정 필요
    {
        if (anim.GetFloat("Direction") == -1)
        {
            if (boxpos.localPosition.x > 0)
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
        }
        else
        {
            if (boxpos.localPosition.x < 0)
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x * 1), boxpos.localPosition.y);
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D col in collider2Ds)
        {
            if (col.tag == "Player")
            {
                Debug.Log("damage1");
            }
        }
    }

    public void Patroll()
    {
        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
        Filp();
    }

    public void Filp()
    {
        RaycastHit2D groundcheck = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);
        if (groundcheck.collider == false)
        {
            if (filp == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                filp = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                filp = true;
            }
        }
    }
    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }


    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);

            FindViewTargets();
        }
    }

    public Collider2D[] FindViewTargets() //대상을 인식하는 코드
                                          //대상의 인식은 총 3단계의 확인 과정을 거쳐야 합니다.
                                          //1. 나의 인식 범위 안에 들어온 대상이 있는가?
                                          //2. 인식 범위 안에 들어온 대상이 나의 시야각 안에 있는가?
                                          //3. 시야각 안에 들어온 대상을 볼 수 없게 가로 막는 장해물이 존재하는가?
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask); //원안에 들어온 타겟중 내가 원하는 타겟만 선별 가능

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            // float angle = Vector3.Angle(lookDir, dir)
            // 아래 두 줄은 위의 코드와 동일하게 동작함. 내부 구현도 동일
            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= m_horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask); //대상을 가리고 있는 오브젝트가 있는지 확인하는 레이캐스트
                if (rayHitedTarget)
                {
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);

                    Targeton = true;

                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, targetPos, Color.red);
                }
            }
        }

        if (hitedTargetContainer.Count > 0)
            return hitedTargetContainer.ToArray();
        else
            return null;
    }


}
