using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyUpper : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        transform.parent.GetComponent<Room_data>().MapPoly = GetComponent<PolygonCollider2D>();
    }
}
