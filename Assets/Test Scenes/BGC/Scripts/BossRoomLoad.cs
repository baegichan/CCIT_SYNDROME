using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
        if(Input.GetKeyDown(KeyCode.W))
        {
                Use_Scene_Change.Change_Boss_Scene();
            }
          
        }
    }
}
