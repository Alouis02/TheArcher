using UnityEngine;

public class Movem : MonoBehaviour
{
    // Variables
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;

    private Vector3 velocity;
    private bool isGrounded;

    // Gravity and Ground Check
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;

    // Jumping
    [SerializeField] private float jumpHeight = 1.5f;

    // References
    private CharacterController controller;
    private Transform cameraTransform;

    // [SerializeField] private Transform aimPosition; // Aim target
    // [SerializeField] private float aimSpeed = 0.5f;
    // [SerializeField] private LayerMask aimMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main?.transform; // Safely get the camera reference

        // Lock the mouse cursor
        SetCursorState(true);
    }

    void Update()
    {
        Move();
 
        // Unlock the cursor for debugging or menus
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorState(false);
        }
        else if (Input.GetMouseButtonDown(0)) // Re-lock cursor on left mouse click
        {
            SetCursorState(true);
        }
    }

    private void Move()
    {
        // Ground Check
        Vector3 groundCheckPosition = controller.bounds.center + Vector3.down * (controller.bounds.extents.y + groundCheckRadius);
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Stabilize when grounded
            }

            // Jump logic
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump triggered!");
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // Input for Movement (W, A, S, D or Arrow Keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDir = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // Rotate the player to face the movement direction
        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Move the player
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        controller.Move(moveDir * speed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void SetCursorState(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}