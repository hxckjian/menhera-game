using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Dialogue currentDialogue;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Register(Dialogue dialogue)
    {
        currentDialogue = dialogue;
    }

    public bool IsDialogueActive()
    {
        return currentDialogue != null && currentDialogue.IsDialogueActive();
    }

    public void ForceCloseDialogue()
    {
        if (currentDialogue != null)
        {
            currentDialogue.ForceCloseDialogue();
            currentDialogue = null;
        }
    }

    public void Clear()
    {
        currentDialogue = null;
    }
}
