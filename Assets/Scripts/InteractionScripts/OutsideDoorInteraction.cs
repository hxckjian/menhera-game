using UnityEngine;

public class OutsideDoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private string sceneButtonLabel = "Open Door";

    public void Interact()
    {
        InteractionUI.Instance.Show(sceneButtonLabel, OnSceneClick);
    }

    private void OnSceneClick()
    {
        Debug.Log("Start Scene for Opening Door");
        InteractionUI.Instance.Hide();
    }
}
