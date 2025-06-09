using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;  // e.g. locker position
    [SerializeField] private float interactionRange = 0.1f; // distance tolerance

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float dist = Vector2.Distance(transform.position, interactionPoint.position);
            if (dist <= interactionRange)
            {
                Debug.Log("Interaction triggered: Show locker options");
                // later: ShowOptionsUI();
            }
        }
    }
}
