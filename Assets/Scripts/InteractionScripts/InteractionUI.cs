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
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Hide();
    }

    public void Show(string label, UnityAction onSceneClick)
    {
        sceneButtonText.text = label;

        // Clear previous listeners to avoid stacking
        sceneButton.onClick.RemoveAllListeners();
        nothingButton.onClick.RemoveAllListeners();

        sceneButton.onClick.AddListener(onSceneClick);
        sceneButton.onClick.AddListener(Hide);

        nothingButton.onClick.AddListener(() =>
        {
            Debug.Log("Nothing clicked.");
            Hide();
        });

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
