using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    [SerializeField] private JumpScareScript jumpscareManager;
    [SerializeField] private AudioSource audioToPause;

    public bool wasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wasTriggered = true;

            if (audioToPause != null && audioToPause.isPlaying)
            {
                audioToPause.Pause();
            }
            
            jumpscareManager.ShowJumpscare();
            gameObject.SetActive(false);
        }
    }

    public void InjectJumpScareManager(JumpScareScript manager)
    {
        this.jumpscareManager = manager;
    }

}