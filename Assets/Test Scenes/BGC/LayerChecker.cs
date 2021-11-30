using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    public bool isAbyssLayer;
    public bool isFieldObject;
    public void Start()
    {
        if(transform.parent.GetComponent<Layers>()!=null)
        {
        
        if(isAbyssLayer)
        {
                transform.parent.GetComponent<Layers>().AbyssLayer.Add(gameObject);
        }
        else
        {
                transform.parent.GetComponent<Layers>().NomalLayer.Add(gameObject);
            }
        }
    
    }

    private void OnEnable()
    {
        if (GetComponent<PlatformEffector2D>() != null)
        {
            GetComponent<PlatformEffector2D>().colliderMask = Physics2D.AllLayers;
        }
    }
}
