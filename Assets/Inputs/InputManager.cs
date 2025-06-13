using System; // Needed for Action
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    // 🔔 Declare the input events here
    public event Action OnMenuToggle;
    public event Action OnInteractToggle;

    private PlayerInput playerInput;
    private InputAction menuOpenCloseAction;
    private InputAction interactAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInput = GetComponent<PlayerInput>();
        menuOpenCloseAction = playerInput.actions["MenuOpenClose"];
        interactAction = playerInput.actions["InteractionOpenClose"];
    }

    private void Update()
    {
        // ✅ Trigger events instead of exposing state
        if (menuOpenCloseAction.WasPressedThisFrame())
            OnMenuToggle?.Invoke();

        if (interactAction.WasPressedThisFrame())
            OnInteractToggle?.Invoke();
    }
}
