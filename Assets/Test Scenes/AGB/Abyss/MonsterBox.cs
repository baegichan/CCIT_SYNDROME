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
 

    bool isclear = false;
    bool isMonsterMap = false;

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
    #region �ɿ� ���� ���� �Լ�

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

        if(abyssBox.transform.childCount == 0 && normalBox.transform.childCount == 0 && !isclear && monsteridQue.Count ==0 && isMonsterMap)
        {
            isclear = true;
         //   RoomClearManager.clear.RoomClear();
        }


    }

    void AbyssMonsterSpawn()
     {
      
        for (int i = 0; i < monsteridQue.Count; i++)
        {
            var d = Instantiate(AbyssMonsterPrefab[monsteridQue.Dequeue()], monsterPosQue.Dequeue(), Quaternion.identity, abyssBox.transform);
          //  d.transform.SetParent(abyssBox.transform, false);
        }
    }

    public void AbyssMonsterAdd(int id, Vector3 pos)
    {
        monsteridQue.Enqueue(id);
        monsterPosQue.Enqueue(pos);

    }
   
    public bool MonsterMap
    {
        set
        {
            isMonsterMap = value;
        }
        get
        {
            return isMonsterMap;
        }
    }
    #endregion

}
