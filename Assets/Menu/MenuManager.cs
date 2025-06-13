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

        if (InputManager.instance != null)
        {
            Debug.Log("Subscribing to InputManager");
            InputManager.instance.OnMenuToggle += HandleMenuToggle;
        }
        else
        {
            Debug.LogWarning("InputManager is STILL null in Start().");
        }

        if (PauseManager.instance != null)
        {
            PauseManager.instance.OnPause += ShowMenu;
            PauseManager.instance.OnUnpause += HideMenu;
        }
        else
        {
            Debug.LogWarning("PauseManager is null in Start().");
        }

        // Start with menu hidden
        mainMenuCanvasGO.SetActive(false);
    }

    private void OnDestroy()
    {
        if (InputManager.instance != null)
            InputManager.instance.OnMenuToggle -= HandleMenuToggle;

        if (PauseManager.instance != null)
        {
            PauseManager.instance.OnPause -= ShowMenu;
            PauseManager.instance.OnUnpause -= HideMenu;
        }
    }

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

    private void ShowMenu()
    {
        if (PauseManager.instance.PauseSource != "pause")
            return;

        mainMenuCanvasGO.SetActive(true);
        GameplayController.instance.SetGameplayEnabled(false);
    }

    private void HideMenu()
    {
        mainMenuCanvasGO.SetActive(false);
        GameplayController.instance.SetGameplayEnabled(true);
    }

    public void OnResumeClick()
    {
        PauseManager.instance.UnpauseScreen();
    }

    public void OnReturnClick()
    {
        PauseManager.instance.UnpauseScreen();
        SceneManager.LoadScene("StartScene");
    }
}
