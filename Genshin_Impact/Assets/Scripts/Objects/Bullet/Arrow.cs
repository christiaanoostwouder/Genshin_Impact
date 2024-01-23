using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public float dmg;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        Debug.Log(dmg);
        enemyHealth.TakeDamage(dmg);
        Destroy(gameObject);
    }
}
