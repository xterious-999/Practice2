using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerControlLock : MonoBehaviour
{
    private PlayerMovement[] movementScripts;
    private PlayerLook[] lookScripts;
    private PlayerHeadBob[] headBobScripts;
    private PlayerInput[] playerInputs;

    private void Awake()
    {
        movementScripts = GetComponentsInChildren<PlayerMovement>(true);
        lookScripts = GetComponentsInChildren<PlayerLook>(true);
        headBobScripts = GetComponentsInChildren<PlayerHeadBob>(true);
        playerInputs = GetComponentsInChildren<PlayerInput>(true);
    }

    public void Lock()
    {
        SetEnabled(movementScripts, false);
        SetEnabled(lookScripts, false);
        SetEnabled(headBobScripts, false);
        SetEnabled(playerInputs, false);
    }

    private static void SetEnabled<T>(T[] components, bool value)
        where T : Behaviour
    {
        foreach (T component in components)
        {
            if (component != null)
                component.enabled = value;
        }
    }
}