using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfGage : MonoBehaviour
{
    // Start is called before the first frame update
    float wolfDashGage;
    float maxWolfDashGage;

    [SerializeField]
    private Image WolfDashBar;
    public float WolfDashGage
    {
        set
        {
            wolfDashGage = value;
            WolfDashBar.fillAmount =wolfDashGage /maxWolfDashGage;
        }
    }

    public float MaxWolfDashGage
    {
        set
        {
            maxWolfDashGage = value;
        }
    }
}
