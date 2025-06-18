using UnityEngine;

public class OutsideDoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Dialogue dialogue;

    [Header("Required Direction for Trigger")]
    [SerializeField] private Direction requiredFacingDirection = Direction.None;

    public Direction RequiredDirection => requiredFacingDirection;

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
