using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private EnemyAttack EnemyAttack;

    public Animator animator;

    void Start()
    {
        EnemyAttack = GetComponent<EnemyAttack>();

        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        animator.SetBool("Attack", false);
        
    }
}
