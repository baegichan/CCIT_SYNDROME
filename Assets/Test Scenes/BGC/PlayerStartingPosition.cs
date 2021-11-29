using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartingPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D ChaRigid;
    private void Start()
    {

        StartCoroutine(Delay());

    }
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.9f);
        Transform StartingPosition = MapManager.s_Instace.map[(int)MapManager.s_Instace.Level, (int)MapManager.s_Instace.Level].transform.Find("NPC");
        transform.position = new Vector3 (StartingPosition.position.x, StartingPosition.position.y+2,3);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
