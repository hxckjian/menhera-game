using UnityEngine;

public class JumpScareScript : MonoBehaviour
{
    [Header("Canvas Settings")]
    public CanvasGroup jumpscareGroup;

    [Header("Animator Settings")]
    public Animator jumpscareAnimator;

    private bool hasPlayed = false;

     void Start()
    {
        HideJumpscare(); // Make sure it starts hidden
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)  && !hasPlayed)
        {
            ShowJumpscare();
        }
    }

    public void ShowJumpscare()
    {
        jumpscareGroup.alpha = 1f;
        jumpscareGroup.interactable = true;
        jumpscareGroup.blocksRaycasts = true;

        // Play animation
        jumpscareAnimator.Play("Jumpscare");
        // jumpscareAnimator.SetTrigger("PlayJumpscare");

        hasPlayed = true;
    }

    void HideJumpscare()
    {
        jumpscareGroup.alpha = 0f;
        jumpscareGroup.interactable = false;
        jumpscareGroup.blocksRaycasts = false;
    }
}
