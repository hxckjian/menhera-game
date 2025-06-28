using UnityEngine;

public class JumpScareScript : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField] private CanvasGroup jumpscareGroup;

    [Header("Animator Settings")]
    [SerializeField] private Animator jumpscareAnimator;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource jumpscareAudio;
    [SerializeField] private AudioClip jumpscareClip;

    // private bool hasPlayed = false;

    public virtual void Start()
    {
        HideJumpscare();
    }

    // Display Jumpscare Animation
    public virtual void ShowJumpscare()
    {
        jumpscareGroup.alpha = 1f;
        jumpscareGroup.interactable = true;
        jumpscareGroup.blocksRaycasts = true;

        jumpscareAnimator.Play("Jumpscare");

        // Play jumpscare sound
        if (jumpscareAudio != null && jumpscareClip != null)
        {
            jumpscareAudio.PlayOneShot(jumpscareClip);
        }

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
