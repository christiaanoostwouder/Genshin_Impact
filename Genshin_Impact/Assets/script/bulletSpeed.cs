using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpeed : MonoBehaviour
{
    public float speed = 500f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rb.transform.forward * speed * Time.fixedDeltaTime;
    }
}
