using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector timeline1;
    public PlayableDirector timeline2;
    public GameObject dialogueCanvas;
    public Dialogue dialogue;

    void Start()
    {
        // Initially hide dialogue
        dialogueCanvas.SetActive(false);

        // When timeline1 ends, show dialogue
        timeline1.stopped += OnTimeline1Ended;

        // Start first timeline
        timeline1.Play();
    }

    void OnTimeline1Ended(PlayableDirector director)
    {
        // Show the dialogue UI
        dialogueCanvas.SetActive(true);

        // Start the dialogue manually
        dialogue.StartDialogueManually();

        // Subscribe to the dialogue completion event
        dialogue.OnDialogueComplete += TriggerTimeline2;
    }

    void TriggerTimeline2()
    {
        // Unsubscribe to avoid duplicates
        dialogue.OnDialogueComplete -= TriggerTimeline2;

        // Play second timeline
        timeline2.Play();
    }
}