using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform bow;
    [SerializeField] private Transform cam;

    [Header("Damage")]
    [SerializeField] private float baseDmg;
    [SerializeField] private float maxDmgMult;
    private float dmgMult;

    [Header("Reload")]
    [SerializeField] private float coolDown;
    private bool canFire = true;

    // Update is called once per frame
    void Update()
    {
        if (CameraSwitch.shootingMode == true)
        {
            if (dmgMult! >= maxDmgMult)
            {
                dmgMult += 1 * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) && canFire)
            {
                Fire(baseDmg * dmgMult);
            }
        }
    }

    void Fire(float damage)
    {
        Debug.Log("je hebt geschoten");
        Vector3 shootDirection = cam.forward;
        GameObject ob = Instantiate(arrow, bow.position, Quaternion.LookRotation(shootDirection));
        ob.GetComponent<Arrow>().dmg = damage;
        dmgMult = 0;
        canFire = false;
        Invoke("ChangeFire", coolDown);
    }

    private bool ChangeFire()
    {
        return canFire = !canFire;
    }
}