using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public bool MenuOpenCloseInput { get; private set; }
    public bool InteractPressed { get; private set; }

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
        MenuOpenCloseInput = menuOpenCloseAction.WasPressedThisFrame();
        InteractPressed = interactAction.WasPressedThisFrame();
    }
}
