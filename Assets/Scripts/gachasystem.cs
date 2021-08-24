using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gachasystem : MonoBehaviour
{
    public List<Item> deck = new List<Item>();
    public int total = 0;
    public List<Item> result = new List<Item>();

    public void ResultSelect()
    {
        result.Add(RandomItem());
    }
    public Item RandomItem()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f)); //���������� ��ȯ

        for(int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if(selectNum <= weight)
            {
                Item temp = new Item(deck[i]);
                return temp;
            }
        }
        return null; // deck[Random.Range(0, deck.Count)];
    }

    
    
    void Start()
    {
        for(int i = 0; i < deck.Count; i++)
        {
            total += deck[i].weight;
        }
    }
}
//�÷��̾ �ݶ��̴��� ���´ٸ� ��ȣ�ۿ� ����
//RŰ�� �̱�
//�������� ��í ���� ��ø �ȵǰ�
//�����Ȱ� �Ҹ�
//�ִϸ��̼� ��í��� : ä�����ٰ� �ٽ� ������� �ִϸ��̼�
//�ִϸ��̼� �˾� : ���� �������� ���� ���� �ŷȴٰ� Ź ������ �ִϸ��̼�