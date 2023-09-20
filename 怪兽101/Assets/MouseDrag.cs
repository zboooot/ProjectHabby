using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{ 
private Vector3 ResetCamera;
private Vector3 Origin;
private Vector3 Diference;
private bool Drag = false;
void Start()
{
    ResetCamera = Camera.main.transform.position;
}
    void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            // Calculate the difference between the current mouse position and the camera's position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Diference = new Vector3(0f, mousePosition.y - Camera.main.transform.position.y, 0f);

            if (!Drag)
            {
                // Start the drag operation
                Drag = true;
                Origin = mousePosition;
            }
        }
        else
        {
            Drag = false;
        }

        if (Drag)
        {
            // Only update the Y component of the camera's position
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Origin.y - Diference.y, Camera.main.transform.position.z);
        }

        // RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = ResetCamera;
        }
    }
}