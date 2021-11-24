using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResercherPoisonMonster : Character
{
    [Header("Prameter")]
    public float patrolSpeed;
    public float atkCooltime = 4;
    public float atkDelay;
    public int clupmonDamage;

    [Header("Refernce")]
    public GameObject player;
    public Transform playerTransform;
    public Animator anim;
    public Vector2 first;
    public Vector2 boxSize;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform wallCheck;
    public Vector2 direction;
    public Transform atkpos;
    public float distance;
    public GameObject ResercherPoisonBullet;

    [Header("Turn state")]
    public bool filp;
    public bool patroll;
    public bool trace;
    public bool Targeton = false;
    public bool Dead;
    public bool movable = false;
    private bool Online;
    //2D sight
    [Header("View Config")] //헤더를 사용하여 관련 필드 그룹화
    //SerializeField 쓴 이유는 인스펙터에선느 접근이 가능하지만 외부 스크립트에서 접근이 불가능하게 막으러고 사용했다. 몬스터마다 각각의 고유 범위가 있기 때문에 이는 참조가 되면 안된다고 생각
    [SerializeField] private bool m_bDebugMode = false;

    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; //시야각을 담은 변수
    [SerializeField] private float m_viewRadius = 1f;  //탐지 범위
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; //회전 값

    [SerializeField] private LayerMask m_viewTargetMask; //볼 수 있는 타겟
    [SerializeField] private LayerMask m_viewObstacleMask; //시야를 가로막는 오브젝트 레이어


    private List<Collider2D> hitedTargetContainer = new List<Collider2D>(); //맞은 타겟을 저장해두는 리스트

    private float m_horizontalViewHalfAngle = 0f;

    private void OnEnable()
    {
        playerTransform = null;
        Targeton = false;
        if (Online)
        {
            anim.SetFloat("Direction", 1);
        }

        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnDisable()
    {
        anim.SetFloat("Direction", 1);
        Vector2 current = transform.localScale;
        current.x = 1;
        transform.localScale = current;
    }

    private void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;// 0.5f을 곱하는 이유는 우리가 보는 각도가 중앙이 되고 그 부분으로 시야각이 10이라면 -10, 10이렇게 되어야 하기 때문이다.
    }

    void Start()
    {
        filp = true;
        patroll = true;
        trace = false;
        anim = GetComponent<Animator>();
        Physics.IgnoreLayerCollision(0, 0);
        Online = true;
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

        if (Hp_Current <= 0)
        {
            Dead = true;
            patroll = false;
            trace = false;
            anim.SetTrigger("Dead");
        }
        
    }

    private void FixedUpdate()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

        Vector3 originPos = transform.position;



        Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);//(-시야각 + 회전값)
        Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);//(시야각 + 회전값)
        Vector3 lookDir = AngleToDirZ(m_viewRotateZ);//보는 방향



        FindViewTargets();
    }

    public void DirectionResercherPoisonmonster(float target, float baseobj)
    {
        if (target < baseobj)
            anim.SetFloat("Direction", -1);
        else
            anim.SetFloat("Direction", 1);
    }

  

    public void ResercherPoisonAttack()
    {
        if (anim.GetFloat("Direction") == -1)
        {
            if (atkpos.localPosition.x > 0)
                atkpos.localPosition = new Vector2(atkpos.localPosition.x * -1, atkpos.localPosition.y);
        }
        else
        {
            if (atkpos.localPosition.x < 0)
                atkpos.localPosition = new Vector2(Mathf.Abs(atkpos.localPosition.x * 1), atkpos.localPosition.y);
        }
        Instantiate(ResercherPoisonBullet, atkpos.transform.position, Quaternion.identity);
    }

    public void Patroll()
    {
        //transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
        Filp();
    }

    public void ResercherPoisonDestroy()
    {

        Destroy(gameObject);
    }

    public void Movetrue()
    {
        movable = true;
    }

    public void Movefalse()
    {
        movable = false;
    }

    public void Filp()
    {
        RaycastHit2D wallcheck = Physics2D.Raycast(wallCheck.position, Vector2.right, 0.3f); //레이케스트를 옆으로 쏴서 확인 된다면 플립 벽체크 넣어야 됨
        if (wallcheck.collider != null)
        {
            if (wallcheck.collider.CompareTag("Wall") == true)
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

        RaycastHit2D groundcheck = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f);

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

    private Vector3 AngleToDirZ(float angleInDegree)// 입력한 -180~180의 값을 Up Vector 기준 Local Direction으로 변환시켜줌.
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        //Angle에 eulerAngles.z를 빼주는 이유는 입력한 Angle을 Local Direction으로 변환 시켜주기 위해서입니다.
        //Mathf.Deg2Rad : 각도를 라디안으로 변환하는, 변환 상수를 나타냅니다. 이 값은 (PI * 2) / 360 입니다.
        //더하기가 아닌 빼기를 하는 이유는 우리가 만든 함수는 시계 방향(오른쪽)이 양수, 반시계 방향(왼쪽)이 음수라고 보고 계산을 하지만
        //유니티에서 Z축은 시계 방향(오른쪽)이 음수, 반시계 방향(왼쪽)이 양수로 반대이기 때문에 빼기(-)를 통해 음수를 양수로, 양수를 음수로 만들어 연산해주는 것 입니다.
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }


    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);//(-시야각 + 회전값)
            Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);//(시야각 + 회전값)
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);//보는 방향

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);//오른쪽 시야각
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);         //보는 방향
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);//왼쪽 시야각

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
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask); //원안 인식범위 안에 들어오는 대상이 있는지 확인하고 레이어 마스크를 통해 원하는 타겟만 선별 가능합니다

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position; //타겟의 위치를 벡터로 받는다
            Vector2 dir = (targetPos - originPos).normalized;   //타겟의 위치 - 현재 위치
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);       //보는방향

            // float angle = Vector3.Angle(lookDir, dir)
            // 아래 두 줄은 위의 코드와 동일하게 동작함. 내부 구현도 동일
            float dot = Vector2.Dot(lookDir, dir); // 두 벡터의 내적을 구한다
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg; //구한 내적을 아크코사인에 넣어서 각도값으로 변환시켜주면 시야각이 바라보고 있는 방향에서 타켓의 위치까지의 각도가 나온다

            if (angle <= m_horizontalViewHalfAngle) //나의 시야에 있다면
            {
                player = GameObject.FindGameObjectWithTag("Player");
                //player = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;//플레이어 피봇 위치 트러짐 떄문에 사용
                Debug.Log(player + "이새끼 때문임1");
                if (playerTransform == null)
                {
                    playerTransform = player.GetComponent<TestPlayer>().SelectChar.transform;//플레이어 피봇 위치 트러짐 떄문에 사용
                }
                else
                    playerTransform = player.GetComponent<TestPlayer>().SelectChar.transform;//플레이어 피봇 위치 트러짐 떄문에 사용
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask); //대상을 가리고 있는 오브젝트가 있는지 확인하는 레이캐스트
                if (rayHitedTarget)
                {
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow); //막혀있다면 노란 레이캐스트
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget); //맞은 타겟을 리스트에 저장

                    Targeton = true;

                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, targetPos, Color.red); //타켓이 인식되었다면 빨간 레이캐스트
                }
            }
        }

        if (hitedTargetContainer.Count > 0) //타겟들에 대한 모든 확인이 끝났을때
            return hitedTargetContainer.ToArray(); //비어있지 않은경우
        else
            return null; //비어 있다면
    }


}



