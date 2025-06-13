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
        if (instance == null)
            instance = this;
    }

    public void PauseScreen(string source = "pause")
    {
        if (IsPaused) return;
        IsPaused = true;
        PauseSource = source;
        Time.timeScale = 0f;
        OnPause?.Invoke();
    }

    public void UnpauseScreen()
    {
        if (!IsPaused) return;
        IsPaused = false;
        PauseSource = "";
        Time.timeScale = 1f;
        OnUnpause?.Invoke();
    }

    public void TogglePause()
    {
        if (IsPaused)
            UnpauseScreen();
        else
            PauseScreen("pause");
    }
}
