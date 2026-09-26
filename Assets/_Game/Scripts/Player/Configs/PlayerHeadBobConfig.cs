using UnityEngine;

[CreateAssetMenu(
    fileName = "PlayerHeadBobConfig",
    menuName = "Config/Player/Head Bob Config")]
public class PlayerHeadBobConfig : ScriptableObject
{
    [Header("Walking")]
    [SerializeField, Min(0f)]
    private float walkFrequency = 7f;

    [SerializeField, Min(0f)]
    private float horizontalAmplitude = 0.014f;

    [SerializeField, Min(0f)]
    private float verticalAmplitude = 0.021f;

    [SerializeField, Min(0f)]
    private float rollAmplitude = 0.35f;

    [Header("Sprinting")]
    [SerializeField, Min(0f)]
    private float sprintFrequency = 9f;

    [SerializeField, Min(0f)]
    private float sprintAmplitudeMultiplier = 1.25f;

    [Header("Smoothing")]
    [SerializeField, Min(0f)]
    private float returnSpeed = 10f;

    public float WalkFrequency => walkFrequency;

    public float HorizontalAmplitude => horizontalAmplitude;
    public float VerticalAmplitude => verticalAmplitude;
    public float RollAmplitude => rollAmplitude;

    public float SprintFrequency => sprintFrequency;
    public float SprintAmplitudeMultiplier => sprintAmplitudeMultiplier;

    public float ReturnSpeed => returnSpeed;
}