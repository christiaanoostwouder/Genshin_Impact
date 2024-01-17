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


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;

        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        dis = Vector3.Distance(player.position, transform.position);

        if(dis > 10 || dis < 50) 
        {
            agent.destination = player.position;
            
        }

        

        if (dis <= 10) 
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
