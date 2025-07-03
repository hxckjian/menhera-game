using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Hide();
    }

    // Show the interaction UI, optionally with a dialogue first
    public void Show(ILabelProvider labelProvider, UnityAction onSceneClick, GameObject dialogueCanvas = null, Dialogue dialogue = null, UnityAction onClose = null)
    {
        PauseTime(); // Pause as early as possible

        // Dialogue comes first
        if (dialogueCanvas != null && dialogue != null)
        {
            dialogueCanvas.SetActive(true);
            dialogue.StartDialogueManually();

            // Wait for dialogue to finish, then show options
            dialogue.OnDialogueComplete += () =>
            {
                dialogueCanvas.SetActive(false);
                ShowInternal(labelProvider, onSceneClick, onClose);
            };
        }
        else
        {
            ShowInternal(labelProvider, onSceneClick, onClose);
        }
    }

    // Show UI elements for player interaction
    private void ShowInternal(ILabelProvider labelProvider, UnityAction onSceneClick, UnityAction onClose)
    {
        // sceneButtonText.text = label;
        var label = labelProvider.GetInteractionLabel();

        label.GetLocalizedStringAsync().Completed += handle =>
        {
            sceneButtonText.text = handle.Result;

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
            EventSystem.current.SetSelectedGameObject(null); 
            EventSystem.current.SetSelectedGameObject(nothingButton.gameObject);
        };
    }

    // Show dialogue without options UI
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

    // Pause gameplay and notify pause system
    private void PauseTime()
    {
        GameplayController.Instance.SetGameplayEnabled(false);
        PauseManager.Instance.PauseScreen("interaction");
    }

    // Resume gameplay and notify pause system
    private void ResumeTime()
    {
        GameplayController.Instance.SetGameplayEnabled(true);
        PauseManager.Instance.UnpauseScreen();
    }

    // Hide all UI elements
    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

     // Check if interaction UI is visible
    public bool IsVisible()
    {
        return canvasGroup.alpha > 0.9f;
    }

    // Static check for global access
    public static bool InstanceIsVisible()
    {
        return Instance != null && Instance.IsVisible();
    }
}
