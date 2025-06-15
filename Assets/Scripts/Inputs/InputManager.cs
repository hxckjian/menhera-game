using System; // Needed for Action
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private InputAction menuOpenCloseAction;
    [SerializeField] private InputAction interactAction;

    public event Action OnMenuToggle;
    public event Action OnInteractToggle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    private void Update()
    {
        if (menuOpenCloseAction.WasPressedThisFrame())
        {
            OnMenuToggle?.Invoke();
        }
        if (interactAction.WasPressedThisFrame())
        {
            OnInteractToggle?.Invoke();
        }
    }

    private void OnEnable()
    {
        menuOpenCloseAction?.Enable();
        interactAction?.Enable();
    }

    private void OnDisable()
    {
        menuOpenCloseAction?.Disable();
        interactAction?.Disable();
    }
}
