using UnityEngine;
using UnityEngine.InputSystem;   // New Input System

// Simple manager that lets you drag 2D objects with a Collider2D
public class DragManager : MonoBehaviour
{
    private Camera cam;          // Reference to the main camera
    private Collider2D selected; // The object we are currently dragging

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (cam == null || Mouse.current == null)
            return;

        // Mouse position in world space
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
        Vector2 mouse2D = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        // 1. Mouse pressed - pick object if exists
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            selected = Physics2D.OverlapPoint(mouse2D);
        }

        // 2. Dragging
        if (Mouse.current.leftButton.isPressed && selected != null)
        {
            mouseWorldPos.z = selected.transform.position.z; // keep Z
            selected.transform.position = mouseWorldPos;
        }

        // 3. Release
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            selected = null;
        }
    }
}
