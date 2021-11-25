using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Name : MonoBehaviour
{
    
    public Slider Name;
    public Character Cha;
    public GameObject Target;
    public Vector2 AdditionalPosition;
    public Unit_HPbar HPbar;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Target.transform.position + AdditionalPosition;
        if (HPbar.Hp_bar.value == 0)
            Destroy(gameObject);
    }

}
