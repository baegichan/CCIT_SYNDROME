using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackenent : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other!=null)
        {
          
          if(other.tag =="enemy")
            {
                Debug.Log("�ù���¥");
                other.GetComponent<SpawnDamage>().damaged();
            }
        }
    }
}
