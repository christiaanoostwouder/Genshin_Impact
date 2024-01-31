using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public float MaxHealth = 100f;
    public float CurrentHealth;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0 && !dead)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        GetComponent<AudioSource>().Stop();
        animator.SetTrigger("Die");
        GetComponent<ThirdPersonMovement>().enabled = false;
    }
}
