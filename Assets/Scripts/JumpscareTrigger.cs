using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    [SerializeField] private JumpScareScript jumpscareManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jumpscareManager.ShowJumpscare();
            gameObject.SetActive(false);
        }
    }
}