using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPOS : MonoBehaviour
{
    public static Vector2 MonPos;       //�����
    public static Vector2 PlayerPos;       //������

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������ ����");
            col.GetComponent<Player>().TestHp -= 1;

            Destroy();
        }
        else if (col.tag == "Platform")
        {
            Debug.Log("������ �ȹ���");

            Invoke("Destroy", 1f);
        }
    }

    void Destroy()
    {
        Destroy(this.gameObject);
    }

    public static Vector2 CalculateVelcoity(Vector2 target, Vector2 origin, float time)
    {
        //define the distance x and y first
        Vector2 distance = target - origin;
        Vector2 distanceXZ = distance; //x��z�� ����̸� �⺻������ �Ÿ��� ���� ����
        distanceXZ.y = 0f;//y�� 0���� ����

        //create a float the represent our distance
        float Sy = distance.y;//���� ������ �Ÿ��� ����
        float Sxz = distanceXZ.magnitude;

        //�ӵ� ���
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //������� ���� ������ �ʱ� �ӵ� ������ ���ο� ���͸� ����� ����
        Vector2 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
}
