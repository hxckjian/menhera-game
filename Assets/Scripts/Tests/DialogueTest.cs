using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class DialogueTest
{
    private GameObject dialogueGO;
    private Dialogue dialogue;
    private TextMeshProUGUI tmpText;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        dialogueGO = new GameObject("DialogueBox");

        Canvas canvas = dialogueGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject tmpObject = new GameObject("TMP_Text");
        tmpObject.transform.SetParent(dialogueGO.transform);
        tmpText = tmpObject.AddComponent<TextMeshProUGUI>();

        dialogue = dialogueGO.AddComponent<Dialogue>();

        typeof(Dialogue).GetField("textComponent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(dialogue, tmpText);

        typeof(Dialogue).GetField("lines", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(dialogue, new string[] { "Hello", "World" });

        typeof(Dialogue).GetField("textSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(dialogue, 0.01f); // fast typing

        yield return null;
    }

    [UnityTest]
    public IEnumerator Dialogue_StartsAndCompletesCorrectly()
    {
        bool completed = false;
        dialogue.OnDialogueComplete += () => completed = true;

        dialogue.StartDialogueManually();

        yield return new WaitForSecondsRealtime(0.1f);

        dialogue.HandleClick(); 
        yield return new WaitForSecondsRealtime(0.1f);

        dialogue.HandleClick(); 
        yield return new WaitForSecondsRealtime(0.1f);

        dialogue.HandleClick(); 
        yield return new WaitForSecondsRealtime(0.1f);

        dialogue.HandleClick(); 
        yield return new WaitForSecondsRealtime(0.1f);

        Assert.IsTrue(completed, "Dialogue did not complete properly.");
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameObject.Destroy(dialogueGO);
        yield return null;
    }
}
