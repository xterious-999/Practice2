using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerLook : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerLookConfig config;

    [Header("References")]
    [SerializeField] private Transform cameraRoot;

    [Header("Input")]
    [SerializeField] private InputActionReference lookAction;

    private float pitch;

    private void OnEnable()
    {
        if (lookAction == null)
        {
            enabled = false;
            return;
        }

        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        if (lookAction != null)
            lookAction.action.Disable();
    }

    private void Start()
    {
        if (config == null)
        {
            enabled = false;
            return;
        }

        if (cameraRoot == null)
        {
            enabled = false;
            return;
        }

        LockCursor();
    }

    private void Update()
    {
        Vector2 input = lookAction.action.ReadValue<Vector2>();

        float yaw = input.x * config.Sensitivity;

        pitch -= input.y * config.Sensitivity;
        pitch = Mathf.Clamp(
            pitch,
            config.MinPitch,
            config.MaxPitch
        );

        transform.Rotate(Vector3.up, yaw, Space.Self);

        cameraRoot.localRotation = Quaternion.Euler(
            pitch,
            0f,
            0f
        );
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}