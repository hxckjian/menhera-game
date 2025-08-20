using System; 
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private InputAction menuOpenCloseAction;
    [SerializeField] private InputAction interactAction;

    public event Action OnMenuToggle;
    public event Action OnInteractToggle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Update()
    {
        // Pressing Escape trigger menu
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
