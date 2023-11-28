using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movement : MonoBehaviour
{
    public float movementSpeed = 5;
    
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(input * Time.deltaTime * movementSpeed);
        
    }

}
