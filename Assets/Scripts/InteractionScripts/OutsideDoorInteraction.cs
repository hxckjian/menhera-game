using UnityEngine;

public class OutsideDoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Dialogue dialogue;

    private void Start()
    {
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
    }

    public void Interact()
    {
        InteractionUI.Instance.ShowDialogueOnly(dialogueCanvas, dialogue);
    }

    private void OnSceneClick()
    {
        Debug.Log("Start Scene for Hiding under table");
        InteractionUI.Instance.Hide();
    }
}
