using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singlegacha : MonoBehaviour
{
    public List<GameObject> GachaList = new List<GameObject>();

    public float coolTime = 6f;
    public float currentTime;
    public Transform[] spawnPoints;
    public int maxGacha = 3;

    Animator animator;
    // Start is called before the first frame update
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
        if (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    public void Gacha()
    {
        animator.SetBool("startGacha", true);
        StartCoroutine(WaitGacha());
    }

    void aniend()
    {
        animator.SetBool("startGacha", false);
    }

    IEnumerator WaitGacha()
    {
        yield return new WaitForSeconds(1f);
        if (maxGacha == 0)
        {
            PlayerMovement.gacha = false;
        }
        else if (currentTime <= 0)
        {
            int rand = Random.Range(0, GachaList.Count);
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];
            print(GachaList[rand]);
            GachaList.RemoveAt(rand);
            GameObject Gacha = Instantiate((GachaList[rand]), spawnPoint.position, spawnPoint.rotation);
            maxGacha = maxGacha - 1;
            currentTime = coolTime;
            PlayerMovement.gacha = false;
        }
    }
}
