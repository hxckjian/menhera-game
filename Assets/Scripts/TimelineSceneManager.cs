using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimelineSceneManager : MonoBehaviour
{
    [Header("Timelines")]
    public PlayableDirector timeline1;
    public PlayableDirector timeline2; // optional

    [Header("Dialogue")]
    public GameObject dialogueCanvas;
    public Dialogue dialogue; // optional

    [Header("Scene Transition")]
    public string nextSceneName;

    private bool hasTriggered = false;

    void Start()
    {
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);

        if (timeline1 != null)
        {
            timeline1.stopped += OnTimeline1Ended;
            timeline1.Play();
        }
        else
        {
            Debug.LogWarning("No timeline1 assigned.");
        }
    }

    void OnTimeline1Ended(PlayableDirector director)
    {
        if (hasTriggered) return;
        hasTriggered = true;

        if (dialogue != null && dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(true);
            dialogue.StartDialogueManually();
            dialogue.OnDialogueComplete += TriggerTimeline2OrScene;
        }
        else
        {
            TriggerTimeline2OrScene();
        }
    }

    void TriggerTimeline2OrScene()
    {
        if (dialogue != null)
            dialogue.OnDialogueComplete -= TriggerTimeline2OrScene;

        if (timeline2 != null)
        {
            timeline2.stopped += OnTimeline2Ended;
            timeline2.Play();
        }
        else
        {
            LoadNextScene();
        }
    }

    void OnTimeline2Ended(PlayableDirector director)
    {
        timeline2.stopped -= OnTimeline2Ended;
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("No next scene assigned.");
        }
    }
}
