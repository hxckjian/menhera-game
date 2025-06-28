using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Dialogue currentDialogue;

    // Ensure singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Register a dialogue instance as currently active
    public void Register(Dialogue dialogue)
    {
        currentDialogue = dialogue;
    }

    // Check if a dialogue is currently active and visible
    public bool IsDialogueActive()
    {
        return currentDialogue != null && currentDialogue.IsDialogueActive();
    }

    // Forcefully closes the current dialogue, if any
    public void ForceCloseDialogue()
    {
        if (currentDialogue != null)
        {
            currentDialogue.ForceCloseDialogue();
            currentDialogue = null;
        }
    }

    // Clears reference to the current dialogue
    public void Clear()
    {
        currentDialogue = null;
    }
}
