using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainScripts
{
    public class LockerInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private string sceneButtonLabel = "Hide in locker";

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
            InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick, dialogueCanvas, dialogue);
        }

        // Executes when the scene button is clicked
        private void OnSceneClick()
        {
            Debug.Log("Start Scene for Locker");
            InteractionUI.Instance.Hide();
            // SceneManager.LoadScene("LockerScene");
            SceneManager.LoadScene("LockerScene");
        }
    }
}
