using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_Power : MonoBehaviour// Made By ����
{
 

    public int a;//�÷��̾� hp
    public int b;//���Ͱ� ������ ������
   

    
    public void Update()
    {
        //������ Update�� �������� ����ó���˾� �Ծ����� if������ Ȯ��
        //if ������ ���ͷκ����� ���� ����
        int aa = a - (b - 50);//��� ���Ϳ��� ���ݹ޴� �������� 50 �����Ѵ�.
       
        if(aa >= 20)//�پ��� ������ ������ 20
        {
            aa = 20;
        }
        a = aa;
    }



}
