using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCam : MonoBehaviour
{
    public Camera freeLookCamera;
    public Transform objectToRotate;
    public float rotationSpeed = 5.0f;

    private void Update()
    {
        // Get the input from the Cinemachine FreeLook camera on the Y-axis
        float yRotationInput = freeLookCamera.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue;

        // Adjust the object's rotation based on the camera movement
        RotateObject(yRotationInput);
    }

    void RotateObject(float input)
    {
        Vector3 currentRotation = objectToRotate.eulerAngles;
        float newRotation = currentRotation.y + input * rotationSpeed * Time.deltaTime;
        objectToRotate.rotation = Quaternion.Euler(currentRotation.x, newRotation, currentRotation.z);
    }
}
