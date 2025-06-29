using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable = null;
    private bool canInteract = true;

    private Direction requiredDirection = Direction.None;
    private bool directionNone = false;

    [Header("Popup Settings")]
    [SerializeField] private InteractionPopupUI popUpInteraction;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractToggle += HandleInteraction;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnInteractToggle -= HandleInteraction;
        }
    }

    public void DisableInteractionPopup()
    {
        popUpInteraction.DisablePopup();
    }

    private void HandleInteraction()
    {
        if (!canInteract || PauseManager.Instance.IsPaused) 
        {
            return;
        }

        if (!CheckDirectionMatch() && !directionNone) 
        {
            return;
        }

        if (currentInteractable != null)
        {
            Debug.Log("E key pressed on interactable.");
            currentInteractable.Interact();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            popUpInteraction.ShowPopup();

            currentInteractable = interactable;
            requiredDirection = interactable.RequiredDirection;  
            if (requiredDirection == Direction.None)
            {
                SetDirectionCheckEnabled(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable) && interactable == currentInteractable)
        {
            popUpInteraction.HidePopup();

            currentInteractable = null;
            requiredDirection = Direction.None;
            SetDirectionCheckEnabled(false);
        }
    }

    public void SetInteractEnabled(bool enabled)
    {
        canInteract = enabled;
    }

    private void SetDirectionCheckEnabled(bool enabled)
    {
        directionNone = enabled;
    }

    private bool CheckDirectionMatch()
    {
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        return player.FacingDirection == requiredDirection;
    }
}
