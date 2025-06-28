using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO;

    [Header("Scripts to be Deactivated on Pause")]
    [SerializeField] private MonoBehaviour[] gameplayScripts;

    [Header("Buttons")]
    [SerializeField] private Button defaultVerticalButton;

    private void Start()
    {
        Debug.Log("MenuManager Start()");

        // Subscribe to menu toggle input
        if (InputManager.Instance != null)
        {
            Debug.Log("Subscribing to InputManager");
            InputManager.Instance.OnMenuToggle += HandleMenuToggle;
        }
        else
        {
            Debug.LogWarning("InputManager is STILL null in Start().");
        }

        // Subscribe to pause events
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.OnPause += ShowMenu;
            PauseManager.Instance.OnUnpause += HideMenu;
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
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnMenuToggle -= HandleMenuToggle;
        }

        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.OnPause -= ShowMenu;
            PauseManager.Instance.OnUnpause -= HideMenu;
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
            PauseManager.Instance.UnpauseScreen();
            return;
        }

        PauseManager.Instance.TogglePause();
    }

    // Show the pause menu
    private void ShowMenu()
    {
        if (PauseManager.Instance.PauseSource != "pause")
        {
            return;
        }

        mainMenuCanvasGO.SetActive(true);
        GameplayController.Instance.SetGameplayEnabled(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultVerticalButton.gameObject);
    }

    // Hide the pause menu
    private void HideMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        GameplayController.Instance.SetGameplayEnabled(true);
    }

    // Resume the game when resume is clicked
    public void OnResumeClick()
    {
        PauseManager.Instance.UnpauseScreen();
    }

    // Return to the start scene
    public void OnReturnClick()
    {
        PauseManager.Instance.UnpauseScreen();
        SceneManager.LoadScene("StartScene");
    }
}
