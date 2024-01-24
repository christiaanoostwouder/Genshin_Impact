using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform player;
    public float accelerationSpeed = 5f;
    public float maxSpeed = 10f;
    public float destroyDelay = 6f;

    private Rigidbody rocketRigidbody;
    private float accelerationTimer = 0.5f;
    private bool shootingUp = true;

    private void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player")?.transform;

        if (rocketRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on the Rocket GameObject");
        }
    }

    private void Update()
    {
        if (player != null && rocketRigidbody != null)
        {
            if (shootingUp)
            {
                // Shoot up for the first second
                rocketRigidbody.AddForce(Vector3.up * accelerationSpeed, ForceMode.Acceleration);

                accelerationTimer -= Time.deltaTime;
                if (accelerationTimer <= 0)
                {
                    // Switch to following the player after the first second
                    shootingUp = false;
                }
            }
            else
            {
                // Accelerate towards the player
                Vector3 directionToPlayer = player.position - transform.position;
                directionToPlayer.Normalize();
                rocketRigidbody.AddForce(directionToPlayer * accelerationSpeed, ForceMode.Acceleration);

                // Maintain constant speed
                rocketRigidbody.velocity = rocketRigidbody.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Player or Rigidbody not assigned to Rocket script");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object you want to destroy the rocket on
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DestroyRocket();
        }
        else
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void DestroyRocket()
    {
        // Add any cleanup or explosion effects here if needed
        Destroy(gameObject);
    }
}
