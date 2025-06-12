using UnityEngine;

public class LockerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneButtonLabel = "Enter Locker";

    [SerializeField] private MenuManager menuManager;

    public void Interact()
    {
        InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick);
    }

    private void OnSceneClick()
    {
        Debug.Log("Start Scene for Locker");
        InteractionUI.Instance.Hide();
    }
}
