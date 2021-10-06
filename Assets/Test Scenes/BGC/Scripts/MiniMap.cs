using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public int[,] Fake_MapData = new int[,] { {1,1,1,1,1 },{ 0,0,1,0,1},{ 0,1,1,1,1},{ 0,1,1,1,0},{1,1,0,0,0 } };
    int Level = 2;
    public int distance = 15;
    //레벨 2 로 생성시
    public GameObject Target;
    public GameObject Canvas;
    //나중에 리소스로 로드로 바꿔야됨
    private void Update()
    {
      if( Input.GetKeyDown(KeyCode.M))
        {
            //OnOff or Load 구현해야됨 지금은 테스트용
            Debug.Log(Fake_MapData[0, 0] + " 나오긴함");
            LoadMiniMap(Fake_MapData);

        }

    }
    public void LoadMiniMap(int[,] LoadMap)
    {
        int height=  Level*distance;
        int width= - Level*distance;
         for(int i = 0;i<Level*2+1;i++)
        {

            for (int j = 0; j < Level * 2 + 1; j++)
            {

                if(CheckMapData(LoadMap[i,j]))
                {
                    Instantiate(Target,new Vector3(width+j*distance,height+i*distance),Quaternion.identity,Canvas.transform).transform.localScale=(new Vector3(1,1,1));
                }
                    

            }
           height -=Level*distance;
           width = -Level *distance;

        }

    }
    public bool CheckMapData(int test)
    {
        if(test == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
