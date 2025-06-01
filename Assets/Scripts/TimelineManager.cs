using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline1;
    [SerializeField] private PlayableDirector timeline2;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private Dialogue dialogue;

    private void Start()
    {
        // Initially hide dialogue
        dialogueCanvas.SetActive(false);

        // When timeline1 ends, show dialogue
        timeline1.stopped += OnTimeline1Ended;

        // Start first timeline
        timeline1.Play();
    }

    // Called when timeline1 finish playing, triggers dialogue
    private void OnTimeline1Ended(PlayableDirector director)
    {
        // Show the dialogue UI
        dialogueCanvas.SetActive(true);

        // Start the dialogue manually
        dialogue.StartDialogueManually();

        // Subscribe to the dialogue completion event
        dialogue.OnDialogueComplete += TriggerTimeline2;
    }

    // Called when dialogue is completed. Starts timeline2
    private void TriggerTimeline2()
    {
        // Unsubscribe to avoid duplicates
        dialogue.OnDialogueComplete -= TriggerTimeline2;

        // Play second timeline
        timeline2.Play();
    }
}