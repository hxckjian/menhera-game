using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimelineSceneManager : MonoBehaviour
{
    private enum StepType { Timeline, Dialogue }

    [System.Serializable]
    private class CharacterFacingInstruction
    {
        public bool setMC = false;
        public SpriteDirectionManager.Direction mcDirection;

        public bool setYandere = false;
        public SpriteDirectionManager.Direction yandereDirection;
    }

    [System.Serializable]
    private class Step
    {
        public StepType stepType;
        public PlayableDirector timeline;
        public GameObject dialogueCanvas;
        public Dialogue dialogue;
        
        public bool setFacingAfterStep = false;
        public CharacterFacingInstruction facingInstructions;
    }

    [Header("Sequence Steps")]
    [SerializeField] private List<Step> sequenceSteps = new List<Step>();

    [Header("Scene Transition")]
    [SerializeField] private string nextSceneName;

    [Header("Default Direction Manager")]
    [SerializeField] private SpriteDirectionManager directionManager;

    public enum Character { MC, Yandere }
    public enum Direction { Left, Right, Up, Down }


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
                Debug.Log("BYEEEE");
                if (current.dialogue != null && current.dialogueCanvas != null)
                {
                    Debug.Log(current.dialogueCanvas);
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
        Step previous = sequenceSteps[currentStepIndex - 1];

        if (previous.setFacingAfterStep && previous.facingInstructions != null)
        {
            if (previous.facingInstructions.setMC)
            {
                directionManager.Face(SpriteDirectionManager.Character.MC, previous.facingInstructions.mcDirection);
            }
            if (previous.facingInstructions.setYandere)
            {
                directionManager.Face(SpriteDirectionManager.Character.Yandere, previous.facingInstructions.yandereDirection);
            }
        }


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

        if (previous.setFacingAfterStep && previous.facingInstructions != null)
        {
            if (previous.facingInstructions.setMC)
            {
                directionManager.Face(SpriteDirectionManager.Character.MC, previous.facingInstructions.mcDirection);
            }
            if (previous.facingInstructions.setYandere)
            {
                directionManager.Face(SpriteDirectionManager.Character.Yandere, previous.facingInstructions.yandereDirection);
            }
        }

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