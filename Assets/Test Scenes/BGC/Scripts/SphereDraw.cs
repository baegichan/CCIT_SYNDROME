using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDraw : MonoBehaviour
{

    public Color LineColor = new Color(1, 1, 1, 1);

    // Update is called once per frame
    private void OnDrawGizmos()
    {



        Gizmos.color = LineColor;
        Gizmos.DrawSphere(this.transform.position, 0.2f);

    }
}


