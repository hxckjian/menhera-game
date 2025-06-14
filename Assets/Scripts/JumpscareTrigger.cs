using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    [SerializeField] private JumpScareScript jumpscareManager;

    public bool wasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wasTriggered = true;
            
            jumpscareManager.ShowJumpscare();
            gameObject.SetActive(false);
        }
    }

    public void InjectJumpScareManager(JumpScareScript manager)
    {
        this.jumpscareManager = manager;
    }

}