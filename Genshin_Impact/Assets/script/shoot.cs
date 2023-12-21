using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float damageTime;
    public GameObject prefab;
    public Camera virtualCamera;
    public float shootCooldown = 1f;
    public float shootTimer;
    private bool hasFired;

    public Transform invAim;
    public float TurnSpeed = 5f; 
    public float VerticalRotMin = -80f; 
    public float VerticalRotMax = 80f; 

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shootTimer <= 0f)
        {
            damageTime = 0;
            hasFired = false;
        }
        if (Input.GetMouseButton(0) && shootTimer <= 0f)
        {

            damageTime += 1 * Time.deltaTime;
            Debug.Log(damageTime.ToString());
        }
        if (Input.GetMouseButtonUp(0) && shootTimer <= 0f)
        {
            if (damageTime >= 3f && !hasFired)
            {
                fullPowerFire();
                hasFired = true;
            }
            else
            {
                fire();
            }

            Debug.Log("release");
            shootTimer = shootCooldown;
        }
    }

    private void FixedUpdate()
    {
        float rotInputX = Input.GetAxis("Mouse X");
        float rotInputY = Input.GetAxis("Mouse Y");

        Vector3 rot = transform.eulerAngles;
        rot.y += rotInputX * TurnSpeed;
        transform.rotation = Quaternion.Euler(rot);

        if (invAim != null)
        {
            rot = invAim.localEulerAngles;
            rot.x -= rotInputY * TurnSpeed;
            if (rot.x > 180)
                rot.x -= 360;
            rot.x = Mathf.Clamp(rot.x, VerticalRotMin, VerticalRotMax);
            invAim.localRotation = Quaternion.Euler(rot);
        }
    }

    void fullPowerFire()
    {
        Debug.Log("full power");

        fire();
    }

    void fire()
    {
        Debug.Log("je hebt geschoten");
        GameObject ob = Instantiate(prefab);
        Vector3 shootDirection = virtualCamera.transform.forward;
        ob.transform.position = transform.position;
        ob.transform.rotation = Quaternion.LookRotation(shootDirection); ;
        Destroy(ob, 3f);
    }

}