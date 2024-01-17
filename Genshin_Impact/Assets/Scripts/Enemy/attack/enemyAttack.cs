using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public BulletRain bulletRain;
    public RocketAttack rocketAttack;
    public SkyBomb skyBomb;

    public float attackCooldown = 2f;
    public float attackRange = 3f;
    private float nextAttackTime = 0f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        

    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                doRandomAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

   

   private void doRandomAttack()
    {
        int randomAttack = Random.Range(1, 4);

        switch(randomAttack)
        {
            case 1:
                bulletRain.executeAttack();
                break;

            case 2:
                if (skyBomb != null)
                {

                }
                break;

            case 3:
                if(rocketAttack != null)
                {

                }
                break;
        }
        
    }

}        
       
        

    
        
    

