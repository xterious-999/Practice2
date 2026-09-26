using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private PlayerMovementConfig config;

    [Header("Input")]
    [SerializeField]
    private InputActionReference moveAction;

    [SerializeField]
    private InputActionReference sprintAction;

    private CharacterController controller;

    private Vector3 horizontalVelocity;
    private float verticalVelocity;

    public Vector3 Velocity => controller.velocity;

    public float HorizontalSpeed
    {
        get
        {
            Vector3 velocity = controller.velocity;
            velocity.y = 0f;

            return velocity.magnitude;
        }
    }

    public bool IsGrounded => controller.isGrounded;

    public bool IsMoving =>
        HorizontalSpeed > 0.1f;

    public bool IsSprinting =>
        sprintAction.action.IsPressed() &&
        IsMoving;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleGravity();
        ApplyMovement();
    }

    private void HandleMovement()
    {
        Vector2 input =
            moveAction.action.ReadValue<Vector2>();

        Vector3 inputDirection =
            transform.right * input.x +
            transform.forward * input.y;

        inputDirection =
            Vector3.ClampMagnitude(
                inputDirection,
                1f
            );

        float targetSpeed =
            IsSprinting
                ? config.SprintSpeed
                : config.WalkSpeed;

        Vector3 targetVelocity =
            inputDirection *
            targetSpeed;

        horizontalVelocity =
            Vector3.MoveTowards(
                horizontalVelocity,
                targetVelocity,
                config.Acceleration *
                Time.deltaTime
            );
    }

    private void HandleGravity()
    {
        if (controller.isGrounded &&
            verticalVelocity < 0f)
        {
            verticalVelocity =
                config.GroundedGravity;

            return;
        }

        verticalVelocity +=
            config.Gravity *
            Time.deltaTime;
    }

    private void ApplyMovement()
    {
        Vector3 velocity =
            horizontalVelocity;

        velocity.y =
            verticalVelocity;

        controller.Move(
            velocity *
            Time.deltaTime
        );
    }
}