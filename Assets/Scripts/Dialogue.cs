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
    [System.Serializable]
    public class DialogueLine
    {
        public LocalizedString localizedLine;
        public string speakerName; //"MC", "Girlfriend"
        public string expression; //sad, happy
    }
    [SerializeField] private CharacterExpressionManager expressionManager;

    [SerializeField] private TextMeshProUGUI textComponent;
    // [SerializeField] private string[] lines;
    // [SerializeField] private LocalizedString[] lines;
    [SerializeField] private DialogueLine[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private GameObject dialogueCanvas; 

    //Addition
    [SerializeField] private float englishFontSize = 160f;
    [SerializeField] private float japaneseFontSize = 120f;

    private int index;
    private bool isTyping = false;

    // Event triggered when the dialogue sequence completes
    public event Action OnDialogueComplete;

    //Addition
    private string fullTypedLine = "";

    // Update is called once per frame
    private void Update()
    {
        //original Input.GetMouseButtonDown(0)
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleClick();
        }
    }

    // public void HandleClick()
    // {
    //     if (isTyping)
    //     {
    //         StopAllCoroutines();
    //         // textComponent.text = lines[index];
    //         lines[index].localizedLine.GetLocalizedStringAsync().Completed += handle =>
    //         {
    //             textComponent.text = handle.Result;
    //         };
    //         isTyping = false;
    //     }
    //     else
    //     {
    //         NextLine();
    //     }
    // }

    //Addition
    public void HandleClick()
{
    if (isTyping)
    {
        StopAllCoroutines();
        textComponent.text = fullTypedLine;
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
    // private IEnumerator TypeLine()
    // {
    //     isTyping = true;
    //     ClearText();

    //     var handle = lines[index].localizedLine.GetLocalizedStringAsync();
    //     yield return handle;

    //     string line = handle.Result;

    //     foreach (char c in line)
    //     {
    //         textComponent.text += c;
    //         yield return new WaitForSecondsRealtime(textSpeed);
    //     }

    //     isTyping = false;
    // }

    //Addition
    private IEnumerator TypeLine()
    {
        isTyping = true;
        ClearText();

        var handle = lines[index].localizedLine.GetLocalizedStringAsync();
        yield return handle;

        string rawLine = handle.Result;
        string processedLine = ApplySmartFontSizing(rawLine);

        fullTypedLine = processedLine;

        for (int i = 0; i < processedLine.Length; i++)
        {
            char c = processedLine[i];

            if (c == '<')
            {
                while (i < processedLine.Length && processedLine[i] != '>')
                {
                    textComponent.text += processedLine[i];
                    i++;
                }
                textComponent.text += '>';
                continue;
            }

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

            // Set expression
            var currentLine = lines[index];
            expressionManager.SetExpression(currentLine.speakerName, currentLine.expression);

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

        if (expressionManager != null && lines.Length > 0)
        {
            var currentLine = lines[index];
            expressionManager.SetExpression(currentLine.speakerName, currentLine.expression);
        }

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


    //Addition
    private string ApplySmartFontSizing(string input)
{
    System.Text.StringBuilder modified = new System.Text.StringBuilder();
    int i = 0;

    while (i < input.Length)
    {
        char c = input[i];

        if (IsEnglish(c))
        {
            modified.Append($"<size={englishFontSize}>");

            // Wrap entire English word or phrase (including spaces)
            while (i < input.Length && (IsEnglish(input[i]) || input[i] == ' '))
            {
                modified.Append(input[i]);
                i++;
            }

            modified.Append("</size>");
        }
        else if (IsJapanese(c))
        {
            modified.Append($"<size={japaneseFontSize}>");

            // Wrap consecutive Japanese characters
            while (i < input.Length && IsJapanese(input[i]))
            {
                modified.Append(input[i]);
                i++;
            }

            modified.Append("</size>");
        }
        else
        {
            // Neutral: punctuation, emoji, symbols, etc.
            modified.Append(c);
            i++;
        }
    }

    return modified.ToString();
}


    private bool IsJapanese(char c) =>
        (c >= '\u3040' && c <= '\u30FF') ||  // Hiragana & Katakana
        (c >= '\u4E00' && c <= '\u9FFF') ||  // Kanji
        (c >= '\uFF66' && c <= '\uFF9D') ||  // Half-width Katakana
        c == '。' || c == '、' || c == '「' || c == '」' || c == '！' || c == '？' ||
        c == '～' || c == '：' || c == '…';

    private bool IsEnglish(char c) =>
        (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') ||
    ":/%()-_=+[],".Contains(c);
}