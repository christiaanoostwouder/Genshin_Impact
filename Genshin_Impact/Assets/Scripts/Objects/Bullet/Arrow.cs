using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public float dmg;
    public GameObject HitMark;

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
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(dmg);
            Instantiate(HitMark, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
