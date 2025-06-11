// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MenuManager : MonoBehaviour
// {
//     [Header("Menu Objects")]
//     [SerializeField] private GameObject mainMenuCanvasGO; 

//     [Header("Scripts to be Deactivated on Pause")]
//     // [SerializeField] private PlayerMovement playerMovement;
//     // [SerializeField] private TestYandereAI yandereMovement;
//     // [SerializeField] private PlayerInteraction playerInteraction;
//     [SerializeField] private MonoBehaviour[] gameplayScripts;

//     private bool isPaused;

//     private void Start()
//     {
//         mainMenuCanvasGO.SetActive(false);
//     }

//     private void Update()
//     {
//         // Listen for Escape button from InputManager
//         if (InputManager.instance.MenuOpenCloseInput)
//         {
//             // if (InteractionUI.InstanceIsVisible())
//             // {
//             //     InteractionUI.Instance.Hide();
//             // }
//             // else 
//             // {
//             //     OpenMainMenu();
//             // }
//             // if (!isPaused)
//             //     Pause();
//             // else
//             //     Unpause();
//             // if escaped is pressed while in interaction screen -> removeInteractionScreen
//             // if escaped is pressed while not paused -> Open Pause Screen
//             // if escaped is pressed while paused -> check whether if its interaction screen
//             // -> if in interaction screen -> Remove Interaction Screen
//             // -> if in pause screen -> Remove Pause Screen
//             if (!PauseManager.instance.IsPaused)
//                 PauseScreen();
//             else
//                 UnpauseScreen();
//         } else if (InputManager.instance.InteractOpenCloseInput)
//         {
//             // if paused -> nothing happen
//             // if unpaused open
//         }
//     }

//     private void DisableScripts()
//     {
//         foreach (var script in gameplayScripts)
//             script.enabled = false;
//     }

//     private void EnableScripts()
//     {
//         foreach (var script in gameplayScripts)
//             script.enabled = true;
//     }

//     public void PauseScreen()
//     {
//         PauseManager.instance.PauseScreen();

//         // playerMovement.enabled = false;
//         // yandereMovement.enabled = false;
//         // playerInteraction.enabled = false;
//         DisableScripts();
//         OpenMainMenu();
//     }

//     public void UnpauseScreen()
//     {
//         // isPaused = false;
//         // Time.timeScale = 1f;

//         // playerMovement.enabled = true;
//         // yandereMovement.enabled = true;
//         // playerInteraction.enabled = true;
//         PauseManager.instance.UnpauseScreen();
//         EnableScripts();
//         CloseAllMenus();
//     }

//     private void OpenMainMenu()
//     {
//         mainMenuCanvasGO.SetActive(true);
//     }

//     private void CloseAllMenus()
//     {
//         mainMenuCanvasGO.SetActive(false);
//     }

//     public void OnResumeClick()
//     {
//         UnpauseScreen();
//     }

//     public void OnReturnClick()
//     {
//         // Ensure Scripts are enabled before starting new game
//         UnpauseScreen();
//         SceneManager.LoadScene("StartScene");
//     }

// }

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
        if (InteractionUI.InstanceIsVisible())
        {
            InteractionUI.Instance.Hide();
            PauseManager.instance.UnpauseScreen();
            return;
        }

        PauseManager.instance.TogglePause();
    }

//     private void ShowMenu()
// {
//     // Only show if pause source is "pause" (from Esc)
//     if (PauseManager.instance.PauseSource != "pause")
//         return;

//     mainMenuCanvasGO.SetActive(true);
//     ToggleGameplayScripts(false);
// }


//     private void HideMenu()
//     {
//         mainMenuCanvasGO.SetActive(false);
//         ToggleGameplayScripts(true);
//     }
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

//     private void ToggleGameplayScripts(bool enabled)
//     {
//         foreach (var script in gameplayScripts)
//         {
//             if (script != null)
//                 script.enabled = enabled;
//         }
//     }



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
