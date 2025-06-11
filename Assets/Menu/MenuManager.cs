using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject mainMenuCanvasGO; 

    [Header("Scripts to be Deactivated on Pause")]
    // [SerializeField] private PlayerMovement playerMovement;
    // [SerializeField] private TestYandereAI yandereMovement;
    // [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private MonoBehaviour[] gameplayScripts;

    private bool isPaused;

    private void Start()
    {
        mainMenuCanvasGO.SetActive(false);
    }

    private void Update()
    {
        // Listen for Escape button from InputManager
        if (InputManager.instance.MenuOpenCloseInput)
        {
            // if (InteractionUI.InstanceIsVisible())
            // {
            //     InteractionUI.Instance.Hide();
            // }
            // else 
            // {
            //     OpenMainMenu();
            // }
            // if (!isPaused)
            //     Pause();
            // else
            //     Unpause();
            if (!PauseManager.instance.IsPaused)
                Pause();
            else
                Unpause();
        }
        // if (InputManager.instance.InteractPressed)
        // {
        //     if (!mainMenuCanvasGO.activeSelf == true)
        //     {
        //         if (!isPaused)
        //             Pause();
        //         else
        //             Unpause();
        //     }
        // }
    }

    private void DisableScripts()
    {
        foreach (var script in gameplayScripts)
            script.enabled = false;
    }

    private void EnableScripts()
    {
        foreach (var script in gameplayScripts)
            script.enabled = true;
    }

    public void Pause()
    {
        PauseManager.instance.PauseGame();

        // playerMovement.enabled = false;
        // yandereMovement.enabled = false;
        // playerInteraction.enabled = false;
        DisableScripts();
        OpenMainMenu();
    }

    public void Unpause()
    {
        // isPaused = false;
        // Time.timeScale = 1f;

        // playerMovement.enabled = true;
        // yandereMovement.enabled = true;
        // playerInteraction.enabled = true;
        PauseManager.instance.UnpauseGame();
        EnableScripts();
        CloseAllMenus();
    }

    private void OpenMainMenu()
    {
        mainMenuCanvasGO.SetActive(true);
    }

    private void CloseAllMenus()
    {
        mainMenuCanvasGO.SetActive(false);
    }

    public void OnResumeClick()
    {
        Unpause();
    }

    public void OnReturnClick()
    {
        // Ensure Scripts are enabled before starting new game
        Unpause();
        SceneManager.LoadScene("StartScene");
    }

}
