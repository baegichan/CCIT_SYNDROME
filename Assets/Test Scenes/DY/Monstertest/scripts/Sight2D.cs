using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight2D : MonoBehaviour
{
    [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; //�þ߰��� ���� ����
    [SerializeField] private float m_viewRadius = 1f;  //Ž�� ����
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; //ȸ�� ��

    [SerializeField] private LayerMask m_viewTargetMask; //Ÿ�ٷ��̾�
    [SerializeField] private LayerMask m_viewObstacleMask; //�þ߸� ���θ��� ������Ʈ ���̾�


    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float m_horizontalViewHalfAngle = 0f;

    private void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
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

    public Collider2D[] FindViewTargets() //����� �ν��ϴ� �ڵ�
                                          //����� �ν��� �� 3�ܰ��� Ȯ�� ������ ���ľ� �մϴ�.
                                          //1. ���� �ν� ���� �ȿ� ���� ����� �ִ°�?
                                          //2. �ν� ���� �ȿ� ���� ����� ���� �þ߰� �ȿ� �ִ°�?
                                          //3. �þ߰� �ȿ� ���� ����� �� �� ���� ���� ���� ���ع��� �����ϴ°�?
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask); //���ȿ� ���� Ÿ���� ���� ���ϴ� Ÿ�ٸ� ���� ����

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            // float angle = Vector3.Angle(lookDir, dir)
            // �Ʒ� �� ���� ���� �ڵ�� �����ϰ� ������. ���� ������ ����
            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= m_horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask); //����� ������ �ִ� ������Ʈ�� �ִ��� Ȯ���ϴ� ����ĳ��Ʈ
                if (rayHitedTarget)
                {
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);

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

