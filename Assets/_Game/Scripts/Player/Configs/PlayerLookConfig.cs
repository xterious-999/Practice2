using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerLookConfig",
    menuName = "Config/Player/Look Config")]
public class PlayerLookConfig : ScriptableObject
{
    [Header("Mouse")]
    [SerializeField, Min(0f)]
    private float sensitivity = 0.08f;

    [Header("Vertical Look")]
    [SerializeField, Range(-90f, 0f)]
    private float minPitch = -85f;

    [SerializeField, Range(0f, 90f)]
    private float maxPitch = 85f;

    public float Sensitivity => sensitivity;

    public float MinPitch => minPitch;
    public float MaxPitch => maxPitch;
}