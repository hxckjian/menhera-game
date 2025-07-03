using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    // [SerializeField] private string[] lines;
    [SerializeField] private LocalizedString[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject dialogueCanvas; 

    private int index;
    private bool isTyping = false;

    // Event triggered when the dialogue sequence completes
    public event Action OnDialogueComplete;

    // Update is called once per frame
    private void Update()
    {
        //original Input.GetMouseButtonDown(0)
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleClick();
        }
    }

    public void HandleClick()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            // textComponent.text = lines[index];
            lines[index].GetLocalizedStringAsync().Completed += handle =>
            {
                textComponent.text = handle.Result;
            };
            isTyping = false;
        }
        else
        {
            NextLine();
        }
    }

    // Clear current text from the text component
    private void ClearText()
    {
        textComponent.text = string.Empty;
    }

    // Coroutine to animate typing of current line
    private IEnumerator TypeLine()
    {
        isTyping = true;
        ClearText();

        var handle = lines[index].GetLocalizedStringAsync();
        yield return handle;

        string line = handle.Result;

        foreach (char c in line)
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

    // Public method to start dialogue manually from outside
    public void StartDialogueManually()
    {
        index = 0;
        DialogueManager.Instance?.Register(this);
        StartCoroutine(TypeLine());
    }

    // Check if dialogue is active or still typing
    public bool IsDialogueActive()
    {
        return (dialogueCanvas != null && dialogueCanvas.activeSelf) && (isTyping || index < lines.Length);
    }

    // Immediately stop dialogue and clear visuals
    public void ForceCloseDialogue()
    {
        StopAllCoroutines();
        isTyping = false;
        ClearText();
        OnDialogueComplete?.Invoke();
        
        DialogueManager.Instance?.Clear();

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
    }

}