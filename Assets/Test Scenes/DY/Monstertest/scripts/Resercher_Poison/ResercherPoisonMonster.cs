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
    [Header("View Config")] //����� ����Ͽ� ���� �ʵ� �׷�ȭ
    //SerializeField �� ������ �ν����Ϳ����� ������ ���������� �ܺ� ��ũ��Ʈ���� ������ �Ұ����ϰ� �������� ����ߴ�. ���͸��� ������ ���� ������ �ֱ� ������ �̴� ������ �Ǹ� �ȵȴٰ� ����
    [SerializeField] private bool m_bDebugMode = false;

    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; //�þ߰��� ���� ����
    [SerializeField] private float m_viewRadius = 1f;  //Ž�� ����
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; //ȸ�� ��

    [SerializeField] private LayerMask m_viewTargetMask; //�� �� �ִ� Ÿ��
    [SerializeField] private LayerMask m_viewObstacleMask; //�þ߸� ���θ��� ������Ʈ ���̾�


    private List<Collider2D> hitedTargetContainer = new List<Collider2D>(); //���� Ÿ���� �����صδ� ����Ʈ

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
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;// 0.5f�� ���ϴ� ������ �츮�� ���� ������ �߾��� �ǰ� �� �κ����� �þ߰��� 10�̶�� -10, 10�̷��� �Ǿ�� �ϱ� �����̴�.
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



        Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);//(-�þ߰� + ȸ����)
        Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);//(�þ߰� + ȸ����)
        Vector3 lookDir = AngleToDirZ(m_viewRotateZ);//���� ����



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
        RaycastHit2D wallcheck = Physics2D.Raycast(wallCheck.position, Vector2.right, 0.3f); //�����ɽ�Ʈ�� ������ ���� Ȯ�� �ȴٸ� �ø� ��üũ �־�� ��
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

    private Vector3 AngleToDirZ(float angleInDegree)// �Է��� -180~180�� ���� Up Vector ���� Local Direction���� ��ȯ������.
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        //Angle�� eulerAngles.z�� ���ִ� ������ �Է��� Angle�� Local Direction���� ��ȯ �����ֱ� ���ؼ��Դϴ�.
        //Mathf.Deg2Rad : ������ �������� ��ȯ�ϴ�, ��ȯ ����� ��Ÿ���ϴ�. �� ���� (PI * 2) / 360 �Դϴ�.
        //���ϱⰡ �ƴ� ���⸦ �ϴ� ������ �츮�� ���� �Լ��� �ð� ����(������)�� ���, �ݽð� ����(����)�� ������� ���� ����� ������
        //����Ƽ���� Z���� �ð� ����(������)�� ����, �ݽð� ����(����)�� ����� �ݴ��̱� ������ ����(-)�� ���� ������ �����, ����� ������ ����� �������ִ� �� �Դϴ�.
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }


    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);//(-�þ߰� + ȸ����)
            Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);//(�þ߰� + ȸ����)
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);//���� ����

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);//������ �þ߰�
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);         //���� ����
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);//���� �þ߰�

            FindViewTargets();
        }
    }

    public Collider2D[] FindViewTargets() //����� �ν��ϴ� �ڵ�
                                          //����� �ν��� �� 3�ܰ��� Ȯ�� ������ ���ľ� �մϴ�.
                                          //1. ���� �ν� ���� �ȿ� ���� ����� �ִ°�?
                                          //2. �ν� ���� �ȿ� ���� ����� ���� �þ߰� �ȿ� �ִ°�?
                                          //3. �þ߰� �ȿ� ���� ����� �� �� ���� ���� ���� ���ع��� �����ϴ°�?
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask); //���� �νĹ��� �ȿ� ������ ����� �ִ��� Ȯ���ϰ� ���̾� ����ũ�� ���� ���ϴ� Ÿ�ٸ� ���� �����մϴ�

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position; //Ÿ���� ��ġ�� ���ͷ� �޴´�
            Vector2 dir = (targetPos - originPos).normalized;   //Ÿ���� ��ġ - ���� ��ġ
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);       //���¹���

            // float angle = Vector3.Angle(lookDir, dir)
            // �Ʒ� �� ���� ���� �ڵ�� �����ϰ� ������. ���� ������ ����
            float dot = Vector2.Dot(lookDir, dir); // �� ������ ������ ���Ѵ�
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg; //���� ������ ��ũ�ڻ��ο� �־ ���������� ��ȯ�����ָ� �þ߰��� �ٶ󺸰� �ִ� ���⿡�� Ÿ���� ��ġ������ ������ ���´�

            if (angle <= m_horizontalViewHalfAngle) //���� �þ߿� �ִٸ�
            {
                player = GameObject.FindGameObjectWithTag("Player");
                //player = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;//�÷��̾� �Ǻ� ��ġ Ʈ���� ������ ���
                Debug.Log(player + "�̻��� ������1");
                if (playerTransform == null)
                {
                    playerTransform = player.GetComponent<TestPlayer>().SelectChar.transform;//�÷��̾� �Ǻ� ��ġ Ʈ���� ������ ���
                }
                else
                    playerTransform = player.GetComponent<TestPlayer>().SelectChar.transform;//�÷��̾� �Ǻ� ��ġ Ʈ���� ������ ���
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask); //����� ������ �ִ� ������Ʈ�� �ִ��� Ȯ���ϴ� ����ĳ��Ʈ
                if (rayHitedTarget)
                {
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow); //�����ִٸ� ��� ����ĳ��Ʈ
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget); //���� Ÿ���� ����Ʈ�� ����

                    Targeton = true;

                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, targetPos, Color.red); //Ÿ���� �νĵǾ��ٸ� ���� ����ĳ��Ʈ
                }
            }
        }

        if (hitedTargetContainer.Count > 0) //Ÿ�ٵ鿡 ���� ��� Ȯ���� ��������
            return hitedTargetContainer.ToArray(); //������� �������
        else
            return null; //��� �ִٸ�
    }


}



