using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;

    private float dis;

    public float speed;

    public float howclose;

    private Rigidbody rig;

    [SerializeField] private NavMeshAgent agent;
    private EnemyAttack attack;

    void Start()
    {
        attack = GetComponent<EnemyAttack>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        this.gameObject.transform.LookAt(player);
        dis = Vector3.Distance(player.position, transform.position);

        if (dis > 5 || dis < 50)
        {
            agent.destination = player.position;
        }

        // Check if the rocket attack is happening, stop the agent if true
        if (dis <= 15 || attack.IsRocketAttack())
        {
            Debug.Log("distance");
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }
}
