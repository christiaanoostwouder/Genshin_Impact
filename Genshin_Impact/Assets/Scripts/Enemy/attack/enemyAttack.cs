using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    private EnemyMovement enemymovement;
    

    public float attackCooldown = 10f;
    public float attackRange = 100f;
    private float nextAttackTime = 1f;

    public float RocketTime;
    private float timer = 5f;

    private Transform player;
    public GameObject Rocket;

    public Transform RocketSpawn;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        enemymovement = GetComponent<EnemyMovement>();

    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                doAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        
    }



    private void doAttack()
    {
        RocketTime -= Time.deltaTime;
        GameObject RocketObj = Instantiate(Rocket, RocketSpawn.transform.position, RocketSpawn.transform.rotation)as GameObject;
        Rigidbody RocketRig = RocketObj.GetComponent<Rigidbody>();

        if (RocketTime < 0f) return;

        RocketTime = timer;

        float speed = enemymovement.speed;

        RocketRig.AddForce(RocketRig.transform.forward * speed);
        Destroy(RocketObj, 0.1f);
    }
        

}        
       
        

    
        
    

