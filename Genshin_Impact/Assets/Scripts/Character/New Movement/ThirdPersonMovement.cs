using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform; // Reference to the camera's transform
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float gravity = -9.81f; // You can adjust this value based on the strength of gravity in your game

    // Character controller variables
    Vector3 velocity;

    void Update()
    {
        // Calculate gravity
        ApplyGravity();

        // Get input values
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Calculate the target angle based on input and camera direction
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Rotate the player
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move the player in the camera's forward direction
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the controller
            controller.Move((moveDirection.normalized * speed + velocity) * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 30f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10f;
            return;
        }
    }

    void ApplyGravity()
    {
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Move the controller with gravity
        controller.Move(velocity * Time.deltaTime);
    }
}


