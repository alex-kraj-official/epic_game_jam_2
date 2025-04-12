using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private Texture2D clickCursor;
    [SerializeField] private Vector2 hotspot = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        SetNormalCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetClickCursor();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetNormalCursor();
        }
    }
    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor, hotspot, CursorMode.Auto);
    }

    public void SetClickCursor()
    {
        Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
    }
}
