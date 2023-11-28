using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    private float dis;

    public float speed;

    public float howclose;

    private Rigidbody rig;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dis = Vector3.Distance(player.position, transform.position);

        if(dis <= howclose) 
        {
            rig.AddForce(transform.forward * speed * Time.deltaTime);
        }

        if (dis > howclose)
        {
            rig.velocity = Vector3.zero;
        }

        if (dis <= 7) 
        {
            rig.velocity = Vector3.zero;
        }

    }

    
}
