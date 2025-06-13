using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class InteractionUI : MonoBehaviour
{
    public static InteractionUI Instance;

    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Buttons")]
    [SerializeField] private TMP_Text sceneButtonText;
    [SerializeField] private Button sceneButton;
    [SerializeField] private Button nothingButton;

    public bool IsPaused { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Hide();
    }

    // public void Show(string label, UnityAction onSceneClick, UnityAction onClose = null)
    // {
    //     sceneButtonText.text = label;

    //     // Clear previous listeners to avoid stacking
    //     sceneButton.onClick.RemoveAllListeners();
    //     nothingButton.onClick.RemoveAllListeners();

    //     sceneButton.onClick.AddListener(onSceneClick);
    //     // sceneButton.onClick.AddListener(Hide);
    //     sceneButton.onClick.AddListener(() =>
    //     {
    //         Hide();
    //         onClose?.Invoke();
    //         ResumeTime();
    //     });

    //     nothingButton.onClick.AddListener(() =>
    //     {
    //         Debug.Log("Nothing clicked.");
    //         Hide();
    //         onClose?.Invoke();
    //         ResumeTime();
    //     });

    //     PauseTime();

    //     canvasGroup.alpha = 1f;
    //     canvasGroup.interactable = true;
    //     canvasGroup.blocksRaycasts = true;
    // }
    public void Show(string label, UnityAction onSceneClick, GameObject dialogueCanvas = null, Dialogue dialogue = null, UnityAction onClose = null)
    {
        PauseTime(); // Pause as early as possible

        // Dialogue comes first
        if (dialogueCanvas != null && dialogue != null)
        {
            // dialogueCanvas.alpha = 1f;
            // dialogueCanvas.interactable = true;
            // dialogueCanvas.blocksRaycasts = true;
            dialogueCanvas.SetActive(true);
            dialogue.StartDialogueManually();

            // Wait for dialogue to finish, then show options
            dialogue.OnDialogueComplete += () =>
            {
                // dialogueCanvas.alpha = 0f;
                // dialogueCanvas.interactable = false;
                // dialogueCanvas.blocksRaycasts = false;
                dialogueCanvas.SetActive(false);
                ShowInternal(label, onSceneClick, onClose);
            };
        }
        else
        {
            ShowInternal(label, onSceneClick, onClose);
        }
    }


    private void ShowInternal(string label, UnityAction onSceneClick, UnityAction onClose)
    {
        sceneButtonText.text = label;

        sceneButton.onClick.RemoveAllListeners();
        nothingButton.onClick.RemoveAllListeners();

        sceneButton.onClick.AddListener(onSceneClick);
        sceneButton.onClick.AddListener(() =>
        {
            Hide();
            onClose?.Invoke();
            ResumeTime(); 
        });

        nothingButton.onClick.AddListener(() =>
        {
            Debug.Log("Nothing clicked.");
            Hide();
            onClose?.Invoke();
            ResumeTime(); 
        });

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void ShowDialogueOnly(GameObject dialogueCanvas = null, Dialogue dialogue = null)
    {
        PauseTime();
        if (dialogueCanvas != null && dialogue != null)
        {
            dialogueCanvas.SetActive(true);
            dialogue.StartDialogueManually();

            dialogue.OnDialogueComplete += () =>
            {
                dialogueCanvas.SetActive(false);
                ResumeTime(); 
            };
        }

    }

    private void PauseTime()
    {
        GameplayController.instance.SetGameplayEnabled(false);
        PauseManager.instance.PauseScreen("interaction");
    }

    private void ResumeTime()
    {
        GameplayController.instance.SetGameplayEnabled(true);
        PauseManager.instance.UnpauseScreen();
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public bool IsVisible()
    {
        return canvasGroup.alpha > 0.9f; // You could cache a boolean too
    }

    public static bool InstanceIsVisible()
    {
        return Instance != null && Instance.IsVisible();
    }
}
