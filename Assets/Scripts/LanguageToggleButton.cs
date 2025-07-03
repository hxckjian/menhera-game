using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageToggleButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Start()
    {
        UpdateButtonText();
    }

    public void OnButtonPressed()
    {
        var current = LocalizationSettings.SelectedLocale;
        var newCode = current.Identifier.Code == "en" ? "ja" : "en";
        var newLocale = LocalizationSettings.AvailableLocales.GetLocale(newCode);

        if (newLocale != null)
        {
            LocalizationSettings.SelectedLocale = newLocale;
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        string code = LocalizationSettings.SelectedLocale.Identifier.Code;
        buttonText.text = code == "en" ? "EN" : "JP";
    }
}
