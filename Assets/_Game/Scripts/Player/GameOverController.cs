using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public sealed class GameOverController : MonoBehaviour
{
    [SerializeField] private Canvas deathCanvas;
    [SerializeField, Min(0f)] private float restartDelay = 0.25f;
    [SerializeField] private bool unlockCursor = true;

    public bool IsGameOver { get; private set; }

    private float restartAllowedTime;
    private bool isRestarting;

    private void Awake()
    {
        deathCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!IsGameOver || isRestarting)
            return;

        if (Time.unscaledTime < restartAllowedTime)
            return;

        if (Mouse.current?.leftButton.wasPressedThisFrame == true)
            RestartScene();
    }

    public void ShowGameOver(Transform player)
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        restartAllowedTime = Time.unscaledTime + restartDelay;

        PlayerControlLock controlLock =
            player.GetComponentInParent<PlayerControlLock>();

        if (controlLock != null)
            controlLock.Lock();

        deathCanvas.gameObject.SetActive(true);

        if (unlockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void RestartScene()
    {
        if (isRestarting)
            return;

        isRestarting = true;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(currentScene.buildIndex);
    }
}