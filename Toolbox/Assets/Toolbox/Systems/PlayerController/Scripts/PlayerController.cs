using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInputActions input;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 1f;

    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting;

    [Header("Camera movement")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float sensitivity = 0.1f;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private void Awake()
    {
        LockMouse();
        controller = GetComponent<CharacterController>();

        input = new PlayerInputActions();

        //Link Movement avec le input system
        input.Gameplay.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        input.Gameplay.Move.canceled += context => moveInput = Vector2.zero;

        //Link Jump et Sprint avec le input system
        input.Gameplay.Jump.performed += context => Jump();
        input.Gameplay.Sprint.performed += context => isSprinting = true;
        input.Gameplay.Sprint.canceled += context => isSprinting = false;

        //Link LookAround avec le input system
        input.Gameplay.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Gameplay.LookAround.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable() => input.Gameplay.Enable();
    private void OnDisable() => input.Gameplay.Disable();

    private void Update()
    {
        HandleMovement();
        HandleLookAround();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float speed = isSprinting ? sprintSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleLookAround()
    {
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        mainCamera.transform.parent.parent.Rotate(Vector3.up * mouseX);
    }

    private void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
