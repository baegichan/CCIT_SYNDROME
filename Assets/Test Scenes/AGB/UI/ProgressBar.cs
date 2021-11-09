using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar Instance // singlton     
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ProgressBar>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Gage");
                    instance = instanceContainer.AddComponent<ProgressBar>();
                }
            }
            return instance;
        }
    }
    private static ProgressBar instance;

    
    public Slider hpBar;
    public float maxHp;
    public float currentHp;

    public GameObject HpLineFolder;
    float unitHp = 200f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHp / maxHp;
    }

    public void GetHpBoost()
    {
        maxHp += 150;
        currentHp += 150;
        float scaleX = (1000f / unitHp) / (maxHp / unitHp);
        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);

        foreach (Transform child in HpLineFolder.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }

        HpLineFolder.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}