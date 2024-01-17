using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform; // Reference to the Maincamera's transform

    [Header("Jump Settings")]
    public float gravity = -9.81f;
    public float jumpForce;

    [Header("Dash Settings")]
    public float dashSpeed;
    public float normSpeed;
    public float dashTime;

    private float speed;
    private Vector3 velocity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;



        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartDash();
            }
            else if (inputDirection.magnitude == 0)
            {
                speed = normSpeed;
            }
        }
        else if (velocity.y > gravity)
        {
            ApplyGravity();
        }

        if (inputDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move((moveDirection.normalized * speed + velocity) * Time.deltaTime);
        }
        //apply velocity to movment
        controller.Move(velocity * Time.deltaTime);
    }

    void ApplyGravity()
    {
        velocity.y += gravity * 3 * Time.deltaTime;
    }

    void Jump()
    {
        velocity.y = jumpForce;
    }

    public void StartDash()
    {
        speed = dashSpeed;
        Invoke("StopDash", dashTime);
    }

    public void StopDash()
    {
        speed = normSpeed * 2;
    }
}


