using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UImanager : MonoBehaviour
{
    public static UImanager UI;

    public int testgauge;
    public GameObject player;
    public GameObject ActiveSkill;
    public GameObject ActiveSkillcool;
    public GameObject PassiveSkill;
    public GameObject PassiveSkillcool;
    public GameObject Item1;
    public GameObject Item2;
    public GameObject petImage;
    private int item1_num;
    private int item2_num;
    public Sprite[] sprites;
    public Sprite[] petsprites;
   
    public Sprite[] Activeskillsprites;
    public Sprite[] Activeskillcoolsprites;
    public Sprite[] Passiveskillsprites;
    public GameObject[] chargesprites = new GameObject[10];
    public Sprite[] chargeImage = new Sprite[2];
    // Start is called before the first frame update
    private void Awake()
    {
        if (UI == null)
        {
            DontDestroyOnLoad(gameObject);
            UI = this;
        }
        else if (UI != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        findplayer();
        ActiveSkill = GameObject.Find("Activeskill");
        PassiveSkill = GameObject.Find("Passiveskill");
        petImage = GameObject.Find("petImage");
        Item1 = GameObject.Find("Item1");
        Item2 = GameObject.Find("Item2");
        for (int i =0; i<10;i++)
        {
            chargesprites[i] = ActiveSkill.transform.GetChild(i).gameObject;
            Debug.Log(chargesprites[i].name);
        }
    }
    void findplayer()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        Charge(testgauge);
    }
    /// <summary>
    /// input capsule object
    /// </summary>
    /// <param name="capsulecode"></param>
    void Get_Skill(GameObject capsule)
    {
        /*
        switch (capsule.GetComponents<capsule>().itemtype)
        { 
            case 0:
                 ActiveSkill.GetComponent<Image>().sprite = Sprites[1];
                ActiveSkillcool.GetComponent<Image>().sprite = 
                Activeskillsprites[capsule.GetComponents<capsule>().itemcode;

                  
                break;
            case 1:
                 PassiveSkill.GetComponent<Image>().sprite = Sprites[3];
                PassiveSkillcool.GetComponent<Image>().sprite = 
                PassiveSkill.GetComponent<Image>().sprite = Passiveskillsprites[capsule.GetComponents<capsule>().itemcode]
                break;
            case 2:
                ;
                 
        }
        */
    }
    public void Get_Item(GameObject item)
    {


        /*
        item.GetComponents<item>().itemcode
         switch()
        {
            case 0:
                item1_num+=1;
                checkitembox(0);
                break;
            case 1:
                item2_num+=1;
                checkitembox(1);
                break;
        }   

        */
    }
    void checkitembox(int itemindex)
    {
        switch(itemindex)
        {
            case 0:
                if(item1_num==0)
                {
                    Item1.GetComponent<Image>().sprite = sprites[5];
                }
                else
                {
                    Item2.GetComponent<Image>().sprite = sprites[6];
                }
                break;
            case 1:
                if(item2_num==0)
                {
                    Item2.GetComponent<Image>().sprite = sprites[5];
                }
                else
                {
                    Item2.GetComponent<Image>().sprite = sprites[6];
                }
                break;
        }
    }
    /// <summary>
    /// Active skill gauge change
    /// </summary>
    /// <param name="gauge"></param>
    public void Charge(int gauge)
    {

        for(int i=0; i<gauge;i++)
        {
            chargesprites[i].GetComponent<Image>().sprite = chargeImage[1];
        }
        //for문 굳이 안써도되는데 혹시모르니 남김
    }
    /// <summary>
    /// discharge 
    /// </summary>
    public void DisCharge()
    {
        for(int i =0; i<10; i++)
        {
            chargesprites[i].GetComponent<Image>().sprite = chargeImage[0];
        }
    }
    /// <summary>
    /// 0=active 1=passive 2=item1 3=item2
    /// </summary>
    /// <param name="type"></param>
    public void setskillui(int type)
    {

        switch(type)
        {
            case 0:
                GameObject.Find("Activeskill").GetComponent<Image>().sprite = sprites[1]; 
                break;
            case 1:
                GameObject.Find("Pasiveskill").GetComponent<Image>().sprite = sprites[4];
                break;
            case 2:
                GameObject.Find("Item1").GetComponent<Image>().sprite = sprites[6];
                break;
            case 3:
                GameObject.Find("Item2").GetComponent<Image>().sprite = sprites[6];
                break;
        }
    }
    /// <summary>
    /// input pet sprite code
    /// </summary>
    /// <param name="petcode"></param>
    void changepetsprite(int petcode)
    {
        petImage.GetComponent<Image>().sprite = petsprites[petcode];

    }
}
