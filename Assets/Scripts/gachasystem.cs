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

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f)); //정수형으로 변환

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
//플레이어가 콜라이더에 들어온다면 상호작용 가능
//R키로 뽑기
//아이템이 가챠 기계과 중첩 안되게
//검은안개 소모
//애니메이션 가챠기계 : 채워졌다가 다시 비워지는 애니메이션
//애니메이션 알약 : 땅에 떨어지면 깜빡 깜빡 거렸다가 탁 켜지는 애니메이션