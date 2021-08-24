using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPOS : MonoBehaviour
{
    public static Vector2 MonPos;       //출발점
    public static Vector2 PlayerPos;       //도착점

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("데미지 받음");
            col.GetComponent<Player>().TestHp -= 1;

            Destroy();
        }
        else if (col.tag == "Platform")
        {
            Debug.Log("데미지 안받음");

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
        Vector2 distanceXZ = distance; //x와z의 평면이면 기본적으로 거리와 같은 벡터
        distanceXZ.y = 0f;//y는 0으로 설정

        //create a float the represent our distance
        float Sy = distance.y;//세로 높이의 거리를 지정
        float Sxz = distanceXZ.magnitude;

        //속도 계산
        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        //계산으로 인해 두축의 초기 속도 가지고 새로운 벡터를 만들수 있음
        Vector2 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;
        return result;
    }
}
