using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public sealed class DoorSequence : MonoBehaviour
{
    private static readonly int OpenHash = Animator.StringToHash("Open");
    private static readonly int CloseHash = Animator.StringToHash("Close");

    [SerializeField] private Animator animator;

    private bool isOpen;
    private bool isPermanentlyClosed;

    private void Reset()
    {
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public bool TryOpen()
    {
        if (isOpen || isPermanentlyClosed)
            return false;

        isOpen = true;
        animator.SetTrigger(OpenHash);

#if UNITY_EDITOR
        Debug.Log($"[DoorSequence:{name}] Door opened.", this);
#endif

        return true;
    }

    public bool TryClosePermanently()
    {
        if (isPermanentlyClosed)
            return false;

        isOpen = false;
        isPermanentlyClosed = true;
        animator.SetTrigger(CloseHash);

#if UNITY_EDITOR
        Debug.Log($"[DoorSequence:{name}] Door closed permanently.", this);
#endif

        return true;
    }
}