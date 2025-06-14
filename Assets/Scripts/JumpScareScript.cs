using UnityEngine;

public class JumpScareScript : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField] private CanvasGroup jumpscareGroup;

    [Header("Animator Settings")]
    [SerializeField] private Animator jumpscareAnimator;

    // private bool hasPlayed = false;

    public virtual void Start()
    {
        HideJumpscare();
    }

    // Jumpscare Check
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space)  && !hasPlayed)
    //     {
    //         ShowJumpscare();
    //     }
    // }

    // Display Jumpscare Animation
    public virtual void ShowJumpscare()
    {
        jumpscareGroup.alpha = 1f;
        jumpscareGroup.interactable = true;
        jumpscareGroup.blocksRaycasts = true;

        jumpscareAnimator.Play("Jumpscare");
        // hasPlayed = true;
    }

    // Hides Jumpscare Canvas
    private void HideJumpscare()
    {
        jumpscareGroup.alpha = 0f;
        jumpscareGroup.interactable = false;
        jumpscareGroup.blocksRaycasts = false;
    }
}
