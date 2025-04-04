using UnityEngine;

public class Movem : MonoBehaviour
{
    private CharacterController playercontrol;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Input")]
    private float movement;
    private float turn;

    [Header("Rotation")]
    [SerializeField] private float mouseSensitivity = 100f;
    private float mouseX;
    private float mouseY;

    private void Start()
    {
        playercontrol = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide cursor
    }

    private void Update()
    {
        Management();
        Movement();
        ApplyGravity();
        RotateWithMouse();

        // Unlock the cursor for debugging or menus
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0)) // Re-lock cursor on left mouse click
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Management()
    {
        movement = Input.GetAxis("Vertical");
        turn = Input.GetAxis("Horizontal");
    }

    private void Movement()
    {
        // Ground Check
        Vector3 groundCheckPosition = playercontrol.bounds.center + Vector3.down * (playercontrol.bounds.extents.y + groundCheckRadius);
        isGrounded = Physics.CheckSphere(groundCheckPosition, groundCheckRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Stabilize velocity when grounded
        }

        // Jump logic
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump triggered!");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Movement
        Vector3 move = new Vector3(turn, 0, movement).normalized;
        move *= walkSpeed;

        playercontrol.Move(move * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        playercontrol.Move(velocity * Time.deltaTime);
    }

    private void RotateWithMouse()
    {
        // Get mouse input
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Clamp vertical rotation to prevent full spin
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        // Rotate player horizontally
        transform.rotation = Quaternion.Euler(0f, mouseX, 0f);

        // Optional: Rotate camera vertically if attached as a child
        Camera.main.transform.localRotation = Quaternion.Euler(mouseY, 0f, 0f);
    }
}