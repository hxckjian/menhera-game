using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
            }
        }
    }

    public void ToggleOptions()
    {
        optionsPanel.SetActive(true);
    }
}
