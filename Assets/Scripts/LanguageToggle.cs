using UnityEngine;
using UnityEngine.Localization.Settings;

namespace MainScripts
{
    public class LanguageToggle : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale("ja");
        }

        public void ToggleLanguage()
        {
            var current = LocalizationSettings.SelectedLocale;
            var newLocale = current.Identifier.Code == "en" ? "ja" : "en";
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(newLocale);
        }
    }
}
