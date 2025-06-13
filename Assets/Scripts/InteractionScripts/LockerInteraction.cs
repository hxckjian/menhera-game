using UnityEngine;

public class LockerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneButtonLabel = "Enter Locker";

    [SerializeField] private MenuManager menuManager;

    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Dialogue dialogue;

    private void Start()
    {
        if (dialogueCanvas != null)
        {
            // dialogueCanvas.alpha = 0f;
            // dialogueCanvas.interactable = false;
            // dialogueCanvas.blocksRaycasts = false;
            dialogueCanvas.SetActive(false);
        }
    }

    public void Interact()
    {
        InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick, dialogueCanvas, dialogue);
    }

    private void OnSceneClick()
    {
        Debug.Log("Start Scene for Locker");
        InteractionUI.Instance.Hide();
    }
}
