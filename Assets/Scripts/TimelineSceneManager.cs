// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Playables;

// public class TimelineSceneManager : MonoBehaviour
// {
//     [Header("Timelines")]
//     [SerializeField] private PlayableDirector timeline1;
//     [SerializeField] private PlayableDirector timeline2; // optional

//     [Header("Dialogue")]
//     [SerializeField] private GameObject dialogueCanvas;
//     [SerializeField] private Dialogue dialogue; // optional

//     [Header("Scene Transition")]
//     [SerializeField] private string nextSceneName;

//     private bool hasTriggered = false;

//     private void Start()
//     {
//         if (dialogueCanvas != null)
//             dialogueCanvas.SetActive(false);

//         if (timeline1 != null)
//         {
//             timeline1.stopped += OnTimeline1Ended;
//             timeline1.Play();
//         }
//         else
//         {
//             Debug.LogWarning("No timeline1 assigned.");
//         }
//     }

//     private void OnTimeline1Ended(PlayableDirector director)
//     {
//         if (hasTriggered) return;
//         hasTriggered = true;

//         if (dialogue != null && dialogueCanvas != null)
//         {
//             dialogueCanvas.SetActive(true);
//             dialogue.StartDialogueManually();
//             dialogue.OnDialogueComplete += TriggerTimeline2OrScene;
//         }
//         else
//         {
//             TriggerTimeline2OrScene();
//         }
//     }

//     private void TriggerTimeline2OrScene()
//     {
//         if (dialogue != null)
//         {
//             dialogueCanvas.SetActive(false);
//             dialogue.OnDialogueComplete -= TriggerTimeline2OrScene;
//         }

//         if (timeline2 != null)
//         {
//             timeline2.stopped += OnTimeline2Ended;
//             timeline2.Play();
//         }
//         else
//         {
//             LoadNextScene();
//         }
//     }

//     private void OnTimeline2Ended(PlayableDirector director)
//     {
//         timeline2.stopped -= OnTimeline2Ended;
//         LoadNextScene();
//     }

//     private void LoadNextScene()
//     {
//         if (!string.IsNullOrEmpty(nextSceneName))
//         {
//             SceneManager.LoadScene(nextSceneName);
//         }
//         else
//         {
//             Debug.Log("No next scene assigned.");
//         }
//     }
// }


using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimelineSceneManager : MonoBehaviour
{
    private enum StepType { Timeline, Dialogue }

    [System.Serializable]
    private class Step
    {
        public StepType stepType;
        public PlayableDirector timeline;
        public GameObject dialogueCanvas;
        public Dialogue dialogue;
    }

    [Header("Sequence Steps")]
    [SerializeField] private List<Step> sequenceSteps = new List<Step>();

    [Header("Scene Transition")]
    [SerializeField] private string nextSceneName;

    private int currentStepIndex = 0;
    private bool isProcessing = false;

    private void Start()
    {
        foreach (var step in sequenceSteps)
        {
            if (step.dialogueCanvas != null)
            {
                step.dialogueCanvas.SetActive(false);
            }
        }
        ProcessNextStep();
    }

    private void ProcessNextStep()
    {
        if (currentStepIndex >= sequenceSteps.Count)
        {
            LoadNextScene();
            return;
        }

        Step current = sequenceSteps[currentStepIndex];
        currentStepIndex++;
        isProcessing = true;

        switch (current.stepType)
        {
            case StepType.Timeline:
                if (current.timeline != null)
                {
                    current.timeline.stopped += OnTimelineEnded;
                    current.timeline.Play();
                }
                else
                {
                    Debug.LogWarning("Timeline is null at step " + currentStepIndex);
                    ProcessNextStep();
                }
                break;

            case StepType.Dialogue:
                if (current.dialogue != null && current.dialogueCanvas != null)
                {
                    current.dialogueCanvas.SetActive(true);
                    current.dialogue.StartDialogueManually();
                    current.dialogue.OnDialogueComplete += OnDialogueCompleted;
                }
                else
                {
                    Debug.LogWarning("Dialogue or canvas missing at step " + currentStepIndex);
                    ProcessNextStep();
                }
                break;
        }
    }

    private void OnTimelineEnded(PlayableDirector director)
    {
        director.stopped -= OnTimelineEnded;
        isProcessing = false;
        ProcessNextStep();
    }

    private void OnDialogueCompleted()
    {
        Step previous = sequenceSteps[currentStepIndex - 1];
        if (previous.dialogueCanvas != null)
            previous.dialogueCanvas.SetActive(false);

        if (previous.dialogue != null)
            previous.dialogue.OnDialogueComplete -= OnDialogueCompleted;

        isProcessing = false;
        ProcessNextStep();
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("No next scene name assigned.");
        }
    }
}