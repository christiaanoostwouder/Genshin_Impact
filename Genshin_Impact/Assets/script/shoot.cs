using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float damageTime;
    public float damage;
    public GameObject prefab;
    public Camera virtualCamera;
    [SerializeField] public float shootCooldown = 1f;
    public float shootTimer;
    private bool hasFired;

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