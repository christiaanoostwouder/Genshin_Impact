using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buoyancy : MonoBehaviour
{
    public float targetHeight = 3f;
    public float buoyancyForce = 5f;
    public float proportionalFactor = 0.5f;
    public float dampingFactor = 0.2f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        float heightDifference = targetHeight - transform.position.y;

        float proportionalForce = heightDifference * proportionalFactor;

        float dampingForce = rb.velocity.y * dampingFactor;

        float totalForce = buoyancyForce * (proportionalForce - dampingForce);

        rb.AddForce(Vector3.up * totalForce);
    }
}
