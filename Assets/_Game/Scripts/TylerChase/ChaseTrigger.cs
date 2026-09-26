using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public sealed class ChaseTrigger : MonoBehaviour
{
    [SerializeField] private TylerChaser tylerChaser;
    [SerializeField] private AudioSource chaseAudioSource;
    [SerializeField] private AudioClip chaseAudioClip;

    private Collider triggerCollider;
    private bool wasActivated;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;

        Rigidbody triggerRigidbody = GetComponent<Rigidbody>();
        triggerRigidbody.isKinematic = true;
        triggerRigidbody.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasActivated)
            return;

        Transform player = FindPlayer(other.transform);

        if (player == null)
            return;

        wasActivated = true;

        tylerChaser.StartChasing(player);

        if (chaseAudioSource != null)
            chaseAudioSource.PlayOneShot(chaseAudioClip, 1.0f);
    }

    private Transform FindPlayer(Transform enteredTransform)
    {
        Transform current = enteredTransform;

        while (current != null)
        {
            if (current.CompareTag("Player"))
                return current;

            current = current.parent;
        }

        return null;
    }
}