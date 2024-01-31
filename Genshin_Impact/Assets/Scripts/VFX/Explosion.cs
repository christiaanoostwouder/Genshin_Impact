using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Use Physics.OverlapSphere to find colliders within a sphere's radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2);

        // Iterate through the colliders and perform actions for each collision
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(10f);
            }
        }
            Destroy(gameObject, 20f);
    }
}
