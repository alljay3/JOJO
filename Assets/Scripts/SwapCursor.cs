using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCursor : MonoBehaviour
{
    [SerializeField] private Texture2D TextureCursor;


    private void Start()
    {
        Cursor.SetCursor(TextureCursor, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = true;
    }
}
