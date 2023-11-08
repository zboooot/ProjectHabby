using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera currentCamera;

    private int currentCameraIndex = 0;

    // Update is called once per frame
    void Update()
    {
        SwitchCamera();
    }

    public void SwitchCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Disable the current virtual camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Move to the next camera or wrap around to the first one
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the new current virtual camera
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}


