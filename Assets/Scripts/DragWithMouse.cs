using UnityEngine;
using System.Collections;

public class DragWithMouse : MonoBehaviour {

    private Vector3 offset;

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(getMousePosition());
    }

    void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(getMousePosition()) + offset;
    }

    private Vector3 getMousePosition()
    {
        return new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }
}
