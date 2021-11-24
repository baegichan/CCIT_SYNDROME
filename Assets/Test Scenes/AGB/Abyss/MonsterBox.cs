using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBox : MonoBehaviour
{

    //int[] monsterId = new int[18];
    Queue<int> monsteridQue = new Queue<int>();
    Queue<Vector3> monsterPosQue = new Queue<Vector3>();
    //Vector3[] monsterPos = new Vector3[50];

    [Header("AbyssMonsterPrefab")]
    public GameObject[] AbyssMonsterPrefab;

    GameObject normalBox;
    GameObject abyssBox;

    


    // Start is called before the first frame update
    private void Awake()
    {
        abyssBox = transform.Find("AbyssMonsterP(Clone)").gameObject;
        normalBox = transform.Find("NormalMonsterP(Clone)").gameObject;
      
    }


    private void OnEnable()
    {
        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Reality)
        {
      
            abyssBox.SetActive(false);
            normalBox.SetActive(true);
        }
        else
        {
            abyssBox.SetActive(true);
            normalBox.SetActive(false);
        }
    }
    // Update is called once per frame
    #region 심연 몬스터 관련 함수

    private void Update()
    {
        if(AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss && !abyssBox.activeSelf )
        {
            abyssBox.SetActive(true);
            normalBox.SetActive(false);
        } 
        if(AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Reality && !normalBox.activeSelf )
        {
            abyssBox.SetActive(false);
            normalBox.SetActive(true);
        }
        
        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Abyss && monsteridQue.Count != 0)
           AbyssMonsterSpawn();




    }
    void AbyssMonsterSpawn()
     {
      
        for (int i = 0; i < monsteridQue.Count; i++)
        {
            var d = Instantiate(AbyssMonsterPrefab[monsteridQue.Dequeue()], monsterPosQue.Dequeue(), Quaternion.identity);
            d.transform.SetParent(abyssBox.transform, false);
        }
    }

    public void AbyssMonsterAdd(int id, Vector3 pos)
    {
        monsteridQue.Enqueue(id);
        monsterPosQue.Enqueue(pos);

    }
   

    #endregion

}
