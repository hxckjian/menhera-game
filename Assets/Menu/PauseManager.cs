using UnityEngine;
using System;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    public bool IsPaused { get; private set; }
    public string PauseSource { get; private set; } = "";

    public event Action OnPause;
    public event Action OnUnpause;

    private void Awake()
    {
        // Implement singleton pattern
        if (instance == null)
            instance = this;
    }

    // Pause the game and notify listeners
    public void PauseScreen(string source = "pause")
    {
        if (IsPaused) return;
        IsPaused = true;
        PauseSource = source;
        Time.timeScale = 0f;
        OnPause?.Invoke();
    }

    // Unpause the game and notify listeners
    public void UnpauseScreen()
    {
        if (!IsPaused) return;
        IsPaused = false;
        PauseSource = "";
        Time.timeScale = 1f;
        OnUnpause?.Invoke();
    }

    // Toggle pause state (e.g., for ESC key or menu toggle)
    public void TogglePause()
    {
        if (IsPaused)
            UnpauseScreen();
        else
            PauseScreen("pause");
    }
}
