using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Power : MonoBehaviour// Made By 성준
{
 

    public int a;//플레이어 hp
    public int b;//몬스터가 입히는 데미지
   

    
    public void Update()
    {
        //지금은 Update로 돌리지만 바위처럼알약 먹었는지 if문으로 확인
        //if 문으로 몬스터로부터의 피해 감지
        int aa = a - (b - 50);//모든 몬스터에게 공격받는 데미지가 50 감소한다.
       
        if(aa >= 20)//줄어드는 데미지 고정값 20
        {
            aa = 20;
        }
        a = aa;
    }



}
