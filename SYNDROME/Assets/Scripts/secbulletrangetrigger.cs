/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secbulletrangetrigger : MonoBehaviour
{
    public Collider2D sec;
    List<GameObject> monsterList;
    List<GameObject> monsterlist = new List<GameObject>();
    

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D sec)
    {
        if(petbullet.col.tag == "enemy")
        { 
        if (sec.CompareTag ("enemy"))
        {
            
            monsterList = new List<GameObject>(monsterlist);
            Debug.Log(monsterList.Count);
            Debug.Log(petbullet.col.gameObject.name);
            // ���⼭ �ݶ��̴��� ������ ���� �±׸� ���� �ֵ��� ����Ʈ�� �޾ƿ�
        }
        if(sec.CompareTag("enemy"))
        {
            monsterList.Add(sec.gameObject);
            Debug.Log(sec.gameObject.name);
        }
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D sec)
    {
        if(sec.CompareTag("enemy"))
        {
            monsterList.Remove(sec.gameObject);
        }    
    }
    */
/*
    void Start()
    {
        
        Collider2D sec = GetComponent<Collider2D>();
         //ontrigger2d ����ؾ� �ɰ� ����
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

   
}
*/