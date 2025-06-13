using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    [SerializeField] private GameObject dialogueCanvas; 

    private int index;
    private bool isTyping = false;

    // Event triggered when the dialogue sequence completes
    public event Action OnDialogueComplete;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    private void ClearText()
    {
        textComponent.text = string.Empty;
    }

    private void StartDialogue()
    {
        index = 0;
        ClearText();
        StartCoroutine(TypeLine()); 
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        ClearText();
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }

        isTyping = false;
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            ClearText();
            StartCoroutine(TypeLine());
        }
        else
        {
            // gameObject.SetActive(false);
            OnDialogueComplete?.Invoke(); //Callback
        }
    }

    // Begin dialogue sequence from index 0
    public void StartDialogueManually()
    {
        index = 0;
        DialogueManager.Instance?.Register(this);
        StartCoroutine(TypeLine());
    }

    public bool IsDialogueActive()
{
    return (dialogueCanvas != null && dialogueCanvas.activeSelf) && (isTyping || index < lines.Length);
}


    public void ForceCloseDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        ClearText();
        OnDialogueComplete?.Invoke();
        DialogueManager.Instance?.Clear();

        if (dialogueCanvas != null)
        dialogueCanvas.SetActive(false);
    }

}