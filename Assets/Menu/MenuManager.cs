using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO;

    [Header("Scripts to be Deactivated on Pause")]
    [SerializeField] private MonoBehaviour[] gameplayScripts;

    private void Start()
    {
        Debug.Log("MenuManager Start()");

        // Subscribe to menu toggle input
        if (InputManager.instance != null)
        {
            Debug.Log("Subscribing to InputManager");
            InputManager.instance.OnMenuToggle += HandleMenuToggle;
        }
        else
        {
            Debug.LogWarning("InputManager is STILL null in Start().");
        }

        // Subscribe to pause events
        if (PauseManager.instance != null)
        {
            PauseManager.instance.OnPause += ShowMenu;
            PauseManager.instance.OnUnpause += HideMenu;
        }
        else
        {
            Debug.LogWarning("PauseManager is null in Start().");
        }

        // Ensure the menu is hidden on start
        mainMenuCanvasGO.SetActive(false);
    }

    private void OnDestroy()
    {
        // Unsubscribe from events
        if (InputManager.instance != null)
            InputManager.instance.OnMenuToggle -= HandleMenuToggle;

        if (PauseManager.instance != null)
        {
            PauseManager.instance.OnPause -= ShowMenu;
            PauseManager.instance.OnUnpause -= HideMenu;
        }
    }

    // Handle menu toggling logic
    private void HandleMenuToggle()
    {
        Debug.Log( DialogueManager.Instance.IsDialogueActive());
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive())
        {
            DialogueManager.Instance.ForceCloseDialogue();
            return;
        }

        if (InteractionUI.InstanceIsVisible())
        {
            InteractionUI.Instance.Hide();
            PauseManager.instance.UnpauseScreen();
            return;
        }

        PauseManager.instance.TogglePause();
    }

    // Show the pause menu
    private void ShowMenu()
    {
        if (PauseManager.instance.PauseSource != "pause")
            return;

        mainMenuCanvasGO.SetActive(true);
        GameplayController.instance.SetGameplayEnabled(false);
    }

    // Hide the pause menu
    private void HideMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        GameplayController.instance.SetGameplayEnabled(true);
    }

    // Resume the game when resume is clicked
    public void OnResumeClick()
    {
        PauseManager.instance.UnpauseScreen();
    }

    // Return to the start scene
    public void OnReturnClick()
    {
        PauseManager.instance.UnpauseScreen();
        SceneManager.LoadScene("StartScene");
    }
}
