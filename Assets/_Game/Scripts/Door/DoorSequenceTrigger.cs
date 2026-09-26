using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public sealed class DoorSequenceTrigger : MonoBehaviour
{
    private const string PlayerTag = "Player";

    private enum TriggerAction
    {
        Open,
        ClosePermanently
    }

    [SerializeField] private DoorSequence door;
    [SerializeField] private TriggerAction action;

    private bool isConsumed;

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isConsumed || !other.CompareTag(PlayerTag))
            return;

        bool activated = action switch
        {
            TriggerAction.Open => door.TryOpen(),
            TriggerAction.ClosePermanently => door.TryClosePermanently(),
            _ => false
        };

        if (!activated)
            return;

        isConsumed = true;
        gameObject.SetActive(false);
    }
}