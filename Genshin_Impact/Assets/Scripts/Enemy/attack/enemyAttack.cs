using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    
    private EnemyMovement enemymovement;
    public Animator animator;

    public float attackCooldown = 10f;
    public float attackRange = 100f;
    private float nextAttackTime = 1f;

    public float RocketTime;
    private float timer = 3f;

    private Transform player;
    public GameObject Rocket;
    public Transform RocketSpawn1;
    public Transform RocketSpawn2;

    private int rocketsToShoot = 7;
    private int rocketsShot = 0;

    // New variable to track whether the rocket attack is happening
    private bool isRocketAttack = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        
        enemymovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= nextAttackTime && rocketsShot < rocketsToShoot)
            {
                // Set isRocketAttack to true when starting the rocket attack
                animator.SetTrigger("Attack");
                isRocketAttack = true;
                StartCoroutine(ShootRockets());
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    IEnumerator ShootRockets()
    {

        
        float timeInterval = timer / rocketsToShoot;

        while (rocketsShot < rocketsToShoot && RocketTime < timer)
        {
            RocketTime += Time.deltaTime;

            if (RocketTime >= timeInterval * rocketsShot)
            {
                
                animator.SetBool("Idle", false);

                rocketsShot++;

                Transform currentSpawnPoint = (rocketsShot % 2 == 0) ? RocketSpawn1 : RocketSpawn2;

                GameObject RocketObj = Instantiate(Rocket, currentSpawnPoint.position, currentSpawnPoint.rotation) as GameObject;
                Rigidbody RocketRig = RocketObj.GetComponent<Rigidbody>();

                float speed = enemymovement.speed;

                RocketRig.AddForce(RocketObj.transform.forward * speed);
                
            }
            else
            {
                
                animator.SetBool("Idle", true);

            }

            yield return null;
        }

        yield return new WaitForSeconds(4f);

        rocketsShot = 0;
        RocketTime = 0f;
        

        // Set isRocketAttack to false when the rocket attack is completed
        isRocketAttack = false;
    }

    // Add a getter method for isRocketAttack
    public bool IsRocketAttack()
    {
        return isRocketAttack;
    }
}
