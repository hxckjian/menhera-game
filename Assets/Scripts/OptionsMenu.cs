using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject firstSliderInOptionsMenu;
    [SerializeField] private GameObject optionMainButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                EnableSelectableInMain();
                 ClearSelected();
                 EventSystem.current.SetSelectedGameObject(optionMainButton);
            }
        }
    }

    public void ToggleOptions()
    {
        optionsPanel.SetActive(true);
        DisableSelectableInMain();
        ClearSelected();
        EventSystem.current.SetSelectedGameObject(firstSliderInOptionsMenu);
        // StartCoroutine(SelectAfterDelay());
    }

    private IEnumerator SelectAfterDelay()
    {
        yield return null; // wait one frame

        EventSystem.current.SetSelectedGameObject(firstSliderInOptionsMenu);
    }

    public void DisableSelectableInMain()
    {
        foreach (Selectable s in mainMenuPanel.GetComponentsInChildren<Selectable>())
        {
            s.interactable = false;
        }
    }

    public void EnableSelectableInMain()
    {
        foreach (Selectable s in mainMenuPanel.GetComponentsInChildren<Selectable>())
        {
            s.interactable = true;
        }
    }

    public void ClearSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
