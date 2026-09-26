using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private PlayerHeadBobConfig config;

    [Header("References")]
    [SerializeField]
    private PlayerMovement movement;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    private float bobTime;

    private void Awake()
    {
        defaultPosition =
            transform.localPosition;

        defaultRotation =
            transform.localRotation;
    }

    private void LateUpdate()
    {
        if (!movement.IsGrounded ||
            !movement.IsMoving)
        {
            ResetBob();
            return;
        }

        ApplyBob();
    }

    private void ApplyBob()
    {
        float frequency =
            movement.IsSprinting
                ? config.SprintFrequency
                : config.WalkFrequency;

        float amplitudeMultiplier =
            movement.IsSprinting
                ? config.SprintAmplitudeMultiplier
                : 1f;

        bobTime +=
            Time.deltaTime *
            frequency;

        float horizontalOffset =
            Mathf.Cos(bobTime) *
            config.HorizontalAmplitude *
            amplitudeMultiplier;

        float verticalOffset =
            Mathf.Sin(bobTime * 2f) *
            config.VerticalAmplitude *
            amplitudeMultiplier;

        float roll =
            Mathf.Sin(bobTime) *
            config.RollAmplitude *
            amplitudeMultiplier;

        Vector3 targetPosition =
            defaultPosition +
            new Vector3(
                horizontalOffset,
                verticalOffset,
                0f
            );

        Quaternion targetRotation =
            defaultRotation *
            Quaternion.Euler(
                0f,
                0f,
                roll
            );

        float interpolation =
            1f -
            Mathf.Exp(
                -config.ReturnSpeed *
                Time.deltaTime
            );

        transform.localPosition =
            Vector3.Lerp(
                transform.localPosition,
                targetPosition,
                interpolation
            );

        transform.localRotation =
            Quaternion.Slerp(
                transform.localRotation,
                targetRotation,
                interpolation
            );
    }

    private void ResetBob()
    {
        bobTime = 0f;

        float interpolation =
            1f -
            Mathf.Exp(
                -config.ReturnSpeed *
                Time.deltaTime
            );

        transform.localPosition =
            Vector3.Lerp(
                transform.localPosition,
                defaultPosition,
                interpolation
            );

        transform.localRotation =
            Quaternion.Slerp(
                transform.localRotation,
                defaultRotation,
                interpolation
            );
    }
}