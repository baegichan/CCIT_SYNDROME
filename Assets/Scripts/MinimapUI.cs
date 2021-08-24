using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class MinimapUI : MonoBehaviour
{
    public GameObject defaultspriteobj;
    private GameObject spawnsprite;
    public enum ObjectState
    {
        Player,
        Monster,
        Item,
        Wall

    }
    // Start is called before the first frame update

    public ObjectState C_ObjectState;
    void Start()
    {

      // spawnsprite.transform.parent = this.gameObject.transform;
        switch(C_ObjectState)
        {
            case ObjectState.Player:
                defaultspriteobj.GetComponent<SpriteRenderer>().color = new Color(0, 254, 254);
                defaultspriteobj.layer = 7;
                spawnsprite = Instantiate(defaultspriteobj, this.gameObject.transform, this.gameObject.transform);
                spawnsprite.transform.position = new Vector3(transform.position.x, transform.position.y+2.6f, transform.position.z);
                defaultspriteobj.layer = 0;
                break;
            case ObjectState.Monster:
                defaultspriteobj.GetComponent<SpriteRenderer>().color = new Color(254, 0,  0);
                defaultspriteobj.layer = 8;
                spawnsprite = Instantiate(defaultspriteobj, this.gameObject.transform, this.gameObject.transform);
                spawnsprite.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
                defaultspriteobj.layer = 0;
                break;
            case ObjectState.Item:
                defaultspriteobj.GetComponent<SpriteRenderer>().color = new Color(255,255,0);
                defaultspriteobj.layer = 9;
                spawnsprite = Instantiate(defaultspriteobj, this.gameObject.transform, this.gameObject.transform);
                spawnsprite.transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z);
                defaultspriteobj.layer = 0;
                break;
            case ObjectState.Wall:
              //  defaultspriteobj.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
              //  spawnsprite = Instantiate(defaultspriteobj, this.gameObject.transform, this.gameObject.transform);
              //  defaultspriteobj.layer = 6;
                break;
            default:
               
                defaultspriteobj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                break;

        }
        if (C_ObjectState!=ObjectState.Wall)
        {
            spawnsprite.transform.localScale = new Vector3(2, 5, spawnsprite.transform.localScale.z);
        }
        else
        {
            //필요성을 못느낌
           // spawnsprite.transform.localScale = new Vector3(spawnsprite.transform.localScale.x, spawnsprite.transform.localScale.y, spawnsprite.transform.localScale.z);
        }
    }
    // Update is called once per frame
    void Update()
    {
       // 
    }
}
