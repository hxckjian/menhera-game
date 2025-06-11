using UnityEngine;

public class LockerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneButtonLabel = "Enter Locker";

    [SerializeField] private MenuManager menuManager;

    public void Interact()
    {
        // InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick);
        // InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick, () => menuManager.UnpauseScreen());
    //     InteractionUI.Instance.Show(sceneButtonLabel, () => {
    // PauseManager.instance.UnpauseScreen(); // or trigger cutscene, etc.
    InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick);
    

    }

    private void OnSceneClick()
    {
        Debug.Log("Start Scene for Locker");
        InteractionUI.Instance.Hide();
    }
    // [SerializeField] private CanvasGroup lockerUI;

    // private void Start()
    // {
    //     HideUI();
    // }

    // public void OnSceneClick()
    // {
    //     Debug.Log("Start Scene");
    //     HideUI();
    // }

    // // Called when Exit Button is clicked and exit application
    // public void OnNothingClick()
    // {
    //     Debug.Log("Nothing");
    //     HideUI();
    // }

    // public void Interact()
    // {
    //     lockerUI.alpha = 1f;
    //     lockerUI.interactable = true;
    //     lockerUI.blocksRaycasts = true;
    //     Debug.Log("Locker interaction UI shown.");
    // }

    // private void HideUI()
    // {
    //     lockerUI.alpha = 0f;
    //     lockerUI.interactable = false;
    //     lockerUI.blocksRaycasts = false;
    // }
}
