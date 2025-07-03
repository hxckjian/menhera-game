using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;

namespace MainScripts
{
    public class SwitchInteraction : MonoBehaviour, IInteractable, ILabelProvider
    {
        // [SerializeField] private string sceneButtonLabel = "Switch off";
        [SerializeField] private LocalizedString localizedLabel;
        [SerializeField] private MenuManager menuManager;

        [Header("Dialogue")]
        [SerializeField] private GameObject dialogueCanvas;
        [SerializeField] private Dialogue dialogue;

        [Header("Required Direction for Trigger")]
        [SerializeField] private Direction requiredFacingDirection = Direction.Up;

        public Direction RequiredDirection => requiredFacingDirection;

        // Initialize by hiding the dialogue canvas (if any)
        private void Start()
        {
            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(false);
            }
        }

        // Called when the player interacts with the locker
        public void Interact()
        {
            InteractionUI.Instance.Show(this, OnSceneClick, dialogueCanvas, dialogue);
        }

        public LocalizedString GetInteractionLabel()
        {
            return localizedLabel;
        }

        // Executes when the scene button is clicked
        private void OnSceneClick()
        {
            Debug.Log("Start Scene for Switch");
            InteractionUI.Instance.Hide();
            SceneManager.LoadScene("SwitchScene");
        }
    }
}
