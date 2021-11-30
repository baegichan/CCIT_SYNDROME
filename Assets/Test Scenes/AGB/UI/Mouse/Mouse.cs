using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    Texture2D Sword;
    [SerializeField]
    Texture2D Cursors;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(Cursors, Vector2.zero, CursorMode.ForceSoftware);
        
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(Cursors, Vector2.zero, CursorMode.ForceSoftware);

    }
    // Update is called once per frame

    private void OnMouseEnter()
    {
        Cursor.SetCursor(Cursors, Vector2.zero, CursorMode.ForceSoftware);
    }
 
}
