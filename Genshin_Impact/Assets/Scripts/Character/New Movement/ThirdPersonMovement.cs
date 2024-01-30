using Cinemachine;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform ruinDrake;
    public Transform cameraTransform; // Reference to the Maincamera's transform
    public GameObject bow;
    [SerializeField] private Animator animator;
    [SerializeField] CinemachineFreeLook VirtualCam3rd;
    [SerializeField] CinemachineFreeLook VirtualCamAttck;

    [Header("Jump Settings")]
    public float jumpForce;

    [Header("Dash Settings")]
    public float dashSpeed;
    public float runSpeed;
    public float normSpeed;
    public float dashTime;

    private float speed;
    private Vector3 velocity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector3 inputDirection;


    private void OnEnable()
    {
        CameraSwitch.shootingMode = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.R))
        {
            speed = normSpeed * CameraSwitch.SwitchCam(VirtualCam3rd, VirtualCamAttck, speed);
        }

        if (controller.isGrounded)
        {
            //Check for Jump or Dash or make speed normal again
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
                animator.SetBool("Moving", true);
            }
        }
        // vertical velocity will never grow bigger than gravity itself
        else if (velocity.y > GamePhysics.gravity)
        {
            ApplyGravity();
        }

        if (CameraSwitch.shootingMode == true)
        {
            //update player rotation based on camera rotation
            float targetAngle = cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            animator.SetBool("ShootingAnim", true);
        }

        if (inputDirection.magnitude >= 0.1f)
        {
            if (CameraSwitch.shootingMode == true)
            {
                AttackModeMove(inputDirection);
            }
            else
            {
                ThirdPersonMove(inputDirection);
            }
        }
        //apply velocity to movment
        controller.Move(velocity * Time.deltaTime);

        //Debug.Log(speed);
    }


    private void ThirdPersonMove(Vector3 inputDirection)
    {
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        controller.Move((moveDirection.normalized * speed + velocity) * Time.deltaTime);
    }

    private void AttackModeMove(Vector3 inputDirection)
    {
        float targetAngle = cameraTransform.eulerAngles.y;
        // Calculate the move direction in the world space based on the camera's rotation
        Vector3 moveDirection = new Vector3(inputDirection.x, 0f, inputDirection.z).normalized;
        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * moveDirection;

        controller.Move((moveDirection * speed + velocity) * Time.deltaTime);

    }

    void ApplyGravity()
    {
        velocity.y += GamePhysics.gravity * 3 * Time.deltaTime;
    }

    void Jump()
    {
        velocity.y = jumpForce;
    }

    public void StartDash()
    {
        speed = dashSpeed;
        Debug.Log(speed);
        Invoke("StopDash", dashTime);
    }

    public void StopDash()
    {
        speed = runSpeed;
    }
}


