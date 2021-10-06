using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Monster Set", menuName = "SYNDROME_MAP/Monster_set")]
public class MonsterSet : ScriptableObject
{
    public GameObject[] Monster;
    public GameObject Get_Random_Monster()
    {
        int Return_OBJ = Random.Range(0,Monster.Length);
        return Monster[Return_OBJ];
    }
    public GameObject Get_Monster(int index)
    {
        return Monster[index];
    }
    public GameObject Get_Monster(string name)
    {
        for(int i = 0;i<Monster.Length;i++)
        {
            if(Monster[i].name==name)
            {
                return Monster[i];  
            }
     
        }
        return null;
    }
    public string Get_Monster_Name(int index)
    {
        return Monster[index].name;
    }
   public int Get_Monster_Index(string Monstername)
    {
        for(int i =0; i<Monster.Length;i++)
        {
            if(Monster[i].name==Monstername)
            {
                return i;
            }
            else
            {
                //¾øÀ½
           
            }
        }
        return -1;
    }
   public void Monster_Spawn(Transform Target,int MonsterIndex)
    {
        Instantiate(Monster[MonsterIndex], Target.position, Quaternion.identity, Target);
    }
    public void Monster_Spawn(Transform Target,string MonsterName)
    {
        Instantiate( Get_Monster(MonsterName),Target.position,Quaternion.identity,Target);
    }
   
}
