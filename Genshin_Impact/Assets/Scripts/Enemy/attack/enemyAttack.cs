using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    private EnemyMovement enemymovement;
    public NavMeshAgent agent;

    public float attackCooldown = 10f;
    public float attackRange = 100f;
    private float nextAttackTime = 1f;

    public float RocketTime;
    private float timer = 3f; // Reduced the timer to 3 seconds

    private Transform player;
    public GameObject Rocket;
    public Transform RocketSpawn1;
    public Transform RocketSpawn2;

    private int rocketsToShoot = 7; // Number of rockets to shoot
    private int rocketsShot = 0; // Counter for the rockets shot

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        enemymovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= nextAttackTime && rocketsShot < rocketsToShoot)
            {
                StartCoroutine(ShootRockets());
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    IEnumerator ShootRockets()
    {
        agent.isStopped = true;

        float timeInterval = timer / rocketsToShoot;

        while (rocketsShot < rocketsToShoot && RocketTime < timer)
        {
            RocketTime += Time.deltaTime;

            if (RocketTime >= timeInterval * rocketsShot)
            {
                rocketsShot++; // Increment the counter when a rocket is shot

                // Alternate between two spawn points
                Transform currentSpawnPoint = (rocketsShot % 2 == 0) ? RocketSpawn1 : RocketSpawn2;

                GameObject RocketObj = Instantiate(Rocket, currentSpawnPoint.position, currentSpawnPoint.rotation) as GameObject;
                Rigidbody RocketRig = RocketObj.GetComponent<Rigidbody>();

                float speed = enemymovement.speed;

                RocketRig.AddForce(RocketObj.transform.forward * speed);
                Destroy(RocketObj, 3f); // Adjust the time the rocket stays before being destroyed
            }

            yield return null;
        }

        yield return new WaitForSeconds(4f); // Pause for 4 seconds

        // Reset the counter and allow the agent to move again after shooting all rockets
        rocketsShot = 0;
        RocketTime = 0f;
        agent.isStopped = false;
    }
}
