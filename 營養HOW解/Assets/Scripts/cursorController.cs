using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorController : MonoBehaviour
{
    public Texture2D cursor,cursorClicked;
    private PlayerInputActions controls;
    private void Awake()
    {
        controls = new PlayerInputActions();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Start()
    {
        controls.UI.ClickForCursor.started += _ => StartedClick();
        controls.UI.ClickForCursor.performed += _ => EndedClick();
    }
    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }
    private void EndedClick()
    {
        ChangeCursor(cursor);
    }
    private void ChangeCursor(Texture2D cursorType)
    {
        // Vector2 hotspot = new Vector2(cursorType.width/2, cursorType.height/2);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }
}
