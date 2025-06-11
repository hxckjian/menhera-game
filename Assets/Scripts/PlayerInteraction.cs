// using UnityEngine;

// public class PlayerInteraction : MonoBehaviour
// {
//     [SerializeField] private Transform interactionPoint;  // e.g. locker position
//     [SerializeField] private float interactionRange = 0.1f; // distance tolerance

//     // Interaction Trigger
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             float dist = Vector2.Distance(transform.position, interactionPoint.position);
//             if (dist <= interactionRange)
//             {
//                 Debug.Log("Interaction triggered: Show locker options");
//                 // later: ShowOptionsUI();
//             }
//         }
//     }
// }


using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable = null;

    private void Update()
    {
        if (InputManager.instance.InteractPressed && currentInteractable != null)
        {
            Debug.Log("E has been entered!");
            // currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
