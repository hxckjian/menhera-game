using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public JumpScareScript jumpscareManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jumpscareManager.ShowJumpscare();
            gameObject.SetActive(false);
        }
    }
}