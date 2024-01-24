using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject playerCamera;
    public float rotationSpeed = 2;

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.Rotate(0, input.x * rotationSpeed, 0);

        playerCamera.transform.Rotate(-input.y * rotationSpeed, 0, 0);
    }
}
