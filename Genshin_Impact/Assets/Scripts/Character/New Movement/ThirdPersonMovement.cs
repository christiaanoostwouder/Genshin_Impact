using Cinemachine;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform; // Reference to the Maincamera's transform
    [SerializeField] CinemachineFreeLook VirtualCam3rd;
    [SerializeField] CinemachineVirtualCamera VirtualCamAttck;

    [Header("Jump Settings")]
    public float jumpForce;

    [Header("Dash Settings")]
    public float dashSpeed;
    public float normSpeed;
    public float dashTime;

    [Header("ShootingMode")]
    public float mouseSensitivity;
    public float lookXLimit;
    public float xRotation;
    private float mouseX;
    private float mouseY;

    private float speed;
    private Vector3 velocity;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;


    private void OnEnable()
    {
        CameraSwitch.shootingMode = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.R))
        {
            CameraSwitch.SwitchCam(VirtualCam3rd, VirtualCamAttck);
        }

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
        else if (velocity.y > GamePhysics.gravity)
        {
            ApplyGravity();
        }

        if (CameraSwitch.shootingMode == true)
        {

        }

        if (inputDirection.magnitude >= 0.1f)
        {
            if (CameraSwitch.shootingMode == false)
            {
                ThirdPersonMove(inputDirection);
            }
            else if (CameraSwitch.shootingMode == true)
            {
                AttackModeMove(inputDirection);
            }
        }
        //apply velocity to movment
        controller.Move(velocity * Time.deltaTime);
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
        Invoke("StopDash", dashTime);
    }

    public void StopDash()
    {
        speed = normSpeed * 2;
    }
}


