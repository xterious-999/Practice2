using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerMovementConfig",
    menuName = "Config/Player/Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("Movement")]
    [SerializeField, Min(0f)]
    private float walkSpeed = 2.5f;

    [SerializeField, Min(0f)]
    private float sprintSpeed = 3.8f;

    [SerializeField, Min(0f)]
    private float acceleration = 12f;

    [Header("Gravity")]
    [SerializeField]
    private float gravity = -20f;

    [SerializeField]
    private float groundedGravity = -2f;

    public float WalkSpeed => walkSpeed;
    public float SprintSpeed => sprintSpeed;
    public float Acceleration => acceleration;

    public float Gravity => gravity;
    public float GroundedGravity => groundedGravity;
}